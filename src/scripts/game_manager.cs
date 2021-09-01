using System;
using SDL2;
using System.Numerics;
using Proj.Modules.Debug;
using Proj.Modules.Ui;
using Proj.Modules.Input;
using Proj.Modules.Misc;
using Proj.Modules.Graphics;
using Proj.Game;
using System.IO;
using System.Reflection;

namespace Proj
{
    static class game_manager
    {
        public static bool is_running = false;

        public static IntPtr window;
        public static IntPtr renderer;

        public static Vector2 window_resolution = new Vector2(1280, 720); 
        public static Vector2 resolution = new Vector2(1280, 720); 

        public static SDL.SDL_Color bg_color;
        
        public static screen_rect screen;

        public static int frame_start = 0;
        public static int frame_length = 0;

        public static string asset_pack = "main";
        public static string executable_path;

        public static bool running() {
            return is_running;
        }

        public static void init(string title, int xpos, int ypos, int width, int height, bool fullscreen) {

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

            screen = new screen_rect();

            set_render_background(26, 26, 28, 255);

            zgui.init();
            texture_handler.init();
            font_handler.init();

            scene_handler.add_scene("text_editor", new text_editor());
            scene_handler.add_scene("city_builder", new city_builder());
            scene_handler.add_scene("bloons", new bloons());
            scene_handler.add_scene("node_editor", new node_editor());
            scene_handler.add_scene("client", new game_client());
            scene_handler.add_scene("server", new game_server());
            scene_handler.add_scene("tilemap", new tilemap_editor());
            scene_handler.add_scene("platformer", new platformer());
            scene_handler.add_scene("rope", new rope());
            scene_handler.add_scene("jezzball", new jezzball());
            scene_handler.load_scene("platformer");

            font_handler.load_font("default", "Sans", 42);
        }

        public static void update() {
            screen.screen_update();
            scene_handler.update();
        }

        public static void render() {
            SDL.SDL_RenderClear(renderer);

            scene_handler.render();

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

        public static void tick_fps(int FPS) {
            frame_length = (int)SDL.SDL_GetTicks() - frame_start;
            int frame_delay = 1000 / FPS;

            if (frame_delay > frame_length) {
                SDL.SDL_Delay((uint)frame_delay - (uint)frame_length);
            }
            frame_start = (int)SDL.SDL_GetTicks();
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
            Debug.send("Loaded asset pack '" + asset_pack_set + "' successfully.");
        }
    }
}
