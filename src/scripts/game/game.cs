using Proj.Modules.Ui;

namespace Proj.Game
{
    public class game : scene
    {
        public override void on_load()
        {
            if(!scene_handler.get_scene("game-template")) {
                scene_handler.add_scene("game-template", new game());
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