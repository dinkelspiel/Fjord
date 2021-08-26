using Proj.Modules.Input;
using Proj.Modules.Ui;
using Proj.Modules.Debug;
using Proj.Modules.Misc;
using Proj.Modules.Graphics;
using System.Collections.Generic;
using SDL2;
using System;
using System.Numerics;

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
        public List<Vector2> path = new List<Vector2>();

        public balloon(int x_set, int y_set, types type_set) {
            x = x_set;
            y = y_set;
            type = type_set;
            speed = 2;
        }

        public void start_path(List<Vector2> path_set) {
            node = 0;
            path = path_set;
            x = path[0].X;
            y = path[0].Y;
        }
    }

    public class bloons : scene {

        List<Vector2> path = new List<Vector2>();

        IntPtr red_balloon_tex = texture_handler.load_texture("bloons/red_balloon.png", game_manager.renderer);
        IntPtr map = texture_handler.load_texture("bloons/map.png", game_manager.renderer);

        List<balloon> enemies = new List<balloon>();

        public bloons() {
            enemies.Add(new balloon(0, 0, types.red));

            path.Add(new Vector2(300, 400));
            path.Add(new Vector2(460, 400));
            path.Add(new Vector2(460, 320));
            path.Add(new Vector2(580, 320));
            path.Add(new Vector2(570, 600));
            path.Add(new Vector2(740, 600));
            path.Add(new Vector2(740, 250));
            path.Add(new Vector2(460, 250));

            enemies[0].start_path(path);
        }

        public override void on_load() {
           
        }

        public override void update() {

            if(mouse.button_just_pressed(0)) {
                Debug.send(mouse.x);
                Debug.send(mouse.y);
                Debug.send('-');
            }


            for(var i = 0; i < enemies.Count; i++) {

                if(math_uti.point_distance(new Vector2(enemies[i].x, enemies[i].y), new Vector2(enemies[i].path[enemies[i].node].X, enemies[i].path[enemies[i].node].Y)) < 5) {
                    enemies[i].node += enemies[i].node < enemies[i].path.Count - 1 ? 1 : 0;
                }

                enemies[i].x += (float)math_uti.lengthdir_x(enemies[i].speed, 180 - math_uti.point_direction(new Vector2(enemies[i].x, enemies[i].y), new Vector2(enemies[i].path[enemies[i].node].X, enemies[i].path[enemies[i].node].Y)) + 180);
                enemies[i].y += (float)math_uti.lengthdir_y(enemies[i].speed, 180 - math_uti.point_direction(new Vector2(enemies[i].x, enemies[i].y), new Vector2(enemies[i].path[enemies[i].node].X, enemies[i].path[enemies[i].node].Y)) + 180);
            }
        }

        public override void render() {
            draw.texture_ext(game_manager.renderer, map, (int)game_manager.resolution.X / 2, (int)game_manager.resolution.Y / 2, 0);
            foreach(balloon bloon in enemies) {
                draw.texture_ext(game_manager.renderer, red_balloon_tex, (int)bloon.x, (int)bloon.y - 32, 0);
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