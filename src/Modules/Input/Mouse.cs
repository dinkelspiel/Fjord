using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Fjord.Modules.Input;

public enum MouseButton
{
    Left,
    Right,
    WheelUp,
    WheelDown
}

public static class Mouse
{
    internal static bool LeftMouseButton, RightMouseButton;
    internal static bool LeftMouseButtonLast, RightMouseButtonLast;

    internal static bool MouseWheelUp, MouseWheelDown;
    internal static bool MouseWheelUpLast, MouseWheelDownLast;

    internal static Vector2 ScreenPosition = new Vector2();
    internal static Vector2 GameScreenPosition = new Vector2();
    internal static Vector2 GameLocalPosition = new Vector2();

    public static bool Pressed(MouseButton button)
    {
        switch (button)
        {
            case MouseButton.Left:
                return LeftMouseButton;
            case MouseButton.Right:
                return RightMouseButton;
            case MouseButton.WheelUp:
                return MouseWheelUp;
            case MouseButton.WheelDown:
                return MouseWheelDown;
            default:
                return false;
        }
    }

    public static bool JustPressed(MouseButton button)
    {
        switch (button)
        {
            case MouseButton.Left:
                return LeftMouseButton && !LeftMouseButtonLast;
            case MouseButton.Right:
                return RightMouseButton && !RightMouseButtonLast;
            case MouseButton.WheelUp:
                return MouseWheelUp && !MouseWheelUpLast;
            case MouseButton.WheelDown:
                return MouseWheelDown && !MouseWheelDownLast;
            default:
                return false;
        }
    }

    public static bool JustReleased(MouseButton button)
    {
        switch (button)
        {
            case MouseButton.Left:
                return !LeftMouseButton && LeftMouseButtonLast;
            case MouseButton.Right:
                return !RightMouseButton && RightMouseButtonLast;
            case MouseButton.WheelUp:
                return !MouseWheelUp && MouseWheelUpLast;
            case MouseButton.WheelDown:
                return !MouseWheelDown && MouseWheelDownLast;
            default:
                return false;
        }
    }

    public static bool AnyPressed(MouseButton button)
    {
        return LeftMouseButton || RightMouseButton || MouseWheelDown || MouseWheelUp;
    }
}
