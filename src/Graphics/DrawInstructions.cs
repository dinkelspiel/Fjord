using Fjord.Scenes;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using static SDL2.SDL;
using static SDL2.SDL_image;

namespace Fjord.Graphics;

public interface IDrawInstruction {
    public int depth { get; set;  }
}

public struct Rectangle : IDrawInstruction {
    public Vector4 rect;
    public Vector4 color;
    public float? borderRadius = null;
    public bool fill;
    public int depth { get; set; } = 0;

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

    public Texture ToTexture()
    {
        IntPtr oldRender = SDL_GetRenderTarget(Game.SDLRenderer);

        IntPtr tex = SDL_CreateTexture(Game.SDLRenderer, SDL_PIXELFORMAT_RGBA8888, (int)SDL_TextureAccess.SDL_TEXTUREACCESS_TARGET, (int)rect.Z, (int)rect.W);
        SDL_SetTextureBlendMode(tex, SDL_BlendMode.SDL_BLENDMODE_BLEND);
        SDL_SetRenderTarget(Game.SDLRenderer, tex);
        var tempRect = this;
        tempRect.rect.X = 0;
        tempRect.rect.Y = 0;
        Draw.RectangleDirect(tempRect);

        SDL_SetRenderTarget(Game.SDLRenderer, oldRender);

        var itex = new Texture(tex).Position(rect.X, rect.Y);

        return itex;
    } 
}

public struct Circle : IDrawInstruction {
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

    public Texture ToTexture()
    {
        IntPtr oldRender = SDL_GetRenderTarget(Game.SDLRenderer);

        IntPtr tex = SDL_CreateTexture(Game.SDLRenderer, SDL_PIXELFORMAT_RGBA8888, (int)SDL_TextureAccess.SDL_TEXTUREACCESS_TARGET, (int)(radius * 2 + 1), (int)(radius * 2 + 1));
        if(color.W != 255)
            SDL_SetTextureBlendMode(tex, SDL_BlendMode.SDL_BLENDMODE_BLEND); 
        SDL_SetRenderTarget(Game.SDLRenderer, tex);
        SDL_SetRenderDrawColor(Game.SDLRenderer, 0, 0, 0, 0);
        SDL_RenderClear(Game.SDLRenderer);

        var tempRect = this;
        tempRect.position.X = tempRect.radius;
        tempRect.position.Y = tempRect.radius;
        Draw.CircleDirect(tempRect);

        SDL_SetRenderTarget(Game.SDLRenderer, oldRender);

        var itex = new Texture(tex).Position(position.X, position.Y);

        return itex;
    }

    public int depth { get; set; }
}

public class Texture : IDrawInstruction {
    public Vector2 position;
    public IntPtr SDLTexture;
    public IntPtr SDLSurface;
    public Vector2 textureSize;
    public Vector2 sizeMultiplier = new(1, 1);
    public bool destroy = false;
    public float angle;
    public Flip flip;
    public float alpha = 255;
    public Center center;
    public bool isCustomCenter = false;
    public Vector2 customCenter = new();
    public Vector4? srcTextureOffset = null;

    public Texture(string path)
    {
        path = path.OSPath();
        if(!Draw.textureCache.ContainsKey(path)) {
            if(File.Exists(path)) {
                SDLSurface = IMG_Load(path);
                SDLTexture = SDL_CreateTextureFromSurface(Game.SDLRenderer, SDLSurface);
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

    public Texture PositionAndDepth(Vector2 position) {
        this.position = position;
        this.depth = -(int)position.Y;
        return this;
    }

    public Texture PositionAndDepth(float x, float y) {
        this.position = new(x, y);
        this.depth = -(int)y;
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
    public Texture SizeMultiplier(Vector2 size)
    {
        this.sizeMultiplier = size;
        return this;
    }

    public Texture SizeMultiplier(float w, float h)
    {
        this.sizeMultiplier = new(w, h);
        return this;
    }

    public Texture Depth(int depth)
    {
        this.depth = depth;
        return this;
    } 

    public Texture Depth(float depth)
    {
        this.depth = (int)depth;
        return this;
    } 

    public Texture Depth(double depth)
    {
        this.depth = (int)depth;
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
        this.isCustomCenter = false;
        return this;
    }

    public Texture Center(Vector2 drawCenter)
    {
        this.customCenter = drawCenter;
        this.isCustomCenter = true;
        return this;
    }

    public Texture Alpha(float alpha)
    {
        this.alpha = alpha;
        return this;
    }

    public Texture Destroy(bool des)
    {
        this.destroy = des;
        return this;
    }

    public Texture SrcTextureOffset(Vector4 off)
    {
        this.srcTextureOffset = off;
        return this;
    }

    public Texture GetRect(out Vector4 outRect)
    {
        SDL_Rect rect = new()
        {
            x = (int)this.position.X,
            y = (int)this.position.Y,
            w = (int)(this.textureSize.X * this.sizeMultiplier.X),
            h = (int)(this.textureSize.Y * this.sizeMultiplier.Y)
        };

        SDL_RendererFlip tmpFlip = this.flip == Graphics.Flip.Horizontal ? SDL_RendererFlip.SDL_FLIP_HORIZONTAL : this.flip == Graphics.Flip.Vertical ? SDL_RendererFlip.SDL_FLIP_VERTICAL : this.flip == Graphics.Flip.Both ? SDL_RendererFlip.SDL_FLIP_HORIZONTAL | SDL_RendererFlip.SDL_FLIP_VERTICAL : SDL_RendererFlip.SDL_FLIP_NONE;

        Vector2 center = new();
        Vector2 textureSize = this.textureSize;

        if(!isCustomCenter)
        {
            switch (this.center)
            {
                case Graphics.Center.TopLeft:
                    {
                        center = new(0, 0);
                    }
                    break;
                case Graphics.Center.TopMiddle:
                    {
                        center = new(rect.w / 2, 0);
                    }
                    break;
                case Graphics.Center.TopRight:
                    {
                        center = new(rect.h, 0);
                    }
                    break;

                case Graphics.Center.MiddleLeft:
                    {
                        center = new(0, rect.h / 2);
                    }
                    break;
                case Graphics.Center.Middle:
                    {
                        center = new(rect.w / 2, rect.h / 2);
                    }
                    break;
                case Graphics.Center.MiddleRight:
                    {
                        center = new(rect.w, rect.h / 2);
                    }
                    break;

                case Graphics.Center.BottomLeft:
                    {
                        center = new(0, rect.h);
                    }
                    break;
                case Graphics.Center.BottomMiddle:
                    {
                        center = new(rect.w / 2, rect.h);
                    }
                    break;
                case Graphics.Center.BottomRight:
                    {
                        center = new(rect.w, rect.h);
                    }
                    break;
            }
        } else
        {
            center = new(customCenter.X, customCenter.Y);
        }

        SDL_Point sdlcenter = new()
        {
            x = (int)center.X,
            y = (int)center.Y
        };
        rect.x -= sdlcenter.x;
        rect.y -= sdlcenter.y;

        outRect = new(rect.x, rect.y, rect.w, rect.h);

        return this;
    }

    public Texture GetTextureAsSolid(Vector4 color)
    {
        // color = new(100, 100, 100, 255);
        // IntPtr surface = SDL_CreateRGBSurface(0,(int)textureSize.X,(int)textureSize.Y,32,0,0,0,0);
        // SDL_Rect dstRect = new();
        // SDL_BlitSurface(SDLSurface, IntPtr.Zero, surface, ref dstRect);

        // IntPtr newTexture = SDL_CreateTextureFromSurface(Game.SDLRenderer, surface);

        IntPtr newTexture = SDL_CreateTexture(Game.SDLRenderer, SDL_PIXELFORMAT_RGBA8888, (int)SDL_TextureAccess.SDL_TEXTUREACCESS_TARGET, (int)textureSize.X, (int)textureSize.Y);

        IntPtr oldRenderer = SDL_GetRenderer(Game.SDLRenderer);
        SDL_SetRenderTarget(Game.SDLRenderer, newTexture);

        SDL_RenderCopy(Game.SDLRenderer, SDLTexture, IntPtr.Zero, IntPtr.Zero);

        SDL_SetRenderTarget(Game.SDLRenderer, oldRenderer);

        SDL_SetTextureBlendMode(newTexture, SDL_BlendMode.SDL_BLENDMODE_BLEND);


        SDL_SetTextureColorMod(newTexture, 0, 0, 0);
        SDL_SetTextureAlphaMod(newTexture, 255);

        SDL_SetTextureColorMod(newTexture, (byte)color.X, (byte)color.Y, (byte)color.Z);

        return SetTexture(newTexture);
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

    public int depth { get; set; }
}

public struct Geometry : IDrawInstruction
{
    public List<SDL_Vertex> verticies = new List<SDL_Vertex>();

    public Geometry()
    {
    }

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

    public int depth { get; set; }
}

public struct Text : IDrawInstruction
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

    public Texture ToTexture()
    {
        IntPtr oldRender = SDL_GetRenderTarget(Game.SDLRenderer);

        Vector2 size = Graphics.Font.DrawSize(font, value, this.size, color);

        IntPtr tex = SDL_CreateTexture(Game.SDLRenderer, SDL_PIXELFORMAT_RGBA8888, (int)SDL_TextureAccess.SDL_TEXTUREACCESS_TARGET, (int)(size.X), (int)(size.Y));
        SDL_SetTextureBlendMode(tex, SDL_BlendMode.SDL_BLENDMODE_BLEND);
        SDL_SetRenderTarget(Game.SDLRenderer, tex);
        SDL_SetRenderDrawColor(Game.SDLRenderer, 0, 0, 0, 0);
        SDL_RenderClear(Game.SDLRenderer);
        var tempRect = this;
        tempRect.position.X = 0;
        tempRect.position.Y = 0;
        Draw.TextDirect(tempRect);

        SDL_SetRenderTarget(Game.SDLRenderer, oldRender);

        var itex = new Texture(tex).Position(position.X, position.Y);

        return itex;
    }

    public int depth { get; set; }
}

public struct Line : IDrawInstruction
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

    public int depth { get; set; }
}