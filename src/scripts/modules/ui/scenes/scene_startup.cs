using Fjord.Modules.Ui;
using Fjord.Modules.Graphics;
using Fjord;
using SDL2;

namespace Fjord.Game
{
    public class scene_startup : scene
    {
        private int time = 0;

        IntPtr title, title2;
        SDL.SDL_Rect title_rect, title_rect2;

        public override void on_load()
        {
            font_handler.get_text_and_rect(game_manager.renderer, "Fjord", "default-bold", out title, out title_rect);
            font_handler.get_text_and_rect(game_manager.renderer, "Made with", "default", out title2, out title_rect2);
        }

        public override void update()
        {
            base.update();

            time++;

            if(time > 420) {
                scene_handler.start_scene_running = false;
                scene_handler.load_scene(scene_handler.string_start_scene);
            }
        }

        public override void render()
        {
            base.render();

            int w, h, a;
            uint f;
            SDL.SDL_QueryTexture(title2, out f, out a, out w, out h);

            draw.texture_ext(game_manager.renderer, title, (int)game_manager.window_resolution.X / 2, (int)game_manager.window_resolution.Y / 2, 0);        
        }
    }
}