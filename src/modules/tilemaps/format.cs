using System.Collections.Generic;
using Fjord.Modules.Mathf;
using Fjord.Modules.Graphics;

namespace Fjord.Modules.Tilemaps
{
    #nullable enable
    public class tilemap
    {
        public List<property> properties;
        public V2 grid_size;
        public V2 tile_size;
        public Dictionary<string, tile> tiles;
        public List<List<Dictionary<string, dynamic>>> tile_map;

        public tilemap() {
            grid_size = new V2(32, 32);
            tile_size = new V2(16, 16);
            properties = new List<property>();
            tiles  = new Dictionary<string, tile>();
            
            tile_map = new List<List<Dictionary<string, dynamic>>>(); 
            for(var i = 0; i < grid_size.x; i++) {
                tile_map.Add(new List<Dictionary<string, dynamic>>());
                for(var j = 0; j < grid_size.y; j++) {
                    tile_map[i].Add(new Dictionary<string, dynamic>() {
                        {"tile_id", ""}
                    });
                }
            }
        }

        public Dictionary<string, dynamic>? get_tile(V2 pos) {
            if(tile_size == null || grid_size == null || tile_map == null) {
                return null;
            }

            V2 fixed_pos = new V2();
            fixed_pos.x = (int)(pos.x / tile_size.x);
            fixed_pos.y = (int)(pos.y / tile_size.y);

            if((fixed_pos.x < grid_size.x && fixed_pos.x >= 0) && (fixed_pos.y < grid_size.y && fixed_pos.y >= 0)) {
                return tile_map[fixed_pos.x][fixed_pos.y];
            }
            return null;
        }
    }

    public struct property {
        public string name;
        public string datatype;
        public dynamic default_value;

        public property(string name, string data, dynamic default_value) {
            this.name = name;
            this.datatype = data;
            this.default_value = default_value;
        }
    }

    public struct tile {
        public string name;
        public string path;
        public texture tex;

        public tile(string name, string path) {
            this.name = name;
            this.path = path;
            this.tex = new texture(path);
        }
    }
    #nullable disable
}