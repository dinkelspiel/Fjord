using Fjord.Scenes;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
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

    public Rectangle Depth(int depth)
    {
        this.depth = depth;
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

    public Circle Depth(int depth)
    {
        this.depth = depth;
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

    public Texture Depth(int depth)
    {
        this.depth = depth;
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

public class Geometry : DrawInstruction
{
    public List<SDL_Vertex> verticies = new List<SDL_Vertex>();

    public Geometry AddVertex(SDL_Vertex v)
    {
        verticies.Add(v);
        return this;
    }

    public Geometry AddVertex(Vector2 position, Vector4 color, Vector2 tex_coord)
    {
        verticies.Add(new SDL_Vertex()
        {
            position = new SDL_FPoint() { x = position.X, y = position.Y },
            color = new SDL_Color() { r = (byte)color.X, g = (byte)color.Y, b = (byte)color.Z, a = (byte)color.W },
            tex_coord = new SDL_FPoint() { x = tex_coord.X, y = tex_coord.Y }
        });
        return this;
    }
    public Geometry AddVertex(Vector2 position, Vector4 color)
    {
        verticies.Add(new SDL_Vertex()
        {
            position = new SDL_FPoint() { x = position.X, y = position.Y },
            color = new SDL_Color() { r = (byte)color.X, g = (byte)color.Y, b = (byte)color.Z, a = (byte)color.W },
            tex_coord = new SDL_FPoint() { x = 0, y = 0 }
        });
        return this;
    }

    public Geometry Depth(int depth)
    {
        this.depth = depth;
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