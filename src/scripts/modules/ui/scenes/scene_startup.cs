using Fjord.Modules.Ui;
using Fjord;

namespace Fjord.Game
{
    public class scene_startup : scene
    {
        private int time = 0;

        public override void on_load()
        {

        }

        public override void update()
        {
            base.update();

            time++;

            if(time > 60) {
                scene_handler.start_scene_running = false;
                scene_handler.load_scene(scene_handler.string_start_scene);
            }
        }

        public override void render()
        {
            base.render();
        }
    }
}