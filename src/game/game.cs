using Fjord.Modules.Game;

namespace Fjord.Game
{
    public class game : scene
    {
        public override void on_load()
        {
            // This is where you load all your scenes 
            // The if statement is so that it doesn't trigger multiple times

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
        }
    }
}