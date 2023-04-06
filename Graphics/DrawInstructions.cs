using Fjord.Scenes;
using System.Numerics;
using static SDL2.SDL;

namespace Fjord.Graphics;

public class DrawInstruction {
    public int depth;
}

public class Rectangle : DrawInstruction {
    public Vector4 rect;
    public Vector4 color;
    public bool fill;

    public Rectangle(Vector4 rect)
    {
        this.rect = rect;
    }

    public Rectangle Rect(Vector4 rect) {
        this.rect = rect;
        return this;
    }

    public Rectangle Color(Vector4 color) {
        this.color = color;
        return this;
    }

    public Rectangle Fill(bool fill)
    {
        this.fill = fill;
        return this;
    }

    public void Render()
    {
        if (Draw.CurrentSceneID is not null)
        {
            SceneHandler.Scenes[Draw.CurrentSceneID].drawBuffer.Add(this);
        }
        else
        {
            Draw.drawBuffer.Add(this);
        }
    }
}

public class Circle : DrawInstruction {
    public Vector2 position;
    public float radius;
    public Vector4 color;
    public bool fill;

    public Circle(Vector2 position, float radius)
    {
        this.position = position;
        this.radius = radius;
    }
}

public class Texture : DrawInstruction
{
    public Vector2 position;
    public Texture texture;
}