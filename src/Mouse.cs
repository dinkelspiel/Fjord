using System.Numerics;

namespace Fjord.Input;

public static class Mouse
{
    public static Vector2 Position = new();
    public static Vector2 RelativePosition = new();
    public static bool Down = false;
    public static bool Pressed = false;
    public static bool ScrollDown = false;
    public static bool ScrollUp = false;
    public static bool ScrollRight = false;
    public static bool ScrollLeft = false;
}