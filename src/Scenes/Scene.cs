using Fjord.Input;
using System.Numerics;
using Fjord.Graphics;
using Fjord.Ui;
using static SDL2.SDL;

namespace Fjord.Scenes;

public abstract class Scene : ICloneable
{
    public string SceneID { get; internal set; }

    public bool AllowWindowResize { get; set; } = false;
    public bool AlwaysRebuildTexture { get; set; } = false;
    public bool AlwaysAtBack { get; set; } = false;
    public bool AlwaysAtFront { get; set; } = false;

    public bool Paused = false;

    internal bool ShowCursor = true;
    internal bool CaptureMouseInput = true;

    internal bool UpdateOnlyIfActive = false;
    internal bool HasRendered = false;

    public SceneKeyboard Keyboard;
    public SceneMouse Mouse;
    public SceneCamera Camera;
    public SceneInput Input;

    public float DeltaTime
    {
        get
        {
            return (float)Game.DeltaTime;
        }
        private set
        {

        }
    }

    public bool MouseInsideScene { get; internal set; }

    public List<Entity> Entities { get; internal set; } = new();
    internal List<Entity> removeEntity = new();

    internal List<Entity> RegisterEntityBacklog = new();

    internal List<IDrawInstruction> drawBuffer = new();
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

    public Vector4 ClearColor = new();

    public void ApplyOriginalAspectRatio()
    {
        float ratio = OriginalWindowSize.X / OriginalWindowSize.Y;
        Vector2 newSize = new Vector2(LocalWindowSize.w, LocalWindowSize.h);
        newSize.Y = newSize.X / ratio;
        RelativeWindowSize.w = newSize.X / Game.Window.Width;
        RelativeWindowSize.h = newSize.Y / Game.Window.Height;
    }

    protected Scene(int width, int height)
    {
        OriginalWindowSize.X = width;
        OriginalWindowSize.Y = height;
        WindowSize.X = width;
        WindowSize.Y = height;

        SceneID = this.GetType().Name;
        
        RenderTarget = SDL_CreateTexture(Game.SDLRenderer, SDL_PIXELFORMAT_RGBA8888,
            (int)SDL_TextureAccess.SDL_TEXTUREACCESS_TARGET, width, height);
        SDL_SetTextureBlendMode(RenderTarget, SDL_BlendMode.SDL_BLENDMODE_BLEND);

        Keyboard = new(SceneID);
        Mouse = new(SceneID);
        Camera = new(this);
        Input = new(SceneID);
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
        // Update Stuff

        if(MouseInsideScene)
        {
            SDL_ShowCursor(ShowCursor ? SDL_ENABLE : SDL_DISABLE);
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

        if (AlwaysRebuildTexture && (MouseInsideScene || !UpdateOnlyIfActive))
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

            HasRendered = false;
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
            bool? foundBefore = null;
            foreach(var scene in SceneHandler.LoadedScenes.AsEnumerable().Reverse())
            {
                if(Helpers.PointInside(GlobalMouse.Position, SceneHandler.Scenes[scene].LocalWindowSize))
                {
                    if(SceneHandler.Scenes[scene].CaptureMouseInput)
                    {
                        if (scene != SceneID)
                        {
                            MouseInsideScene = false;
                            foundBefore = true;
                            break;
                        }

                        MouseInsideScene = true;
                        foundBefore = false;
                        break;
                    }
                }
            }

            if (foundBefore is null)
            {
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

        // Render Stuff

        if(!Paused)
            Camera.Update(WindowSize);

        if((!UpdateOnlyIfActive || !HasRendered) && !Paused)
        {
            SDL_SetRenderTarget(Game.SDLRenderer, RenderTarget);
            SDL_SetRenderDrawColor(Game.SDLRenderer, (byte)ClearColor.X, (byte)ClearColor.Y, (byte)ClearColor.Z, (byte)ClearColor.W);
            SDL_RenderClear(Game.SDLRenderer);

            Draw.CurrentSceneID = SceneID;

            try {
                Update();
            } catch(Exception e) {
                Debug.Log(LogLevel.Error, $"Scene \"{SceneID}\" update crashed!");
                Debug.Log(LogLevel.Message, e.ToString());
            }

            foreach(Entity e in Entities)
            {
                try {
                    e.UpdateCall();
                } catch(Exception err) {
                    Debug.Log(LogLevel.Error, $"Entity \"{e.GetType().Name}\" crashed!");
                    Debug.Log(LogLevel.Message, err.ToString());
                    RemoveEntity(e);
                }            
            }

            Draw.CurrentSceneID = null;

            Draw.DrawDrawBuffer(drawBuffer, SceneID);

            drawBuffer = new();

            HasRendered = true;
        }

        SDL_SetRenderTarget(Game.SDLRenderer, IntPtr.Zero);
        SDL_RenderCopy(Game.SDLRenderer, RenderTarget, IntPtr.Zero, ref LocalWindowSize);

        if (AllowWindowResize)
        {
            FUI.ResizeableRectangle(ref RelativeWindowSize, SceneID);
        }

        foreach (Entity e in RegisterEntityBacklog)
        {
            Entities.Add(e);
        }
        RegisterEntityBacklog = new();

        foreach (Entity e in removeEntity)
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