using Proj.Modules.Input;
using Proj.Modules.Ui;
using Proj.Modules.Debug;
using Proj.Modules.Misc;
using Proj.Modules.Graphics;
using Proj.Modules.Camera;
using System.Collections.Generic;
using SDL2;
using System;
using System.Numerics;
using Newtonsoft.Json;

namespace Proj.Game {

    public enum types : int {
        red = 0
    }

    class balloon {
        public float x;
        public float y;
        public types type;

        public double speed;

        public int node = -2;

        public balloon(int x_set, int y_set, types type_set) {
            x = x_set;
            y = y_set;
            type = type_set;
            speed = 2;
        }

        public void start_path(map_format map) {
            node = 0;
            x = map.nodes[0].X;
            y = map.nodes[0].Y;
        }
    }

    public class bloons : scene {

        List<Vector2> path = new List<Vector2>();

        IntPtr red_balloon_tex;
        map_format map;
        List<IntPtr> textures = new List<IntPtr>();

        List<balloon> enemies = new List<balloon>();

        Vector2 pos;

        public bloons() {
            enemies.Add(new balloon(0, 0, types.red));
        }

        public void load_map(string path) {
            string full_path = game_manager.executable_path + "\\src\\resources\\bloons\\data\\maps\\" + path;
            string file = System.IO.File.ReadAllText(full_path);
            map_format map_ = JsonConvert.DeserializeObject<map_format>(file);

            foreach(string item in map_.textures) {
                Debug.send(game_manager.asset_pack);
                textures.Add(texture_handler.load_texture(item, game_manager.renderer));
            }

            map = map_;
        }

        public override void on_load() {
            game_manager.set_asset_pack("bloons");

            load_map("map.json");
            enemies[0].start_path(map);

            red_balloon_tex = texture_handler.load_texture("red_balloon.png", game_manager.renderer);
        }

        public override void update() {
            int move_speed = 4;
            if(input.get_key_pressed(input.key_w)) {
                pos.Y -= move_speed;
            } else if(input.get_key_pressed(input.key_s)) {
                pos.Y += move_speed;
            }

            if(input.get_key_pressed(input.key_a)) {
                pos.X -= move_speed;
            } else if(input.get_key_pressed(input.key_d)) {
                pos.X += move_speed;
            }

            camera.set_viewport(pos.X, pos.Y);

            for(var i = 0; i < enemies.Count; i++) {

                if(math_uti.point_distance(new Vector2(enemies[i].x, enemies[i].y), new Vector2(map.nodes[enemies[i].node].X, map.nodes[enemies[i].node].Y)) < 5) {
                    enemies[i].node += enemies[i].node < map.nodes.Count - 1 ? 1 : 0;
                }

                enemies[i].x += (float)math_uti.lengthdir_x(enemies[i].speed, 180 - math_uti.point_direction(new Vector2(enemies[i].x, enemies[i].y), new Vector2(map.nodes[enemies[i].node].X, map.nodes[enemies[i].node].Y)) + 180);
                enemies[i].y += (float)math_uti.lengthdir_y(enemies[i].speed, 180 - math_uti.point_direction(new Vector2(enemies[i].x, enemies[i].y), new Vector2(map.nodes[enemies[i].node].X, map.nodes[enemies[i].node].Y)) + 180);
            }
        }

        public override void render() {
            foreach(IntPtr texture in textures) {
                draw.texture(game_manager.renderer, texture, (int)game_manager.resolution.X / 2, (int)game_manager.resolution.Y / 2, 0, new Vector2(), true);
            }
            
            foreach(balloon bloon in enemies) {
                draw.texture(game_manager.renderer, red_balloon_tex, (int)bloon.x, (int)bloon.y - 32, 0, new Vector2(), true);
            }

            foreach(Vector2 point in path) {
                SDL.SDL_Rect rect;
                rect.x = (int)point.X - 3;
                rect.y = (int)point.Y - 3;
                rect.w = 6;
                rect.h = 6;
                draw.rect(game_manager.renderer,rect, 255, 255, 255, 255, true);
            }

        }
    }
}