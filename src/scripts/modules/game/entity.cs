using System.Numerics;
using Proj.Modules.Graphics;
using SDL2;
using System;

namespace Proj.Modules.Game
{
    public class entity
    {
        public Vector2 transform = new Vector2(0, 0);
        
        public IntPtr texture = texture_handler.default_texture;
        public float texture_angle = 0;
        public Vector2 texture_origin = new Vector2(0, 0);
        public float texture_xscale = 1;
        public float texture_yscale = 1;

        public bool visible = true;

        public virtual void update() {}
        public virtual void render() {
            uint f;
            int a, w, h;
            SDL.SDL_QueryTexture(texture, out f, out a, out w, out h);
            draw.texture_ext(game_manager.renderer, texture, (int)transform.X, (int)transform.Y, texture_angle, (int)(w * texture_xscale), (int)(h * texture_yscale), texture_origin, true);
        }
    }
}