using Fjord.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static SDL2.SDL;

namespace ShooterThingy
{
    internal static class EventHandler
    {
        internal static void HandleEvents()
        {
            while (SDL_PollEvent(out SDL_Event e) != 0)
            {
                switch (e.type)
                {
                    case SDL_EventType.SDL_QUIT:
                        Game.Stop();
                        break;
                    case SDL_EventType.SDL_KEYDOWN:
                        switch (e.key.keysym.sym)
                        {
                            case SDL_Keycode.SDLK_d:
                                Keyboard.AddKey(Key.D);
                                break;
                            case SDL_Keycode.SDLK_LCTRL:
                                if(!Keyboard.pressedModifiers.Contains(Mod.LCtrl))
                                    Keyboard.pressedModifiers.Add(Mod.LCtrl);
                                break;
                            case SDL_Keycode.SDLK_LSHIFT:
                                if (!Keyboard.pressedModifiers.Contains(Mod.LShift))
                                    Keyboard.pressedModifiers.Add(Mod.LShift);
                                break;
                        }

                        break;
                    case SDL_EventType.SDL_KEYUP:
                        switch (e.key.keysym.sym)
                        {
                            case SDL_Keycode.SDLK_d:
                                Keyboard.downKeys.Remove(Key.D);
                                break;
                            case SDL_Keycode.SDLK_LCTRL:
                                Keyboard.pressedModifiers.Remove(Mod.LCtrl);
                                break;
                            case SDL_Keycode.SDLK_LSHIFT:
                                Keyboard.pressedModifiers.Remove(Mod.LShift);
                                break;
                        }

                        break;
                    case SDL_EventType.SDL_MOUSEMOTION:
                        int _x;
                        int _y;
                        SDL_GetMouseState(out _x, out _y);
                        Mouse.Position = new Vector2(_x, _y);
                        break;
                    case SDL_EventType.SDL_MOUSEBUTTONDOWN:
                        if (!Mouse.Down)
                            Mouse.Pressed = true;
                        else
                            Mouse.Pressed = false;
                        Mouse.Down = true;
                        break;
                    case SDL_EventType.SDL_MOUSEBUTTONUP:
                        Mouse.Down = false;
                        break;
                }
            }
        }
    }
}
