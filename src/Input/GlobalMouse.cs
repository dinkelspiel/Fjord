using System.Numerics;
using Fjord.Scenes;

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

    public static bool Down(MB mouseButton, params Mod[] mods)
    {
        bool containsModifiers = true;
        foreach (var i in mods)
        {
            if (!GlobalKeyboard.pressedModifiers.Contains(i))
            {
                containsModifiers = false;
                break;
            }
        }

        return downKeys[mouseButton] && containsModifiers;
    }

    public static bool Down()
    {
        return downKeys.Any((val) => val.Value == true);
    }

    public static bool Down(params Mod[] mods)
    {
        bool containsModifiers = true;
        foreach (var i in mods)
        {
            if (!GlobalKeyboard.pressedModifiers.Contains(i))
            {
                containsModifiers = false;
                break;
            }
        }

        return downKeys.Any((val) => val.Value == true) && containsModifiers;
    }


    public static bool Pressed(MB mouseButton)
    {
        return pressedKeys[mouseButton];
    }

    public static bool Pressed(MB mouseButton, params Mod[] mods)
    {
        bool containsModifiers = true;
        foreach (var i in mods)
        {
            if (!GlobalKeyboard.pressedModifiers.Contains(i))
            {
                containsModifiers = false;
                break;
            }
        }

        return pressedKeys[mouseButton] && containsModifiers;
    }

    public static bool Pressed()
    {
        return pressedKeys.Any((val) => val.Value == true);
    }

    public static bool Pressed(params Mod[] mods)
    {
        bool containsModifiers = true;
        foreach (var i in mods)
        {
            if (!GlobalKeyboard.pressedModifiers.Contains(i))
            {
                containsModifiers = false;
                break;
            }
        }

        return pressedKeys.Any((val) => val.Value == true) && containsModifiers;
    }

    public static bool Released(MB mouseButton)
    {
        return downKeysLast[mouseButton] && !downKeys[mouseButton];
    }

    public static bool Released(MB mouseButton, params Mod[] mods)
    {
        bool containsModifiers = true;
        foreach (var i in mods)
        {
            if (!GlobalKeyboard.pressedModifiers.Contains(i))
            {
                containsModifiers = false;
                break;
            }
        }

        return (downKeysLast[mouseButton] && !downKeys[mouseButton]) && containsModifiers;
    }

    public static bool Released()
    {
        return downKeys.Any((mouseButton) => downKeysLast[mouseButton.Key] && !downKeys[mouseButton.Key]);
    }

    public static bool Released(params Mod[] mods)
    {
        bool containsModifiers = true;
        foreach (var i in mods)
        {
            if (!GlobalKeyboard.pressedModifiers.Contains(i))
            {
                containsModifiers = false;
                break;
            }
        }

        return (downKeys.Any((mouseButton) => downKeysLast[mouseButton.Key] && !downKeys[mouseButton.Key])) && containsModifiers;
    }
}