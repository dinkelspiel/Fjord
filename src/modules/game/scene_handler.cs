using System.Collections.Generic;
using System;
using System.Linq;
using System.IO;
using static SDL2.SDL;
using Fjord.Modules.Graphics;
using Fjord.Modules.Mathf;
using Fjord.Modules.Camera;
using Fjord.Modules.Tilemaps;
using Newtonsoft.Json;

namespace Fjord.Modules.Game {
    public abstract class scene {
        private tilemap tiles;
        private List<entity> entities = new List<entity>();
        private V4 background_color = new V4(26, 26, 28, 255);
        private V2 resolution = new V2(1920, 1080);

        public tilemap get_tiles() {
            return tiles;
        }

        public void set_tiles(tilemap tiles) {
            this.tiles = tiles;
        }

        public List<entity> get_entities() {
            return entities;
        }

        public void add_entity(entity e) {
            e.set_parent(this);
            entities.Add(e);
        }

        public void set_background(V4 color) {
            background_color = color;
        }

        public void set_background(int r, int g, int b) {
            background_color.x = r;
            background_color.y = g;
            background_color.z = b;
        }

        public V4 get_background() {
            return background_color;
        }

        public void set_resolution(int w, int h) {
            resolution = new V2(w, h);
        }

        public void set_resolution(V2 resolution) {
            this.resolution = resolution;
        }

        public V2 get_resolution() {
            return resolution;
        }

        public virtual void update() {}

        public void updatecall() {
            update();

            foreach(entity e in entities) {
                e.update();
            }
        }

        public virtual void render() {}

        public void rendercall() { 
            render();

            if(!this.tiles.Equals(default(tilemap))) {
                for(var i = 0; i < tiles.grid_size.x; i++) {
                    for(var j = 0; j < tiles.grid_size.y; j++) {
                        V2 pos = new V2((int)(i * tiles.tile_size.x), (int)(j * tiles.tile_size.y));
                        // draw.rect(new V4(pos.x, pos.y, tiles.tile_size.x, tiles.tile_size.y), color.black, false); // Draw tilemap outline

                        if(tiles.tile_map[i][j].Keys.ToList().Contains("tile_id")) {
                            if(tiles.tiles.Keys.ToList().Contains(tiles.tile_map[i][j]["tile_id"])) {
                                texture tile_texture = (texture)tiles.tiles[tiles.tile_map[i][j]["tile_id"]].tex.Clone();
                                tile_texture.set_origin(draw_origin.CENTER);
                                tile_texture.set_depth(-10000);
                                if(tiles.tile_map[i][j].Keys.ToList().Contains("rotation"))
                                    tile_texture.set_angle(tiles.tile_map[i][j]["rotation"]);
                                draw.texture(pos, tile_texture);
                            }
                        }
                    }
                }
            }

            List<entity> sorted_entities = new List<entity>(entities);
            // sorted_entities = entities.OrderBy(e => e.depth).ToList();
            sorted_entities = sorted_entities.OrderBy(e => e.depth).ToList();

            foreach(entity e in sorted_entities) {
                e.render();
            }

            List<draw.texture_buffer> sorted_textures = new List<draw.texture_buffer>(draw.get_texture_buffer());
            sorted_textures = sorted_textures.OrderBy(e => e.tex.get_depth()).ToList();
            foreach(draw.texture_buffer e in sorted_textures) {
                draw.texture_direct((V2)e.position - (V2)camera.get(), e.tex);
            }
        }
        public virtual void on_load() {}
        public virtual void on_unload() {}
    }

    public static class scene_handler {
        private static Dictionary<string, scene> scenes = new Dictionary<string, scene>();
        private static string current_scene; 
        private static int scenes_loaded = 0;

        public static void register(string id, scene scene_add) {
            scenes.Add(id, scene_add);
        }
        
        public static void start(string id) {
            if(current_scene != null)
                scenes[current_scene].on_unload();

            current_scene = id;
            
            try {
                scenes[current_scene].on_load();

                SDL_SetRenderDrawColor(game.renderer, scenes[current_scene].get_background());
                SDL_RenderSetLogicalSize(game.renderer, scenes[current_scene].get_resolution().x, scenes[current_scene].get_resolution().y);
            } catch(Exception e) {
                Debug.Debug.send("-- OnLoad Error --");
                game.stop(e);
            }

            Debug.Debug.send("Loaded scene '" + id + "' successfully!");

            scenes_loaded++;
        }

        public static void load_tilemap(string id, string tilemap) {
            string JsonString = "";

            if(File.Exists(game.get_resource_folder() + "/" + game.asset_pack + "/data/tilemaps/" + tilemap + ".ftm")) {
                JsonString = File.ReadAllText(game.get_resource_folder() + "/" + game.asset_pack + "/data/tilemaps/" + tilemap + ".ftm");
            } else {
                Debug.Debug.send("Loading of tilemap '" + tilemap + "' failed!");
                return;
            }

            tilemap format = JsonConvert.DeserializeObject<tilemap>(JsonString);
            foreach(string key in format.tiles.Keys) {
                format.tiles[key].tex.set_texture(format.tiles[key].path);
            }
            
            scenes[id].set_tiles(format);
        }

        public static Dictionary<string, dynamic> get_tile(V2 pos) {
            V2 fixed_pos = new V2();
            fixed_pos.x = pos.x / scenes[current_scene].get_tiles().tile_size.x;
            fixed_pos.y = pos.y / scenes[current_scene].get_tiles().tile_size.y;

            if(!(fixed_pos.x >= 0 && fixed_pos.x < scenes[current_scene].get_tiles().grid_size.x)) {
                return scenes[current_scene].get_tiles().tile_map[0][0];
            }
            if(!(fixed_pos.y >= 0 && fixed_pos.y < scenes[current_scene].get_tiles().grid_size.y)) {
                return scenes[current_scene].get_tiles().tile_map[0][0];
            }

            return scenes[current_scene].get_tiles().tile_map[fixed_pos.x][fixed_pos.y];
        }

        public static void stop() {
            if(current_scene != null)
                scenes[current_scene].on_unload();
        }

        public static void update() {
            if(scenes.Count > 0) {
                scenes[current_scene].updatecall();
            }
        }

        public static void render() {
            if(scenes.Count > 0) {
                scenes[current_scene].rendercall();
            }
        }

        public static bool scene_exists(string id) {
            return scenes.ContainsKey(id);
        }

        public static scene get_current_scene() {
            return scenes[current_scene];
        }

        public static scene get(string id) {
            return scenes[id];
        }
    }
}