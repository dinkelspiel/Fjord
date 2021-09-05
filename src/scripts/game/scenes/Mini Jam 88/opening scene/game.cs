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
    public class opening_scene : scene
    {
        int time = 0;
        IntPtr player_tex;
        IntPtr alert_tex;
        IntPtr hand_tex;
        IntPtr poster_tex;

        Vector2 playerpos = new Vector2(-250, 0);
        Vector2 alertpos = new Vector2(-400, -400);
        Vector2 handpos = new Vector2(-400, -400);
        Vector2 camera_pos = new Vector2(0, 0);
        Vector2 poster_pos = new Vector2(-400, 400);

        int player_angle = 90;

        tilemap level;

        public override void on_load()
        {
            game_manager.set_asset_pack("MiniJam88");
            player_tex = texture_handler.load_texture("player.png", game_manager.renderer);
            alert_tex = texture_handler.load_texture("alert.png", game_manager.renderer);
            hand_tex = texture_handler.load_texture("hand.png", game_manager.renderer);
            poster_tex = texture_handler.load_texture("poster.png", game_manager.renderer);
        
            level = new tilemap(20, 20, 8, 8, 8, 8);
            var full_path = game_manager.executable_path + "\\src\\resources\\MiniJam88\\data\\tilemaps\\openingscene.json";
            var file = System.IO.File.ReadAllText(full_path);

            level = JsonConvert.DeserializeObject<tilemap>(file);

            level.load_atlas();
            level.zoom = 2.5;
            level.position = new Vector2(-130, -130);
        }

        public override void update()
        {
            time++;

            camera_pos.X += (0 - camera_pos.X) / 6;
            camera_pos.Y += (0 - camera_pos.Y) / 6;
            camera.set_viewport(camera_pos.X, camera_pos.Y);

            if(time < 140) { 
                int move_sp = 2;
                playerpos.X += (float)math_uti.lengthdir_x(move_sp, 0);
                playerpos.Y += (float)math_uti.lengthdir_y(move_sp, 0);
            }

            else if(time == 160) {
                alertpos = new Vector2(28, -30);
            }

            else if(time == 200) {
                handpos = new Vector2(26, 6);
            }

            else if(time > 200 && time < 210) {
                int move_sp = 2;
                handpos.X += (float)math_uti.lengthdir_x(move_sp, 0);
                handpos.Y += (float)math_uti.lengthdir_y(move_sp, 0);                
            } else if(time == 240 ) {
                poster_pos = new Vector2(0, 0);
            } else if(time == 460 ) {
                poster_pos = new Vector2(-400, -400);
                handpos = new Vector2(-400, -400);
                alertpos = new Vector2(-400, -400);
            } else if(time > 510) {
                player_angle = 0;
                int move_sp = 2;
                playerpos.X += (float)math_uti.lengthdir_x(move_sp, 270);
                playerpos.Y += (float)math_uti.lengthdir_y(move_sp, 270);                
            }

            if(time > 600) {
                scene_handler.load_scene("MiniJam88");
            }
        }

        public override void render() 
        {
            level.draw_tilemap();

            draw.texture_ext(game_manager.renderer, hand_tex, (int)handpos.X, (int)handpos.Y, 0, 25, 25, new SDL2.SDL.SDL_Point(12, 12), true);
            draw.texture_ext(game_manager.renderer, player_tex, (int)playerpos.X, (int)playerpos.Y, player_angle, 30, 30, new SDL2.SDL.SDL_Point(5, 5), true);
            draw.texture_ext(game_manager.renderer, alert_tex, (int)alertpos.X, (int)alertpos.Y, 0, 10, 40, new SDL2.SDL.SDL_Point(1, 10), true);
            draw.texture_ext(game_manager.renderer, poster_tex, (int)poster_pos.X, (int)poster_pos.Y, 0, 141, 200, new SDL2.SDL.SDL_Point(70, 100), true);
        }
    }
}