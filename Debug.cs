using static SDL2.SDL;

namespace ShooterThingy;

public static class Debug {
    private static bool DebugMode = false;
    public static SDL_FRect DebugWindowOffset = new ()
    {
        x = 0f,
        y = 0f,
        w = 0.2f,
        h = 0f
    };
    public static void SetDebugMode(bool debugMode) {
        DebugMode = debugMode;
    }

    public static bool GetDebugMode()
    {
        return DebugMode;
    }
}