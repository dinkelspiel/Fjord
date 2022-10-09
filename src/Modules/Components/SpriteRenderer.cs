using Fjord.Modules.Entity;
using Fjord.Modules.Graphics;

namespace Fjord.Modules.Components
{
    public class SpriteRenderer : Component
    {
        private bool Visible = true;
        private Texture _Texture;

        public SpriteRenderer(Entity.Entity parent, Texture texture) : base(parent) {
            this._Texture = texture;
        } 

        public override void OnAwake()
        {
            base.OnAwake();
        }

        public override void OnRender()
        {
            if(Visible)
                Draw.Texture(Get<Transform>().Position, _Texture);
        }

        public override void OnSleep()
        {
            base.OnSleep();
        }

        public override void OnUpdate()
        {
            _Texture.SetScale(Get<Transform>().Scale);
            _Texture.SetAngle((int)Get<Transform>().Rotation);
        }
    }
}