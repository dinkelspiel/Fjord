using Fjord.Modules.Entity;
using Fjord.Modules.Graphics;

namespace Fjord.Modules.Components
{
    public class SpriteRenderer : Component
    {
        private bool Visible = true;
        public Texture Texture;

        public SpriteRenderer(Entity.Entity parent, Texture texture) : base(parent) {
            this.Texture = texture;
        }

        public void SetTexture(Texture texture)
        {
            this.Texture = texture;
        }

        public override void OnAwake()
        {
            base.OnAwake();
        }

        public override void OnRender()
        {
            if(Visible)
                Draw.Texture(Get<Transform>().Position, Texture);
        }

        public override void OnSleep()
        {
            base.OnSleep();
        }

        public override void OnUpdate()
        {
            Texture.SetScale(Get<Transform>().Scale);
            Texture.SetAngle((int)Get<Transform>().Rotation);
        }
    }
}