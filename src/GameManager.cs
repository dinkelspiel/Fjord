using System.Numerics;
using static SDL2.SDL;
using static SDL2.SDL_ttf;
using static SDL2.SDL_image;
using Fjord.Input;
using Fjord.Graphics;
using Fjord.Scenes;
using Fjord.Ui;

namespace Fjord;

public class Window
{
    public int Width;
    public int Height;
}
public static class Game
{
    public static IntPtr SDLWindow;
    public static IntPtr SDLRenderer;
    
    public static Window Window = new();

    internal static bool Running = true;

    private static ulong _timeNow = 0;
    private static ulong _timeLast = 0;
    public static double DeltaTime { internal set; get; } = 0.0;

    internal static float InputFPS = 0;
    internal static float UpdateFPS = 0;
    internal static float ProgramFPS = 0;

    private static double _fpsCapLast = 0;
    public static double FPSMax = 144;

    public static void Initialize(string title, int width, int height)
    {   
        #if DEBUG
            Debug.Log("Running in debug mode");
        #else
            Debug.Log("Running in release mode");
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

        SDL_SetRenderDrawBlendMode(SDLRenderer, SDL_BlendMode.SDL_BLENDMODE_BLEND);

        IMG_Init(IMG_InitFlags.IMG_INIT_PNG);
        Font.Initialize();
        Debug.Initialize();
        GlobalInput.Initialize();
        SceneHandler.Initialize();
        Debug.Log("Fjord Initalized");
    }

    public static void Stop()
    {
        Debug.Log("Fjord Stopped");
        Running = false;
        SDL_DestroyRenderer(SDLRenderer);
        SDL_DestroyWindow(SDLWindow);
        Font.Destroy();

        List<string> PrintLogs = new();

        foreach(DebugLog log in Debug.Logs)
        {
            if(log.level != LogLevel.User)
                PrintLogs.Add(String.Format("[{0}] {1} {2} -> {3}", log.time, log.level.ToString(), log.sender, log.message));
            else
                PrintLogs.Add(String.Format("[{0}] {1} -> {2}", log.time, log.level.ToString(), log.message));
        }

        var Path = "./Logs/Log_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt";
        if(!Directory.Exists("./Logs"))
        {
            Directory.CreateDirectory("./Logs");
        }
        if(PrintLogs.Count > 0) 
        {
            File.WriteAllLines(Path, PrintLogs);
        }
    }
    
    public static void Run()
    {
        while (Running)
        {
            var FPSCapNow = SDL_GetTicks64();
            var FPSCapDelta = FPSCapNow - _fpsCapLast;

            if(FPSCapDelta > 1000/FPSMax)
            {
                _timeNow = SDL_GetPerformanceCounter();
                DeltaTime = ((_timeNow - _timeLast)*1000 / (double)SDL_GetPerformanceFrequency()) * 0.001;
                if(DeltaTime > 0.01)
                {
                    DeltaTime = 0.01;
                }
                _timeLast = _timeNow;

                _fpsCapLast = FPSCapNow;

                ulong programStart = SDL_GetPerformanceCounter();

                ulong inputStart = SDL_GetPerformanceCounter();
                EventHandler.HandleEvents();
                ulong inputEnd = SDL_GetPerformanceCounter();

                ulong updateStart = SDL_GetPerformanceCounter();
                Update();
                ulong updateEnd = SDL_GetPerformanceCounter();

                ulong programEnd = SDL_GetPerformanceCounter();



                var elapsed = (inputEnd - inputStart) / (float)SDL_GetPerformanceFrequency();
                InputFPS = 1f / elapsed;

                elapsed = (updateEnd - updateStart) / (float)SDL_GetPerformanceFrequency();
                UpdateFPS = 1f / elapsed;

                elapsed = (programEnd - programStart) / (float)SDL_GetPerformanceFrequency();
                ProgramFPS = 1f / elapsed;

                for (var i = 0; i < GlobalKeyboard.downKeys.Length; i++)
                {
                    GlobalKeyboard.pressedKeys[i] = false;
                    GlobalKeyboard.downKeysLast[i] = GlobalKeyboard.downKeys[i];
                }

                foreach (var key in GlobalMouse.pressedKeys.Keys.ToList())
                {
                    GlobalMouse.pressedKeys[key] = false;
                    GlobalMouse.downKeysLast[key] = GlobalMouse.downKeys[key];
                }
                GlobalMouse.downKeys[MB.ScrollDown] = false;
                GlobalMouse.downKeys[MB.ScrollLeft] = false;
                GlobalMouse.downKeys[MB.ScrollRight] = false;
                GlobalMouse.downKeys[MB.ScrollUp] = false;

                if(SceneHandler.LoadedScenes.All(s => SceneHandler.Scenes[s].MouseInsideScene != true))
                {
                    SDL_ShowCursor(SDL_ENABLE);
                }   
                
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }
    }

    public static void Update()
    {
        SDL_GetWindowSize(SDLWindow, out Window.Width, out Window.Height);

        SDL_SetRenderDrawColor(SDLRenderer, 0, 0, 0, 255);
        SDL_RenderClear(SDLRenderer);

        foreach (string id in new List<string>(SceneHandler.LoadedScenes))
        {
            SceneHandler.Scenes[id].UpdateCall();
        }

        Draw.DrawDrawBuffer(Draw.drawBuffer, null);
        Draw.drawBuffer = new();

        SDL_RenderPresent(SDLRenderer);

        if (GlobalKeyboard.Pressed(Key.D, Mod.LShift, Mod.LCtrl))
        {
            if (!SceneHandler.IsLoaded<InspectorScene>())
                SceneHandler.Load<InspectorScene>();
            else
                SceneHandler.Unload<InspectorScene>();
        }

        if (GlobalKeyboard.Pressed(Key.C, Mod.LShift, Mod.LCtrl))
        {
            if (!SceneHandler.IsLoaded<ConsoleScene>()) {
                SceneHandler.Load<ConsoleScene>();
            } else
                SceneHandler.Unload<ConsoleScene>();
        }

        if (GlobalKeyboard.Pressed(Key.F, Mod.LShift, Mod.LCtrl))
        {
            if (!SceneHandler.IsLoaded<PerformanceScene>())
            {
                SceneHandler.Load<PerformanceScene>();
            }
            else
            {
                PerformanceScene scene = SceneHandler.Get<PerformanceScene>()!;
                if (scene.WindowSize.X < 450)
                {
                    if(scene.Position < 2)
                        scene.SetRelativeWindowSize(scene.RelativeWindowSize.x, scene.RelativeWindowSize.y, scene.RelativeWindowSize.w + 0.14f, scene.RelativeWindowSize.h);
                    else
                        scene.SetRelativeWindowSize(scene.RelativeWindowSize.x - 0.14f, scene.RelativeWindowSize.y, scene.RelativeWindowSize.w, scene.RelativeWindowSize.h);
                    scene.Size = true;
                } else
                {
                    if (scene.Position < 2)
                        scene.SetRelativeWindowSize(scene.RelativeWindowSize.x, scene.RelativeWindowSize.y, scene.RelativeWindowSize.w - 0.14f, scene.RelativeWindowSize.h);
                    else
                        scene.SetRelativeWindowSize(scene.RelativeWindowSize.x + 0.14f, scene.RelativeWindowSize.y, scene.RelativeWindowSize.w, scene.RelativeWindowSize.h);
                    scene.Size = false;
                    SceneHandler.Unload<PerformanceScene>();
                }
                    
            }
        }
    }
}