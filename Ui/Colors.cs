using static SDL2.SDL;

namespace Fjord.Ui;

public static class UiColors {
    public static SDL_Color Background = new SDL_Color() {
        r = 23,
        g = 24,
        b = 24,
        a = 255
    };

    public static SDL_Color TextColor = new SDL_Color() {
        r = 255,
        g = 255,
        b = 255,
        a = 255
    };

    public static SDL_Color SuccessTextColor = new SDL_Color() {
        r = 75,
        g = 122,
        b = 75,
        a = 255
    };

    public static SDL_Color SuccessBackgroundColor = new SDL_Color() {
        r = 34,
        g = 41,
        b = 34,
        a = 255
    };

    public static SDL_Color ErrorTextColor = new SDL_Color() {
        r = 255,
        g = 104,
        b = 107,
        a = 255
    };

    
    public static SDL_Color ContainerHoverColor = new SDL_Color() {
        r = 52,
        g = 97,
        b = 152,
        a = 255
    };

    public static SDL_Color ContainerPressedColor = new SDL_Color() {
        r = 65,
        g = 121,
        b = 190,
        a = 255
    };

    public static SDL_Color ContainerIdleColor = new SDL_Color() {
        r = 39,
        g = 73,
        b = 114,
        a = 255
    };

    public static SDL_Color SpacerColor = new SDL_Color() {
        r = 50,
        g = 50,
        b = 50,
        a = 255
    };
}