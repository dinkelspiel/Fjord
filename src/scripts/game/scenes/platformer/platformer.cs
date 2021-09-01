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

        }

        public override void update() {
            Player.update();

            int move_sp = 4;
            if(input.get_key_pressed(input.key_w)) {
                Player.transform.Y -= move_sp;
            } else if(input.get_key_pressed(input.key_s)) {
                Player.transform.Y += move_sp;
            } 

            if(input.get_key_pressed(input.key_a)) {
                Player.transform.X -= move_sp;
            } else if(input.get_key_pressed(input.key_d)) {
                Player.transform.X += move_sp;
            } 
        }

        public override void render() {
            Tilemap.draw_tilemap();
            Player.render();
        }
    }
}