using Fjord.Modules.Mathf;
using Fjord.Modules.Graphics;
using static SDL2.SDL;
using System;

namespace Fjord.Modules.Game
{
    public class entity
    {
        public V2f position = new V2f(0, 0);
        
        public IntPtr texture = texture_handler.default_texture;
        public V2 texture_size;

        public float texture_angle = 0;
        public V2 texture_origin = new V2(0, 0);

        public float texture_xscale = 1;
        public float texture_yscale = 1;
        public flip_type texture_flip = flip_type.none;

        public int texture_right;
        public int texture_left;
        public int texture_top;
        public int texture_bottom;

        public bool visible = true;

        public virtual void update() {
            uint format;
            int access;
            SDL_QueryTexture(texture, out format, out access, out texture_size.x, out texture_size.y);

            uint f;
            int a, w, h;
            SDL_QueryTexture(texture, out f, out a, out w, out h);
            w = (int)(w * texture_xscale);
            h = (int)(h * texture_yscale);

            texture_right = (int)position.x + w / 2;
            texture_left = (int)position.x - w / 2;
            texture_top = (int)position.y - h / 2;
            texture_bottom = (int)position.y + h / 2;
        }

        public virtual void render() {
            uint f;
            int a, w, h;
            SDL_QueryTexture(texture, out f, out a, out w, out h);

            SDL_Point origin;
            origin.x = (int)texture_origin.x;
            origin.y = (int)texture_origin.y;

            draw.texture_ext(game.renderer, texture, (int)position.x, (int)position.y, texture_angle, texture_xscale, texture_yscale, true, draw_origin.CENTER, texture_flip);
        }
    }
}