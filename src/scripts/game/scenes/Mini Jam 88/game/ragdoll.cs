using static Proj.Modules.Debug.Debug;
using Proj.Modules.Misc;
using Proj.Modules.Game;
using Proj.Modules.Graphics;
using Proj.Modules.Input;
using Proj.Modules.Ui;
using Proj.Modules.Debug;
using Proj.Modules.Camera;
using System.Numerics;
using SDL2;

namespace Proj.Game
{
    public class ragdoll_entity : entity
    {
        player_entity player;
        public bool should_die = false;
        int angle;
        float speed;
        tilemap Level;

        public ragdoll_entity(int tex_id, int x, int y, player_entity player, int angle, int speed, tilemap level) {
            this.player = player;   
            this.speed = speed;
            this.Level = level;

            game_manager.set_asset_pack("MiniJam88");
            texture = texture_handler.load_texture("ragdolls/ragdoll_" + tex_id + ".png", game_manager.renderer);       

            texture_xscale = 2.5f;
            texture_yscale = 2.5f;

            position.X = x;
            position.Y = y;

            this.angle = angle;
            texture_angle = new Random().Next(-180, 180);

            texture_origin = new Vector2(12, 12);
        }

        public override void update()
        {
            base.update();

            speed -= speed / 12;

            if(Level.get_collision_at_pixel((int)(position.X + (float)math_uti.lengthdir_x(speed, angle)), (int)(position.Y + (float)math_uti.lengthdir_y(speed, angle)))) 
                speed = 0;

            position.X += (float)math_uti.lengthdir_x(speed, angle);
            position.Y += (float)math_uti.lengthdir_y(speed, angle);
        }

        public override void render()
        {
            base.render();
        }
    }
}