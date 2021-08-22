using SDL2;
using System;

namespace Proj.Modules.Ui {
    public static class texture_handler {
        public static IntPtr load_texture(string file, IntPtr renderer) {
            IntPtr tmp_surface = SDL_image.IMG_Load("src/resources/" + game_manager.asset_pack + "/assets/images/" + file);
            IntPtr texture = SDL.SDL_CreateTextureFromSurface(game_manager.renderer, tmp_surface);
            SDL.SDL_FreeSurface(tmp_surface);
            return texture;
        }
    }
}