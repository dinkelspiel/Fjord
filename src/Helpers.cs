using System.Numerics;
using Fjord.Ui;
using SDL2;
using static SDL2.SDL;
using System.Runtime.CompilerServices;
using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Fjord;

public static class Helpers
{
    public static float RadiansToDegrees(float radians)
    {
        return (float)((180 / Math.PI) * radians);
    }

    public static float DegreesToRadians(float degrees)
    {
        return (float)((System.Math.PI / 180) * degrees);
    }

    public static float PointDirection(Vector2 origin, Vector2 target)
    {
        return RadiansToDegrees((float)Math.Atan2(target.Y - origin.Y, target.X - origin.X));
    }

    public static float PointDistance(Vector2 pos, Vector2 pos2) {
        return (float)Math.Pow((Math.Pow(pos2.X - pos.X, 2)) + (Math.Pow(pos2.Y-pos.Y, 2)), 0.5);
    }

    public static bool PointInside(Vector2 point, SDL_Rect rect)
    {
        return (point.X > rect.x && point.X < rect.x + rect.w && point.Y > rect.y && point.Y < rect.y + rect.h);
    }

    public static bool PointInside(Vector2 point, SDL_FRect rect)
    {
        return (point.X > rect.x && point.X < rect.x + rect.w && point.Y > rect.y && point.Y < rect.y + rect.h);
    }

    public static bool PointInside(Vector2 point, Vector4 rect)
    {
        return (point.X > rect.X && point.X < rect.X + rect.Z && point.Y > rect.Y && point.Y < rect.Y + rect.W);
    }

    public static double LengthDirX(double length, double angle)
    {
        angle = 180 - angle + 180;
        return length * Math.Cos(angle * Math.PI / -180);
    }

    public static double LengthDirY(double length, double angle)
    {
        angle = 180 - angle + 180;
        return length * Math.Sin(angle * Math.PI / -180);
    }

    public static Vector2 LengthDir(double length, double angle)
    {
        return new Vector2((float)LengthDirX(length, angle), (float)LengthDirY(length, angle));
    }

    public static float AngleDifference(float dest, float src) {
        var a = src - dest;
        a += (a > 180) ? -360 : (a < -180) ? 360 : 0;
        return a;
    }

    public static float MeanAngle(float[] angles)
    {
        double x = angles.Select(a => Math.Cos(DegreesToRadians(a))).Sum() / angles.Length;
        double y = angles.Select(a => Math.Sin(RadiansToDegrees(a))).Sum() / angles.Length;
        return RadiansToDegrees((float)Math.Atan2(y, x));
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

    public static float Lerp(float firstFloat, float secondFloat, float by)
    {
        return firstFloat + (secondFloat - firstFloat) * by;
    }

    public static Vector4 Lerp(Vector4 a, Vector4 b, float by)
    {
        return new(
            a.X + (b.X - a.X) * by,
            a.Y + (b.Y - a.Y) * by,
            a.Z + (b.Z - a.Z) * by,
            a.W + (b.W - a.W) * by
        );
    }

    public static Vector4 ColorToV4(SDL_Color col) {
        return new(col.r, col.g, col.b, col.a);
    }

    public static SDL_Color V4ToColor(Vector4 v) {
        return new() {
            r = (byte)v.X,
            g = (byte)v.Y,
            b = (byte)v.Z,
            a = (byte)v.W
        };
    }

    public static Vector4 ToV4(this SDL_Color color) 
    {
        return new(color.r, color.g, color.b, color.a);
    }

    public static SDL_Color ToCol(this Vector4 v) 
    {
        return new()
        {
            r = (byte)v.X,
            g = (byte)v.Y,
            b = (byte)v.Z,
            a = (byte)v.W
        };
    }
}

static class Extensions
{

    public static IEnumerable<String> SplitInParts(this String s, Int32 partLength)
    {
        if (s == null)
            throw new ArgumentNullException(nameof(s));
        if (partLength <= 0)
            throw new ArgumentException("Part length has to be positive.", nameof(partLength));

        for (var i = 0; i < s.Length; i += partLength)
            yield return s.Substring(i, Math.Min(partLength, s.Length - i));
    }

    public static HAlign<T> IntoHAlign<T>(this List<T> s)
    {
        HAlign<T> a = new();

        foreach(T i in s)
        {
            a.Add(i);
        }

        return a;
    }

    public static List<T> IntoList<T>(this HAlign<T> s)
    {
        List<T> a = new();

        foreach (T i in s)
        {
            a.Add(i);
        }

        return a;
    }

    public static string OSPath(this string path, [CallerFilePath] string caller = "")
    {
        String newPath = path;
        if (GetPlatform() == Platform.Windows)
            newPath = path.Replace("/", "\\");
        else
            newPath = path.Replace("\\", "/");
        return newPath;
    }

    public enum Platform
    {
        Windows,
        Linux,
        OSX,
        Unknown
    }

    public static Platform GetPlatform()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            return Platform.Windows;

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            return Platform.Linux;

        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            return Platform.OSX;

        return Platform.Unknown;
    }
}