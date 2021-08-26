using Proj.Modules.Input;
using Proj.Modules.Ui;
using Proj.Modules.Debug;
using Proj.Modules.Graphics;
using Proj.Modules.Misc;
using System.Collections.Generic;
using SDL2;
using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Proj.Game {
    public class export_output {
        public List<Vector2> nodes { get; set; }
        public List<IntPtr> textures { get; set; }
    }

    public class node_editor : scene {
        List<Vector2> nodes = new List<Vector2>();
        
        bool load_tex = false;
        bool load_tex_button = false;
        bool export_button = false;
        bool export_input = false;
        string value = "";
        string export_file = "";

        List<IntPtr> textures = new List<IntPtr>();

        public node_editor() {

        }

        public override void on_load() {
            game_manager.set_asset_pack("bloons");

            font_handler.load_font("bloons_font", "FiraCode", 22);

        }

        public override void update() {

            if(mouse.button_just_pressed(0) && !math_uti.mouse_inside(280, 10, 200, 30) && !math_uti.mouse_inside(280, 50, 80, 30) && !math_uti.mouse_inside(0, 0, 250, 1280) && !math_uti.mouse_inside(280, 100, 200, 30) && !math_uti.mouse_inside(280, 140, 100, 30)) {
                nodes.Add(new Vector2(mouse.x, mouse.y));
            }
            
            if(math_uti.mouse_inside(280, 20, 200, 30) && mouse.button_just_pressed(0)) {
                load_tex = !load_tex;
            }

            if(load_tex) {
                input.set_input_state("load_texture");
            } else if(input.input_state == "load_texture") {
                input.set_input_state("general");
            }

            if(math_uti.mouse_inside(280, 100, 200, 30) && mouse.button_just_pressed(0)) {
                export_input = !export_input;
            }

            if(load_tex_button) {
                load_tex_button = false;
                load_tex = false;
                if(value != "") {
                    textures.Add(texture_handler.load_texture(value, game_manager.renderer));
                }
            }

            if(export_input) {
                input.set_input_state("export_file");
            } else if(input.input_state == "export_file") {
                input.set_input_state("general");
            }

            if(mouse.button_just_pressed(1)) {
                for(var i = 0; i < nodes.Count; i++) {
                    if(math_uti.point_distance(new Vector2(nodes[i].X, nodes[i].Y), new Vector2(mouse.x, mouse.y)) < 5) {
                        nodes.RemoveAt(i);
                        break;
                    }
                }
            }

            if(export_button) {
                export_button = false;
                export_input = false;

                var _export_output = new export_output {
                    nodes = nodes,
                    textures = textures
                }; 

                var json_string = JsonConvert.SerializeObject(_export_output);

                Debug.send(json_string);
            }
        }

        public override void render() {
            SDL.SDL_Rect rect;
            rect.x = 0;
            rect.y = 0;
            rect.w = 270;
            rect.h = 1280;
            draw.rect(game_manager.renderer, rect, 0, 0, 0, 100, true);

            foreach(IntPtr tex in textures) {
                draw.texture_ext(game_manager.renderer, tex, 1280 / 2 , 720 / 2, 0);
            }

            for(var i = 0; i < nodes.Count; i++) {
                string text = "#" + i + " X: " + nodes[i].X.ToString() + " Y: " + nodes[i].Y.ToString();
                IntPtr texture;
                SDL.SDL_Rect rect1;
                font_handler.get_text_and_rect(game_manager.renderer, text, "bloons_font", out texture, out rect1, 0, 0);
                draw.texture_ext(game_manager.renderer, texture, 135, 20 * (i + 1), 0);
                
                SDL.SDL_Rect rect2;

                rect2.x = (int)nodes[i].X - 3;
                rect2.y = (int)nodes[i].Y - 3;
                rect2.w = 6;
                rect2.h = 6;

                rect1.x = 125 - rect1.w / 2;
                rect1.y = 20 * (i + 1) - rect1.h / 2;

                if(math_uti.mouse_inside(rect1.x, rect1.y, rect1.w, rect1.h - 8)) {
                    if(mouse.button_just_pressed(0)) {
                        if(i > 0) {
                            var save = nodes[i];
                            nodes[i] = nodes[i - 1];
                            nodes[i - 1] = save;
                        }
                    } 
                    if(mouse.button_just_pressed(1)) {
                        if(i < nodes.Count - 1) {
                            var save = nodes[i];
                            nodes[i] = nodes[i + 1];
                            nodes[i + 1] = save;
                        }
                    }
                    draw.rect(game_manager.renderer, rect2, 52, 134, 235, 255, true);

                    SDL.SDL_Rect marker;
                    marker.x = 2;
                    marker.y = rect1.y;
                    marker.w = 5;
                    marker.h = 22;

                    draw.rect(game_manager.renderer, marker, 52, 134, 235, 255, true);
                } else {
                    draw.rect(game_manager.renderer, rect2, 255, 255, 255, 255, true);
                }
            }

            zgui.input_box(280, 10, 200, 30, "bloons_font", ref value, "load_texture");
            zgui.button(280, 50, 80, 30, ref load_tex_button, "bloons_font", "Load");

            zgui.input_box(280, 100, 200, 30, "bloons_font", ref export_file, "export_file");
            zgui.button(280, 140, 100, 30, ref export_button, "bloons_font", "Export");
        }
    }
}