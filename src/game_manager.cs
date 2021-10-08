using System;
using SDL2;
using System.Numerics;
using Fjord.Modules.Debug;
using Fjord.Modules.Ui;
using Fjord.Modules.Input;
using Fjord.Modules.Misc;
using Fjord.Modules.Graphics;
using Fjord.Modules.Game;
using Fjord.Game;
using System.IO;
using System.Reflection;

namespace Fjord
{
    static class game_manager
    {
        public static bool is_running = false;

        public static IntPtr window;
        public static IntPtr renderer;

        public static Vector2 window_resolution;
        public static Vector2 resolution;

        public static SDL.SDL_Color bg_color;

        public static ulong frame_now = SDL.SDL_GetPerformanceCounter();
        public static ulong frame_last = 0;

        public static double delta_time_ms = 0;
        public static double delta_time = 0;

        public static string asset_pack = "main";
        public static string executable_path;

        public static string[] sys_args;

        private static int[] fps_avg_arr = new int[120];
        private static int fps_avg_count = 0;

        public static bool running() {
            return is_running;
        }

        public static void init(string title, int xpos, int ypos, int width, int height, bool fullscreen, string[] sys_args) {

            game_manager.sys_args = sys_args;

            window_resolution = new Vector2(width, height);
            resolution = new Vector2(width, height);

            SDL.SDL_WindowFlags flags = 0;
            if (fullscreen) {
                flags = SDL.SDL_WindowFlags.SDL_WINDOW_FULLSCREEN;
            }

            if (SDL.SDL_Init(SDL.SDL_INIT_EVERYTHING) == 0) {
                Debug.send("SDL initialized without errors");
                
                window = SDL.SDL_CreateWindow(title, xpos, ypos, width, height, flags);

                Debug.send("Window created without errors");

                SDL_ttf.TTF_Init();

                renderer = SDL.SDL_CreateRenderer(window, -1, SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED);
                SDL.SDL_SetRenderDrawBlendMode(renderer, SDL.SDL_BlendMode.SDL_BLENDMODE_BLEND);
                
                Debug.send("Renderer created without errors");

                is_running = true;
            } else {
                is_running = false;
            }

            executable_path = Directory.GetCurrentDirectory();

            Language.load_langfile("en_US");

            set_render_background(26, 26, 28, 255);

            zgui.init();
            texture_handler.init();
            font_handler.init();

            scene game_;
            game_ = new game();
            game_.on_load();

            font_handler.load_font("default", "Sans", 42);
        }

        public static void update() {
            scene_handler.update();

            fps_avg_arr[fps_avg_count] = (int)(1000 / delta_time_ms);
            fps_avg_count++;

            if(fps_avg_count > fps_avg_arr.Length - 1) {
                fps_avg_count = 0;
            }
        }

        public static void render() {
            SDL.SDL_RenderClear(renderer);

            scene_handler.render();

            debug_gui.draw_fps();

            SDL.SDL_RenderPresent(renderer);

            mouse.llmb = mouse.lmb;
            mouse.lrmb = mouse.rmb;

            for(var i = 0; i < input.pressed_keys.Length; i++) {
                input.last_frame[i] = input.pressed_keys[i];
            }
        }

        public static void stop() {

            //debug_web.listener.Close();
            scene_handler.stop();

            SDL.SDL_DestroyWindow(window);
            SDL.SDL_DestroyRenderer(renderer);
            SDL.SDL_Quit();

            Debug.send("Game cleaned without errors");
            System.Environment.Exit(0);
        }

        public static int get_fps() {
            return (int)Queryable.Average(fps_avg_arr.AsQueryable());
        }

        public static int get_fps_exact() {
            return (int)(1000 / delta_time_ms);
        }

        [Obsolete("\"tick_fps(int FPS)\" is deprecated. Use \"delta_time\" multiplied to your framerate dependant variables.")]
        public static void tick_fps(int FPS) {
            double frame_delay = 1000 / FPS;

            if (frame_delay > game_manager.delta_time_ms) {
                SDL.SDL_Delay((uint)(frame_delay - game_manager.delta_time_ms));
            }
        }

        public static void set_render_resolution(IntPtr renderer, int width, int height) {
            SDL.SDL_RenderSetLogicalSize(renderer, width, height);
            resolution.X = width;
            resolution.Y = height;
            //SDL.SDL_RenderSetLogicalSize(game_manager.renderer, 300, 169);
        }

        public static void set_render_background(byte r, byte g, byte b, byte a) {
            SDL.SDL_Color color;
            color.r = r;
            color.g = g;
            color.b = b;
            color.a = a;

            SDL.SDL_SetRenderDrawColor(game_manager.renderer, r, g, b, a);

            bg_color = color;
        }

        public static void set_asset_pack(string asset_pack_set) {
            asset_pack = asset_pack_set;
        }

        public static void load_icon() {
            IntPtr icon = SDL_image.IMG_Load("resources/" + game_manager.asset_pack + "/assets/images/icon.png");
            SDL.SDL_SetWindowIcon(game_manager.window, icon);
        }
    }
}
