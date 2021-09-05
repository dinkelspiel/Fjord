using static Proj.Modules.Debug.Debug;
using Proj.Modules.Misc;
using Proj.Modules.Game;
using Proj.Modules.Graphics;
using Proj.Modules.Debug;
using Proj.Modules.Input;
using Proj.Modules.Ui;
using Proj.Modules.Camera;
using Newtonsoft.Json;
using System.Numerics;

namespace Proj.Game
{
    public class game : scene
    {
        player_entity player;
        Vector2 camera_pos = new Vector2(0, 0);

        tilemap level;

        List<enemy_entity> enemies = new List<enemy_entity>();
        List<ragdoll_entity> ragdolls = new List<ragdoll_entity>();

        public override void on_load()
        {
            if(!scene_handler.get_scene("MiniJam88")) {
                scene_handler.add_scene("MiniJam88", new game());
                scene_handler.add_scene("Opening", new opening_scene());
                scene_handler.load_scene("Opening");
            }

            game_manager.set_render_background(96, 96, 96, 255);

            level = new tilemap(40, 40, 8, 8, 8, 8);
            var full_path = game_manager.executable_path + "\\src\\resources\\MiniJam88\\data\\tilemaps\\map2.json";
            var file = System.IO.File.ReadAllText(full_path);

            level = JsonConvert.DeserializeObject<tilemap>(file);

            level.load_atlas();
            level.zoom = 2.5;
            level.position = new Vector2(0, 0);

            player = new player_entity(level);
            game_manager.set_render_resolution(game_manager.renderer, 450, 253);
            
            level.position.X -= 100;
            level.position.Y -= 100;

            var rand = new Random();
            for(var i = 0; i < 400; i++) {

                // 530 0
                // 350 380
                // 80 420
                // 270 580
                // -25 153
                
                var r = rand.Next(6);
                switch(r) {
                    case 0:
                        enemies.Add(new enemy_entity(530, 0, player, level));
                        break;
                    case 1:
                        enemies.Add(new enemy_entity(350, 380, player, level));
                        break;
                    case 2:
                        enemies.Add(new enemy_entity(80, 420, player, level));
                        break;
                    case 3:
                        enemies.Add(new enemy_entity(270, 580, player, level));
                        break;
                    case 4:
                        enemies.Add(new enemy_entity(-25, 153, player, level));
                        break;
                }
                
            }
        }

        public override void update()
        {
            base.update();
            player.update();
            
            camera_pos.X += (player.position.X - camera_pos.X) / 6;
            camera_pos.Y += (player.position.Y - camera_pos.Y) / 6;
            camera.set_viewport(camera_pos.X, camera_pos.Y);

            for(var i = 0; i < enemies.Count; i++) {
                enemies[i].update();
                if(enemies[i].should_die) {
                    ragdolls.Add(new ragdoll_entity(enemies[i].tex_id, (int)enemies[i].position.X, (int)enemies[i].position.Y, player, (int)math_uti.point_direction(player.position, enemies[i].position), 5, level));
                    enemies.RemoveAt(i);
                }
            }

            for(var i = 0; i < ragdolls.Count; i++) {
                ragdolls[i].update();
            }
        }

        public override void render()
        {
            base.render();

            level.draw_tilemap();

            foreach(enemy_entity en in enemies) {
                en.render();
            }

            for(var i = 0; i < ragdolls.Count; i++) {
                ragdolls[i].render();
            }

            player.render();
        }
    }
}