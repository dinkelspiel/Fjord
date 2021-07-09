using Proj.Modules.Input;
using Proj.Modules.Ui;
using Proj.Modules.Debug;
using SDL2;

namespace Proj.Game {
    public class main_scene : scene {
        int x, y = 0;
        int move_speed = 5;

        gui_element bg, sidebar, button;

        public main_scene() {
            bg = new gui_element(new center_constraint(), new center_constraint(), new percentage_constraint(0.9f), new percentage_constraint(0.9f));
            bg.set_color(230, 221, 198, 255, 12);

            sidebar = new gui_element();
            bg.add_child(ref sidebar);
            sidebar.set_size_constraint(new percentage_constraint(0.3f), new percentage_constraint(0.95f), 6);
            sidebar.set_position_constraint(new percentage_constraint(0.17f),new center_constraint(), 6);
            sidebar.set_color(255, 255, 255, 255, 12);

            button = new gui_element();
            button.set_size_constraint(new aspect_constraint(1), new percentage_constraint(0.1f), 6);
            button.set_position_constraint(new pixel_constraint(50f), new pixel_constraint(50f), 6);
            button.set_color(100, 100, 100, 255, 1);
            sidebar.add_child(ref button);
            
        }

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

            if(button.mouse_hovered()) {
                button.set_color(200, 200, 200, 255, 3);
            } else {
                button.set_color(100, 100, 100, 255, 12);
            }

            bg.update();
        }

        public override void render() {
            bg.render();

            SDL.SDL_Rect rect;
            rect.x = x;
            rect.y = y;
            rect.w = rect.h = 100;
            //draw.rect(game_manager.renderer, rect, 255, 255, 255, 255, true);
        }
    }
}