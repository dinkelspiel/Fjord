using System.Numerics;

namespace Fjord.Input;

public enum MB
{
    Left,
    Right,
    ScrollUp,
    ScrollDown,
    ScrollRight,
    ScrollLeft
}

public static class GlobalMouse
{
    public static Vector2 Position = new();
    internal static Vector2 RelativePosition = new();
    
    internal static Dictionary<MB, bool> downKeys = new() {
        {MB.Left, false},
        {MB.Right, false},
        {MB.ScrollUp, false},
        {MB.ScrollDown, false},
        {MB.ScrollLeft, false},
        {MB.ScrollRight, false}
    };
    internal static Dictionary<MB, bool> pressedKeys = new() {
        {MB.Left, false},
        {MB.Right, false},
        {MB.ScrollUp, false},
        {MB.ScrollDown, false},
        {MB.ScrollLeft, false},
        {MB.ScrollRight, false}
    };
    internal static Dictionary<MB, bool> downKeysLast = new() {
        {MB.Left, false},
        {MB.Right, false},
        {MB.ScrollUp, false},
        {MB.ScrollDown, false},
        {MB.ScrollLeft, false},
        {MB.ScrollRight, false}
    };

    public static bool Down(MB mouseButton)
    {
        return downKeys[mouseButton];
    }

    public static bool Down()
    {
        return downKeys.Any((val) => val.Value == true);
    }

    public static bool Pressed(MB mouseButton)
    {
        return pressedKeys[mouseButton];
    }

    public static bool Pressed()
    {
        return pressedKeys.Any((val) => val.Value == true);
    }

    public static bool Released(MB mouseButton)
    {
        return downKeysLast[mouseButton] && !downKeys[mouseButton];
    }

    public static bool Released()
    {
        return downKeys.Any((mouseButton) => downKeysLast[mouseButton.Key] && !downKeys[mouseButton.Key]);
    }
}