// using static Proj.Modules.Debug.Debug;
// using Proj.Modules.Misc;
// using Proj.Modules.Game;
// using Proj.Modules.Graphics;
// using Proj.Modules.Debug;
// using Proj.Modules.Input;
// using Proj.Modules.Ui;
// using Proj.Modules.Camera;
// using System.Numerics;

// namespace Proj.Game
// {
//     public class gun_entity : entity
//     {
//         List<bullet_entity> bullets = new List<bullet_entity>();

//         IntPtr bullet_texture;

//         public gun_entity() {
//             game_manager.set_asset_pack("MiniJam88");
//             texture = texture_handler.load_texture("gun.png", game_manager.renderer);
//             bullet_texture = texture_handler.load_texture("bullet.png", game_manager.renderer);
//         }

//         public override void update()
//         {
//             base.update();
//             texture_angle = (float)math_uti.point_direction(position, new Vector2(mouse.x - 640 + position.X, mouse.y - 360 + position.Y));
            
//             if(Math.Abs(texture_angle) > 90) {
//                 texture_flip = flip_type.vertical;
//             } else {
//                 texture_flip = flip_type.none;
//             }

//             if(mouse.button_just_pressed(0)) {
//                 bullets.Add(new bullet_entity((int)position.X, (int)position.Y, 4, 180 - (int)texture_angle + 180, bullet_texture));
//             }

//             foreach(bullet_entity bullet in bullets) {
//                 bullet.update();
//             }
//         }

//         public override void render()
//         {
//             base.render();

//             foreach(bullet_entity bullet in bullets) {
//                 bullet.render();
//             }
//         }
//     }
// }