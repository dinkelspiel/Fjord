using Fjord.Modules.Window;
using Fjord.Modules.Graphics;
using Fjord.Modules.Debug;

using static SDL2.SDL;
using static SDL2.SDL_image;
using static SDL2.SDL_mixer;
using static SDL2.SDL_gfx;
using static SDL2.SDL_ttf;

using System.Collections.Generic;
using System.Numerics;
using System.IO;
using System;
using System.Text;
using System.Linq;
using Fjord.Modules.Input;
using Fjord.Modules.Misc;

namespace Fjord;

public static class Game {
    private static bool _isRunning = true;
    internal static IntPtr Window;
    internal static IntPtr Renderer;
    internal static IntPtr RenderTexture;
    public static Vector2 Size;

    internal static string ExecutablePath; 

    public static List<string> Log = new List<string>();
    
    static Game()
    {
        // FOR DEBUG CHANGE ON RELEASE DUMMY. YOU WILL FORGET. CHANGE ON RELEASE awlkfjaewlkfjawlkfjawlkifj
        if (OS.GetOS() == OS.Platform.Windows)
            ExecutablePath = String.Join("\\", System.Reflection.Assembly.GetEntryAssembly().Location.Split("\\").ToList().GetRange(0, System.Reflection.Assembly.GetEntryAssembly().Location.Split("\\").ToList().Count - 4));
        else
            ExecutablePath = String.Join("/", System.Reflection.Assembly.GetEntryAssembly().Location.Split("/").ToList().GetRange(0, System.Reflection.Assembly.GetEntryAssembly().Location.Split("/").ToList().Count - 4));
    }

    public static void Stop(Exception e) {
        if(e is not null) {
            Debug.SendInternal(e.Message + e.StackTrace.Split('\n')[0].Replace(" at ", " In ").Replace("  ", "").Replace("\n", ""));
            Debug.SendInternal("\n" + e.Message + "\n" + e.StackTrace);
        }

        Stop(1);
    }

    public static void Stop(int exit_code=0) {

        //debug_web.listener.Close();
        SceneHandler.Stop();

        SDL_DestroyWindow(Window);
        SDL_DestroyRenderer(Renderer);
        SDL_Quit();
        IMG_Quit();
        // Mix_Quit();

        Debug.SendInternal("Game cleaned");

        var time = DateTime.Now.ToString("yyyy/MMM/dd");
        var file = "Logs/" + time + "/" + DateTime.Now.ToString("HH.mm.ss") + ".txt";

        Directory.CreateDirectory("Logs/" + time);
        File.WriteAllLines(file, Log);

        System.Environment.Exit(0);
    }

    public static void Init(string Title, Vector2 Size, SDL_WindowFlags Flags=0) {
        
        SDL_Init(SDL_INIT_EVERYTHING);
        // SDL_SetHint(SDL_HINT_RENDER_SCALE_QUALITY, "2");

        Debug.SendInternal("SDL Initialized");

        Window = SDL_CreateWindow(Title, SDL_WINDOWPOS_CENTERED, SDL_WINDOWPOS_CENTERED, (int)Size.X, (int)Size.Y, Flags);
        TTF_Init();
        SDL_GL_SetAttribute(SDL_GLattr.SDL_GL_MULTISAMPLEBUFFERS, 1);
        SDL_GL_SetAttribute(SDL_GLattr.SDL_GL_MULTISAMPLESAMPLES, 4);
        SDL_SetHint(SDL_HINT_RENDER_SCALE_QUALITY, "2");

        Debug.SendInternal("Window Created");

        Renderer = SDL_CreateRenderer(Window, 0, SDL_RendererFlags.SDL_RENDERER_ACCELERATED);
        SDL_SetRenderDrawBlendMode(Renderer, SDL_BlendMode.SDL_BLENDMODE_BLEND);

        RenderTexture = SDL_CreateTexture(Renderer, (uint)SDL_PIXELFORMAT_RGBA8888, (int)SDL_TextureAccess.SDL_TEXTUREACCESS_TARGET, (int)Size.X, (int)Size.Y);

        Debug.SendInternal("Renderer Created");

        Game.Size = Size;
    }

    public static void Run(Scene Scene) {

        Scene.OnAwake();

        List<float> frames = new List<float>();

        while(_isRunning) {
            var start = SDL_GetPerformanceCounter();

            EventHandler.PollEvents();
            Update();
            Render();

            var end = SDL_GetPerformanceCounter();

            var elapsed = ((end - start) / (float)SDL_GetPerformanceFrequency());
            frames.Add(1.0f / elapsed);

            Keyboard._LastFramePressedKeys = (bool[])Keyboard._PressedKeys.Clone();

            // Debug.SendInternal($"FPS: {frames.Average()}");
        }
    }

    private static void Update() {
        SceneHandler.Update();
    }

    private static void Render() {
        // SDL_SetRenderTarget(Renderer, RenderTexture);
        SDL_RenderClear(Renderer);
        SDL_Rect _backgroundRect = new SDL_Rect(0, 0, (int)Size.X, (int)Size.Y);
        SDL_SetRenderDrawColor(Game.Renderer, 0, 0, 0, 0);
        SDL_RenderFillRect(Game.Renderer, ref _backgroundRect);
        SDL_SetRenderDrawBlendMode(Game.Renderer, SDL_BlendMode.SDL_BLENDMODE_BLEND);

        SceneHandler.Render();

        // SDL_SetRenderTarget(Renderer, IntPtr.Zero);
        // SDL_RenderCopyEx(Renderer, RenderTexture, IntPtr.Zero, IntPtr.Zero, (double)Camera.Rotation, IntPtr.Zero, SDL_RendererFlip.SDL_FLIP_NONE);
        SDL_RenderPresent(Renderer);
    }

    public static int GetTicks() {
        return (int)SDL_GetTicks();
    }
}