using SDL2;
using System;

namespace Proj.Modules.Graphics {
    public static class texture_handler {
        public static IntPtr default_texture;

        public static void init() {
            IntPtr tmp_surface = SDL_image.IMG_Load("src/scripts/modules/graphics/error.png");
            default_texture = SDL.SDL_CreateTextureFromSurface(game_manager.renderer, tmp_surface);
            SDL.SDL_FreeSurface(tmp_surface);       
        }

        public static IntPtr load_texture(string file, IntPtr renderer) {
            IntPtr tmp_surface = SDL_image.IMG_Load("src/resources/" + game_manager.asset_pack + "/assets/images/" + file);;
            IntPtr texture;
            if(File.Exists("src/resources/" + game_manager.asset_pack + "/assets/images/" + file)) {
                texture = SDL.SDL_CreateTextureFromSurface(game_manager.renderer, tmp_surface);
            } else {
                texture = default_texture;
            }
             
            SDL.SDL_FreeSurface(tmp_surface);
            return texture;
        }
    }
}