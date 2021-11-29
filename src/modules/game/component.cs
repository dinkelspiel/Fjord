using Fjord.Modules.Mathf;
using Fjord.Modules.Graphics;
using static SDL2.SDL;
using System;
using Fjord.Modules.Camera;

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
        public texture sprite = new texture();

        public bool visible = true;

        public override void update()
        {
            sprite.set_scale(parent.get_component<Transform>().scale);
            sprite.set_angle(parent.get_component<Transform>().rotation);
        }

        public override void render()
        {
            if(visible)
                draw.texture(parent.get_component<Transform>().position - camera.camera_position, sprite);
        }
    }
}