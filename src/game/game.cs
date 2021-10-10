using Fjord.Modules.Game;
using Fjord.Modules.Graphics;
using Fjord.Modules.Noise;
using SDL2;

namespace Fjord.Game
{
    public class game : scene
    {

        double[,] map = Noise.make_perlin_noise(100, 100);

        public override void on_load()
        {
            // This is where you load all your scenes 
            // The if statement is so that it doesn't trigger multiple times

            game_manager.set_render_resolution(game_manager.renderer, 156, 87);

            if(!scene_handler.get_scene("game-template")) {

                // Add all scenes
                scene_handler.add_scene("game-template", new game());

                // Load the first scene this can later be called in any file as for example a win condition to switch scene.
                scene_handler.load_scene("game-template");
            }
        }

        public override void update()
        {
            base.update();
        }

        public override void render()
        {
            base.render();

            for(int i = 0; i < 100; i++) {
                for(int j = 0; j < 100; j++) {
                    draw.rect(game_manager.renderer, new SDL.SDL_Rect(i, j, 1, 1), (byte)(map[i, j] * 255), (byte)(map[i, j] * 255), (byte)(map[i, j] * 255), 255, true, false);
                }
            }
        }
    }
}