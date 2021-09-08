using System.Collections.Generic;
using Fjord.Modules.Debug;

namespace Fjord.Modules.Ui {
    public class scene {
        public virtual void update() {}
        public virtual void render() {}
        public virtual void on_load() {}
        public virtual void on_unload() {}
    }

    public static class scene_handler {
        private static Dictionary<string, scene> scenes = new Dictionary<string, scene>();
        private static string current_scene; 
        public static string string_start_scene;
        public static bool start_scene_running = false;

        public static void add_scene(string id, scene scene_add) {
            scenes.Add(id, scene_add);
        }
        
        public static void load_scene(string id) {
            if(!start_scene_running) {
                if(current_scene != null)
                    scenes[current_scene].on_unload();
                current_scene = id;
                scenes[current_scene].on_load();
                Debug.Debug.send("Loaded scene '" + id + "' successfully!");
            } else {
                Debug.Debug.send("Can't load scene during game startup!");
            }
        }

        public static void start_scene(string id) {
            string_start_scene = id;
        }

        public static void stop() {
            scenes[current_scene].on_unload();
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

        public static bool get_scene(string id) {
            return scenes.ContainsKey(id);
        }
    }
}