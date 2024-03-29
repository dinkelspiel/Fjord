using System.Numerics;

namespace Fjord.Graphics;

public static class UiStyles 
{
    public static float GlobalBorderRadius = 0f;    

    public static Vector4 Background = new() {
        X = 23,
        Y = 24,
        Z = 24,
        W = 255
    };

    public static Vector4 TextColor = new() {
        X = 255,
        Y = 255,
        Z = 255,
        W = 255
    };

    public static Vector4 SuccessTextColor = new() {
        X = 75,
        Y = 122,
        Z = 75,
        W = 255
    };

    public static Vector4 SuccessBackgroundColor = new() {
        X = 34,
        Y = 41,
        Z = 34,
        W = 255
    };

    public static Vector4 ErrorTextColor = new() {
        X = 255,
        Y = 104,
        Z = 107,
        W = 255
    };

    public static Vector4 WarningTextColor = new() {
        X = 255,
        Y = 217,
        Z = 102,
        W = 255
    };
    
    public static Vector4 ContainerHoverColor = new() {
        X = 173,
        Y = 51,
        Z = 51,
        W = 255
    };

    public static Vector4 ContainerHoverPressedColor = new() {
        X = 193,
        Y = 71,
        Z = 71,
        W = 255
    };

    public static Vector4 ContainerPressedColor = new() {
        X = 183,
        Y = 61,
        Z = 61,
        W = 255
    };

    public static Vector4 ContainerIdleColor = new() {
        X = 163,
        Y = 41,
        Z = 41,
        W = 255
    };

    public static Vector4 SpacerColor = new() {
        X = 50,
        Y = 50,
        Z = 50,
        W = 255
    };
}