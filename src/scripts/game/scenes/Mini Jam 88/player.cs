using static Proj.Modules.Debug.Debug;
using Proj.Modules.Misc;
using Proj.Modules.Game;
using Proj.Modules.Graphics;
using Proj.Modules.Input;
using Proj.Modules.Ui;
using Proj.Modules.Camera;
using System.Numerics;
using SDL2;

namespace Proj.Game
{
    public class player_entity : entity
    {
        int direction = 0;

        public player_entity() {
            game_manager.set_asset_pack("MiniJam88");
            texture = texture_handler.load_texture("player.png", game_manager.renderer);

            SDL.SDL_Point size;
            uint format;
            int access;
            SDL.SDL_QueryTexture(texture, out format, out access, out size.x, out size.y);

            size.x = (int)(size.x * texture_xscale); 
            size.y = (int)(size.y * texture_yscale); 

            // SDL.SDL_Point center;
            // center.x = size.x / 2;
            // center.y = size.y / 2;

            texture_origin = new Vector2(position.X + size.x / 2, position.Y + size.y / 2);
        }

        public override void update()
        {
            base.update();

            direction++;
            texture_angle = direction;

            int move_speed = 4;
            if(input.get_key_pressed(input.key_w)) {
                position.Y -= move_speed;
            } else if(input.get_key_pressed(input.key_s)) {
                position.Y += move_speed;
            }

            if(input.get_key_pressed(input.key_a)) {
                position.X -= move_speed;
            } else if(input.get_key_pressed(input.key_d)) {
                position.X += move_speed;
            }
        }

        public override void render()
        {
            base.render();
        }
    }
}