using System.Numerics;
using static SDL2.SDL;

namespace Fjord.Modules.Graphics;

public static class ColorHelper {
    public static SDL_Color Into(this Vector4 color) {
        return new SDL_Color(
            (byte)color.X,
            (byte)color.Y,
            (byte)color.Z,
            (byte)color.W
        );
    }

    public static Vector4 Into(this SDL_Color color)
    {
        return new Vector4(
            (byte)color.r,
            (byte)color.g,
            (byte)color.b,
            (byte)color.a
        );
    }
}
public static class Color
{
    public static Vector4 Black = new Vector4(0, 0, 0, 255);
    public static Vector4 White = new Vector4(255, 255, 255, 255);
    public static Vector4 Red = new Vector4(255, 0, 0, 255);
    public static Vector4 Green = new Vector4(0, 255, 0, 255);
    public static Vector4 Blue = new Vector4(0, 0, 255, 255);
}