using Fjord.Scenes;
using System.Numerics;
using static SDL2.SDL;
using static SDL2.SDL_image;

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
    
    public Circle Position(Vector2 position) {
        this.position = position;
        return this;
    }

    public Circle Radius(float radius) {
        this.radius = radius;
        return this;
    }

    public Circle Color(Vector4 color) {
        this.color = color;
        return this;
    }

    public Circle Fill(bool fill) {
        this.fill = fill;
        return this;
    }

    public void Render() {
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

public class Texture : DrawInstruction {
    public Vector2 position;
    public IntPtr SDLTexture;
    public Vector2 textureSize;
    public Texture(string path)
    {
        if(File.Exists(path)) {
            SDLTexture = IMG_LoadTexture(Game.SDLRenderer, path);
        } else {
            throw new FileNotFoundException($"No image exists to load at path '{path}'");
        }
        SDL_QueryTexture(SDLTexture, out uint format, out int access, out int w, out int h);
        textureSize = new Vector2(w, h);
    }

    public Texture(IntPtr texture)
    {
        this.SDLTexture = texture;
        SDL_QueryTexture(SDLTexture, out uint format, out int access, out int w, out int h);
        textureSize = new Vector2(w, h);
    }

    public Texture SetTexture(IntPtr texture) {
        this.SDLTexture = texture;
        return this;
    }

    public Texture Position(Vector2 position) {
        this.position = position;
        return this;
    }

    public Texture TextureSize(Vector2 size) {
        this.textureSize = size;
        return this;
    }

    public void Render() {
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