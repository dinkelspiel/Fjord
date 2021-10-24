using Fjord.Modules.Mathf;
using Fjord.Modules.Graphics;
using static SDL2.SDL;

namespace Fjord.Modules.Game {
    public abstract class component {
        public dynamic parent;

        public virtual void on_load() {}
        public virtual void update() {}
        public virtual void render() {}
    }

    public class Transform : component
    {
        public V2f position = new V2f(0, 0);
        public V2f scale = new V2f(1, 1);
        public float rotation = 0f;
    }

    public class Sprite_Renderer : component {
        public IntPtr texture = texture_handler.default_texture;
        public V2 texture_size = new V2(0, 0);

        public V2 texture_origin = new V2(0, 0);

        public flip_type texture_flip = flip_type.none;

        public int texture_right;
        public int texture_left;
        public int texture_top;
        public int texture_bottom;

        public bool visible = true;
        
        public override void update()
        {
            uint format;
            int access;
            SDL_QueryTexture(texture, out format, out access, out texture_size.x, out texture_size.y);

            uint f;
            int a, w, h;
            SDL_QueryTexture(texture, out f, out a, out w, out h);
            w = (int)(w * parent.get_component<Transform>().scale.x);
            h = (int)(h * parent.get_component<Transform>().scale.y);

            texture_right = (int)parent.get_component<Transform>().position.x + w / 2;
            texture_left = (int)parent.get_component<Transform>().position.x - w / 2;
            texture_top = (int)parent.get_component<Transform>().position.y - h / 2;
            texture_bottom = (int)parent.get_component<Transform>().position.y + h / 2;
        }

        public override void render()
        {
            uint f;
            int a, w, h;
            SDL_QueryTexture(texture, out f, out a, out w, out h);

            SDL_Point origin;
            origin.x = (int)texture_origin.x;
            origin.y = (int)texture_origin.y;

            if(visible)
                draw.texture_ext(game.renderer, texture, (int)parent.get_component<Transform>().position.x, (int)parent.get_component<Transform>().position.y, parent.get_component<Transform>().rotation, parent.get_component<Transform>().scale.x, parent.get_component<Transform>().scale.y, true, draw_origin.CENTER, texture_flip);
        }
    }
}