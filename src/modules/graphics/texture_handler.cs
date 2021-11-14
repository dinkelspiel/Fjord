using static SDL2.SDL;
using static SDL2.SDL_image;
using System;
using System.IO;

namespace Fjord.Modules.Graphics {
    public static class texture_handler {
        public static IntPtr default_texture;

        public static void init() {
            IntPtr tmp_surface = IMG_Load(game.get_resource_folder() + "/general/assets/images/error.png");
            default_texture = SDL_CreateTextureFromSurface(game.renderer, tmp_surface);
            SDL_FreeSurface(tmp_surface);       
        }

        public static IntPtr load_texture(string file) {
            IntPtr tmp_surface = IMG_Load(game.get_resource_folder() + "/" + game.asset_pack + "/assets/images/" + file);
            IntPtr texture = default_texture;
            if(File.Exists(game.get_resource_folder() + "/" + game.asset_pack + "/assets/images/" + file)) {
                texture = SDL_CreateTextureFromSurface(game.renderer, tmp_surface);
            } else {
                Debug.Debug.error("Image not found: " + game.get_resource_folder() + "/" + game.asset_pack + "/assets/images/" + file);
                game.stop();
                return texture;
            }
             
            SDL_FreeSurface(tmp_surface);
            return texture;
        }
    }
}