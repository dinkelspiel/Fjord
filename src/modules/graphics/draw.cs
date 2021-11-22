using static SDL2.SDL;
using static SDL2.SDL_image;
using System;
using System.Threading;
using System.Threading.Tasks;
using Fjord.Modules.Camera;
using Fjord.Modules.Mathf;
using System.Runtime.InteropServices;

namespace Fjord.Modules.Graphics {

    public enum flip_type {
        none = 0,
        horizontal = 1,
        vertical = 2,
        both = 3
    }

    public enum draw_origin {
        TOP_LEFT,
        TOP_MIDDLE,
        TOP_RIGHT,
        BOTTOM_RIGHT,
        BOTTOM_MIDDLE,
        BOTTOM_LEFT,
        MIDDLE_LEFT,
        MIDDLE_RIGHT,
        CENTER
    }

    public static class draw {
        public static void rect(V4 rect, V4 color, bool fill=true) {
            SDL_Color old_color;
            SDL_GetRenderDrawColor(game.renderer, out old_color.r, out old_color.g, out old_color.b, out old_color.a);
            SDL_SetRenderDrawColor(game.renderer, (byte)color.x, (byte)color.y, (byte)color.z, (byte)color.w);

            SDL_Rect converted_rect = new SDL_Rect(rect.x, rect.y, rect.z, rect.w);

            if(fill) {
                SDL_RenderFillRect(game.renderer, ref converted_rect);
            } else {
                SDL_RenderDrawRect(game.renderer, ref converted_rect);
            }

            SDL_SetRenderDrawColor(game.renderer, old_color.r, old_color.g, old_color.b, old_color.a);
        }

        public static void circle(V2 position, int radius, V4 color) {
            int x = radius;
            int y = 0;
            int err = 0;
        
            while (x >= y)
            {

                draw.line(new V2(position.x + x, position.y + y), new V2(position.x + y, position.y + x), color);
                draw.line(new V2(position.x - y, position.y + x), new V2(position.x - x, position.y + y), color);
                draw.line(new V2(position.x - x, position.y - y), new V2(position.x - y, position.y - x), color);
                draw.line(new V2(position.x + y, position.y - x), new V2(position.x + x, position.y - y), color);
                
                if (err <= 0)
                {
                    y += 1;
                    err += 2*y + 1;
                }

                if (err > 0)
                {
                    x -= 1;
                    err -= 2*x + 1;
                }
            }
        }

        public static void line(V2 position, V2 position_2, V4 color) {
            SDL_Color old_color;
            SDL_GetRenderDrawColor(game.renderer, out old_color.r, out old_color.g, out old_color.b, out old_color.a);
            SDL_SetRenderDrawColor(game.renderer, (byte)color.x, (byte)color.y, (byte)color.z, (byte)color.w);

            SDL_RenderDrawLine(game.renderer, position.x, position.y, position_2.x, position_2.y);

            SDL_SetRenderDrawColor(game.renderer, old_color.r, old_color.g, old_color.b, old_color.a);
        }

        public static void texture(V2 position, texture tex) {
            
            IntPtr final_texture = tex.get_texture();

            SDL_Rect src, dest;

            src = new SDL_Rect(0, 0, tex.get_size().x, tex.get_size().y);
            dest = new SDL_Rect(position.x, position.y, tex.get_size().x, tex.get_size().y);

            // Origin Handling

            dest.x -= tex.get_origin().x;
            dest.y -= tex.get_origin().y;

            // Scale Handling

            dest.w = (int)(dest.w * tex.get_scale().x);
            dest.h = (int)(dest.h * tex.get_scale().y);

            // Flip Handling

            SDL_RendererFlip flip_sdl = SDL_RendererFlip.SDL_FLIP_NONE; 
            
            if(tex.get_fliptype() == flip_type.both) {
                flip_sdl = SDL_RendererFlip.SDL_FLIP_HORIZONTAL | SDL_RendererFlip.SDL_FLIP_VERTICAL;
            } else if(tex.get_fliptype() == flip_type.horizontal) {
                flip_sdl = SDL_RendererFlip.SDL_FLIP_HORIZONTAL;
            } else if(tex.get_fliptype() == flip_type.vertical) {
                flip_sdl = SDL_RendererFlip.SDL_FLIP_VERTICAL;
            }

            // Alpha Handling

            SDL_SetTextureAlphaMod(final_texture, (byte)tex.get_alpha());

            // Draw

            SDL_Point center = new SDL_Point(tex.get_origin().x, tex.get_origin().y);
            SDL_RenderCopyEx(game.renderer, final_texture, ref src, ref dest, tex.get_angle(), ref center, flip_sdl);    
        }

        public static void text(V2 position, string font, int font_size, string text) {
            IntPtr tex; 
            font_handler.get_texture(text, font, out tex, 0, 0, 255, 255, 255, 255);

            float scale = font_size / font_handler.get_font_size(font);

            texture final_tex = new texture();
            final_tex.set_texture(tex);
            final_tex.set_scale(new V2f(scale, scale));

            draw.texture(position, final_tex);
        }

        public static void text(V2 position, string font, int font_size, string text, V4 color) {
            IntPtr tex; 
            font_handler.get_texture(text, font, out tex, 0, 0, (byte)color.x, (byte)color.y, (byte)color.z, (byte)color.w);

            float scale = font_size / font_handler.get_font_size(font);

            texture final_tex = new texture();
            final_tex.set_texture(tex);
            final_tex.set_scale(new V2f(scale, scale));

            draw.texture(position, final_tex);
        }
    }
}