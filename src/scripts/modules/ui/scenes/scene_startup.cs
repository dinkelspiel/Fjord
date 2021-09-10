using Fjord.Modules.Ui;
using Fjord.Modules.Graphics;
using Fjord.Modules.Debug;
using Fjord.Modules.Input;
using Fjord;
using SDL2;

namespace Fjord.Game
{
    public class scene_startup : scene
    {
        private int time = 0;
        int radius = 400;

        private IntPtr logo;

        public override void on_load()
        {
            game_manager.set_asset_pack("general");
            logo = texture_handler.load_texture("icon_large.png", game_manager.renderer);
        }

        public override void update()
        {
            base.update();

            time++;

            if(time > 170) {
                scene_handler.start_scene_running = false;
                scene_handler.load_scene(scene_handler.string_start_scene);
            }
        }

        public override void render()
        {
            base.render();
            var r = game_manager.bg_color.r;
            var g = game_manager.bg_color.g;
            var b = game_manager.bg_color.b;
            var a = game_manager.bg_color.a;

            draw.texture_ext(game_manager.renderer, logo, (int)game_manager.resolution.X / 2, (int)game_manager.resolution.Y / 2, 0, 1, 1);

            draw.text(game_manager.renderer, (int)game_manager.resolution.X / 2 - 620, (int)game_manager.resolution.Y / 2, "default", 72, "Made using ");
            draw.text(game_manager.renderer, (int)game_manager.resolution.X / 2, (int)game_manager.resolution.Y / 2, "default-bold", 128, "Fjord");
        }
    }
}