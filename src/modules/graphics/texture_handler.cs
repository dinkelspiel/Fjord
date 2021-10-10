using static SDL2.SDL;
using static SDL2.SDL_image;
using System;

namespace Fjord.Modules.Graphics {
    public static class texture_handler {
        public static IntPtr default_texture;

        public static void init() {
            IntPtr tmp_surface = IMG_Load("src/modules/graphics/error.png");
            default_texture = SDL_CreateTextureFromSurface(game_manager.renderer, tmp_surface);
            SDL_FreeSurface(tmp_surface);       
        }

        public static IntPtr load_texture(string file, IntPtr renderer) {
            IntPtr tmp_surface = IMG_Load("resources/" + game_manager.asset_pack + "/assets/images/" + file);
            IntPtr texture;
            if(File.Exists("resources/" + game_manager.asset_pack + "/assets/images/" + file)) {
                texture = SDL_CreateTextureFromSurface(game_manager.renderer, tmp_surface);
            } else {
                texture = default_texture;
            }
             
            SDL_FreeSurface(tmp_surface);
            return texture;
        }
    }
}