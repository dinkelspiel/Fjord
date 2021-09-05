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
    public class player_entity : entity
    {
        private class drift_mark {
            public int x;
            public int y;
            public int angle;
            public int life;

            public drift_mark(int x, int y, int angle, int life) {
                this.x = x;
                this.y = y;
                this.angle = angle;
                this.life = life;
            }
        }

        tilemap Level;

        IntPtr cart;
        int direction = 0;

        Vector2 rotation_speed = new Vector2();
        float rotation_acc = 0.5f;
        int rotation_max_speed = 4;
        int rotation_deacc_mult = 6;

        int move_acc = 7;
        int move_angle = 0;
        public float move_speed = 0;
        int move_max_speed = 6;
        int move_deacc_mult = 12;

        int move_rotation_mult = 6;

        List<drift_mark> drift_marks = new List<drift_mark>();
        int create_drift_mark = 0;
        IntPtr drift_mark_tex;

        public player_entity(tilemap Level) {
            game_manager.set_asset_pack("MiniJam88");
            texture = texture_handler.load_texture("player.png", game_manager.renderer);

            cart = texture_handler.load_texture("cart.png", game_manager.renderer);
            drift_mark_tex = texture_handler.load_texture("drift_mark.png", game_manager.renderer);

            SDL.SDL_Point size;
            uint format;
            int access;
            SDL.SDL_QueryTexture(texture, out format, out access, out size.x, out size.y);

            size.x = (int)(size.x * texture_xscale); 
            size.y = (int)(size.y * texture_yscale); 

            // SDL.SDL_Point center;
            // center.x = size.x / 2;
            // center.y = size.y / 2;

            texture_origin = new Vector2(4, 4);

            texture_xscale = 2.5f;
            texture_yscale = 2.5f;

            this.Level = Level;
            this.Level.position.X = -40;
            this.Level.position.Y = -40;
        }

        public override void update()
        {
            base.update();

            if(input.get_key_pressed(input.key_w)) {
                move_speed += move_acc;
                move_speed = Math.Clamp(move_speed, -move_max_speed, move_max_speed);
            } else {
                move_speed -= move_speed / move_deacc_mult;
            }

            if(input.get_key_pressed(input.key_a)) {
                rotation_speed.X += rotation_acc;
            } else {
                rotation_speed.X -= rotation_speed.X / rotation_deacc_mult;
            }

            if(input.get_key_pressed(input.key_d)) {
                rotation_speed.Y += rotation_acc;
            } else {
                rotation_speed.Y -= rotation_speed.Y / rotation_deacc_mult;
            }

            rotation_speed.X = Math.Clamp(rotation_speed.X, -rotation_max_speed, rotation_max_speed);
            rotation_speed.Y = Math.Clamp(rotation_speed.Y, -rotation_max_speed, rotation_max_speed);

            texture_angle -= rotation_speed.X;
            texture_angle += rotation_speed.Y;

            if(Level.get_collision_at_pixel((int)(position.X + math_uti.lengthdir_x(move_speed, 270 - move_angle)), (int)(position.Y + math_uti.lengthdir_y(move_speed, 270 - move_angle)))) 
                move_speed = 0;

            position.X += (float)math_uti.lengthdir_x(move_speed, 270 - move_angle);
            position.Y += (float)math_uti.lengthdir_y(move_speed, 270 - move_angle);

            move_angle -= (int)(move_angle - (180 - texture_angle + 180)) / move_rotation_mult;
            // texture_angle = (int)math_uti.point_direction(position, new Vector2(mouse.x - 640 + position.X, mouse.y - 360 + position.Y)) + 90;

            if(create_drift_mark < 0 && move_speed > 1) {
                drift_marks.Add(new drift_mark((int)position.X, (int)position.Y, 180 - (int)move_angle + 180, 50));
                create_drift_mark = 0;
            }

            create_drift_mark--;
        }   

        public override void render()
        {
            for(var i = 0; i < drift_marks.Count; i++) {
                draw.texture_ext(game_manager.renderer, drift_mark_tex, drift_marks[i].x, drift_marks[i].y, drift_marks[i].angle, 20, 30, new SDL.SDL_Point(4, 6), true);
                drift_marks[i].life -= 1;
                if(drift_marks[i].life < 0) {
                    drift_marks.RemoveAt(i);
                }
            }



            base.render();
            draw.rect(game_manager.renderer, new SDL.SDL_Rect((int)(position.X + (float)math_uti.lengthdir_x(move_speed * 10, 270 - move_angle)), (int)(position.Y + (float)math_uti.lengthdir_y(move_speed * 10, 270 - move_angle)), 1, 1), 255, 255, 0, 255, true, true);

            draw.texture_ext(game_manager.renderer, cart, (int)position.X + (int)math_uti.lengthdir_x(25, texture_angle - 90), (int)position.Y + (int)math_uti.lengthdir_y(25, texture_angle - 90), texture_angle, 20, 30, new SDL.SDL_Point(4, 6), true);

            // var i = 0;
            // foreach(IntPtr texture in cart) {
            //     draw.texture_ext(game_manager.renderer, texture, 64, 64 - 2 * i, math_uti.point_direction(new Vector2(64, 64), new Vector2(mouse.x, mouse.y)), 30, 20, new SDL.SDL_Point(5, 3), false);

            //     i++;
            // }
        }
    }
}