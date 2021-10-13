using System;
using static SDL2.SDL;
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
            game_manager.init("Engine", SDL_WINDOWPOS_CENTERED, SDL_WINDOWPOS_CENTERED, 1920, 1080, false, sys_args);
            while(game_manager.running()) {
                game_manager.frame_last = game_manager.frame_now;
                game_manager.frame_now = SDL_GetPerformanceCounter();

                game_manager.delta_time_ms = (double)((game_manager.frame_now - game_manager.frame_last)*1000 / (double)SDL_GetPerformanceFrequency());
                game_manager.delta_time = (double)((game_manager.frame_now - game_manager.frame_last)*10 / (double)SDL_GetPerformanceFrequency());

                event_handler.handle_events();
                try {
                     game_manager.update();
                } catch (Exception e) {
                    game_manager.stopwitherror(e);

                    throw;
                }
                    
                try {
                     game_manager.render();
                } catch (Exception e) {
                    game_manager.stopwitherror(e);

                    throw;
                }
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
