using SDL2;
using System;
using System.Threading;
using System.Threading.Tasks;
using Proj.Modules.Camera;
using System.Numerics;

namespace Proj.Modules.Graphics {
    public enum flip_type {
        none = 0,
        horizontal = 1,
        vertical = 2,
        both = 3
    }

    public static class draw {
        public static void rect(IntPtr renderer, SDL.SDL_Rect rect, byte r, byte g, byte b, byte a, bool fill) {
            SDL.SDL_Color old_color;
            SDL.SDL_GetRenderDrawColor(game_manager.renderer, out old_color.r, out old_color.g, out old_color.b, out old_color.a);
            SDL.SDL_SetRenderDrawColor(game_manager.renderer, r, g, b, a);

            if(fill) {
                SDL.SDL_RenderFillRect(game_manager.renderer, ref rect);
            } else {
                SDL.SDL_RenderDrawRect(game_manager.renderer, ref rect);
            }

            SDL.SDL_SetRenderDrawColor(game_manager.renderer, old_color.r, old_color.g, old_color.b, old_color.a);
        }

        public static void round_rect(IntPtr renderer, SDL.SDL_Rect rect, byte r, byte g, byte b, byte a, int border_radius, bool fill) {
            SDL.SDL_Color old_color;
            SDL.SDL_GetRenderDrawColor(game_manager.renderer, out old_color.r, out old_color.g, out old_color.b, out old_color.a);
            SDL.SDL_SetRenderDrawColor(game_manager.renderer, r, g, b, a);

            SDL.SDL_Rect horizontal;
            horizontal.x = rect.x;
            horizontal.y = rect.y + border_radius;
            horizontal.w = rect.w;
            horizontal.h = rect.h - border_radius * 2;

            SDL.SDL_Rect vertical1;
            vertical1.x = rect.x + border_radius;
            vertical1.y = rect.y;
            vertical1.w = rect.w - border_radius * 2;
            vertical1.h = border_radius;

            SDL.SDL_Rect vertical2;
            vertical2.x = rect.x + border_radius;
            vertical2.y = rect.y + rect.h - border_radius;
            vertical2.w = rect.w - border_radius * 2;
            vertical2.h = border_radius;

            if(fill) {

                SDL.SDL_RenderFillRect(game_manager.renderer, ref horizontal);
                SDL.SDL_RenderFillRect(game_manager.renderer, ref vertical1);
                SDL.SDL_RenderFillRect(game_manager.renderer, ref vertical2);

                draw.quarter(game_manager.renderer, rect.x + border_radius, rect.y + border_radius, border_radius, r, g, b, a, 1);
                draw.quarter(game_manager.renderer, rect.x + rect.w - border_radius - 1, rect.y + border_radius, border_radius, r, g, b, a, 2);
                draw.quarter(game_manager.renderer, rect.x + rect.w - border_radius - 1, rect.y + rect.h - border_radius - 1, border_radius, r, g, b, a, 3);
                draw.quarter(game_manager.renderer, rect.x + border_radius, rect.y + rect.h - border_radius - 1, border_radius, r, g, b, a, 4);

            } else {
                SDL.SDL_RenderDrawRect(game_manager.renderer, ref rect);
            }

            SDL.SDL_SetRenderDrawColor(game_manager.renderer, old_color.r, old_color.g, old_color.b, old_color.a);
        }

        public static void circle(IntPtr renderer, int x, int y, int radius, byte r, byte g, byte b, byte a){
            SDL.SDL_Color oldcolor;
            SDL.SDL_GetRenderDrawColor(game_manager.renderer, out oldcolor.r, out oldcolor.g, out oldcolor.b, out oldcolor.a);
            SDL.SDL_SetRenderDrawColor(renderer, r, g, b, a);
            for (int w = 0; w < radius * 2; w++)
            {
                for (int h = 0; h < radius * 2; h++)
                {
                    int dx = radius - w; // horizontal offset
                    int dy = radius - h; // vertical offset
                    if ((dx*dx + dy*dy) <= (radius * radius))
                    {
                        SDL.SDL_RenderDrawPoint(renderer, x + dx, y + dy);
                    }
                }
            }
            SDL.SDL_SetRenderDrawColor(renderer, oldcolor.r, oldcolor.g, oldcolor.b, oldcolor.a);
        }  

        public static void quarter(IntPtr renderer, int x, int y, int radius, byte r, byte g, byte b, byte a, int side){
            SDL.SDL_Color oldcolor;
            SDL.SDL_GetRenderDrawColor(game_manager.renderer, out oldcolor.r, out oldcolor.g, out oldcolor.b, out oldcolor.a);
            SDL.SDL_SetRenderDrawColor(renderer, r, g, b, a);
            for (int w = 0; w < radius * 2; w++)
            {
                for (int h = 0; h < radius * 2; h++)
                {
                    int dx = radius - w; // horizontal offset
                    int dy = radius - h; // vertical offset
                    if ((dx*dx + dy*dy) <= (radius * radius))
                    {
                        if(w > radius && h > radius && side == 1) {
                            SDL.SDL_RenderDrawPoint(renderer, x + dx, y + dy);
                        }
                        else if(w < radius && h > radius && side == 2) {
                            SDL.SDL_RenderDrawPoint(renderer, x + dx, y + dy);
                        }
                        else if(w < radius && h < radius && side == 3) {
                            SDL.SDL_RenderDrawPoint(renderer, x + dx, y + dy);
                        }
                        else if(w > radius && h < radius && side == 4) {
                            SDL.SDL_RenderDrawPoint(renderer, x + dx, y + dy);
                        }
                    }
                }
            }
            SDL.SDL_SetRenderDrawColor(renderer, oldcolor.r, oldcolor.g, oldcolor.b, oldcolor.a);
        } 
    
        public static void polygon(IntPtr renderer, byte r, byte g, byte b, byte a, Vector4[] lines) {
            SDL.SDL_Color old_color;
            SDL.SDL_GetRenderDrawColor(game_manager.renderer, out old_color.r, out old_color.g, out old_color.b, out old_color.a);
            SDL.SDL_SetRenderDrawColor(game_manager.renderer, r, g, b, a);

            foreach(Vector4 line in lines) {
                SDL.SDL_RenderDrawLine(renderer, (int)line.X, (int)line.Y, (int)line.Z, (int)line.W);
            }
            
            SDL.SDL_SetRenderDrawColor(game_manager.renderer, old_color.r, old_color.g, old_color.b, old_color.a);
        }
    
        public static void texture(IntPtr renderer, IntPtr texture, int x, int y, double angle, bool relative=false, flip_type flip=flip_type.none) {
            SDL.SDL_Point size;
            uint format;
            int access;
            SDL.SDL_QueryTexture(texture, out format, out access, out size.x, out size.y);

            SDL.SDL_Rect src, dest;

            SDL.SDL_Point center;
            center.x = size.x / 2;
            center.y = size.y / 2;

            src.x = src.y = 0;
            src.w = size.x;
            src.h = size.y;

            dest.x = x - size.x / 2 - (relative ? (int)camera.camera_position.X : 0);
            dest.y = y - size.y / 2 - (relative ? (int)camera.camera_position.Y : 0);
            dest.w = size.x;
            dest.h = size.y;
            
            SDL.SDL_RendererFlip flip_sdl = SDL.SDL_RendererFlip.SDL_FLIP_NONE; 
            
            if(flip == flip_type.both) {
                flip_sdl = SDL.SDL_RendererFlip.SDL_FLIP_HORIZONTAL | SDL.SDL_RendererFlip.SDL_FLIP_VERTICAL;
            } else if(flip == flip_type.horizontal) {
                flip_sdl = SDL.SDL_RendererFlip.SDL_FLIP_HORIZONTAL;
            } else if(flip == flip_type.vertical) {
                flip_sdl = SDL.SDL_RendererFlip.SDL_FLIP_VERTICAL;
            }

            SDL.SDL_RenderCopyEx(renderer, texture, ref src, ref dest, angle, ref center, flip_sdl);
        }

        public static void texture_ext(IntPtr renderer, IntPtr texture, int x, int y, double angle, int dest_w, int dest_h, bool relative=false, flip_type flip=flip_type.none) {
            SDL.SDL_Point size;
            uint format;
            int access;
            SDL.SDL_QueryTexture(texture, out format, out access, out size.x, out size.y);

            SDL.SDL_Rect src, dest;

            SDL.SDL_Point center;
            center.x = size.x / 2;
            center.y = size.y / 2;

            src.x = src.y = 0;
            src.w = size.x;
            src.h = size.y;

            dest.x = x - size.x / 2 - (relative ? (int)camera.camera_position.X : 0);
            dest.y = y - size.y / 2 - (relative ? (int)camera.camera_position.Y : 0);
            dest.w = dest_w;
            dest.h = dest_h;
            
            SDL.SDL_RendererFlip flip_sdl = SDL.SDL_RendererFlip.SDL_FLIP_NONE; 
            
            if(flip == flip_type.both) {
                flip_sdl = SDL.SDL_RendererFlip.SDL_FLIP_HORIZONTAL | SDL.SDL_RendererFlip.SDL_FLIP_VERTICAL;
            } else if(flip == flip_type.horizontal) {
                flip_sdl = SDL.SDL_RendererFlip.SDL_FLIP_HORIZONTAL;
            } else if(flip == flip_type.vertical) {
                flip_sdl = SDL.SDL_RendererFlip.SDL_FLIP_VERTICAL;
            }

            SDL.SDL_RenderCopyEx(renderer, texture, ref src, ref dest, angle, ref center, flip_sdl);
        }
    }
}