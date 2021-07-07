using Proj.Modules.Input;
using Proj.Modules.Ui;
using SDL2;

namespace Proj.Game {
    public class main_scene : scene {
        int x, y = 0;
        int move_speed = 5;

        public override void update() {
            if(input.get_key_pressed(input.key_w)) {
                y -= move_speed;
            } else if(input.get_key_pressed(input.key_s)) {
                y += move_speed;
            }

            if(input.get_key_pressed(input.key_a)) {
                x -= move_speed;
            } else if(input.get_key_pressed(input.key_d)) {
                x += move_speed;
            }
        }

        public override void render() {
            SDL.SDL_Rect rect;
            rect.x = x;
            rect.y = y;
            rect.w = rect.h = 100;
            SDL.SDL_SetRenderDrawColor(game_manager.renderer, 0, 0, 0, 255);
            SDL.SDL_RenderFillRect(game_manager.renderer, ref rect);
            SDL.SDL_SetRenderDrawColor(game_manager.renderer, 255, 255, 255, 255);
        }
    }
}