using Proj.Modules.Input;
using Proj.Modules.Ui;
using Proj.Modules.Debug;
using SDL2;
using System;

namespace Proj.Game {
    public class main_scene : scene {

        gui_element footer, b1, b2, b3, b4, body, s1, s2, s3, s4, s5, reset_screen;
        int[] squares = new int[5];
        int[] order = new int[5]{0, 1, 2, 3, 4};
        int score = 0;
        int counter = 0;

        IntPtr texture1, texture2;
        SDL.SDL_Rect rect1, rect2;

        public main_scene() {
            footer = new gui_element();
            footer.set_position_constraint(new center_constraint(), new percentage_constraint(0.9f), 1);
            footer.set_size_constraint(new pixel_constraint(512), new pixel_constraint(128), 1);
            footer.set_color(0, 0, 0, 255, 1);

            b1 = new gui_element();
            b1.set_position_constraint(new pixel_constraint(64), new center_constraint(), 1);
            b1.set_size_constraint(new percentage_constraint(0.25f), new aspect_constraint(1f), 1);
            b1.set_color(100, 100, 100, 255, 1);
            footer.add_child(ref b1);

            b2 = new gui_element();
            b2.set_position_constraint(new pixel_constraint(192), new center_constraint(), 1);
            b2.set_size_constraint(new percentage_constraint(0.25f), new aspect_constraint(1f), 1);
            b2.set_color(130, 130, 130, 255, 1);
            footer.add_child(ref b2);

            b3 = new gui_element();
            b3.set_position_constraint(new pixel_constraint(320), new center_constraint(), 1);
            b3.set_size_constraint(new percentage_constraint(0.25f), new aspect_constraint(1f), 1);
            b3.set_color(100, 100, 100, 255, 1);
            footer.add_child(ref b3);

            b4 = new gui_element();
            b4.set_position_constraint(new pixel_constraint(448), new center_constraint(), 1);
            b4.set_size_constraint(new percentage_constraint(0.25f), new aspect_constraint(1f), 1);
            b4.set_color(130, 130, 130, 255, 1);
            footer.add_child(ref b4);

            body = new gui_element();
            body.set_position_constraint(new center_constraint(), new percentage_constraint(0.405f), 1);
            body.set_size_constraint(new pixel_constraint(512), new percentage_constraint(0.81f), 1);
            body.set_color(220, 220, 220);

            s1 = new gui_element();
            s1.set_size_constraint(new pixel_constraint(128), new pixel_constraint(128), 1);
            s1.set_color(0, 0, 0, 255, 1);

            s2 = new gui_element();
            s2.set_size_constraint(new pixel_constraint(128), new pixel_constraint(128), 1);
            s2.set_color(0, 0, 0, 255, 1);

            s3 = new gui_element();
            s3.set_size_constraint(new pixel_constraint(128), new pixel_constraint(128), 1);
            s3.set_color(0, 0, 0, 255, 1);

            s4 = new gui_element();
            s4.set_size_constraint(new pixel_constraint(128), new pixel_constraint(128), 1);
            s4.set_color(0, 0, 0, 255, 1);

            s5 = new gui_element();
            s5.set_size_constraint(new pixel_constraint(128), new pixel_constraint(128), 1);
            s5.set_color(0, 0, 0, 255, 1);

            body.add_child(ref s1);
            body.add_child(ref s2);
            body.add_child(ref s3);
            body.add_child(ref s4);
            body.add_child(ref s5);

            reset_screen = new gui_element();
            reset_screen.set_position_constraint(new center_constraint(), new center_constraint(), 1);
            reset_screen.set_size_constraint(new percentage_constraint(1), new percentage_constraint(1), 1);
            reset_screen.set_color(0, 0, 0, 100, 1);

            Random random = new Random();
            for(int i = 0; i < squares.Length; i++) {
                squares[i] = random.Next(0, 4);
            }
        }

        private void regen() {
            Random random = new Random();
            for(int i = 0; i < squares.Length; i++) {
                squares[i] = random.Next(0, 4);
            }
        }

        private void pressed(int key) {
            order[0] = order[4];
            order[4] = order[3];
            order[3] = order[2];
            order[2] = order[1];
            order[1] = order[0];
            Random random = new Random();
            if(key == squares[squares.Length - 1]) {
                score++;
                squares[4] = squares[3];
                squares[3] = squares[2];
                squares[2] = squares[1];
                squares[1] = squares[0];
                squares[0] = random.Next(0, 4);
            } else {
                Debug.send(score);
                score = 0;
                regen();
                input.set_input_state("restart");
            }
        }

        public override void update() {
            footer.update();
            body.update();
            reset_screen.update();

            font_handler.get_text_and_rect(game_manager.renderer, 0, 0, score.ToString(), "default", out texture1, out rect1);
            font_handler.get_text_and_rect(game_manager.renderer, 1280 / 2, 720 / 2, "Restart by pressing 'R'", "default", out texture2, out rect2);

            counter++;

            if(input.get_input_state() == "general") {
                b1.set_color(100, 100, 100, 255, 3);
                if(input.get_key_just_pressed(input.key_e)) {
                    b1.set_color(255, 255, 255, 255, 3);
                    pressed(0);
                }

                b2.set_color(130, 130, 130, 255, 3);
                if(input.get_key_just_pressed(input.key_f)) {
                    b2.set_color(255, 255, 255, 255, 3);
                    pressed(1);
                }

                b3.set_color(100, 100, 100, 255, 3);
                if(input.get_key_just_pressed(input.key_j)) {
                    b3.set_color(255, 255, 255, 255, 3);
                    pressed(2);
                }

                b4.set_color(130, 130, 130, 255, 3);
                if(input.get_key_just_pressed(input.key_i)) {
                    b4.set_color(255, 255, 255, 255, 3);
                    pressed(3);
                }
            } else {
                if(input.get_key_just_pressed(input.key_r)) {
                    input.set_input_state("general");
                }
            }
        }

        public override void render() {
            footer.render();
            body.render();

            SDL.SDL_Rect rect;
            rect.w = rect.h = 128;
            int i = 0;

            if(input.get_input_state() == "general") {
                foreach(int square in squares) {
                    switch(i) {
                        case 0:
                            s1.set_position_constraint(new pixel_constraint(128 * square + 64), new pixel_constraint(128 * i + (i * 10) - 30), 1);
                            break;
                        case 1:
                            s2.set_position_constraint(new pixel_constraint(128 * square + 64), new pixel_constraint(128 * i + (i * 10) - 30), 1);
                            break;
                        case 2:
                            s3.set_position_constraint(new pixel_constraint(128 * square + 64), new pixel_constraint(128 * i + (i * 10) - 30), 1);
                            break;
                        case 3:
                            s4.set_position_constraint(new pixel_constraint(128 * square + 64), new pixel_constraint(128 * i + (i * 10) - 30), 1);
                            break;
                        case 4:
                            s5.set_position_constraint(new pixel_constraint(128 * square + 64), new pixel_constraint(128 * i + (i * 10) - 30), 1);
                            break;
                    }

                    i++;  
                }
                
                SDL.SDL_Rect dest;
                dest.x = dest.y = 0;
                dest.w = dest.h = 200;
                SDL.SDL_RenderCopy(game_manager.renderer, texture1, ref dest, ref rect1);
            } else {
                reset_screen.render();      
                SDL.SDL_Rect dest;
                dest.x = dest.y = 0;
                dest.w = dest.h = 200;
                SDL.SDL_RenderCopy(game_manager.renderer, texture2, ref dest, ref rect2);     
            }
        }
    }
}