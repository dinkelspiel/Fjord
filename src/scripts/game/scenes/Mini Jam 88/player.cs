using static Proj.Modules.Debug.Debug;
using Proj.Modules.Misc;
using Proj.Modules.Game;
using Proj.Modules.Graphics;
using Proj.Modules.Input;
using Proj.Modules.Ui;
using Proj.Modules.Camera;

namespace Proj.Game
{
    public class player_entity : entity
    {
        public player_entity() {
            game_manager.set_asset_pack("MiniJam88");
            texture = texture_handler.load_texture("player.png", game_manager.renderer);
        }

        public override void update()
        {
            base.update();

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