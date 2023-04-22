using Fjord.Scenes;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using static SDL2.SDL;
using static SDL2.SDL_image;

namespace Fjord.Graphics;

public class DrawInstruction : ICloneable {
    public int depth;

    public object Clone()
    {
        return this.MemberwiseClone();
    }
}

public class Rectangle : DrawInstruction {
    public Vector4 rect;
    public Vector4 color;
    public float? borderRadius = null;
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

    public Rectangle BorderRadius(float radius)
    {
        this.borderRadius = radius;
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

    public Texture RenderToTexture()
    {
        IntPtr oldRender = SDL_GetRenderTarget(Game.SDLRenderer);

        IntPtr tex = SDL_CreateTexture(Game.SDLRenderer, SDL_PIXELFORMAT_RGBA8888, (int)SDL_TextureAccess.SDL_TEXTUREACCESS_TARGET, (int)rect.Z, (int)rect.W);
        SDL_SetRenderTarget(Game.SDLRenderer, tex);
        var tempRect = (Rectangle)this.Clone();
        tempRect.rect.X = 0;
        tempRect.rect.Y = 0;
        Draw.RectangleDirect(tempRect);

        SDL_SetRenderTarget(Game.SDLRenderer, oldRender);

        var itex = new Texture(tex).Position(rect.X, rect.Y);

        return itex;
    }
}

public class Circle : DrawInstruction {
    public Vector2 position;
    public float radius;
    public Vector4 color;
    public CirlceAnimation? hoverAnimation;
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

    public Circle HoverAnimation(CirlceAnimation anim)
    {
        this.hoverAnimation = anim;
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

    public Texture RenderToTexture()
    {
        IntPtr oldRender = SDL_GetRenderTarget(Game.SDLRenderer);

        IntPtr tex = SDL_CreateTexture(Game.SDLRenderer, SDL_PIXELFORMAT_RGBA8888, (int)SDL_TextureAccess.SDL_TEXTUREACCESS_TARGET, (int)(radius * 2), (int)(radius * 2));
        SDL_SetRenderTarget(Game.SDLRenderer, tex);
        var tempRect = (Circle)this.Clone();
        tempRect.position.X = tempRect.radius;
        tempRect.position.Y = tempRect.radius;
        Draw.CircleDirect(tempRect);

        SDL_SetRenderTarget(Game.SDLRenderer, oldRender);

        var itex = new Texture(tex).Position(position.X, position.Y);

        return itex;
    }
}

public class Texture : DrawInstruction {
    public Vector2 position;
    public IntPtr SDLTexture;
    public Vector2 textureSize;
    public bool destroy = true;
    public float angle;
    public Flip flip;
    public Center center;

    public Texture(string path)
    {
        if(!Draw.textureCache.ContainsKey(path)) {
            if(File.Exists(path)) {
                SDLTexture = IMG_LoadTexture(Game.SDLRenderer, path);
                Draw.textureCache.Add(path, SDLTexture);
            } else {
                throw new FileNotFoundException($"No image exists to load at path '{path}'");
            }
        } else {
            SDLTexture = Draw.textureCache[path];
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

    public Texture Position(float x, float y) {
        this.position = new(x, y);
        return this;
    }

    public Texture Size(Vector2 size) {
        this.textureSize = size;
        return this;
    }

    public Texture Size(float w, float h) {
        this.textureSize = new(w, h);
        return this;
    }

    public Texture Depth(int depth)
    {
        this.depth = depth;
        return this;
    } 

    public Texture Angle(float angle)
    {
        this.angle = angle;
        return this;
    }

    public Texture Flip(Flip flip)
    {   
        this.flip = flip;
        return this;
    }

    public Texture Center(Center drawCenter)
    {
        this.center = drawCenter;
        return this;
    }

    public Texture Destroy(bool des)
    {
        this.destroy = des;
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

public class Text : DrawInstruction
{
    public string font;
    public string value;
    public Vector2 position;
    public int size;
    public Vector4 color;

    public Text(string font, string text)
    {
        this.font = font;
        this.value = text;
    }

    public Text(string text)
    {
        this.value = text;
        this.font = Graphics.Font.DefaultFont;
    }

    public Text Font(string font)
    {
        this.font = font;
        return this;
    }

    public Text Position(Vector2 position)
    {
        this.position = position;
        return this;
    }

    public Text Position(float x, float y)
    {
        this.position = new(x, y);
        return this;
    }

    public Text Size(int size)
    {
        this.size = size;
        return this;
    }

    public Text Color(Vector4 color)
    {
        this.color = color;
        return this;
    }

    public Text Color(SDL_Color color)
    {
        this.color = new(color.r, color.g, color.b, color.a);
        return this;
    }

    public Text Depth(int depth)
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

    public Texture RenderToTexture()
    {
        IntPtr oldRender = SDL_GetRenderTarget(Game.SDLRenderer);

        Vector2 size = Graphics.Font.DrawSize(font, value, this.size, color);

        IntPtr tex = SDL_CreateTexture(Game.SDLRenderer, SDL_PIXELFORMAT_RGBA8888, (int)SDL_TextureAccess.SDL_TEXTUREACCESS_TARGET, (int)(size.X), (int)(size.Y));
        SDL_SetRenderTarget(Game.SDLRenderer, tex);
        var tempRect = (Text)this.Clone();
        tempRect.position.X = 0;
        tempRect.position.Y = 0;
        Draw.TextDirect(tempRect);

        SDL_SetRenderTarget(Game.SDLRenderer, oldRender);

        var itex = new Texture(tex).Position(position.X, position.Y);

        return itex;
    }
}

public class Line : DrawInstruction
{
    public Vector2 point1 = new();
    public Vector2 point2 = new();
    public Vector4 color = new();

    public Line(Vector2 point1, Vector2 point2)
    {
        this.point1 = point1;
        this.point2 = point2;
    }

    public Line Color(Vector4 color)
    {
        this.color = color;
        return this;
    }

    public Line Color(SDL_Color color)
    {
        this.color = new(color.r, color.g, color.b, color.a);
        return this;
    }

    public Line Depth(int depth)
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