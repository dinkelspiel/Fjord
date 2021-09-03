using Proj.Modules.Input;
using Proj.Modules.Ui;
using Proj.Modules.Debug;
using Proj.Modules.Graphics;
using Proj.Modules.Camera;
using Proj.Modules.Tools;
using Proj.Modules.Misc;
using System.Collections.Generic;
using SDL2;
using System;
using System.Numerics;
using Newtonsoft.Json;

namespace Proj.Game {
    public class tilemap_editor : scene {
        
        float zoom = 1;
        Vector2 pos;

        bool load_tex = false;
        string load_texture_string = "";

        bool load_tex_button = false;

        bool export_file = false;
        string export_file_string;

        bool export_file_button = false;

        string asset_pack = "general";
        bool change_asset_pack = false;

        tilemap Tilemap = new tilemap(80, 45, 8, 8);
        IntPtr atlas;

        int grid_x, grid_y, grid_x_end, grid_y_end;

        int selected_tile = -1;

        public override void on_load() {
            game_manager.set_asset_pack("test");
            atlas = texture_handler.load_texture("atlas.png", game_manager.renderer);

            game_manager.set_asset_pack("general");
            font_handler.load_font("font", "FiraCode", 22);
        }

        public override void update() {
            if(mouse.scrolling(0)) {
                zoom += 0.1f;
            } else if(mouse.scrolling(1)) {
                zoom -= 0.1f;
            }

            int move_sp = 4;
            if(input.get_key_pressed(input.key_w, "general")) {
                pos.Y -= move_sp;
            } else if(input.get_key_pressed(input.key_s, "general")) {
                pos.Y += move_sp;
            }

            if(input.get_key_pressed(input.key_a, "general")) {
                pos.X -= move_sp;
            } else if(input.get_key_pressed(input.key_d, "general")) {
                pos.X += move_sp;
            }   

            camera.set_viewport(pos.X, pos.Y);   

            if(math_uti.mouse_inside(470, 10, 200, 30) && mouse.button_just_pressed(0)) {
                change_asset_pack = !change_asset_pack;
            }  else if(!math_uti.mouse_inside(470, 10, 200, 30) && mouse.button_just_pressed(0)) {
                change_asset_pack = false;
            }

            if(change_asset_pack) {
                input.set_input_state("set_asset_pack");
            } else if(input.input_state == "set_asset_pack") {
                input.set_input_state("general");
            }

            if(math_uti.mouse_inside(260, 10, 200, 30) && mouse.button_just_pressed(0)) {
                load_tex = !load_tex;
            } else if(!math_uti.mouse_inside(260, 10, 200, 30) && mouse.button_just_pressed(0)) {
                load_tex = false;
            }

            if(load_tex) {
                input.set_input_state("load_texture");
            } else if(input.input_state == "load_texture") {
                input.set_input_state("general");
            }

            if(load_tex_button) {
                game_manager.set_asset_pack(asset_pack);
                Tilemap.textures.Add(load_texture_string);
                load_tex_button = false;
                load_tex = false;
                load_texture_string = "";
            }

            if(math_uti.mouse_inside(260, 100, 200, 30) && mouse.button_just_pressed(0)) {
                export_file = !export_file;
            } else if(!math_uti.mouse_inside(260, 100, 200, 30) && mouse.button_just_pressed(0)) {
                export_file = false;
            }

            if(export_file) {
                input.set_input_state("export_tilemap");
            } else if(input.input_state == "export_tilemap") {
                input.set_input_state("general");
            }

            if(export_file_button) {
                export_file_button = false;
                export_file = false;

                var _export_output = new tilemap(Tilemap.w, Tilemap.h, Tilemap.grid_w, Tilemap.grid_h) {
                    textures = Tilemap.textures,
                    map = Tilemap.map,
                    asset_pack = asset_pack
                }; 

                var json_string = JsonConvert.SerializeObject(_export_output);

                string full_path = game_manager.executable_path + "\\src\\resources\\" + asset_pack + "\\data\\tilemaps\\" + export_file_string;
                System.IO.File.WriteAllText(full_path, json_string);
            }

            grid_x = (int)(Tilemap.grid_w * zoom) - (int)camera.camera_position.X - (int)(Tilemap.grid_w * zoom);
            grid_y = (int)(Tilemap.grid_h * zoom) - (int)camera.camera_position.Y - (int)(Tilemap.grid_h * zoom);
            grid_x_end = grid_x + Tilemap.w * (int)(Tilemap.grid_w * zoom);
            grid_y_end = grid_y + Tilemap.h * (int)(Tilemap.grid_h * zoom);

            if(mouse.button_just_pressed(0) && selected_tile != -1 && math_uti.mouse_inside(grid_x, grid_y, grid_x_end, grid_y_end)) {
                var x_ = (mouse.x - grid_x);
                var y_ = (mouse.y - grid_y);
                var w_ = Tilemap.grid_w * zoom;
                var h_ = Tilemap.grid_h * zoom;
    
                x_ = x_ / (int)w_;
                y_ = y_ / (int)h_;

                if(x_ < Tilemap.w && x_ > -1 && y_ < Tilemap.h && y_ > -1)
                    Tilemap.map[(int)x_, (int)y_] = selected_tile + 1;
            }
        }

        public override void render() {
            for(var i = 0; i < Tilemap.w; i++) {
                for(var j = 0; j < Tilemap.h; j++) {
                    SDL.SDL_Rect rect;
                    rect.x = i * (int)(Tilemap.grid_w * zoom) + (grid_x);
                    rect.y = j * (int)(Tilemap.grid_h * zoom) + (grid_y);
                    rect.w = (int)(Tilemap.grid_w * zoom);
                    rect.h = (int)(Tilemap.grid_h * zoom);
                    draw.rect(game_manager.renderer, rect, 255, 255, 255, 255, false);

                    if(Tilemap.map[i, j] > 0) {
                        int w, h;
                        w = (int)(Tilemap.grid_w * zoom);
                        h = (int)(Tilemap.grid_h * zoom);
                    }
                }
            }

            SDL.SDL_Rect rect1;
            rect1.x = 0;
            rect1.y = 0;
            rect1.w = 250;
            rect1.h = 1280;
            draw.rect(game_manager.renderer, rect1, 0, 0, 0, 200, true);

            int iw = 0;
            draw.texture(game_manager.renderer, atlas, 10, 10, 0);

            zgui.input_box(260, 10, 200, 30, "font", ref load_texture_string, "texture path", "load_texture");
            zgui.button(260, 50, 80, 30, ref load_tex_button, "font", "Load");

            zgui.input_box(260, 100, 200, 30, "font", ref export_file_string, "export path", "export_tilemap");
            zgui.button(260, 140, 100, 30, ref export_file_button, "font", "Export");

            zgui.input_box(470, 10, 200, 30, "font", ref asset_pack, "", "set_asset_pack");
        }
    }
}