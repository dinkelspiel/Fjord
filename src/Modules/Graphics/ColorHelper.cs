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
}