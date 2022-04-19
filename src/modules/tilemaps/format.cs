using System.Collections.Generic;
using Fjord.Modules.Mathf;
using Fjord.Modules.Graphics;

namespace Fjord.Modules.Tilemaps
{
    public class tilemap
    {
        public List<property>? properties;
        public V2? grid_size;
        public V2? tile_size;
        public Dictionary<string, tile>? tiles;
        public List<List<Dictionary<string, dynamic>>>? tile_map;
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
}