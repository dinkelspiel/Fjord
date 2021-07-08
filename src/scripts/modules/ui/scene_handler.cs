using System.Collections.Generic;

namespace Proj.Modules.Ui {
    public class scene {
        public virtual void update() {}
        public virtual void render() {}
    }

    public static class scene_handler {
        private static Dictionary<string, scene> scenes = new Dictionary<string, scene>();
        private static string current_scene; 

        public static void add_scene(string id, scene scene_add) {
            scenes.Add(id, scene_add);
        }
        
        public static void load_scene(string id) {
            current_scene = id;
        }

        public static void update() {
            if(scenes.Count > 0) {
                scenes[current_scene].update();
            }
        }

        public static void render() {
            if(scenes.Count > 0) {
                scenes[current_scene].render();
            }
        }
    }
}