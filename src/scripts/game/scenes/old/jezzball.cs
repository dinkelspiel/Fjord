using Proj.Modules.Input;
using Proj.Modules.Ui;
using Proj.Modules.Debug;
using Proj.Modules.Graphics;
using System.Collections.Generic;
using SDL2;
using System;
using Proj.Modules.Tools;
using System.Numerics;

namespace Proj.Game {
    public class jezzball : scene {

        tilemap World = new tilemap(80, 45, 16, 16);

        SDL.SDL_Point expand_pos;
        bool expand_dir;
        int expand_iteration;
        bool expand = false;

        public override void on_load() {
            World.position = new Vector2(8, 8);

            World.asset_pack = "jezzball";
            World.textures.Add("wall.png");
            World.load_textures();

            for(var i = 0; i < 80; i++) {
                for(var j = 0; j < 45; j++) {
                    if(i == 0 || j ==0 || j == 44 || i == 79)
                        World.map[i, j] = 1;
                }
            }
        }

        public override void update() {
            if(expand) {
                expand_iteration++;
                bool x_done1 = false;
                bool x_done2 = false;

                bool y_done1 = false;
                bool y_done2 = false;
                if(!expand_dir) {
                    if(expand_pos.x + expand_iteration < 79 && World.map[expand_pos.x + expand_iteration, expand_pos.y] != 1)
                        World.map[expand_pos.x + expand_iteration, expand_pos.y] = 1;
                    else 
                        x_done1 = true;

                    if(expand_pos.x - expand_iteration > 0 && World.map[expand_pos.x - expand_iteration, expand_pos.y] != 1)
                        World.map[expand_pos.x - expand_iteration, expand_pos.y] = 1;
                    else 
                        x_done2 = true;
                } else {
                    if(expand_pos.y + expand_iteration < 44 && World.map[expand_pos.x, expand_pos.y + expand_iteration] != 1)
                        World.map[expand_pos.x, expand_pos.y + expand_iteration] = 1;
                    else 
                        y_done1 = true;
                        
                    if(expand_pos.y - expand_iteration > 0 && World.map[expand_pos.x, expand_pos.y - expand_iteration] != 1)
                        World.map[expand_pos.x, expand_pos.y - expand_iteration] = 1;       
                    else 
                        y_done2 = true;             
                }

                if(x_done1 && x_done2 || y_done1 && y_done2) 
                    expand = false;
            }

            if(mouse.button_just_pressed(0) && !expand) {
                int x = (int)mouse.x / 16;
                int y = (int)mouse.y / 16;
                World.map[x, y] = 1;
                expand_pos.x = x;
                expand_pos.y = y;
                expand_iteration = 0;

                expand = true;
            }

            if(input.get_key_just_pressed(input.key_r) && !expand) {
                expand_dir = !expand_dir;
            }
        }

        public override void render() {
            World.draw_tilemap();
        }
    }
}