using static SDL2.SDL;
using Fjord.Modules.Input;

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
                    double game_res = game.resolution.x;
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
                            input.pressed_keys[input.key_a] = true;
                            break;
                        case SDL_Keycode.SDLK_b:
                            input.pressed_keys[input.key_b] = true;
                            break;
                        case SDL_Keycode.SDLK_c:
                            input.pressed_keys[input.key_c] = true;
                            break;
                        case SDL_Keycode.SDLK_d:
                            input.pressed_keys[input.key_d] = true;
                            break;
                        case SDL_Keycode.SDLK_e:
                            input.pressed_keys[input.key_e] = true;
                            break;
                        case SDL_Keycode.SDLK_f:
                            input.pressed_keys[input.key_f] = true;
                            break;
                        case SDL_Keycode.SDLK_g:
                            input.pressed_keys[input.key_g] = true;
                            break;
                        case SDL_Keycode.SDLK_h:
                            input.pressed_keys[input.key_h] = true;
                            break;
                        case SDL_Keycode.SDLK_i:
                            input.pressed_keys[input.key_i] = true;
                            break;
                        case SDL_Keycode.SDLK_j:
                            input.pressed_keys[input.key_j] = true;
                            break;
                        case SDL_Keycode.SDLK_k:
                            input.pressed_keys[input.key_k] = true;
                            break;
                        case SDL_Keycode.SDLK_l:
                            input.pressed_keys[input.key_l] = true;
                            break;
                        case SDL_Keycode.SDLK_m:
                            input.pressed_keys[input.key_m] = true;
                            break;
                        case SDL_Keycode.SDLK_n:
                            input.pressed_keys[input.key_n] = true;
                            break;
                        case SDL_Keycode.SDLK_o:
                            input.pressed_keys[input.key_o] = true;
                            break;
                        case SDL_Keycode.SDLK_p:
                            input.pressed_keys[input.key_p] = true;
                            break;
                        case SDL_Keycode.SDLK_q:
                            input.pressed_keys[input.key_q] = true;
                            break;
                        case SDL_Keycode.SDLK_r:
                            input.pressed_keys[input.key_r] = true;
                            break;
                        case SDL_Keycode.SDLK_s:
                            input.pressed_keys[input.key_s] = true;
                            break;
                        case SDL_Keycode.SDLK_t:
                            input.pressed_keys[input.key_t] = true;
                            break;
                        case SDL_Keycode.SDLK_u:
                            input.pressed_keys[input.key_u] = true;
                            break;
                        case SDL_Keycode.SDLK_v:
                            input.pressed_keys[input.key_v] = true;
                            break;
                        case SDL_Keycode.SDLK_w:
                            input.pressed_keys[input.key_w] = true;
                            break;
                        case SDL_Keycode.SDLK_x:
                            input.pressed_keys[input.key_x] = true;
                            break;
                        case SDL_Keycode.SDLK_y:
                            input.pressed_keys[input.key_y] = true;
                            break;
                        case SDL_Keycode.SDLK_z:
                            input.pressed_keys[input.key_z] = true;
                            break;
                        case SDL_Keycode.SDLK_1:
                            input.pressed_keys[input.key_1] = true;
                            break;
                        case SDL_Keycode.SDLK_2:
                            input.pressed_keys[input.key_2] = true;
                            break;
                        case SDL_Keycode.SDLK_3:
                            input.pressed_keys[input.key_3] = true;
                            break;
                        case SDL_Keycode.SDLK_4:
                            input.pressed_keys[input.key_4] = true;
                            break;
                        case SDL_Keycode.SDLK_5:
                            input.pressed_keys[input.key_5] = true;
                            break;
                        case SDL_Keycode.SDLK_6:
                            input.pressed_keys[input.key_6] = true;
                            break;
                        case SDL_Keycode.SDLK_7:
                            input.pressed_keys[input.key_7] = true;
                            break;
                        case SDL_Keycode.SDLK_8:
                            input.pressed_keys[input.key_8] = true;
                            break;
                        case SDL_Keycode.SDLK_9:
                            input.pressed_keys[input.key_9] = true;
                            break;
                        case SDL_Keycode.SDLK_0:
                            input.pressed_keys[input.key_0] = true;
                            break;
                        case SDL_Keycode.SDLK_F1:
                            input.pressed_keys[input.key_f1] = true;
                            break;
                        case SDL_Keycode.SDLK_F2:
                            input.pressed_keys[input.key_f2] = true;
                            break;
                        case SDL_Keycode.SDLK_F3:
                            input.pressed_keys[input.key_f3] = true;
                            break;
                        case SDL_Keycode.SDLK_F4:
                            input.pressed_keys[input.key_f4] = true;
                            break;
                        case SDL_Keycode.SDLK_F5:
                            input.pressed_keys[input.key_f5] = true;
                            break;
                        case SDL_Keycode.SDLK_F6:
                            input.pressed_keys[input.key_f6] = true;
                            break;
                        case SDL_Keycode.SDLK_F7:
                            input.pressed_keys[input.key_f7] = true;
                            break;
                        case SDL_Keycode.SDLK_F8:
                            input.pressed_keys[input.key_f8] = true;
                            break;
                        case SDL_Keycode.SDLK_F9:
                            input.pressed_keys[input.key_f9] = true;
                            break;
                        case SDL_Keycode.SDLK_F10:
                            input.pressed_keys[input.key_f10] = true;
                            break;
                        case SDL_Keycode.SDLK_F11:
                            input.pressed_keys[input.key_f11] = true;
                            break;
                        case SDL_Keycode.SDLK_F12:
                            input.pressed_keys[input.key_f12] = true;
                            break;
                        case SDL_Keycode.SDLK_ESCAPE:
                            input.pressed_keys[input.key_escape] = true;
                            break;
                        case SDL_Keycode.SDLK_BACKQUOTE:
                            input.pressed_keys[input.key_backquote] = true;
                            break;
                        case SDL_Keycode.SDLK_MINUS:
                            input.pressed_keys[input.key_minus] = true;
                            break;
                        case SDL_Keycode.SDLK_EQUALS:
                            input.pressed_keys[input.key_equals] = true;
                            break;
                        case SDL_Keycode.SDLK_BACKSPACE:
                            input.pressed_keys[input.key_backspace] = true;
                            break;
                        case SDL_Keycode.SDLK_TAB:
                            input.pressed_keys[input.key_tab] = true;
                            break;
                        case SDL_Keycode.SDLK_LEFTBRACKET:
                            input.pressed_keys[input.key_leftbracket] = true;
                            break;
                        case SDL_Keycode.SDLK_RIGHTBRACKET:
                            input.pressed_keys[input.key_rightbracket] = true;
                            break;
                        case SDL_Keycode.SDLK_BACKSLASH:
                            input.pressed_keys[input.key_backslash] = true;
                            break;
                        case SDL_Keycode.SDLK_CAPSLOCK:
                            input.pressed_keys[input.key_capslock] = true;
                            break;
                        case SDL_Keycode.SDLK_SEMICOLON:
                            input.pressed_keys[input.key_semicolon] = true;
                            break;
                        case SDL_Keycode.SDLK_QUOTE:
                            input.pressed_keys[input.key_quote] = true;
                            break;
                        case SDL_Keycode.SDLK_RETURN:
                            input.pressed_keys[input.key_return] = true;
                            break;
                        case SDL_Keycode.SDLK_LSHIFT:
                            input.pressed_keys[input.key_lshift] = true;
                            break;
                        case SDL_Keycode.SDLK_COMMA:
                            input.pressed_keys[input.key_comma] = true;
                            break;
                        case SDL_Keycode.SDLK_PERIOD:
                            input.pressed_keys[input.key_period] = true;
                            break;
                        case SDL_Keycode.SDLK_SLASH:
                            input.pressed_keys[input.key_slash] = true;
                            break;
                        case SDL_Keycode.SDLK_RSHIFT:
                            input.pressed_keys[input.key_rshift] = true;
                            break;
                        case SDL_Keycode.SDLK_LCTRL:
                            input.pressed_keys[input.key_lctrl] = true;
                            break;
                        case SDL_Keycode.SDLK_LALT:
                            input.pressed_keys[input.key_lalt] = true;
                            break;
                        case SDL_Keycode.SDLK_SPACE:
                            input.pressed_keys[input.key_space] = true;
                            break;
                        case SDL_Keycode.SDLK_RALT:
                            input.pressed_keys[input.key_ralt] = true;
                            break;
                        case SDL_Keycode.SDLK_APPLICATION:
                            input.pressed_keys[input.key_application] = true;
                            break;
                        case SDL_Keycode.SDLK_RCTRL:
                            input.pressed_keys[input.key_rctrl] = true;
                            break;
                        case SDL_Keycode.SDLK_UP:
                            input.pressed_keys[input.key_up] = true;
                            break;
                        case SDL_Keycode.SDLK_DOWN:
                            input.pressed_keys[input.key_down] = true;
                            break;
                        case SDL_Keycode.SDLK_LEFT:
                            input.pressed_keys[input.key_left] = true;
                            break;
                        case SDL_Keycode.SDLK_RIGHT:
                            input.pressed_keys[input.key_right] = true;
                            break;
                    }
                    break;
                case SDL_EventType.SDL_KEYUP:
                    switch(events.key.keysym.sym) {
                        case SDL_Keycode.SDLK_a:
                            input.pressed_keys[input.key_a] = false;
                            break;
                        case SDL_Keycode.SDLK_b:
                            input.pressed_keys[input.key_b] = false;
                            break;
                        case SDL_Keycode.SDLK_c:
                            input.pressed_keys[input.key_c] = false;
                            break;
                        case SDL_Keycode.SDLK_d:
                            input.pressed_keys[input.key_d] = false;
                            break;
                        case SDL_Keycode.SDLK_e:
                            input.pressed_keys[input.key_e] = false;
                            break;
                        case SDL_Keycode.SDLK_f:
                            input.pressed_keys[input.key_f] = false;
                            break;
                        case SDL_Keycode.SDLK_g:
                            input.pressed_keys[input.key_g] = false;
                            break;
                        case SDL_Keycode.SDLK_h:
                            input.pressed_keys[input.key_h] = false;
                            break;
                        case SDL_Keycode.SDLK_i:
                            input.pressed_keys[input.key_i] = false;
                            break;
                        case SDL_Keycode.SDLK_j:
                            input.pressed_keys[input.key_j] = false;
                            break;
                        case SDL_Keycode.SDLK_k:
                            input.pressed_keys[input.key_k] = false;
                            break;
                        case SDL_Keycode.SDLK_l:
                            input.pressed_keys[input.key_l] = false;
                            break;
                        case SDL_Keycode.SDLK_m:
                            input.pressed_keys[input.key_m] = false;
                            break;
                        case SDL_Keycode.SDLK_n:
                            input.pressed_keys[input.key_n] = false;
                            break;
                        case SDL_Keycode.SDLK_o:
                            input.pressed_keys[input.key_o] = false;
                            break;
                        case SDL_Keycode.SDLK_p:
                            input.pressed_keys[input.key_p] = false;
                            break;
                        case SDL_Keycode.SDLK_q:
                            input.pressed_keys[input.key_q] = false;
                            break;
                        case SDL_Keycode.SDLK_r:
                            input.pressed_keys[input.key_r] = false;
                            break;
                        case SDL_Keycode.SDLK_s:
                            input.pressed_keys[input.key_s] = false;
                            break;
                        case SDL_Keycode.SDLK_t:
                            input.pressed_keys[input.key_t] = false;
                            break;
                        case SDL_Keycode.SDLK_u:
                            input.pressed_keys[input.key_u] = false;
                            break;
                        case SDL_Keycode.SDLK_v:
                            input.pressed_keys[input.key_v] = false;
                            break;
                        case SDL_Keycode.SDLK_w:
                            input.pressed_keys[input.key_w] = false;
                            break;
                        case SDL_Keycode.SDLK_x:
                            input.pressed_keys[input.key_x] = false;
                            break;
                        case SDL_Keycode.SDLK_y:
                            input.pressed_keys[input.key_y] = false;
                            break;
                        case SDL_Keycode.SDLK_z:
                            input.pressed_keys[input.key_z] = false;
                            break;
                        case SDL_Keycode.SDLK_1:
                            input.pressed_keys[input.key_1] = false;
                            break;
                        case SDL_Keycode.SDLK_2:
                            input.pressed_keys[input.key_2] = false;
                            break;
                        case SDL_Keycode.SDLK_3:
                            input.pressed_keys[input.key_3] = false;
                            break;
                        case SDL_Keycode.SDLK_4:
                            input.pressed_keys[input.key_4] = false;
                            break;
                        case SDL_Keycode.SDLK_5:
                            input.pressed_keys[input.key_5] = false;
                            break;
                        case SDL_Keycode.SDLK_6:
                            input.pressed_keys[input.key_6] = false;
                            break;
                        case SDL_Keycode.SDLK_7:
                            input.pressed_keys[input.key_7] = false;
                            break;
                        case SDL_Keycode.SDLK_8:
                            input.pressed_keys[input.key_8] = false;
                            break;
                        case SDL_Keycode.SDLK_9:
                            input.pressed_keys[input.key_9] = false;
                            break;
                        case SDL_Keycode.SDLK_0:
                            input.pressed_keys[input.key_0] = false;
                            break;
                        case SDL_Keycode.SDLK_F1:
                            input.pressed_keys[input.key_f1] = false;
                            break;
                        case SDL_Keycode.SDLK_F2:
                            input.pressed_keys[input.key_f2] = false;
                            break;
                        case SDL_Keycode.SDLK_F3:
                            input.pressed_keys[input.key_f3] = false;
                            break;
                        case SDL_Keycode.SDLK_F4:
                            input.pressed_keys[input.key_f4] = false;
                            break;
                        case SDL_Keycode.SDLK_F5:
                            input.pressed_keys[input.key_f5] = false;
                            break;
                        case SDL_Keycode.SDLK_F6:
                            input.pressed_keys[input.key_f6] = false;
                            break;
                        case SDL_Keycode.SDLK_F7:
                            input.pressed_keys[input.key_f7] = false;
                            break;
                        case SDL_Keycode.SDLK_F8:
                            input.pressed_keys[input.key_f8] = false;
                            break;
                        case SDL_Keycode.SDLK_F9:
                            input.pressed_keys[input.key_f9] = false;
                            break;
                        case SDL_Keycode.SDLK_F10:
                            input.pressed_keys[input.key_f10] = false;
                            break;
                        case SDL_Keycode.SDLK_F11:
                            input.pressed_keys[input.key_f11] = false;
                            break;
                        case SDL_Keycode.SDLK_F12:
                            input.pressed_keys[input.key_f12] = false;
                            break;
                        case SDL_Keycode.SDLK_ESCAPE:
                            input.pressed_keys[input.key_escape] = false;
                            break;
                        case SDL_Keycode.SDLK_BACKQUOTE:
                            input.pressed_keys[input.key_backquote] = false;
                            break;
                        case SDL_Keycode.SDLK_MINUS:
                            input.pressed_keys[input.key_minus] = false;
                            break;
                        case SDL_Keycode.SDLK_EQUALS:
                            input.pressed_keys[input.key_equals] = false;
                            break;
                        case SDL_Keycode.SDLK_BACKSPACE:
                            input.pressed_keys[input.key_backspace] = false;
                            break;
                        case SDL_Keycode.SDLK_TAB:
                            input.pressed_keys[input.key_tab] = false;
                            break;
                        case SDL_Keycode.SDLK_LEFTBRACKET:
                            input.pressed_keys[input.key_leftbracket] = false;
                            break;
                        case SDL_Keycode.SDLK_RIGHTBRACKET:
                            input.pressed_keys[input.key_rightbracket] = false;
                            break;
                        case SDL_Keycode.SDLK_BACKSLASH:
                            input.pressed_keys[input.key_backslash] = false;
                            break;
                        case SDL_Keycode.SDLK_CAPSLOCK:
                            input.pressed_keys[input.key_capslock] = false;
                            break;
                        case SDL_Keycode.SDLK_SEMICOLON:
                            input.pressed_keys[input.key_semicolon] = false;
                            break;
                        case SDL_Keycode.SDLK_QUOTE:
                            input.pressed_keys[input.key_quote] = false;
                            break;
                        case SDL_Keycode.SDLK_RETURN:
                            input.pressed_keys[input.key_return] = false;
                            break;
                        case SDL_Keycode.SDLK_LSHIFT:
                            input.pressed_keys[input.key_lshift] = false;
                            break;
                        case SDL_Keycode.SDLK_COMMA:
                            input.pressed_keys[input.key_comma] = false;
                            break;
                        case SDL_Keycode.SDLK_PERIOD:
                            input.pressed_keys[input.key_period] = false;
                            break;
                        case SDL_Keycode.SDLK_SLASH:
                            input.pressed_keys[input.key_slash] = false;
                            break;
                        case SDL_Keycode.SDLK_RSHIFT:
                            input.pressed_keys[input.key_rshift] = false;
                            break;
                        case SDL_Keycode.SDLK_LCTRL:
                            input.pressed_keys[input.key_lctrl] = false;
                            break;
                        case SDL_Keycode.SDLK_LALT:
                            input.pressed_keys[input.key_lalt] = false;
                            break;
                        case SDL_Keycode.SDLK_SPACE:
                            input.pressed_keys[input.key_space] = false;
                            break;
                        case SDL_Keycode.SDLK_RALT:
                            input.pressed_keys[input.key_ralt] = false;
                            break;
                        case SDL_Keycode.SDLK_APPLICATION:
                            input.pressed_keys[input.key_application] = false;
                            break;
                        case SDL_Keycode.SDLK_RCTRL:
                            input.pressed_keys[input.key_rctrl] = false;
                            break;
                        case SDL_Keycode.SDLK_UP:
                            input.pressed_keys[input.key_up] = false;
                            break;
                        case SDL_Keycode.SDLK_DOWN:
                            input.pressed_keys[input.key_down] = false;
                            break;
                        case SDL_Keycode.SDLK_LEFT:
                            input.pressed_keys[input.key_left] = false;
                            break;
                        case SDL_Keycode.SDLK_RIGHT:
                            input.pressed_keys[input.key_right] = false;
                            break;
                    }
                    break;
                #endregion
                }
            }
        }
    }
}