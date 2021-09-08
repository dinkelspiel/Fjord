using Fjord.Modules.Ui;
using Fjord;

namespace Fjord.Game
{
    public class game : scene
    {
        public override void on_load()
        {
            if(!scene_handler.get_scene("game-template")) {
                scene_handler.add_scene("game-template", new game());
                scene_handler.start_scene("game-template");
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