using System.Collections.Generic;
using System.Linq;
using Fjord.Modules.Debug;

namespace Fjord.Modules.Game {
    public abstract class scene {
        List<entity> entities = new List<entity>();

        public void add_entity(entity e) {
            entities.Add(e);
        }

        public virtual void update() { foreach(entity e in entities) { e.update(); } }
        public virtual void render() { 
            List<entity> sorted_entities = entities.OrderBy(e => e.depth).ToList();
            foreach(entity e in sorted_entities) { 
                e.render(); 
            } 
        }
        public virtual void on_load() {}
        public virtual void on_unload() {}
    }

    public static class scene_handler {
        private static Dictionary<string, scene> scenes = new Dictionary<string, scene>();
        private static string current_scene; 
        private static int scenes_loaded = 0;

        public static void add_scene(string id, scene scene_add) {
            scenes.Add(id, scene_add);
        }
        
        public static void load_scene(string id) {
            if(current_scene != null)
                scenes[current_scene].on_unload();

            current_scene = id;
            scenes[current_scene].on_load();

            Debug.Debug.send("Loaded scene '" + id + "' successfully!");

            scenes_loaded++;
        }

        public static void stop() {
            if(current_scene != null)
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