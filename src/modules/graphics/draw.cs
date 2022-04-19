using static SDL2.SDL;
using static SDL2.SDL_image;
using static SDL2.SDL_ttf;
using System;
using System.Collections.Generic;
using Fjord.Modules.Camera;
using Fjord.Modules.Mathf;

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

        public class texture_buffer {
            public texture tex;
            public V2 position;
            public texture_buffer(texture tex, V2 position) {
                this.tex = tex;
                this.position = position;
            }
        }

        private static Dictionary<string, IntPtr> fonts = new Dictionary<string, IntPtr>();
        private static Dictionary<string, IntPtr> texture_cache = new Dictionary<string, IntPtr>();
        private static List<texture_buffer> draw_texture_buffer = new List<texture_buffer>();

        public static void rect(V4 rect, V4 color, bool fill=true, int border_radius=0, double angle=0, draw_origin origin=draw_origin.CENTER) {
            if(border_radius == 0) {
                draw.rectangle(rect, color, fill, angle, origin);
            } else {
                draw.round_rectangle(rect, color, fill, border_radius);
                if(angle != 0) {
                    Debug.Debug.warn("Rotation isn't supported with border-radius yet.");
                }
            }
        }

        private static void rectangle(V4 rect, V4 color, bool fill=true, double angle=0, draw_origin origin=draw_origin.CENTER) {
            // IntPtr target_texture = SDL_CreateTexture(game.renderer, SDL_PIXELFORMAT_RGBA8888, (int)SDL_TextureAccess.SDL_TEXTUREACCESS_TARGET, rect.z, rect.w);
            // SDL_SetRenderTarget(game.renderer, target_texture);

            V4 old_color = SDL_GetRenderDrawColor(game.renderer);
            SDL_SetRenderDrawColor(game.renderer, color);

            SDL_Rect converted_rect = helpers.v4_to_sdl(rect);
            // SDL_Rect draw_rect = new SDL_Rect(0, 0, rect.z, rect.w);

            if(fill) {
                SDL_RenderFillRect(game.renderer, ref converted_rect);
            } else {
                SDL_RenderDrawRect(game.renderer, ref converted_rect);
            }

            // SDL_SetRenderTarget(game.renderer, (IntPtr)0);
            SDL_SetRenderDrawColor(game.renderer, old_color);

            // SDL_Point point;
            // switch(origin) {
            //     case draw_origin.TOP_LEFT:
            //         point = new SDL_Point(0, 0);
            //         break;
            //     case draw_origin.TOP_MIDDLE:
            //         point = new SDL_Point(rect.z / 2, 0);
            //         break;
            //     case draw_origin.TOP_RIGHT:
            //         point = new SDL_Point(rect.z, 0);
            //         break;

            //     case draw_origin.MIDDLE_LEFT:
            //         point = new SDL_Point(0, rect.w / 2);
            //         break;
            //     case draw_origin.CENTER:
            //         point = new SDL_Point(rect.z / 2, rect.w / 2);
            //         break;
            //     case draw_origin.MIDDLE_RIGHT:
            //         point = new SDL_Point(rect.z, rect.w / 2);
            //         break;

            //     case draw_origin.BOTTOM_LEFT:
            //         point = new SDL_Point(0, rect.w);
            //         break;
            //     case draw_origin.BOTTOM_MIDDLE:
            //         point = new SDL_Point(rect.z / 2, rect.w);
            //         break;
            //     case draw_origin.BOTTOM_RIGHT:
            //         point = new SDL_Point(rect.z, rect.w);
            //         break;
                
            //     default:
            //         point = new SDL_Point(rect.z / 2, rect.w / 2);
            //         break; 
            // }

            // SDL_RenderCopyEx(game.renderer, target_texture, ref draw_rect, ref converted_rect, angle, ref point, SDL_RendererFlip.SDL_FLIP_NONE);
            // SDL_DestroyTexture(target_texture);
        }

        private static void round_rectangle(V4 rect, V4 color, bool fill=true, int border_radius=0) {
            SDL_Color old_color;
            SDL_GetRenderDrawColor(game.renderer, out old_color.r, out old_color.g, out old_color.b, out old_color.a);
            SDL_SetRenderDrawColor(game.renderer, (byte)color.x, (byte)color.y, (byte)color.z, (byte)color.w);

            SDL_Rect main_rect = new SDL_Rect(rect.x, rect.y + border_radius, rect.z, rect.w - border_radius * 2);
            SDL_Rect top_rect = new SDL_Rect(rect.x + border_radius, rect.y, rect.z - border_radius * 2, border_radius);
            SDL_Rect bottom_rect = new SDL_Rect(rect.x + border_radius, rect.y + rect.w - border_radius, rect.z - border_radius * 2, border_radius);

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

        public static void new_line(V2 p1, V2 p2, V4 color, V4 reset_color) {
            V2 new_p1 = new V2();
            V2 new_p2 = new V2();

            if(Math.Abs(p1.x) != p1.x || Math.Abs(p2.x) != p2.x) {
                if(p1.x < p2.x) {
                    int offset = Math.Abs(p1.x);
                    new_p1.x = 0;
                    new_p2.x = p2.x + offset;
                } else {
                    int offset = Math.Abs(p2.x);
                    new_p2.x = 0;
                    new_p1.x = p1.x + offset;
                }
            } else {
                if(p1.x < p2.x) {
                    int offset = Math.Abs(p1.x);
                    new_p1.x = 0;
                    new_p2.x = p2.x - offset;
                } else {
                    int offset = Math.Abs(p2.x);
                    new_p2.x = 0;
                    new_p1.x = p1.x - offset;
                }
            }

            if(Math.Abs(p1.y) != p1.y || Math.Abs(p2.y) != p2.y) {
                if(p1.y < p2.y) {
                    int offset = Math.Abs(p1.y);
                    new_p1.y = 0;
                    new_p2.y = p2.y + offset;
                } else {
                    int offset = Math.Abs(p2.y);
                    new_p2.y = 0;
                    new_p1.y = p1.y + offset;
                }
            } else {
                if(p1.y < p2.y) {
                    int offset = Math.Abs(p1.y);
                    new_p1.y = 0;
                    new_p2.y = p2.y - offset;
                } else {
                    int offset = Math.Abs(p2.y);
                    new_p2.y = 0;
                    new_p1.y = p1.y - offset;
                }
            }

            int w = new_p1.x > new_p2.x ? new_p1.x : new_p2.x;
            int h = new_p1.y > new_p2.y ? new_p1.y : new_p2.y;

            IntPtr tex = SDL_CreateTexture(game.renderer, SDL_PIXELFORMAT_RGBA8888, (int)SDL_TextureAccess.SDL_TEXTUREACCESS_TARGET, w, h);
            SDL_SetRenderTarget(game.renderer, tex);
            SDL_SetTextureBlendMode(tex, SDL_BlendMode.SDL_BLENDMODE_BLEND);
            V4 old_color = SDL_GetRenderDrawColor(game.renderer);
            SDL_SetRenderDrawColor(game.renderer, reset_color);
            SDL_RenderClear(game.renderer);

            SDL_Rect rec = new SDL_Rect(0, 0, w, h);

            int x = p1.x < p2.x ? p1.x : p2.x;
            int y = p1.y < p2.y ? p1.y : p2.y;

            SDL_Rect dest = new SDL_Rect(x, y, w, h);
            SDL_SetRenderDrawColor(game.renderer, color);

            SDL_RenderDrawLine(game.renderer, new_p1.x, new_p1.y, new_p2.x, new_p2.y);

            SDL_SetRenderTarget(game.renderer, (IntPtr)0);
            SDL_SetRenderDrawColor(game.renderer, old_color);
            SDL_Point point = new SDL_Point(0, 0);
            SDL_RenderCopyEx(game.renderer, tex, ref rec, ref dest, 0, ref point, SDL_RendererFlip.SDL_FLIP_NONE);
            SDL_DestroyTexture(tex);
        }

        public static void new_line(V2 p1, V2 p2, V4 color) {
            V2 new_p1 = new V2();
            V2 new_p2 = new V2();

            if(Math.Abs(p1.x) != p1.x || Math.Abs(p2.x) != p2.x) {
                if(p1.x < p2.x) {
                    int offset = Math.Abs(p1.x);
                    new_p1.x = 0;
                    new_p2.x = p2.x + offset;
                } else {
                    int offset = Math.Abs(p2.x);
                    new_p2.x = 0;
                    new_p1.x = p1.x + offset;
                }
            } else {
                if(p1.x < p2.x) {
                    int offset = Math.Abs(p1.x);
                    new_p1.x = 0;
                    new_p2.x = p2.x - offset;
                } else {
                    int offset = Math.Abs(p2.x);
                    new_p2.x = 0;
                    new_p1.x = p1.x - offset;
                }
            }

            if(Math.Abs(p1.y) != p1.y || Math.Abs(p2.y) != p2.y) {
                if(p1.y < p2.y) {
                    int offset = Math.Abs(p1.y);
                    new_p1.y = 0;
                    new_p2.y = p2.y + offset;
                } else {
                    int offset = Math.Abs(p2.y);
                    new_p2.y = 0;
                    new_p1.y = p1.y + offset;
                }
            } else {
                if(p1.y < p2.y) {
                    int offset = Math.Abs(p1.y);
                    new_p1.y = 0;
                    new_p2.y = p2.y - offset;
                } else {
                    int offset = Math.Abs(p2.y);
                    new_p2.y = 0;
                    new_p1.y = p1.y - offset;
                }
            }

            int w = new_p1.x > new_p2.x ? new_p1.x : new_p2.x;
            int h = new_p1.y > new_p2.y ? new_p1.y : new_p2.y;

            IntPtr tex = SDL_CreateTexture(game.renderer, SDL_PIXELFORMAT_RGBA8888, (int)SDL_TextureAccess.SDL_TEXTUREACCESS_TARGET, w, h);
            SDL_SetRenderTarget(game.renderer, tex);
            SDL_SetTextureBlendMode(tex, SDL_BlendMode.SDL_BLENDMODE_BLEND);
            V4 old_color = SDL_GetRenderDrawColor(game.renderer);
            SDL_SetRenderDrawColor(game.renderer, new V4(0, 0, 0, 0));
            SDL_RenderClear(game.renderer);

            SDL_Rect rec = new SDL_Rect(0, 0, w, h);

            int x = p1.x < p2.x ? p1.x : p2.x;
            int y = p1.y < p2.y ? p1.y : p2.y;

            SDL_Rect dest = new SDL_Rect(x, y, w, h);
            SDL_SetRenderDrawColor(game.renderer, color);

            SDL_RenderDrawLine(game.renderer, new_p1.x, new_p1.y, new_p2.x, new_p2.y);

            SDL_SetRenderTarget(game.renderer, (IntPtr)0);
            SDL_SetRenderDrawColor(game.renderer, old_color);
            SDL_Point point = new SDL_Point(0, 0);
            SDL_RenderCopyEx(game.renderer, tex, ref rec, ref dest, 0, ref point, SDL_RendererFlip.SDL_FLIP_NONE);
            SDL_DestroyTexture(tex);
        }

        public static void texture(V2 position, texture tex) {
            draw_texture_buffer.Add(new texture_buffer((texture) tex.Clone(), position));
        }

        public static void texture_direct(V2 position, texture tex) {
            
            IntPtr final_texture = tex.get_texture();

            SDL_Rect src, dest;

            src = new SDL_Rect(0, 0, tex.get_texture_size().x, tex.get_texture_size().y);
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

            // Camera Relative Handling

            if(tex.get_relative()) {
                dest.x -= (int)camera.get().x;
                dest.y -= (int)camera.get().y;
            }

            // Draw

            SDL_Point center = new SDL_Point(tex.get_origin().x, tex.get_origin().y);
            SDL_RenderCopyEx(game.renderer, final_texture, ref src, ref dest, tex.get_angle(), ref center, flip_sdl);    
        }

        public static List<texture_buffer> get_texture_buffer() {
            return draw_texture_buffer;
        }

        public static void clean_texture_buffer() {
            draw_texture_buffer = new List<texture_buffer>();
        }

        public static void text(V2 position, string font_id, int font_size, string text) {
            IntPtr font;
            
            if(!fonts.ContainsKey(font_id)) {
                string path = game.executable_path.Replace("\\", "/") + "/" + game.get_resource_folder() + "/" + game.asset_pack + "/assets/fonts/" + font_id + ".ttf";
                font = TTF_OpenFont(path, 255);
                fonts.Add(font_id, font);
            } else {
                font = fonts[font_id];
            }

            // this is the color in rgb format,
            // maxing out all would give you the color white,
            // and it will be your text's color
            SDL_Color White = new SDL_Color(255, 255, 255, 255);

            // as TTF_RenderText_Solid could only be used on
            // SDL_Surface then you have to create the surface first
            IntPtr surfaceMessage = TTF_RenderText_Solid(font, text, White); 

            // now you can convert it into a texture
            IntPtr Message = SDL_CreateTextureFromSurface(game.renderer, surfaceMessage);

            SDL_Rect Message_rect; //create a rect
            Message_rect.x = position.x;  //controls the rect's x coordinate 
            Message_rect.y = position.y; // controls the rect's y coordinte

            uint f; int a;
            SDL_Rect src = new SDL_Rect();
            src.x = 0;
            src.y = 0;

            SDL_QueryTexture(Message, out f, out a, out src.w, out src.h);

            float scale = 255 / font_size;
            Message_rect.w = (int)(src.w / scale);
            Message_rect.h = (int)(src.h / scale);

            SDL_RenderCopy(game.renderer, Message, ref src, ref Message_rect);

            // Don't forget to free your surface and texture
            SDL_FreeSurface(surfaceMessage);
            SDL_DestroyTexture(Message);
        }

        public static void text(V2 position, string font_id, int font_size, string text, V4 color) {
            SDL_Rect src = new SDL_Rect(0, 0, 0, 0);

            uint f; int a;

            SDL_QueryTexture(get_text_texture(font_id, text, color), out f, out a, out src.w, out src.h);

            SDL_Rect dest = new SDL_Rect(position.x, position.y, src.w / (255 / font_size), src.h / (255 / font_size));

            SDL_RenderCopy(game.renderer, get_text_texture(font_id, text, color), ref src, ref dest);
        }

        public static IntPtr get_text_texture(string font_id, string text, V4 color) {
            string key_ = hash.HashString(font_id + text + color.x.ToString() + color.y.ToString() + color.z.ToString() + color.w.ToString());
            if(!texture_cache.ContainsKey(key_)) {
                IntPtr font;
                    
                if(!fonts.ContainsKey(font_id)) {
                    string path = game.executable_path.Replace("\\", "/") + "/" + game.get_resource_folder() + "/" + game.asset_pack + "/assets/fonts/" + font_id + ".ttf";
                    font = TTF_OpenFont(path, 255);
                    fonts.Add(font_id, font);
                } else {
                    font = fonts[font_id];
                }

                SDL_Color White = new SDL_Color((byte)color.x, (byte)color.y, (byte)color.z, (byte)color.w);
                IntPtr surfaceMessage = TTF_RenderText_Solid(font, text, White); 

                IntPtr Message = SDL_CreateTextureFromSurface(game.renderer, surfaceMessage);

                texture_cache.Add(key_, Message);

                SDL_FreeSurface(surfaceMessage);

                return texture_cache[key_];
            } else {
                return texture_cache[key_];
            }
        }

        public static V4 get_text_rect(V2 position, string font_id, int font_size, string text) {
            IntPtr font;
                
            if(!fonts.ContainsKey(font_id)) {
                string path = game.executable_path.Replace("\\", "/") + "/" + game.get_resource_folder() + "/" + game.asset_pack + "/assets/fonts/" + font_id + ".ttf";
                font = TTF_OpenFont(path, 255);
                fonts.Add(font_id, font);
            } else {
                font = fonts[font_id];
            }

            SDL_Color White = new SDL_Color(255, 255, 255, 255);

            IntPtr surfaceMessage = TTF_RenderText_Solid(font, text, White); 

            IntPtr Message = SDL_CreateTextureFromSurface(game.renderer, surfaceMessage);

            uint f; int a;
            SDL_Rect src = new SDL_Rect();
            src.x = 0;
            src.y = 0;

            SDL_QueryTexture(Message, out f, out a, out src.w, out src.h);

                // Don't forget to free your surface and texture
            SDL_FreeSurface(surfaceMessage);
            SDL_DestroyTexture(Message);

            return new V4(position.x, position.y, src.w / (255 / font_size), src.h / (255 / font_size));
        }

        public static void load_font(string font_id) {
            if(!fonts.ContainsKey(font_id)) {
                string path = game.executable_path.Replace("\\", "/") + "/" + game.get_resource_folder() + "/" + game.asset_pack + "/assets/fonts/" + font_id + ".ttf";
                IntPtr font = TTF_OpenFont(path, 255);
                fonts.Add(font_id, font);
            }
        }
    }
}