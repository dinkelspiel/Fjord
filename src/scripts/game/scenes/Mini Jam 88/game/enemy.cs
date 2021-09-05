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
    public class enemy_entity : entity
    {
        private enum states {
            turn_right,
            turn_left,
            forward,
            same
        }

        player_entity player;
        public int tex_id;
        public bool should_die = false;
        
        states last_state = 0;
        states state = 0;
        int swtich_state = 0;

        tilemap Level;

        public enemy_entity(int x, int y, player_entity player, tilemap level) {
            this.player = player;  
            this.Level = level; 

            game_manager.set_asset_pack("MiniJam88");
            var rand = new Random();
            tex_id = rand.Next(6);
            texture = texture_handler.load_texture("enemies/enemy_" + tex_id + ".png", game_manager.renderer);       

            texture_xscale = 2.5f;
            texture_yscale = 2.5f;

            position.X = x;
            position.Y = y;

            texture_origin = new Vector2(4, 4);
        }

        private void switch_state() {
            Array values = Enum.GetValues(typeof(states));
            Random random = new Random();
            last_state = state;
            state = (states)values.GetValue(random.Next(values.Length));
            swtich_state = 20;
            if(state == states.same) {
                state = last_state;
            }
        }

        public override void update()
        {
            base.update();

            if(swtich_state < 0) {
                switch_state();
            }

            swtich_state -= 1;

            int move_speed = 2;
            int turn_speed = 2;
            switch(state) {               
                case states.turn_right:
                    texture_angle += turn_speed;
                    break;
                case states.turn_left:
                    texture_angle -= turn_speed;
                    break;
                case states.forward:
                    if(Level.get_collision_at_pixel((int)(position.X + (float)math_uti.lengthdir_x(move_speed, texture_angle - 90)), (int)(position.Y + (float)math_uti.lengthdir_y(move_speed, texture_angle - 90)))) {
                        switch_state();
                    }
                    position.X += (float)math_uti.lengthdir_x(move_speed, texture_angle - 90);
                    position.Y += (float)math_uti.lengthdir_y(move_speed, texture_angle - 90);
                    break;
            }

            if(math_uti.point_distance(position, player.position) < 20 && player.move_speed > 1) {
                should_die = true;
            }
        }

        public override void render()
        {
            base.render();
        }
    }
}