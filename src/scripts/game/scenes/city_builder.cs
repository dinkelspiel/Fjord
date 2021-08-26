using Proj.Modules.Input;
using Proj.Modules.Ui;
using Proj.Modules.Debug;
using Proj.Modules.Graphics;
using Proj.Modules.Misc;
using SDL2;
using System;
using System.Numerics;

namespace Proj.Game {
    public class city_builder : scene {

        IntPtr grass_tile = texture_handler.load_texture("city_builder/grass.png", game_manager.renderer);

        SDL.SDL_Point selected, closest;
        double closest_length = 0;

        SDL.SDL_Rect rect, rect2;
        SDL.SDL_Color bg, o1, o2, o3;

        public int[,] Map = new int[,] {
            {1, 1, 1, 1},
            {1, 1, 1, 1},
            {1, 1, 1, 1},
            {1, 1, 1, 1}
        };

        public city_builder() {
            selected.x = -1;
            selected.y = -1;

            rect.x = 10;
            rect.y = 10;
            rect.w = 500;
            rect.h = 281; 

            o1.r = 24;
            o1.g = 24;
            o1.b = 24;

            o2.r = 34;
            o2.g = 34;
            o2.b = 34;
            
            o3.r = 9;
            o3.g = 9;
            o3.b = 9;

            bg.r = 13;
            bg.g = 11;
            bg.b = 15;
            bg.a = 100;

            o1.a = o2.a = o3.a = 255;
        }

        public override void on_load() {
            game_manager.set_render_resolution(game_manager.renderer, 300, 169);
        }

        public override void update() {
            if(mouse.button_just_pressed(0)){
                for(var i = 0; i < 4; i++) {
                    for(var j = 0; j < 4; j++) {
                        int remove = j % 2 == 0 ? 13 : 0;
                        int x = 100 + i * 26 + remove;
                        int y =  50 + j * 8;
                        double offset = Math.Clamp(math_uti.point_distance(new Vector2(mouse.x / 4.26f, mouse.y / 4.26f), new Vector2(x, y)), 0, 20) / 2.5;
                        //draw.texture_ext(game_manager.renderer, grass_tile, x, y + (int)offset, 0);

                        double length = math_uti.point_distance(new Vector2(mouse.x / 4.26f, mouse.y / 4.26f), new Vector2(x, y + (int)offset));

                        if(length < closest_length) {
                            closest.x = i;
                            closest.y = j;
                            closest_length = length;
                        }
                    }
                }
                selected.x = closest.x;
                selected.y = closest.y;
            }

            if(mouse.button_just_pressed(1)) {
                selected.x = -1;
                selected.y = -1;
            }
        }

        public override void render() {
            for(var i = 0; i < 4; i++) {
                for(var j = 0; j < 4; j++) {
                    if(i == selected.x && j == selected.y)
                        continue;

                    int remove = j % 2 == 0 ? 15 : 0;
                    int x = 100 + i * 30 + remove;
                    int y =  50 + j * 7;

                    double offset = Math.Clamp(math_uti.point_distance(new Vector2(mouse.x / 4.26f, mouse.y / 4.26f), new Vector2(x, y)), 0, 15) / 2.5;

                    SDL.SDL_Rect rect;
                    rect.x = x;
                    rect.y = y + (int)offset;
                    rect.w = 1;
                    rect.h = 1;
                    
                    draw.texture_ext(game_manager.renderer, grass_tile, x, y + (int)offset, 0);
                    draw.rect(game_manager.renderer, rect, 255, 255, 255, 255, true);
                }
            }

            //zgui.window(rect, bg, o1, o2, o3, "text", "Test Window", true);

            //int yes = 30;

            //zgui.window_movement(ref rect.x, ref rect.y, ref rect.w, ref yes);
        }
    }
}