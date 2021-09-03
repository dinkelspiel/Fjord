using Proj.Modules.Input;
using Proj.Modules.Ui;
using Proj.Modules.Debug;
using Proj.Modules.Graphics;
using SDL2;
using System;
using System.Numerics;

namespace Proj.Game {
    public class main_scene : scene {

        Vector2[] hit_objects = new Vector2[6]; 
        int size = 140;

        public main_scene() {
            reset();
        }

        private void reset() {
            var random = new Random();
            for(var i = 0; i < 6; i ++) {
                hit_objects[i] = new Vector2(size * random.Next(4), -size * i);
            }
        }

        public override void on_load()
        {
            game_manager.set_render_resolution(game_manager.renderer, 1280, 720);
        }

        public override void update() {
            if(input.get_key_just_pressed(input.key_r)) {
                reset();
            }

            for(var i = 0; i < 6; i++) {
                hit_objects[i].Y += 10;
                Debug.send(hit_objects[i].Y);

                if(hit_objects[i].Y > size * 5) {
                    var random = new Random();
                    hit_objects[i] = new Vector2(size * random.Next(4), -size);
                }
            }
        }

        public override void render() {
            for(var i = 0; i < 6; i++) {
                SDL.SDL_Rect rect;
                rect.x = (int)hit_objects[i].X;
                rect.y = (int)hit_objects[i].Y;
                rect.w = rect.h = size;
                draw.rect(game_manager.renderer, rect, 255, 255, 255, 255, true);
            }
        }
    }
}