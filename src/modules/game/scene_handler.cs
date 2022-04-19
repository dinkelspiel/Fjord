using System.Collections.Generic;
using System;
using System.Linq;
using Fjord.Modules.Graphics;

namespace Fjord.Modules.Game {
    public abstract class scene {
        List<entity> entities = new List<entity>();

        public void add_entity(entity e) {
            entities.Add(e);
        }

        public virtual void update() { foreach(entity e in entities) { e.update(); } }

        public virtual void render() { 
            List<dynamic> sorted = new List<dynamic>();
            sorted.AddRange(entities);
            sorted.AddRange(draw.get_texture_buffer());
            sorted = sorted.OrderBy(e => e is entity ? e.depth : e.tex.get_depth()).ToList();
            foreach(dynamic e in sorted) {
                if(e is entity) {
                    Console.WriteLine("Hello");
                    e.render();
                } else {
                    draw.texture_direct(e.position, e.tex);
                }
            }

            // List<entity> sorted_entities = entities.OrderBy(e => e.depth).ToList();
            // foreach(entity e in entities) { 
            //     e.render(); 
            // } 

            // List<texture_buffer> sorted_texture_buffer = draw_texture_buffer.OrderBy(e => e.tex.get_depth()).ToList();
            // foreach(texture_buffer e in sorted_texture_buffer) { 
            //     texture_direct(e.position, e.tex);
            // } 

            draw.clean_texture_buffer();
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