using Proj.Modules.Input;
using Proj.Modules.Ui;
using Proj.Modules.Debug;
using Proj.Modules.Graphics;
using System.Collections.Generic;
using SDL2;
using System;
using Newtonsoft.Json;
using Proj.Modules.Tools;
using System.Numerics;

namespace Proj.Game {
    public class platformer : scene {

        string full_path;
        string file;
        tilemap Tilemap;

        player Player = new player();

        public platformer() {
            full_path = game_manager.executable_path + "\\src\\resources\\platformer\\data\\tilemaps\\map.json";
            file = System.IO.File.ReadAllText(full_path);

            Tilemap = JsonConvert.DeserializeObject<tilemap>(file);

            Tilemap.load_textures();

            Tilemap.zoom = 2.5;
            Tilemap.position = new Vector2(0, 0);
        }

        public override void on_load() {
            Player.texture_origin = new Vector2();

            Player.texture_xscale = 2.5f;
            Player.texture_yscale = 2.5f;
        }   

        public override void update() {
            Player.update();

            int move_sp = 4;
            int x_sp = 0;
            int y_sp = 0;
            if(input.get_key_pressed(input.key_w)) {
                y_sp -= move_sp;
            } else if(input.get_key_pressed(input.key_s)) {
                y_sp += move_sp;
            } 

            if(input.get_key_pressed(input.key_a)) {
                x_sp -= move_sp;
            } else if(input.get_key_pressed(input.key_d)) {
                x_sp += move_sp;
            } 

            if(Tilemap.get_data_at_pixel((int)Player.position.X + x_sp, (int)Player.position.Y) == 1) 
                x_sp = 0;

            if(Tilemap.get_data_at_pixel((int)Player.position.X, (int)Player.position.Y + y_sp) == 1) 
                y_sp = 0;

            Player.position.X += x_sp;
            Player.position.Y += y_sp;
        }

        public override void render() {
            Tilemap.draw_tilemap();
            Player.render();
        }
    }
}