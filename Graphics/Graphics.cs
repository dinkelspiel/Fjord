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

    internal static void RectangleDirect(Vector4 rect, Vector4 color, bool fill) {
        Helpers.SDL_SetRenderDrawColor(Game.SDLRenderer, new SDL_Color() {
            r = (byte)color.X,
            g = (byte)color.Y,
            b = (byte)color.Z,
            a = (byte)color.W
        });
        SDL_Rect SDLRect = new () {
            x = (int)rect.X,
            y = (int)rect.Y,
            w = (int)rect.Z,
            h = (int)rect.W
        };
        if(fill)
            SDL_RenderFillRect(Game.SDLRenderer, ref SDLRect);
        else 
            SDL_RenderDrawRect(Game.SDLRenderer, ref SDLRect);
        SDL_SetRenderDrawColor(Game.SDLRenderer, 0, 0, 0, 255);
    }

    internal static void CircleDirect(Vector2 position, float radius, Vector4 color, bool fill) {
        if(fill)
            throw new NotImplementedException("Drawing filled circles is currently unsupported");
            // if(filledCircleRGBA(Game.SDLRenderer, 100, 100, 100, 255, 255, 255, 255) == -1) {
            //     Debug.Log(LogLevel.Error, "Failed to draw filledCircleRGBA");
            // }
        else
            if(aacircleRGBA(Game.SDLRenderer, 100, 100, 100, 255, 255, 255, 255) == -1) {
                Debug.Log(LogLevel.Error, "Failed to draw aacircleRGBA");
            }
        SDL_SetRenderDrawColor(Game.SDLRenderer, 0, 0, 0, 255);
    }

    internal static void TextureDirect(Vector2 position, Texture texture)
    {
        SDL_Rect rect = new()
        {
            x = (int)position.X,
            y = (int)position.Y,
            w = (int)texture.textureSize.X,
            h = (int)texture.textureSize.Y
        };
        SDL_RenderCopy(Game.SDLRenderer, texture.SDLTexture, IntPtr.Zero, ref rect);
    }

    internal static void DrawDrawBuffer(List<DrawInstruction> drawBufferLocal) {
        List<DrawInstruction> sortedDrawBuffer = drawBufferLocal.OrderBy(e => e.depth).ToList();

        foreach(DrawInstruction drawInsObj in sortedDrawBuffer) {
            if(drawInsObj.GetType() == typeof(Rectangle)) {
                Rectangle drawIns = (Rectangle)drawInsObj;
                Draw.RectangleDirect(drawIns.rect, drawIns.color, drawIns.fill);
            } else if(drawInsObj.GetType() == typeof(Circle)) {
                Circle drawIns = (Circle)drawInsObj;
                Draw.CircleDirect(drawIns.position, drawIns.radius, drawIns.color, drawIns.fill);
            } else if(drawInsObj.GetType() == typeof(Texture))
            {
                Texture drawIns = (Texture)drawInsObj;
                Draw.TextureDirect(drawIns.position, drawIns);
            }
        }
    }
}