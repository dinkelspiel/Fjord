using Proj.Modules.Graphics;
using System.Numerics;
using Proj.Modules.Misc;

namespace Proj.Modules.Tools {
    public class tilemap {

        public string asset_pack;

        public int[,] map;
        public int grid_w, grid_h;
        public int w, h;
        public Vector2 position = new Vector2(0, 0);

        public List<string> textures = new List<string>();
        List<IntPtr> textures_intptr = new List<IntPtr>();

        public double zoom = 1;

        public tilemap(int w, int h, int grid_w, int grid_h) {
            map = new int[w,h];
            this.grid_w = grid_w;
            this.grid_h = grid_h;
            this.w = w;
            this.h = h;
        }

        public void load_textures() {
            string ass = game_manager.asset_pack;
            game_manager.set_asset_pack(asset_pack);

            foreach(string texture in textures) {
                textures_intptr.Add(texture_handler.load_texture(texture, game_manager.renderer));
            }

            game_manager.set_asset_pack(ass);
        }

        public Vector2 get_at_pixel(int x, int y) {
            int _x = x - (int)position.X;
            int _y = y - (int)position.Y;
            _x = _x / (int)(grid_w * zoom);
            _y = _y / (int)(grid_h * zoom);

            if(_x >= 0 && _x < w && _y >= 0 && _y < h)
                return(new Vector2(_x, _y));
            else 
                return(new Vector2(-1, -1));
        }

        public int get_data_at_pixel(int x, int y) {
            int _x = x - (int)position.X;
            int _y = y - (int)position.Y;
            _x = _x / (int)(grid_w * zoom);
            _y = _y / (int)(grid_h * zoom);

            if(_x >= 0 && _x < w && _y >= 0 && _y < h)
                return(map[_x, _y]);
            else 
                return(-1);
        }

        public void draw_tilemap() {
            for(var i = 0; i < w; i++) {
                for(var j = 0; j < h; j++) {
                    if(map[i, j] != -1)
                        if(map[i, j] == 0) 
                            continue;
                        draw.texture_ext(game_manager.renderer, textures_intptr[map[i, j] - 1], (int)(position.X + (i * grid_w) * zoom), (int)(position.Y + (j * grid_h) * zoom), 0, (int)(grid_w * zoom), (int)(grid_h * zoom), new Vector2(0, 0), true);
                }
            }
        }
    }
}