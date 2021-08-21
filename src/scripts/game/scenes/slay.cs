using Proj.Modules.Input;
using Proj.Modules.Ui;
using Proj.Modules.Debug;
using System.Collections.Generic;
using SDL2;
using System;

namespace Proj.Game {
    public class slay : scene {
        
        int height, width;
        int[,] map = new int[4, 4];

        public slay() {
            height = width = 4;
        }

        private void draw_map(int[,] map) {
            for(var i = 0; i < width; i++) {
                for(var j = 0; j < height; j++) {
                    SDL.SDL_Rect rect = new SDL.SDL_Rect();
                    rect.x = 50 + i * 16 + i * 2;
                    rect.y = 50 + j * 16 + j * 2;
                    rect.w = rect.h = 16;
                    draw.rect(game_manager.renderer, rect, 255, 255, 255, 255, true);
                }
            }
        }

        public override void on_load()
        {
            game_manager.set_render_resolution(game_manager.renderer, 300, 169);
        }

        public override void update() {

        }

        public override void render() {
            draw_map(map);
        }
    }
}