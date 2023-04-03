using System.Numerics;
using static SDL2.SDL;
using static SDL2.SDL_ttf;
using ImGuiNET;
using Fjord.Input;

namespace ShooterThingy;

public class Window
{
    public int Width;
    public int Height;
}
public static class Game
{
    public static IntPtr SDLWindow;
    public static IntPtr SDLRenderer;
    
    public static Window Window;

    internal static bool Running = true;

    public static void Initialize(string title, int width, int height)
    {   
        #if DEBUG
            Console.WriteLine("Running in debug mode");
        #else
            Console.WriteLine("Running in release mode");
        #endif
        
        SDL_Init(SDL_INIT_EVERYTHING);
        
        SDLWindow = SDL_CreateWindow(title, SDL_WINDOWPOS_CENTERED, SDL_WINDOWPOS_CENTERED, width, height,
            SDL_WindowFlags.SDL_WINDOW_OPENGL | SDL_WindowFlags.SDL_WINDOW_RESIZABLE);

        SDLRenderer = SDL_CreateRenderer(SDLWindow, 0, SDL_RendererFlags.SDL_RENDERER_ACCELERATED);

        Window = new()
        {
            Width = width,
            Height = height
        };
        
        Font.Initialize();
    }

    public static void Stop()
    {
        Running = false;
        SDL_DestroyRenderer(SDLRenderer);
        SDL_DestroyWindow(SDLWindow);
        Font.Destroy();
    }
    
    public static void Run()
    {
        bool open = false;
        while (Running)
        {
            EventHandler.HandleEvents();
            
            Update();

            Render(ref open);
        }
    }

    public static void Update()
    {
        SDL_GetWindowSize(SDLWindow, out Window.Width, out Window.Height);
        
        foreach (string id in SceneHandler.LoadedScenes)
        {
            SceneHandler.Scenes[id].UpdateCall();
        }
    }

    public static void Render(ref bool open)
    {
        SDL_SetRenderDrawColor(SDLRenderer, 0, 0, 0, 255);
        SDL_RenderClear(SDLRenderer);

        List<string> Unload = new();

        foreach (string id in SceneHandler.LoadedScenes)
        {
            SceneHandler.Scenes[id].RenderCall();

            Ui.Button(
                new Vector2(
                    SceneHandler.Scenes[id].LocalWindowSize.x,
                    SceneHandler.Scenes[id].LocalWindowSize.y - 30
                ),
                "Unload",
                () =>
                {
                    Unload.Add(id);
                });
        }

        foreach (string id in Unload)
        {
            SceneHandler.Unload(id);
        }

        if (Debug.GetDebugMode())
        {
            new UiBuilder(new Vector4((int)(Game.Window.Width * 0.8), 0, (int)(Game.Window.Width * 0.2), (int)Game.Window.Height))
                .Title("Debug")
                .Container(
                    new UiBuilder()
                        .Title("Unloaded Scenes")
                        .ForEach(SceneHandler.Scenes.ToList().Where((v) => !SceneHandler.LoadedScenes.Contains(v.Key)).ToList(), (val) =>
                        {
                            return new List<object>() {
                                new UiTitle(val.Key) ,
                                new UiButton("Load", () => SceneHandler.Load(val.Key)),
                            };   
                        })
                        .Title("Loaded Scenes")
                        .ForEach(SceneHandler.LoadedScenes, (scene) =>
                        {
                            return new List<object>()
                            {
                                new UiTitle(scene),
                                new UiButton("Unload", () =>
                                {
                                    SceneHandler.Unload(scene);
                                })
                            };
                        })
                        .Build()
                )
                .Render();
        }

        Keyboard.pressedKeys = new();
        Mouse.Pressed = false;
     
        SDL_RenderPresent(SDLRenderer);
    }
}