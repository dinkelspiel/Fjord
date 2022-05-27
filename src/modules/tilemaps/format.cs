using System.Collections.Generic;
using Fjord.Modules.Mathf;
using Fjord.Modules.Graphics;

namespace Fjord.Modules.Tilemaps
{
    #nullable enable
    public struct tilemap
    {
        public List<property> properties;
        public V2 grid_size;
        public V2 tile_size;
        public Dictionary<string, tile> tiles;
        public List<List<Dictionary<string, dynamic>>> tile_map;

        public Dictionary<string, dynamic>? get_tile(V2 pos) {
            #nullable enable
            if(tile_size is null || grid_size is null || tile_map is null) {
                return null;
            }
            #nullable disable

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