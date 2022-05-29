using static SDL2.SDL;
using Fjord.Modules.Input;
using Fjord.Modules.Game;

namespace Fjord.Modules.Misc {
    static class event_handler {
        public static void handle_events() {
            mouse.wheel_down = false;
            mouse.wheel_up = false;
            while (SDL_PollEvent(out SDL_Event events) != 0) {
                switch(events.type) {
                case SDL_EventType.SDL_QUIT:
                    game.is_running = false;
                    break;
                case SDL_EventType.SDL_MOUSEMOTION:
                    SDL_GetMouseState(out mouse.screen_position.x, out mouse.screen_position.y);
                    double window_res = game.window_resolution.x;
                    double game_res = scene_handler.get_current_scene().get_resolution().x;
                    double offset = window_res / game_res;
                    mouse.game_position.x = (int)(mouse.screen_position.x / offset);
                    mouse.game_position.y = (int)(mouse.screen_position.y / offset);
                    break;
                #region mouse
                case SDL_EventType.SDL_MOUSEBUTTONDOWN:
                    if(events.button.button == SDL_BUTTON_LEFT) {
                        mouse.lmb = true;
                    }
                    if(events.button.button == SDL_BUTTON_RIGHT) {
                        mouse.rmb = true;
                    }
                    break;
                case SDL_EventType.SDL_MOUSEBUTTONUP:
                    if(events.button.button == SDL_BUTTON_LEFT) {
                        mouse.lmb = false;
                    }
                    if(events.button.button == SDL_BUTTON_RIGHT) {
                        mouse.rmb = false;
                    }
                    break;
                case SDL_EventType.SDL_MOUSEWHEEL:
                    if(events.wheel.y > 0) { // scroll up
                        mouse.wheel_up = true;
                        mouse.wheel_down = false;
                    }
                    else if(events.wheel.y < 0) { // scroll down
                        mouse.wheel_down = true;
                        mouse.wheel_up = false;
                    }
                    break;
                #endregion
                #region keys
                case SDL_EventType.SDL_KEYDOWN:
                    switch(events.key.keysym.sym) {
                        case SDL_Keycode.SDLK_a:
                            keyboard.pressed_keys[(int)key.a] = true;
                            break;
                        case SDL_Keycode.SDLK_b:
                            keyboard.pressed_keys[(int)key.b] = true;
                            break;
                        case SDL_Keycode.SDLK_c:
                            keyboard.pressed_keys[(int)key.c] = true;
                            break;
                        case SDL_Keycode.SDLK_d:
                            keyboard.pressed_keys[(int)key.d] = true;
                            break;
                        case SDL_Keycode.SDLK_e:
                            keyboard.pressed_keys[(int)key.e] = true;
                            break;
                        case SDL_Keycode.SDLK_f:
                            keyboard.pressed_keys[(int)key.f] = true;
                            break;
                        case SDL_Keycode.SDLK_g:
                            keyboard.pressed_keys[(int)key.g] = true;
                            break;
                        case SDL_Keycode.SDLK_h:
                            keyboard.pressed_keys[(int)key.h] = true;
                            break;
                        case SDL_Keycode.SDLK_i:
                            keyboard.pressed_keys[(int)key.i] = true;
                            break;
                        case SDL_Keycode.SDLK_j:
                            keyboard.pressed_keys[(int)key.j] = true;
                            break;
                        case SDL_Keycode.SDLK_k:
                            keyboard.pressed_keys[(int)key.k] = true;
                            break;
                        case SDL_Keycode.SDLK_l:
                            keyboard.pressed_keys[(int)key.l] = true;
                            break;
                        case SDL_Keycode.SDLK_m:
                            keyboard.pressed_keys[(int)key.m] = true;
                            break;
                        case SDL_Keycode.SDLK_n:
                            keyboard.pressed_keys[(int)key.n] = true;
                            break;
                        case SDL_Keycode.SDLK_o:
                            keyboard.pressed_keys[(int)key.o] = true;
                            break;
                        case SDL_Keycode.SDLK_p:
                            keyboard.pressed_keys[(int)key.p] = true;
                            break;
                        case SDL_Keycode.SDLK_q:
                            keyboard.pressed_keys[(int)key.q] = true;
                            break;
                        case SDL_Keycode.SDLK_r:
                            keyboard.pressed_keys[(int)key.r] = true;
                            break;
                        case SDL_Keycode.SDLK_s:
                            keyboard.pressed_keys[(int)key.s] = true;
                            break;
                        case SDL_Keycode.SDLK_t:
                            keyboard.pressed_keys[(int)key.t] = true;
                            break;
                        case SDL_Keycode.SDLK_u:
                            keyboard.pressed_keys[(int)key.u] = true;
                            break;
                        case SDL_Keycode.SDLK_v:
                            keyboard.pressed_keys[(int)key.v] = true;
                            break;
                        case SDL_Keycode.SDLK_w:
                            keyboard.pressed_keys[(int)key.w] = true;
                            break;
                        case SDL_Keycode.SDLK_x:
                            keyboard.pressed_keys[(int)key.x] = true;
                            break;
                        case SDL_Keycode.SDLK_y:
                            keyboard.pressed_keys[(int)key.y] = true;
                            break;
                        case SDL_Keycode.SDLK_z:
                            keyboard.pressed_keys[(int)key.z] = true;
                            break;
                        case SDL_Keycode.SDLK_1:
                            keyboard.pressed_keys[(int)key.one] = true;
                            break;
                        case SDL_Keycode.SDLK_2:
                            keyboard.pressed_keys[(int)key.two] = true;
                            break;
                        case SDL_Keycode.SDLK_3:
                            keyboard.pressed_keys[(int)key.three] = true;
                            break;
                        case SDL_Keycode.SDLK_4:
                            keyboard.pressed_keys[(int)key.four] = true;
                            break;
                        case SDL_Keycode.SDLK_5:
                            keyboard.pressed_keys[(int)key.five] = true;
                            break;
                        case SDL_Keycode.SDLK_6:
                            keyboard.pressed_keys[(int)key.six] = true;
                            break;
                        case SDL_Keycode.SDLK_7:
                            keyboard.pressed_keys[(int)key.seven] = true;
                            break;
                        case SDL_Keycode.SDLK_8:
                            keyboard.pressed_keys[(int)key.eight] = true;
                            break;
                        case SDL_Keycode.SDLK_9:
                            keyboard.pressed_keys[(int)key.nine] = true;
                            break;
                        case SDL_Keycode.SDLK_0:
                            keyboard.pressed_keys[(int)key.zero] = true;
                            break;
                        case SDL_Keycode.SDLK_F1:
                            keyboard.pressed_keys[(int)key.f1] = true;
                            break;
                        case SDL_Keycode.SDLK_F2:
                            keyboard.pressed_keys[(int)key.f2] = true;
                            break;
                        case SDL_Keycode.SDLK_F3:
                            keyboard.pressed_keys[(int)key.f3] = true;
                            break;
                        case SDL_Keycode.SDLK_F4:
                            keyboard.pressed_keys[(int)key.f4] = true;
                            break;
                        case SDL_Keycode.SDLK_F5:
                            keyboard.pressed_keys[(int)key.f5] = true;
                            break;
                        case SDL_Keycode.SDLK_F6:
                            keyboard.pressed_keys[(int)key.f6] = true;
                            break;
                        case SDL_Keycode.SDLK_F7:
                            keyboard.pressed_keys[(int)key.f7] = true;
                            break;
                        case SDL_Keycode.SDLK_F8:
                            keyboard.pressed_keys[(int)key.f8] = true;
                            break;
                        case SDL_Keycode.SDLK_F9:
                            keyboard.pressed_keys[(int)key.f9] = true;
                            break;
                        case SDL_Keycode.SDLK_F10:
                            keyboard.pressed_keys[(int)key.f10] = true;
                            break;
                        case SDL_Keycode.SDLK_F11:
                            keyboard.pressed_keys[(int)key.f11] = true;
                            break;
                        case SDL_Keycode.SDLK_F12:
                            keyboard.pressed_keys[(int)key.f12] = true;
                            break;
                        case SDL_Keycode.SDLK_ESCAPE:
                            keyboard.pressed_keys[(int)key.escape] = true;
                            break;
                        case SDL_Keycode.SDLK_BACKQUOTE:
                            keyboard.pressed_keys[(int)key.backquote] = true;
                            break;
                        case SDL_Keycode.SDLK_MINUS:
                            keyboard.pressed_keys[(int)key.minus] = true;
                            break;
                        case SDL_Keycode.SDLK_EQUALS:
                            keyboard.pressed_keys[(int)key.equals] = true;
                            break;
                        case SDL_Keycode.SDLK_BACKSPACE:
                            keyboard.pressed_keys[(int)key.backspace] = true;
                            break;
                        case SDL_Keycode.SDLK_TAB:
                            keyboard.pressed_keys[(int)key.tab] = true;
                            break;
                        case SDL_Keycode.SDLK_LEFTBRACKET:
                            keyboard.pressed_keys[(int)key.leftbracket] = true;
                            break;
                        case SDL_Keycode.SDLK_RIGHTBRACKET:
                            keyboard.pressed_keys[(int)key.rightbracket] = true;
                            break;
                        case SDL_Keycode.SDLK_BACKSLASH:
                            keyboard.pressed_keys[(int)key.backslash] = true;
                            break;
                        case SDL_Keycode.SDLK_CAPSLOCK:
                            keyboard.pressed_keys[(int)key.capslock] = true;
                            break;
                        case SDL_Keycode.SDLK_SEMICOLON:
                            keyboard.pressed_keys[(int)key.semicolon] = true;
                            break;
                        case SDL_Keycode.SDLK_QUOTE:
                            keyboard.pressed_keys[(int)key.quote] = true;
                            break;
                        case SDL_Keycode.SDLK_RETURN:
                            keyboard.pressed_keys[(int)key.enter] = true;
                            break;
                        case SDL_Keycode.SDLK_LSHIFT:
                            keyboard.pressed_keys[(int)key.lshift] = true;
                            break;
                        case SDL_Keycode.SDLK_COMMA:
                            keyboard.pressed_keys[(int)key.comma] = true;
                            break;
                        case SDL_Keycode.SDLK_PERIOD:
                            keyboard.pressed_keys[(int)key.period] = true;
                            break;
                        case SDL_Keycode.SDLK_SLASH:
                            keyboard.pressed_keys[(int)key.slash] = true;
                            break;
                        case SDL_Keycode.SDLK_RSHIFT:
                            keyboard.pressed_keys[(int)key.rshift] = true;
                            break;
                        case SDL_Keycode.SDLK_LCTRL:
                            keyboard.pressed_keys[(int)key.lctrl] = true;
                            break;
                        case SDL_Keycode.SDLK_LALT:
                            keyboard.pressed_keys[(int)key.lalt] = true;
                            break;
                        case SDL_Keycode.SDLK_SPACE:
                            keyboard.pressed_keys[(int)key.space] = true;
                            break;
                        case SDL_Keycode.SDLK_RALT:
                            keyboard.pressed_keys[(int)key.ralt] = true;
                            break;
                        case SDL_Keycode.SDLK_APPLICATION:
                            keyboard.pressed_keys[(int)key.application] = true;
                            break;
                        case SDL_Keycode.SDLK_RCTRL:
                            keyboard.pressed_keys[(int)key.rctrl] = true;
                            break;
                        case SDL_Keycode.SDLK_UP:
                            keyboard.pressed_keys[(int)key.up] = true;
                            break;
                        case SDL_Keycode.SDLK_DOWN:
                            keyboard.pressed_keys[(int)key.down] = true;
                            break;
                        case SDL_Keycode.SDLK_LEFT:
                            keyboard.pressed_keys[(int)key.left] = true;
                            break;
                        case SDL_Keycode.SDLK_RIGHT:
                            keyboard.pressed_keys[(int)key.right] = true;
                            break;
                    }
                    break;
                case SDL_EventType.SDL_KEYUP:
                    switch(events.key.keysym.sym) {
                        case SDL_Keycode.SDLK_a:
                            keyboard.pressed_keys[(int)key.a] = false;
                            break;
                        case SDL_Keycode.SDLK_b:
                            keyboard.pressed_keys[(int)key.b] = false;
                            break;
                        case SDL_Keycode.SDLK_c:
                            keyboard.pressed_keys[(int)key.c] = false;
                            break;
                        case SDL_Keycode.SDLK_d:
                            keyboard.pressed_keys[(int)key.d] = false;
                            break;
                        case SDL_Keycode.SDLK_e:
                            keyboard.pressed_keys[(int)key.e] = false;
                            break;
                        case SDL_Keycode.SDLK_f:
                            keyboard.pressed_keys[(int)key.f] = false;
                            break;
                        case SDL_Keycode.SDLK_g:
                            keyboard.pressed_keys[(int)key.g] = false;
                            break;
                        case SDL_Keycode.SDLK_h:
                            keyboard.pressed_keys[(int)key.h] = false;
                            break;
                        case SDL_Keycode.SDLK_i:
                            keyboard.pressed_keys[(int)key.i] = false;
                            break;
                        case SDL_Keycode.SDLK_j:
                            keyboard.pressed_keys[(int)key.j] = false;
                            break;
                        case SDL_Keycode.SDLK_k:
                            keyboard.pressed_keys[(int)key.k] = false;
                            break;
                        case SDL_Keycode.SDLK_l:
                            keyboard.pressed_keys[(int)key.l] = false;
                            break;
                        case SDL_Keycode.SDLK_m:
                            keyboard.pressed_keys[(int)key.m] = false;
                            break;
                        case SDL_Keycode.SDLK_n:
                            keyboard.pressed_keys[(int)key.n] = false;
                            break;
                        case SDL_Keycode.SDLK_o:
                            keyboard.pressed_keys[(int)key.o] = false;
                            break;
                        case SDL_Keycode.SDLK_p:
                            keyboard.pressed_keys[(int)key.p] = false;
                            break;
                        case SDL_Keycode.SDLK_q:
                            keyboard.pressed_keys[(int)key.q] = false;
                            break;
                        case SDL_Keycode.SDLK_r:
                            keyboard.pressed_keys[(int)key.r] = false;
                            break;
                        case SDL_Keycode.SDLK_s:
                            keyboard.pressed_keys[(int)key.s] = false;
                            break;
                        case SDL_Keycode.SDLK_t:
                            keyboard.pressed_keys[(int)key.t] = false;
                            break;
                        case SDL_Keycode.SDLK_u:
                            keyboard.pressed_keys[(int)key.u] = false;
                            break;
                        case SDL_Keycode.SDLK_v:
                            keyboard.pressed_keys[(int)key.v] = false;
                            break;
                        case SDL_Keycode.SDLK_w:
                            keyboard.pressed_keys[(int)key.w] = false;
                            break;
                        case SDL_Keycode.SDLK_x:
                            keyboard.pressed_keys[(int)key.x] = false;
                            break;
                        case SDL_Keycode.SDLK_y:
                            keyboard.pressed_keys[(int)key.y] = false;
                            break;
                        case SDL_Keycode.SDLK_z:
                            keyboard.pressed_keys[(int)key.z] = false;
                            break;
                        case SDL_Keycode.SDLK_1:
                            keyboard.pressed_keys[(int)key.one] = false;
                            break;
                        case SDL_Keycode.SDLK_2:
                            keyboard.pressed_keys[(int)key.two] = false;
                            break;
                        case SDL_Keycode.SDLK_3:
                            keyboard.pressed_keys[(int)key.three] = false;
                            break;
                        case SDL_Keycode.SDLK_4:
                            keyboard.pressed_keys[(int)key.four] = false;
                            break;
                        case SDL_Keycode.SDLK_5:
                            keyboard.pressed_keys[(int)key.five] = false;
                            break;
                        case SDL_Keycode.SDLK_6:
                            keyboard.pressed_keys[(int)key.six] = false;
                            break;
                        case SDL_Keycode.SDLK_7:
                            keyboard.pressed_keys[(int)key.seven] = false;
                            break;
                        case SDL_Keycode.SDLK_8:
                            keyboard.pressed_keys[(int)key.eight] = false;
                            break;
                        case SDL_Keycode.SDLK_9:
                            keyboard.pressed_keys[(int)key.nine] = false;
                            break;
                        case SDL_Keycode.SDLK_0:
                            keyboard.pressed_keys[(int)key.zero] = false;
                            break;
                        case SDL_Keycode.SDLK_F1:
                            keyboard.pressed_keys[(int)key.f1] = false;
                            break;
                        case SDL_Keycode.SDLK_F2:
                            keyboard.pressed_keys[(int)key.f2] = false;
                            break;
                        case SDL_Keycode.SDLK_F3:
                            keyboard.pressed_keys[(int)key.f3] = false;
                            break;
                        case SDL_Keycode.SDLK_F4:
                            keyboard.pressed_keys[(int)key.f4] = false;
                            break;
                        case SDL_Keycode.SDLK_F5:
                            keyboard.pressed_keys[(int)key.f5] = false;
                            break;
                        case SDL_Keycode.SDLK_F6:
                            keyboard.pressed_keys[(int)key.f6] = false;
                            break;
                        case SDL_Keycode.SDLK_F7:
                            keyboard.pressed_keys[(int)key.f7] = false;
                            break;
                        case SDL_Keycode.SDLK_F8:
                            keyboard.pressed_keys[(int)key.f8] = false;
                            break;
                        case SDL_Keycode.SDLK_F9:
                            keyboard.pressed_keys[(int)key.f9] = false;
                            break;
                        case SDL_Keycode.SDLK_F10:
                            keyboard.pressed_keys[(int)key.f10] = false;
                            break;
                        case SDL_Keycode.SDLK_F11:
                            keyboard.pressed_keys[(int)key.f11] = false;
                            break;
                        case SDL_Keycode.SDLK_F12:
                            keyboard.pressed_keys[(int)key.f12] = false;
                            break;
                        case SDL_Keycode.SDLK_ESCAPE:
                            keyboard.pressed_keys[(int)key.escape] = false;
                            break;
                        case SDL_Keycode.SDLK_BACKQUOTE:
                            keyboard.pressed_keys[(int)key.backquote] = false;
                            break;
                        case SDL_Keycode.SDLK_MINUS:
                            keyboard.pressed_keys[(int)key.minus] = false;
                            break;
                        case SDL_Keycode.SDLK_EQUALS:
                            keyboard.pressed_keys[(int)key.equals] = false;
                            break;
                        case SDL_Keycode.SDLK_BACKSPACE:
                            keyboard.pressed_keys[(int)key.backspace] = false;
                            break;
                        case SDL_Keycode.SDLK_TAB:
                            keyboard.pressed_keys[(int)key.tab] = false;
                            break;
                        case SDL_Keycode.SDLK_LEFTBRACKET:
                            keyboard.pressed_keys[(int)key.leftbracket] = false;
                            break;
                        case SDL_Keycode.SDLK_RIGHTBRACKET:
                            keyboard.pressed_keys[(int)key.rightbracket] = false;
                            break;
                        case SDL_Keycode.SDLK_BACKSLASH:
                            keyboard.pressed_keys[(int)key.backslash] = false;
                            break;
                        case SDL_Keycode.SDLK_CAPSLOCK:
                            keyboard.pressed_keys[(int)key.capslock] = false;
                            break;
                        case SDL_Keycode.SDLK_SEMICOLON:
                            keyboard.pressed_keys[(int)key.semicolon] = false;
                            break;
                        case SDL_Keycode.SDLK_QUOTE:
                            keyboard.pressed_keys[(int)key.quote] = false;
                            break;
                        case SDL_Keycode.SDLK_RETURN:
                            keyboard.pressed_keys[(int)key.enter] = false;
                            break;
                        case SDL_Keycode.SDLK_LSHIFT:
                            keyboard.pressed_keys[(int)key.lshift] = false;
                            break;
                        case SDL_Keycode.SDLK_COMMA:
                            keyboard.pressed_keys[(int)key.comma] = false;
                            break;
                        case SDL_Keycode.SDLK_PERIOD:
                            keyboard.pressed_keys[(int)key.period] = false;
                            break;
                        case SDL_Keycode.SDLK_SLASH:
                            keyboard.pressed_keys[(int)key.slash] = false;
                            break;
                        case SDL_Keycode.SDLK_RSHIFT:
                            keyboard.pressed_keys[(int)key.rshift] = false;
                            break;
                        case SDL_Keycode.SDLK_LCTRL:
                            keyboard.pressed_keys[(int)key.lctrl] = false;
                            break;
                        case SDL_Keycode.SDLK_LALT:
                            keyboard.pressed_keys[(int)key.lalt] = false;
                            break;
                        case SDL_Keycode.SDLK_SPACE:
                            keyboard.pressed_keys[(int)key.space] = false;
                            break;
                        case SDL_Keycode.SDLK_RALT:
                            keyboard.pressed_keys[(int)key.ralt] = false;
                            break;
                        case SDL_Keycode.SDLK_APPLICATION:
                            keyboard.pressed_keys[(int)key.application] = false;
                            break;
                        case SDL_Keycode.SDLK_RCTRL:
                            keyboard.pressed_keys[(int)key.rctrl] = false;
                            break;
                        case SDL_Keycode.SDLK_UP:
                            keyboard.pressed_keys[(int)key.up] = false;
                            break;
                        case SDL_Keycode.SDLK_DOWN:
                            keyboard.pressed_keys[(int)key.down] = false;
                            break;
                        case SDL_Keycode.SDLK_LEFT:
                            keyboard.pressed_keys[(int)key.left] = false;
                            break;
                        case SDL_Keycode.SDLK_RIGHT:
                            keyboard.pressed_keys[(int)key.right] = false;
                            break;
                    }
                    break;
                #endregion
                }
            }
        }
    }
}