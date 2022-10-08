using Fjord.Modules.Entity;
using Fjord.Modules.Graphics;
using System.Numerics;

namespace Fjord.Modules.Components
{
    public class Transform : Component
    {
        public Vector2 Position = new ();
        public Vector2 Scale = new ();
        public float Rotation = 0;

        public Transform(Entity.Entity parent) : base(parent) {}
    }
}