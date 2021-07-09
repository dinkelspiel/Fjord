using System;
using SDL2;
using Proj.Modules.Misc;
using Proj.Modules.Ui;
using System.Threading;
using System.Threading.Tasks;

namespace Proj
{
    class Program
    {
        private static void game_thread() {
            game_manager.init("Engine", SDL.SDL_WINDOWPOS_CENTERED, SDL.SDL_WINDOWPOS_CENTERED, 1280, 720, false);
            while(game_manager.running()) {
                event_handler.handle_events();
                game_manager.update();
                game_manager.render();

                game_manager.tick_fps(60);
            }

            game_manager.stop();
        }

        private static void debug_thead() {
            //debug_web.start_server();
        }

        static void Main(string[] args) {
            // Task game_task = Task.Factory.StartNew(() => game_thread());
            // Task debug_web_task = Task.Factory.StartNew(() => debug_thead());
            // Task.WaitAll(game_task, debug_web_task);
            game_thread();
        }
    }
}
