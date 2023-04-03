using System.Numerics;
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
}