using Fjord.Modules.Graphics;
using System.Numerics;
using System.Collections.Generic;
using System;
using Fjord.Modules.Misc;
using static Fjord.Modules.Misc.math_uti;
using static SDL2.SDL;

namespace Fjord.Modules.Game {
    public static class tilemap_funcs {
        public static string create_pos(int x, int y) {
            var x__ = x.ToString().Length == 1 ? "0" + x.ToString() : x.ToString();
            var y__ = y.ToString().Length == 1 ? "0" + y.ToString() : y.ToString();
            return x__ + "," + y__; 
        }

        public static int get_x(string pos) {
            return Int32.Parse(pos.Split(',')[0]);
        }
    
        public static int get_y(string pos) {
            return Int32.Parse(pos.Split(',')[1]);
        }
    }

    public class tilemap {

        public string asset_pack;

        public Dictionary<string, Vector2> map;
        public Dictionary<string, bool> collision_map;
        public int grid_w, grid_h;
        public int w, h;
        public Vector2 position = new Vector2(0, 0);

        public string atlas_str;
        public IntPtr atlas;
        public int atlas_gridw;
        public int atlas_gridh;

        public double zoom = 1;

        public tilemap(int w, int h, int grid_w, int grid_h, int atlas_gridw_, int atlas_gridh_) {
            map = new Dictionary<string, Vector2>();
            collision_map = new Dictionary<string, bool>();
            for(var i = 0; i < w; i++) {
                for(var j = 0; j < h; j++) {
                    map.Add(tilemap_funcs.create_pos(i, j), new Vector2(0, 0));
                    collision_map.Add(tilemap_funcs.create_pos(i, j), false);
                }
            }

            this.grid_w = grid_w;
            this.grid_h = grid_h;
            this.w = w;
            this.h = h;
            this.atlas_gridw = atlas_gridw_;
            this.atlas_gridh = atlas_gridh_;
        }

        public void load_atlas() {
            string ass = game_manager.asset_pack;
            game_manager.set_asset_pack(asset_pack);

            atlas = texture_handler.load_texture(atlas_str, game_manager.renderer);
            
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
                return(map[tilemap_funcs.create_pos(_x, _y)]);
            else 
                return new Vector2(-1, -1);
        }

        public bool get_collision_at_pixel(int x, int y) {
            int _x = x - (int)position.X;
            int _y = y - (int)position.Y;
            _x = _x / (int)(grid_w * zoom);
            _y = _y / (int)(grid_h * zoom);

            if(_x >= 0 && _x < w && _y >= 0 && _y < h)
                return(collision_map[tilemap_funcs.create_pos(_x, _y)]);
            else 
                return false;
        }

        public void draw_tilemap() {
            for(var i = 0; i < w; i++) {
                for(var j = 0; j < h; j++) {
                    if(map[tilemap_funcs.create_pos(i, j)] != new Vector2(-1, -1)) {
                        // SDL.SDL_Rect src_rect = new SDL.SDL_Rect((int)map[tilemap_funcs.create_pos(i, j)].X * 8, (int)map[tilemap_funcs.create_pos(i, j)].X * 8, 8, 8);
                        // SDL.SDL_Rect dest_rect = new SDL.SDL_Rect((int)(position.X + (i * grid_w) * zoom),  (int)(position.Y + (j * grid_h) * zoom), (int)(grid_w * zoom), (int)(grid_h * zoom));

                        // SDL.SDL_RenderCopyEx(game_manager.renderer, atlas, ref src_rect, ref dest_rect, 0, ref point, SDL.SDL_RendererFlip.SDL_FLIP_NONE);

                        draw.texture_atlas(game_manager.renderer, atlas, (int)map[tilemap_funcs.create_pos(i, j)].X * 8, (int)map[tilemap_funcs.create_pos(i, j)].Y * 8, 8, 8, (int)(position.X + (i * grid_w) * zoom),  (int)(position.Y + (j * grid_h) * zoom), 0, (int)(grid_w * zoom), (int)(grid_h * zoom), new SDL_Point(0, 0), true);
                        //draw.texture_ext(game_manager.renderer, textures_intptr[map[i, j] - 1], (int)(position.X + (i * grid_w) * zoom), (int)(position.Y + (j * grid_h) * zoom), 0, (int)(grid_w * zoom), (int)(grid_h * zoom), point, true);
                    }
                }
            }
        }
    }
}