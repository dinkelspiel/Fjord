using static Proj.Modules.Debug.Debug;
using Proj.Modules.Misc;
using Proj.Modules.Game;
using Proj.Modules.Graphics;
using Proj.Modules.Input;
using Proj.Modules.Ui;
using Proj.Modules.Camera;
using System.Numerics;

namespace Proj.Game
{
    public class MiniJam88 : scene
    {

        player_entity player;
        Vector2 camera_pos = new Vector2(0, 0);
    
        public override void on_load()
        {
            player = new player_entity();
            game_manager.set_render_resolution(game_manager.renderer, 300, 169);
        }

        public override void update()
        {
            base.update();
            player.update();
            
            camera_pos.X += (player.position.X - camera_pos.X) / 4;
            camera_pos.Y += (player.position.Y - camera_pos.Y) / 4;
            camera.set_viewport(camera_pos.X, camera_pos.Y);
        }

        public override void render()
        {
            base.render();
            player.render();
        }
    }
}