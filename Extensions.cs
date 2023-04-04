using SDL2;

namespace Fjord.Extensions;

public static class Extensions
{
    public static int SDL_SetRenderDrawColor(IntPtr renderer, SDL.SDL_Color color)
    {
        return SDL.SDL_SetRenderDrawColor(renderer, color.r, color.g ,color.b, color.a);
    }
}