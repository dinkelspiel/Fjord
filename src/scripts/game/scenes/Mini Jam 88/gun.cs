using static Proj.Modules.Debug.Debug;
using Proj.Modules.Misc;
using Proj.Modules.Game;
using Proj.Modules.Graphics;
using Proj.Modules.Input;
using Proj.Modules.Ui;
using Proj.Modules.Camera;

namespace Proj.Game
{
    public class gun_entity : entity
    {
        int direction = 0;

        public gun_entity() {
            game_manager.set_asset_pack("MiniJam88");
            texture = texture_handler.load_texture("player.png", game_manager.renderer);
        }

        public override void update()
        {
            base.update();
            direction += 1;
            texture_angle = direction;
        }

        public override void render()
        {
            base.render();
        }
    }
}