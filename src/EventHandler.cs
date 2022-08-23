using static SDL2.SDL;

namespace Fjord;

internal static class EventHandler {
    public static void PollEvents() {
        while (SDL_PollEvent(out SDL_Event events) != 0) {
            switch(events.type) {
            case SDL_EventType.SDL_QUIT:
                Game.Stop();
                break;      
            }
        }
    }
}


