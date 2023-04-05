using System.Numerics;
using static SDL2.SDL;

namespace Fjord.Graphics;

internal class DrawInstruction {
    public int depth;
}

internal class DrawInsRectangle : DrawInstruction {
    public Vector4 rect;
    public Vector4 color;
    public bool fill;
}

internal class DrawInsCircle : DrawInstruction {
    public Vector2 position;
    public float radius;
    public Vector4 color;
    public bool fill;
}

internal class DrawInsTexture : DrawInstruction
{
    public Vector2 position;
    public Texture texture;
}