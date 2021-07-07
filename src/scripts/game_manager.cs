using System;
using SDL2;
using System.Numerics;
using Proj.Modules;
using Proj.Game;

namespace Proj
{
    static class game_manager
    {
        public static bool is_running = false;

        public static IntPtr window;
        public static IntPtr renderer;

        public static int frame_start = 0;
        public static int frame_length = 0;

        public static bool running() {
            return is_running;
        }

        public static void init(string title, int xpos, int ypos, int width, int height, bool fullscreen) {

            SDL.SDL_WindowFlags flags = 0;
            if (fullscreen) {
                flags = SDL.SDL_WindowFlags.SDL_WINDOW_FULLSCREEN;
            }

            if (SDL.SDL_Init(SDL.SDL_INIT_EVERYTHING) == 0) {
                Debug.send(Debug.get(), "SDL initialized without errors");
                
                window = SDL.SDL_CreateWindow(title, xpos, ypos, width, height, flags);

                Debug.send(Debug.get(), "Window created without errors");


                renderer = SDL.SDL_CreateRenderer(window, -1, 0);
                SDL.SDL_SetRenderDrawColor(renderer, 47, 49, 90, 255); 
                SDL.SDL_SetRenderDrawBlendMode(renderer, SDL.SDL_BlendMode.SDL_BLENDMODE_BLEND);
                
                Debug.send(Debug.get(), "Renderer created without errors");

                is_running = true;
            } else {
                is_running = false;
            }

            scene_handler.add_scene("main", new main_scene());
            scene_handler.load_scene("main");
        }

        public static void update() {
            scene_handler.update();
        }

        public static void render() {
            SDL.SDL_RenderClear(renderer);

            scene_handler.render();

            SDL.SDL_RenderPresent(renderer);
        }

        public static void stop() {
            SDL.SDL_DestroyWindow(window);
            SDL.SDL_DestroyRenderer(renderer);
            SDL.SDL_Quit();
            Debug.send(Debug.get(), "Game cleaned without errors");
        }

        public static void tick_fps(int FPS) {
            frame_length = (int)SDL.SDL_GetTicks() - frame_start;
            int frame_delay = 1000 / FPS;

            if (frame_delay > frame_length) {
                SDL.SDL_Delay((uint)frame_delay - (uint)frame_length);
            }
            frame_start = (int)SDL.SDL_GetTicks();
        }
    }
}
