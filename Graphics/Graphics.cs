using System.Drawing;
using System.Numerics;
using Fjord.Scenes;
using static SDL2.SDL;
using static SDL2.SDL_gfx;

namespace Fjord.Graphics;

public static class Draw
{
    internal static string? CurrentSceneID = null;

    internal static List<DrawInstruction> drawBuffer = new();

    public static void FillRectangle(Vector4 rect, Vector4 color, int depth=0) {
        new Rectangle(rect)
        {
            color = color,
            depth = depth,
            fill = true
        }.Render();
    }
    
    public static void Rectangle(Vector4 rect, Vector4 color, int depth=0) {
        new Rectangle(rect) {
            color = color,
            depth = depth,
            fill = false
        }.Render();
    }

    public static void FillCircle(Vector2 position, float radius, Vector4 color, int depth=0) {
        new Circle(position, radius) {
            color = color,
            depth = depth,
            fill = true
        }.Render();
    }

    public static void Circle(Vector2 position, float radius, Vector4 color, int depth=0) {
        new Circle(position, radius) {
            color = color,
            depth = depth,
            fill = false
        }.Render();
    }

    public static void Texture(Vector2 position, IntPtr texture, int depth=0)
    {
        new Texture(texture)
        {
            position = position,
            depth = depth
        }.Render();
    }

    public static void Geometry(List<SDL_Vertex> verts)
    {
        new Geometry()
        {
            verticies = verts
        }.Render();
    }

    internal static void RectangleDirect(Rectangle rectangle) {
        Helpers.SDL_SetRenderDrawColor(Game.SDLRenderer, new SDL_Color() {
            r = (byte)rectangle.color.X,
            g = (byte)rectangle.color.Y,
            b = (byte)rectangle.color.Z,
            a = (byte)rectangle.color.W
        });
        SDL_Rect SDLRect = new () {
            x = (int)rectangle.rect.X,
            y = (int)rectangle.rect.Y,
            w = (int)rectangle.rect.Z,
            h = (int)rectangle.rect.W
        };
        if(rectangle.fill)
            SDL_RenderFillRect(Game.SDLRenderer, ref SDLRect);
        else 
            SDL_RenderDrawRect(Game.SDLRenderer, ref SDLRect);
        SDL_SetRenderDrawColor(Game.SDLRenderer, 0, 0, 0, 255);
    }

    internal static void CircleDirect(Circle circle) {
        if(circle.fill)
            throw new NotImplementedException("Drawing filled circles is currently unsupported");
            // if(filledCircleRGBA(Game.SDLRenderer, 100, 100, 100, 255, 255, 255, 255) == -1) {
            //     Debug.Log(LogLevel.Error, "Failed to draw filledCircleRGBA");
            // }
        else
            if(aacircleRGBA(Game.SDLRenderer, (short)circle.position.X, (short)circle.position.Y, (short)circle.radius, (byte)circle.color.X, (byte)circle.color.Y, (byte)circle.color.Z, (byte)circle.color.W) == -1) {
                Debug.Log(LogLevel.Error, "Failed to draw aacircleRGBA");
            }
        SDL_SetRenderDrawColor(Game.SDLRenderer, 0, 0, 0, 255);
    }

    internal static void TextureDirect(Texture texture)
    {
        SDL_Rect rect = new()
        {
            x = (int)texture.position.X,
            y = (int)texture.position.Y,
            w = (int)texture.textureSize.X,
            h = (int)texture.textureSize.Y
        };
        SDL_RenderCopy(Game.SDLRenderer, texture.SDLTexture, IntPtr.Zero, ref rect);
    }

    internal static void GeometryDirect(Geometry geometry)
    {
        SDL_RenderGeometry(Game.SDLRenderer, IntPtr.Zero, geometry.verticies.ToArray(), geometry.verticies.Count, IntPtr.Zero, 0);
    }

    internal static void DrawDrawBuffer(List<DrawInstruction> drawBufferLocal) {
        List<DrawInstruction> sortedDrawBuffer = drawBufferLocal.OrderBy(e => e.depth).ToList();

        foreach(DrawInstruction drawInsObj in sortedDrawBuffer) {
            if(drawInsObj.GetType() == typeof(Rectangle)) {
                Rectangle drawIns = (Rectangle)drawInsObj;
                Draw.RectangleDirect(drawIns);
            } else if(drawInsObj.GetType() == typeof(Circle)) {
                Circle drawIns = (Circle)drawInsObj;
                Draw.CircleDirect(drawIns);
            } else if(drawInsObj.GetType() == typeof(Texture))
            {
                Texture drawIns = (Texture)drawInsObj;
                Draw.TextureDirect(drawIns);
            } else if(drawInsObj.GetType() == typeof(Geometry))
            {
                Geometry drawIns = (Geometry)drawInsObj;
                Draw.GeometryDirect(drawIns);
            }
        }
    }
}