using System;
using SDL2;
using Fjord.Modules.Misc;
using Fjord.Modules.Debug;
using Fjord.Modules.Input;
using System.Threading;
using System.Threading.Tasks;

namespace Fjord
{
    class Program
    {
        static string[] sys_args;

        private static void game_thread() {
            game_manager.init("Engine", SDL.SDL_WINDOWPOS_CENTERED, SDL.SDL_WINDOWPOS_CENTERED, 1920, 1080, false, sys_args);
            while(game_manager.running()) {
                game_manager.frame_last = game_manager.frame_now;
                game_manager.frame_now = SDL.SDL_GetPerformanceCounter();

                game_manager.delta_time_ms = (double)((game_manager.frame_now - game_manager.frame_last)*1000 / (double)SDL.SDL_GetPerformanceFrequency());
                game_manager.delta_time = (double)((game_manager.frame_now - game_manager.frame_last)*10 / (double)SDL.SDL_GetPerformanceFrequency());

                event_handler.handle_events();
                game_manager.update();
                game_manager.render();
            }

            game_manager.stop();
        }

        static void Main(string[] args) {
            // Task game_task = Task.Factory.StartNew(() => game_thread());
            // Task.WaitAll(game_task, debug_web_task);
            sys_args = args;
            game_thread();
        }
    }
}
