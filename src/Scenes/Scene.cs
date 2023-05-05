using Fjord.Input;
using System.Numerics;
using Fjord.Graphics;
using Fjord.Ui;
using static SDL2.SDL;

namespace Fjord.Scenes;

public abstract class Scene : ICloneable
{
    private string SceneID;

    public bool AllowWindowResize = false;
    public bool AlwaysRebuildTexture = false;
    public bool AlwaysAtBack = false;
    public bool AlwaysAtFront = false;

    internal bool showCursor = true;
    internal bool captureMouseInput = true;

    public SceneKeyboard Keyboard;
    public SceneMouse Mouse;
    public SceneCamera Camera;

    public float DeltaTime
    {
        get
        {
            return (float)Game.GetDeltaTime();
        }
        private set
        {

        }
    }

    public bool MouseInsideScene { get; internal set; }

    internal List<Entity> Entities = new();
    internal List<Entity> removeEntity = new();

    internal List<Entity> RegisterEntityBacklog = new();

    internal List<DrawInstruction> drawBuffer = new();
    internal SDL_FRect RelativeWindowSize = new()
    {
        x = 0.1f,
        y = 0.1f,
        w = 0.9f,
        h = 0.9f
    };

    internal SDL_FRect RelativeWindowSizeFinal = new();

    public Vector2 WindowSize = new();
    internal SDL_Rect LocalWindowSize = new();
    internal Vector2 OriginalWindowSize = new();
    internal IntPtr RenderTarget;
    internal SDL_Color ClearColor = new()
    {
        r = 0,
        g = 0,
        b = 0,
        a = 0
    };

    public void SetClearColor(SDL_Color cc)
    {
        ClearColor = cc;
    }

    public void SetClearColor(Vector4 cc)
    {
        ClearColor = new()
        {
            r = (byte)cc.X,
            g = (byte)cc.Y,
            b = (byte)cc.Z,
            a = (byte)cc.W
        };
    }

    public void SetClearColor(int r, int g, int b, int a)
    {
        ClearColor = new()
        {
            r = (byte)r,
            g = (byte)g,
            b = (byte)b,
            a = (byte)a
        };
    }

    public SDL_Color GetClearColor()
    {
        return ClearColor;
    }

    public string GetSceneID()
    {
        return SceneID;
    }
    
    public void SetShowCursor(bool showCursor)
    {
        this.showCursor = showCursor;
    }

    public void SetCaptureMouse(bool capture)
    {
        this.captureMouseInput = capture;
    }

    public void ApplyOriginalAspectRatio()
    {
        float ratio = OriginalWindowSize.X / OriginalWindowSize.Y;
        Vector2 newSize = new Vector2(LocalWindowSize.w, LocalWindowSize.h);
        newSize.Y = newSize.X / ratio;
        RelativeWindowSize.w = newSize.X / Game.Window.Width;
        RelativeWindowSize.h = newSize.Y / Game.Window.Height;
    }

    protected Scene(int width, int height, string id)
    {
        OriginalWindowSize.X = width;
        OriginalWindowSize.Y = height;

        SceneID = id;
        
        RenderTarget = SDL_CreateTexture(Game.SDLRenderer, SDL_PIXELFORMAT_RGBA8888,
            (int)SDL_TextureAccess.SDL_TEXTUREACCESS_TARGET, width, height);
        SDL_SetTextureBlendMode(RenderTarget, SDL_BlendMode.SDL_BLENDMODE_BLEND);

        Keyboard = new(id);
        Mouse = new(id);
        Camera = new(this);
    }

    public Scene SetRelativeWindowSize(SDL_FRect RelativeWindow) {
        this.RelativeWindowSize = new SDL_FRect() {
            x = RelativeWindow.x,
            y = RelativeWindow.y,
            w = RelativeWindow.w,
            h = RelativeWindow.h
        };

        return this;
    } 
    
    public Scene SetRelativeWindowSize(float x, float y, float w, float h) {
        this.RelativeWindowSize = new SDL_FRect() {
            x = x,
            y = y,
            w = w,
            h = h
        };

        return this;
    } 

    public Scene SetAllowWindowResize(bool Debug) {
        this.AllowWindowResize = Debug;

        return this;
    }

    public Scene SetAlwaysRebuildTexture(bool Allow)
    {
        this.AlwaysRebuildTexture = Allow;

        return this;
    }

    public Scene SetAlwaysAtBack(bool Always)
    {
        this.AlwaysAtBack = Always;

        return this;
    }

    public Scene SetAlwaysAtFront(bool Always)
    {
        this.AlwaysAtFront = Always;

        return this;
    }

    public void RegisterEntity(Entity e)
    {
        this.RegisterEntityBacklog.Add(e);
    }

    public void RemoveEntity(Entity e)
    {
        this.removeEntity.Add(e);
    }

    public virtual void Awake() {}
    public virtual void Sleep() {}
    public virtual void Update() {}
    public virtual void Render() {}

    internal void AwakeCall()
    {
        Awake();
    }

    internal void SleepCall()
    {
        Sleep();
    }
    
    internal void UpdateCall()
    {
        if(MouseInsideScene)
        {
            SDL_ShowCursor(showCursor ? SDL_ENABLE : SDL_DISABLE);
        }

        LocalWindowSize = new()
        {
            x = (int)(RelativeWindowSize.x * Game.Window.Width),
            y = (int)(RelativeWindowSize.y * Game.Window.Height),
            w = (int)((RelativeWindowSize.w - RelativeWindowSize.x) * Game.Window.Width),
            h = (int)((RelativeWindowSize.h - RelativeWindowSize.y) * Game.Window.Height)
        };

        if(WindowSize.X == 0 && WindowSize.Y == 0) {
            WindowSize = new(OriginalWindowSize.X, OriginalWindowSize.Y);
        }

        if (AlwaysRebuildTexture)
        {
            WindowSize = new() {
                X = LocalWindowSize.w,
                Y = LocalWindowSize.h
            };

            SDL_DestroyTexture(RenderTarget);
            RenderTarget = SDL_CreateTexture(Game.SDLRenderer, SDL_PIXELFORMAT_RGBA8888,
            (int)SDL_TextureAccess.SDL_TEXTUREACCESS_TARGET, (int)LocalWindowSize.w, (int)LocalWindowSize.h);
            SDL_SetTextureBlendMode(RenderTarget, SDL_BlendMode.SDL_BLENDMODE_BLEND);

            Mouse.Position.X = (GlobalMouse.Position.X - LocalWindowSize.x);
            Mouse.Position.Y = (GlobalMouse.Position.Y - LocalWindowSize.y);

            Mouse.LocalPosition.X = (GlobalMouse.Position.X - LocalWindowSize.x) + Camera.Offset.X;
            Mouse.LocalPosition.Y = (GlobalMouse.Position.Y - LocalWindowSize.y) + Camera.Offset.Y;
        } else
        {
            float wRatio = (float)WindowSize.X / (float)LocalWindowSize.w;
            float hRatio = (float)WindowSize.Y / (float)LocalWindowSize.h;

            Mouse.Position.X = (GlobalMouse.Position.X - LocalWindowSize.x) * wRatio;
            Mouse.Position.Y = (GlobalMouse.Position.Y - LocalWindowSize.y) * hRatio;

            Mouse.LocalPosition.X = (GlobalMouse.Position.X - LocalWindowSize.x) * wRatio + Camera.Offset.X;
            Mouse.LocalPosition.Y = (GlobalMouse.Position.Y - LocalWindowSize.y) * hRatio + Camera.Offset.Y;
        }

        if (Mouse.Pressed(MB.Left) && Helpers.PointInside(GlobalMouse.Position, LocalWindowSize) && !AlwaysAtBack)
        {
            SceneHandler.LoadedScenes.Remove(SceneID);
            SceneHandler.LoadedScenes.Insert(SceneHandler.LoadedScenes.Count, SceneID);
        }

        if (Helpers.PointInside(GlobalMouse.Position, LocalWindowSize)) 
        {
            List<string> loadedScenes = new(SceneHandler.LoadedScenes);
            List<string> eligble = new();
            foreach(var scene in loadedScenes)
            {
                if(Helpers.PointInside(GlobalMouse.Position, SceneHandler.Scenes[scene].LocalWindowSize))
                {
                    if(SceneHandler.Scenes[scene].captureMouseInput)
                    {
                        eligble.Add(scene);
                    }
                }
            }
            loadedScenes.Reverse();
            if(eligble.Count > 1)
            {
                if(loadedScenes.First((scene) => eligble.Contains(scene)) == SceneID)
                {
                    MouseInsideScene = true;
                } else {
                    MouseInsideScene = false;
                }
            } else {
                MouseInsideScene = true;
            }
        } else 
        {
            MouseInsideScene = false;
        }

        if (AlwaysAtBack)
        {
            SceneHandler.LoadedScenes.Remove(SceneID);
            SceneHandler.LoadedScenes.Insert(0, SceneID);
        }

        if (AlwaysAtFront)
        {
            SceneHandler.LoadedScenes.Remove(SceneID);
            SceneHandler.LoadedScenes.Add(SceneID);
        }

        Camera.Update(WindowSize);

        Update();

        foreach(Entity e in Entities)
        {
            e.UpdateCall();
        }
    }

    internal void RenderCall()
    {
        SDL_SetRenderTarget(Game.SDLRenderer, RenderTarget);
        SDL_SetRenderDrawColor(Game.SDLRenderer, ClearColor.r, ClearColor.g, ClearColor.b, ClearColor.a);
        SDL_RenderClear(Game.SDLRenderer);
        
        Draw.CurrentSceneID = SceneID;
        Render();

        foreach(Entity e in Entities)
        {
            e.RenderCall();
        }
        Draw.CurrentSceneID = null;

        Draw.DrawDrawBuffer(drawBuffer, SceneID);

        drawBuffer = new();

        SDL_SetRenderTarget(Game.SDLRenderer, IntPtr.Zero);

        SDL_SetRenderTarget(Game.SDLRenderer, IntPtr.Zero);
        SDL_RenderCopy(Game.SDLRenderer, RenderTarget, IntPtr.Zero, ref LocalWindowSize);

        if (AllowWindowResize)
        {
            FUI.ResizeableRectangle(ref RelativeWindowSize, SceneID);
        }

        foreach(Entity e in RegisterEntityBacklog)
        {
            Entities.Add(e);
        }
        RegisterEntityBacklog = new();

        foreach(Entity e in removeEntity)
        {
            Entities.Remove(e);
        }
        removeEntity = new();
    }

    public object Clone()
    {
        return this.MemberwiseClone();
    }
}