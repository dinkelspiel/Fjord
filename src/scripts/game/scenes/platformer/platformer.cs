using Proj.Modules.Input;
using Proj.Modules.Ui;
using Proj.Modules.Debug;
using Proj.Modules.Graphics;
using System.Collections.Generic;
using SDL2;
using System;
using Newtonsoft.Json;
using Proj.Modules.Tools;

namespace Proj.Game {
    public class platformer : scene {

        string full_path;
        string file;
        tilemap Tilemap;

        public platformer() {
            full_path = game_manager.executable_path + "\\src\\resources\\platformer\\data\\tilemaps\\map.json";
            file = System.IO.File.ReadAllText(full_path);

            Tilemap = JsonConvert.DeserializeObject<tilemap>(file);

            Tilemap.load_textures();

            Tilemap.zoom = 2.5;
        }

        public override void on_load() {

        }

        public override void update() {

        }

        public override void render() {
            Tilemap.draw_tilemap(0, 0);
        }
    }
}