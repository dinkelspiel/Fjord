using Fjord.Debug;
using Fjord.Input;
using System.Numerics;
using static SDL2.SDL;

namespace ShooterThingy;

public abstract class Scene : ICloneable
{
    private bool AllowWindowResize = true;
    private bool AlwaysRebuildTexture = false;
    public Vector2 LocalMousePosition = new();
    internal SDL_FRect RelativeWindowSize = new()
    {
        x = 0.1f,
        y = 0.1f,
        w = 0.9f,
        h = 0.9f
    };

    internal SDL_FRect RelativeWindowSizeFinal = new ();

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

    public Scene(int width, int height)
    {
        OriginalWindowSize.X = width;
        OriginalWindowSize.Y = height;
        
        RenderTarget = SDL_CreateTexture(Game.SDLRenderer, SDL_PIXELFORMAT_RGBA8888,
            (int)SDL_TextureAccess.SDL_TEXTUREACCESS_TARGET, width, height);
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

    public Scene SetAllowWindowResize(bool Debug) {
        this.AllowWindowResize = Debug;

        return this;
    }

    public Scene SetAlwaysRebuildTexture(bool Allow)
    {
        this.AlwaysRebuildTexture = Allow;

        return this;
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
        RelativeWindowSizeFinal = Debug.GetDebugMode() ? new()
        {
            x = RelativeWindowSize.x - Debug.DebugWindowOffset.x,
            y = RelativeWindowSize.y - Debug.DebugWindowOffset.y,
            w = RelativeWindowSize.w - Debug.DebugWindowOffset.w,
            h = RelativeWindowSize.h - Debug.DebugWindowOffset.h,
        } : RelativeWindowSize;

        LocalWindowSize = new()
        {
            x = (int)(RelativeWindowSizeFinal.x * Game.Window.Width),
            y = (int)(RelativeWindowSizeFinal.y * Game.Window.Height),
            w = (int)((RelativeWindowSizeFinal.w - RelativeWindowSizeFinal.x) * Game.Window.Width),
            h = (int)((RelativeWindowSizeFinal.h - RelativeWindowSizeFinal.y) * Game.Window.Height)
        };

        if (AlwaysRebuildTexture)
        {
            SDL_DestroyTexture(RenderTarget);
            RenderTarget = SDL_CreateTexture(Game.SDLRenderer, SDL_PIXELFORMAT_RGBA8888,
            (int)SDL_TextureAccess.SDL_TEXTUREACCESS_TARGET, (int)LocalWindowSize.w, (int)LocalWindowSize.h);

            LocalMousePosition.X = (Mouse.Position.X - LocalWindowSize.x);
            LocalMousePosition.Y = (Mouse.Position.Y - LocalWindowSize.y);
        } else
        {
            LocalMousePosition.X = (Mouse.Position.X - LocalWindowSize.x) / ((float)LocalWindowSize.w / OriginalWindowSize.X);
            LocalMousePosition.Y = (Mouse.Position.Y - LocalWindowSize.y) / ((float)LocalWindowSize.h / OriginalWindowSize.Y);
        }

        
        Update();
    }

    internal void RenderCall()
    {
        SDL_SetRenderTarget(Game.SDLRenderer, RenderTarget);
        SDL_SetRenderDrawColor(Game.SDLRenderer, ClearColor.r, ClearColor.g, ClearColor.b, ClearColor.a);
        SDL_RenderClear(Game.SDLRenderer);
        SDL_SetRenderDrawColor(Game.SDLRenderer, 0, 0, 0, 255);
        
        Render();

        SDL_SetRenderTarget(Game.SDLRenderer, IntPtr.Zero);

        SDL_SetRenderTarget(Game.SDLRenderer, IntPtr.Zero);
        SDL_RenderCopy(Game.SDLRenderer, RenderTarget, IntPtr.Zero, ref LocalWindowSize);

        if (AllowWindowResize)
        {
            Ui.ResizeableRectangle(ref RelativeWindowSize);
        }
    }

    public object Clone()
    {
        return this.MemberwiseClone();
    }
}