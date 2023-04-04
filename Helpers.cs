using System.Numerics;
using SDL2;
using static SDL2.SDL;

namespace Fjord;

public static class Helpers
{
    public static float PointDirection(Vector2 pos, Vector2 pos2) {
        return (float)System.Math.Atan2(pos2.Y-pos.Y, pos2.X-pos.X);
    } 

    public static float PointDistance(Vector2 pos, Vector2 pos2) {
        return (float)Math.Pow((Math.Pow(pos2.X - pos.X, 2)) + (Math.Pow(pos2.Y-pos.Y, 2)), 0.5);
    }

    public static bool PointInside(Vector2 point, SDL_Rect rect)
    {
        return (point.X > rect.x && point.X < rect.x + rect.w && point.Y > rect.y && point.Y < rect.y + rect.h);
    }

    public static SDL_FRect RectToFRect(SDL_Rect rect)
    {
        return new SDL_FRect()
        {
            x = rect.x,
            y = rect.y,
            w = rect.w,
            h = rect.h
        };
    }
    
    public static SDL_Rect FRectToRect(SDL_FRect rect)
    {
        return new SDL_Rect()
        {
            x = (int)rect.x,
            y = (int)rect.y,
            w = (int)rect.w,
            h = (int)rect.h
        };
    }

    public static int SDL_SetRenderDrawColor(IntPtr renderer, SDL.SDL_Color color)
    {
        return SDL.SDL_SetRenderDrawColor(renderer, color.r, color.g ,color.b, color.a);
    }
}

static class StringExtensions {

  public static IEnumerable<String> SplitInParts(this String s, Int32 partLength) {
    if (s == null)
      throw new ArgumentNullException(nameof(s));
    if (partLength <= 0)
      throw new ArgumentException("Part length has to be positive.", nameof(partLength));

    for (var i = 0; i < s.Length; i += partLength)
      yield return s.Substring(i, Math.Min(partLength, s.Length - i));
  }

}