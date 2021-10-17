using static SDL2.SDL;
using static SDL2.SDL_image;
using System;

namespace Fjord.Modules.Graphics {
    public static class texture_handler {
        public static IntPtr default_texture;

        public static void init() {
            string tmp = game_manager.asset_pack;
            game_manager.set_asset_pack("general");
            IntPtr tmp_surface = load_texture("error.png", game_manager.renderer);
            default_texture = SDL_CreateTextureFromSurface(game_manager.renderer, tmp_surface);
            SDL_FreeSurface(tmp_surface);       

            game_manager.set_asset_pack(tmp);
        }

        public static IntPtr load_texture(string file, IntPtr renderer) {
            IntPtr tmp_surface = IMG_Load(game_manager.get_resource_folder() + "/" + game_manager.asset_pack + "/assets/images/" + file);
            IntPtr texture;
            if(File.Exists(game_manager.get_resource_folder() + "/" + game_manager.asset_pack + "/assets/images/" + file)) {
                texture = SDL_CreateTextureFromSurface(game_manager.renderer, tmp_surface);
            } else {
                texture = default_texture;
            }
             
            SDL_FreeSurface(tmp_surface);
            return texture;
        }
    }
}