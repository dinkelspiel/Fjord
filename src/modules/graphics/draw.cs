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

    public enum draw_quarter {
        TOP_LEFT,
        TOP_RIGHT,
        BOTTOM_LEFT,
        BOTTOM_RIGHT
    }

    public static class draw {
        public static void rect(V4 rect, V4 color, bool fill=true, int border_radius=0) {
            if(border_radius == 0) {
                draw.rectangle(rect, color, fill);
            } else {
                draw.round_rectangle(rect, color, fill, border_radius);
            }
        }

        private static void rectangle(V4 rect, V4 color, bool fill=true) {
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

        private static void round_rectangle(V4 rect, V4 color, bool fill=true, int border_radius=0) {
            SDL_Color old_color;
            SDL_GetRenderDrawColor(game.renderer, out old_color.r, out old_color.g, out old_color.b, out old_color.a);
            SDL_SetRenderDrawColor(game.renderer, (byte)color.x, (byte)color.y, (byte)color.z, (byte)color.w);

            SDL_Rect main_rect = new SDL_Rect(rect.x, rect.y + border_radius, rect.z, rect.w - border_radius * 2);
            SDL_Rect top_rect = new SDL_Rect(rect.x + border_radius, rect.y, rect.z - border_radius * 2, border_radius);
            SDL_Rect bottom_rect = new SDL_Rect(rect.x + border_radius, rect.y + rect.z - border_radius, rect.z - border_radius * 2, border_radius);

            if(fill) {
                SDL_RenderFillRect(game.renderer, ref main_rect);
                SDL_RenderFillRect(game.renderer, ref top_rect);
                SDL_RenderFillRect(game.renderer, ref bottom_rect);
                draw.quarter(new V2(rect.x + border_radius, rect.y + border_radius), border_radius, draw_quarter.TOP_LEFT, color, fill);
                draw.quarter(new V2(rect.x + rect.z - border_radius, rect.y + border_radius), border_radius, draw_quarter.TOP_RIGHT, color, fill);
                draw.quarter(new V2(rect.x + border_radius, rect.y + rect.w - border_radius), border_radius, draw_quarter.BOTTOM_LEFT, color, fill);
                draw.quarter(new V2(rect.x + rect.z - border_radius, rect.y + rect.w - border_radius), border_radius, draw_quarter.BOTTOM_RIGHT, color, fill);
            } else {
                SDL_RenderDrawRect(game.renderer, ref main_rect);
                SDL_RenderDrawRect(game.renderer, ref top_rect);
                SDL_RenderDrawRect(game.renderer, ref bottom_rect);
                draw.quarter(new V2(rect.x + border_radius, rect.y + border_radius), border_radius, draw_quarter.TOP_LEFT, color, fill);
                draw.quarter(new V2(rect.x + rect.z - border_radius, rect.y + border_radius), border_radius, draw_quarter.TOP_RIGHT, color, fill);
                draw.quarter(new V2(rect.x + border_radius, rect.y + rect.w - border_radius), border_radius, draw_quarter.BOTTOM_LEFT, color, fill);
                draw.quarter(new V2(rect.x + rect.z - border_radius, rect.y + rect.w - border_radius), border_radius, draw_quarter.BOTTOM_RIGHT, color, fill);
            }

            SDL_SetRenderDrawColor(game.renderer, old_color.r, old_color.g, old_color.b, old_color.a);  
        }

        public static void circle(V2 position, int radius, V4 color, bool fill=true) {
            if(fill) {
                filled_circle(position, radius, color);
            } else {
                outlined_circle(position, radius, color);
            }
        }
        
        // Reference: https://stackoverflow.com/questions/1201200/fast-algorithm-for-drawing-filled-circles
        private static void filled_circle(V2 position, int radius, V4 color) {
            int x = radius;
            int y = 0;
            int err = 0;
        
            while (x >= y)
            {
                draw.line(new V2(position.x + x, position.y + y), new V2(position.x + x, position.y - y), color);
                draw.line(new V2(position.x - x, position.y + y), new V2(position.x - x, position.y - y), color);
                draw.line(new V2(position.x - y, position.y + x), new V2(position.x - y, position.y - x), color);
                draw.line(new V2(position.x + y, position.y + x), new V2(position.x + y, position.y - x), color);

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

        // Reference: Circle drawing algorithm https://www.geeksforgeeks.org/bresenhams-circle-drawing-algorithm/ 
        private static void outlined_circle(V2 position, int radius, V4 color) {
            int x = radius;
            int y = 0;
            int err = 0;
        
            while (x >= y)
            {
                draw.rect(new V4(position.x + x, position.y + y, 1, 1), color);
                draw.rect(new V4(position.x + y, position.y + x, 1, 1), color);
                draw.rect(new V4(position.x - y, position.y + x, 1, 1), color);
                draw.rect(new V4(position.x - x, position.y + y, 1, 1), color);
                draw.rect(new V4(position.x - x, position.y - y, 1, 1), color);
                draw.rect(new V4(position.x - y, position.y - x, 1, 1), color);
                draw.rect(new V4(position.x + y, position.y - x, 1, 1), color);
                draw.rect(new V4(position.x + x, position.y - y, 1, 1), color);
                
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

        public static void quarter(V2 position, int radius, draw_quarter quarter, V4 color, bool fill=true) {
            if(fill) {
                filled_quarter(position, radius, quarter, color);
            } else {
                outlined_quarter(position, radius, quarter, color);
            }
        }

        private static void filled_quarter(V2 position, int radius, draw_quarter quarter, V4 color) {
            int x = radius;
            int y = 0;
            int err = 0;
        
            while (x >= y)
            {
                switch(quarter) {
                    case draw_quarter.TOP_LEFT:
                        draw.line(new V2(position.x - x, position.y - y), new V2(position.x - x, position.y), color);
                        draw.line(new V2(position.x - y, position.y - x), new V2(position.x - y, position.y), color);              
                        break;
                    case draw_quarter.TOP_RIGHT:
                        draw.line(new V2(position.x + x, position.y - y), new V2(position.x + x, position.y), color);
                        draw.line(new V2(position.x + y, position.y - x), new V2(position.x + y, position.y), color);   
                        break;
                    case draw_quarter.BOTTOM_LEFT:
                        draw.line(new V2(position.x - x, position.y + y), new V2(position.x - x, position.y), color);
                        draw.line(new V2(position.x - y, position.y + x), new V2(position.x - y, position.y), color);                 
                        break;
                    case draw_quarter.BOTTOM_RIGHT:
                        draw.line(new V2(position.x + x, position.y + y), new V2(position.x + x, position.y), color);
                        draw.line(new V2(position.x + y, position.y + x), new V2(position.x + y, position.y), color);            
                        break;
                }

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

        // Reference: Circle drawing algorithm https://www.geeksforgeeks.org/bresenhams-circle-drawing-algorithm/ 
        private static void outlined_quarter(V2 position, int radius, draw_quarter quarter, V4 color) {
            int x = radius;
            int y = 0;
            int err = 0;
        
            while (x >= y)
            {
                switch(quarter) {
                    case draw_quarter.TOP_LEFT:
                        draw.rect(new V4(position.x - x, position.y - y, 1, 1), color);
                        draw.rect(new V4(position.x - y, position.y - x, 1, 1), color);
                        break;  
                    case draw_quarter.TOP_RIGHT:
                        draw.rect(new V4(position.x + y, position.y - x, 1, 1), color);
                        draw.rect(new V4(position.x + x, position.y - y, 1, 1), color);
                        break;
                    case draw_quarter.BOTTOM_LEFT:
                        draw.rect(new V4(position.x - y, position.y + x, 1, 1), color);
                        draw.rect(new V4(position.x - x, position.y + y, 1, 1), color);
                        break;
                    case draw_quarter.BOTTOM_RIGHT:
                        draw.rect(new V4(position.x + x, position.y + y, 1, 1), color);
                        draw.rect(new V4(position.x + y, position.y + x, 1, 1), color);
                        break;
                }
                
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

        // Reference: https://en.wikipedia.org/wiki/Bresenham%27s_line_algorithm
        public static void line(V2 position, V2 position_2, V4 color) {
            // calculate dx , dy
            int dx = position_2.x - position.x;
            int dy = position_2.y - position.y;

            // Depending upon absolute value of dx & dy
            // choose number of steps to put pixel as
            // steps = abs(dx) > abs(dy) ? abs(dx) : abs(dy) 
            int steps = Math.Abs(dx) > Math.Abs(dy) ? Math.Abs(dx) : Math.Abs(dy);

            // calculate increment in x & y for each steps
            float Xinc = dx / (float) steps;
            float Yinc = dy / (float) steps;

            // Put pixel for each step
            float X = position.x;
            float Y = position.y;
            for (int i = 0; i <= steps; i++)
            {
                draw.rect( new V4((int)Math.Round((decimal)X), (int)Math.Round((decimal)Y), 1, 1) , color);
                X += Xinc;
                Y += Yinc;
            }
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