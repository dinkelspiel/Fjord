using Proj.Modules.Input;
using Proj.Modules.Ui;
using Proj.Modules.Debug;
using SDL2;
using System;

namespace Proj.Game {
    public class main_scene : scene {

        gui_element footer, b1, b2, b3, b4, body;
        int[] squares = new int[5];
        int score = 0;
        int counter = 0;

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

            Random random = new Random();
            for(int i = 0; i < squares.Length; i++) {
                squares[i] = random.Next(0, 4);
            }

            foreach(int square in squares) {
                Debug.send(square);
            }
        }

        private void regen() {
            Random random = new Random();
            for(int i = 0; i < squares.Length; i++) {
                squares[i] = random.Next(0, 4);
            }
        }

        private void pressed(int key) {
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
                game_manager.is_running = false;
            }
        }

        public override void update() {
            footer.update();
            body.update();

            counter++;


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

            // if(input.get_key_just_pressed(input.key_r)) {
            //     regen();
            // }
            Debug.send(score);
        }

        public override void render() {
            footer.render();
            body.render();

            SDL.SDL_Rect rect;
            rect.w = rect.h = 128;
            int i = 0;
            foreach(int square in squares) {
                rect.x = 128 * square + 384;
                rect.y =  i * 128 - 96 + (i * 10);
                draw.rect(game_manager.renderer, rect, 0, 0, 0, 255, true);
                i++;
            }
        }
    }
}