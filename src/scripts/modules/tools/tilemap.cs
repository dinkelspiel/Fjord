using Proj.Modules.Graphics;
using System.Numerics;
using Proj.Modules.Misc;
using SDL2;

namespace Proj.Modules.Tools {
    public class tilemap {

        public string asset_pack;

        public Dictionary<Vector2, Vector2> map;
        public int grid_w, grid_h;
        public int w, h;
        public Vector2 position = new Vector2(0, 0);

        public string atlas_str;
        public IntPtr atlas;
        public int atlas_gridw;
        public int atlas_gridh;

        public double zoom = 1;

        public tilemap(int w, int h, int grid_w, int grid_h, int atlas_gridw_, int atlas_gridh_) {
            map = new Dictionary<Vector2, Vector2>();
            for(var i = 0; i < w; i++) {
                for(var j = 0; j < h; j++) {
                    map.Add(new Vector2(i, j), new Vector2(0, 0));
                }
            }

            this.grid_w = grid_w;
            this.grid_h = grid_h;
            this.w = w;
            this.h = h;
            this.atlas_gridw = atlas_gridw_;
            this.atlas_gridh = atlas_gridh_;
        }

        public void load_textures() {
            string ass = game_manager.asset_pack;
            game_manager.set_asset_pack(asset_pack);

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

        public Vector2 get_data_at_pixel(int x, int y) {
            int _x = x - (int)position.X;
            int _y = y - (int)position.Y;
            _x = _x / (int)(grid_w * zoom);
            _y = _y / (int)(grid_h * zoom);

            if(_x >= 0 && _x < w && _y >= 0 && _y < h)
                return(map[new Vector2(_x, _y)]);
            else 
                return new Vector2(-1, -1);
        }

        public void draw_tilemap() {
            for(var i = 0; i < w; i++) {
                for(var j = 0; j < h; j++) {
                    if(map[new Vector2(i, j)] != new Vector2(-1, -1)) {
                        SDL.SDL_Point point;
                        point.x = 0;
                        point.y = 0;

                        SDL.SDL_Rect src_rect = new SDL.SDL_Rect((int)map[new Vector2(i, j)].X * 8, (int)map[new Vector2(i, j)].X * 8, 8, 8);
                        SDL.SDL_Rect dest_rect = new SDL.SDL_Rect((int)(position.X + (i * grid_w) * zoom),  (int)(position.Y + (j * grid_h) * zoom), (int)(grid_w * zoom), (int)(grid_h * zoom));

                        SDL.SDL_RenderCopyEx(game_manager.renderer, atlas, ref src_rect, ref dest_rect, 0, ref point, SDL.SDL_RendererFlip.SDL_FLIP_NONE);

                        //draw.texture_ext(game_manager.renderer, textures_intptr[map[i, j] - 1], (int)(position.X + (i * grid_w) * zoom), (int)(position.Y + (j * grid_h) * zoom), 0, (int)(grid_w * zoom), (int)(grid_h * zoom), point, true);
                    }
                }
            }
        }
    }
}