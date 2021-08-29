using System.Collections.Generic;
using Proj.Modules.Debug;

namespace Proj.Modules.Ui {
    public class scene {
        public virtual void update() {}
        public virtual void render() {}
        public virtual void on_load() {}
        public virtual void on_unload() {}
    }

    public static class scene_handler {
        private static Dictionary<string, scene> scenes = new Dictionary<string, scene>();
        private static string current_scene; 

        public static void add_scene(string id, scene scene_add) {
            scenes.Add(id, scene_add);
        }
        
        public static void load_scene(string id) {
            if(current_scene != null)
                scenes[current_scene].on_unload();
            current_scene = id;
            scenes[current_scene].on_load();
            Debug.Debug.send("Loaded scene '" + id + "' successfully!");
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