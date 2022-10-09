using Fjord.Modules.Entity;
using Fjord.Modules.Graphics;
using System.Numerics;

namespace Fjord.Modules.Components
{
    public class Transform : Component
    {
        public Vector2 Position = new (0, 0);
        public Vector2 Scale = new (1, 1);
        public float Rotation = 0;

        public Transform(Entity.Entity parent) : base(parent) {}
    }
}