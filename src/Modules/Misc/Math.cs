using System;
using System.Numerics;

namespace System;
public static class MathF
{
    public static double GetDistance(Vector2 origin, Vector2 target)
    {
        return Math.Sqrt(Math.Pow(target.X - origin.X, 2) + Math.Pow(target.X - origin.X, 2));
    }

    public static double GetDirection(Vector2 origin, Vector2 target)
    {
        return RadianToDegrees(Math.Atan2(target.Y - origin.Y, target.X - origin.X));
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

    public static double RadianToDegrees(double radians)
    {
        return (180 / Math.PI) * radians;
    }

    public static double DegreesToRadian(double degrees)
    {
        return (System.Math.PI / 180) * degrees;
    }

    public static int AngleDifference(int angle1, int angle2)
    {
        var a = angle2 - angle1;
        a += (a > 180) ? -360 : (a < -180) ? 360 : 0;
        return a;
    }
}