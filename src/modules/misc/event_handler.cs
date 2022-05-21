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
                            keyboard.pressed_keys[keyboard.key_a] = true;
                            break;
                        case SDL_Keycode.SDLK_b:
                            keyboard.pressed_keys[keyboard.key_b] = true;
                            break;
                        case SDL_Keycode.SDLK_c:
                            keyboard.pressed_keys[keyboard.key_c] = true;
                            break;
                        case SDL_Keycode.SDLK_d:
                            keyboard.pressed_keys[keyboard.key_d] = true;
                            break;
                        case SDL_Keycode.SDLK_e:
                            keyboard.pressed_keys[keyboard.key_e] = true;
                            break;
                        case SDL_Keycode.SDLK_f:
                            keyboard.pressed_keys[keyboard.key_f] = true;
                            break;
                        case SDL_Keycode.SDLK_g:
                            keyboard.pressed_keys[keyboard.key_g] = true;
                            break;
                        case SDL_Keycode.SDLK_h:
                            keyboard.pressed_keys[keyboard.key_h] = true;
                            break;
                        case SDL_Keycode.SDLK_i:
                            keyboard.pressed_keys[keyboard.key_i] = true;
                            break;
                        case SDL_Keycode.SDLK_j:
                            keyboard.pressed_keys[keyboard.key_j] = true;
                            break;
                        case SDL_Keycode.SDLK_k:
                            keyboard.pressed_keys[keyboard.key_k] = true;
                            break;
                        case SDL_Keycode.SDLK_l:
                            keyboard.pressed_keys[keyboard.key_l] = true;
                            break;
                        case SDL_Keycode.SDLK_m:
                            keyboard.pressed_keys[keyboard.key_m] = true;
                            break;
                        case SDL_Keycode.SDLK_n:
                            keyboard.pressed_keys[keyboard.key_n] = true;
                            break;
                        case SDL_Keycode.SDLK_o:
                            keyboard.pressed_keys[keyboard.key_o] = true;
                            break;
                        case SDL_Keycode.SDLK_p:
                            keyboard.pressed_keys[keyboard.key_p] = true;
                            break;
                        case SDL_Keycode.SDLK_q:
                            keyboard.pressed_keys[keyboard.key_q] = true;
                            break;
                        case SDL_Keycode.SDLK_r:
                            keyboard.pressed_keys[keyboard.key_r] = true;
                            break;
                        case SDL_Keycode.SDLK_s:
                            keyboard.pressed_keys[keyboard.key_s] = true;
                            break;
                        case SDL_Keycode.SDLK_t:
                            keyboard.pressed_keys[keyboard.key_t] = true;
                            break;
                        case SDL_Keycode.SDLK_u:
                            keyboard.pressed_keys[keyboard.key_u] = true;
                            break;
                        case SDL_Keycode.SDLK_v:
                            keyboard.pressed_keys[keyboard.key_v] = true;
                            break;
                        case SDL_Keycode.SDLK_w:
                            keyboard.pressed_keys[keyboard.key_w] = true;
                            break;
                        case SDL_Keycode.SDLK_x:
                            keyboard.pressed_keys[keyboard.key_x] = true;
                            break;
                        case SDL_Keycode.SDLK_y:
                            keyboard.pressed_keys[keyboard.key_y] = true;
                            break;
                        case SDL_Keycode.SDLK_z:
                            keyboard.pressed_keys[keyboard.key_z] = true;
                            break;
                        case SDL_Keycode.SDLK_1:
                            keyboard.pressed_keys[keyboard.key_1] = true;
                            break;
                        case SDL_Keycode.SDLK_2:
                            keyboard.pressed_keys[keyboard.key_2] = true;
                            break;
                        case SDL_Keycode.SDLK_3:
                            keyboard.pressed_keys[keyboard.key_3] = true;
                            break;
                        case SDL_Keycode.SDLK_4:
                            keyboard.pressed_keys[keyboard.key_4] = true;
                            break;
                        case SDL_Keycode.SDLK_5:
                            keyboard.pressed_keys[keyboard.key_5] = true;
                            break;
                        case SDL_Keycode.SDLK_6:
                            keyboard.pressed_keys[keyboard.key_6] = true;
                            break;
                        case SDL_Keycode.SDLK_7:
                            keyboard.pressed_keys[keyboard.key_7] = true;
                            break;
                        case SDL_Keycode.SDLK_8:
                            keyboard.pressed_keys[keyboard.key_8] = true;
                            break;
                        case SDL_Keycode.SDLK_9:
                            keyboard.pressed_keys[keyboard.key_9] = true;
                            break;
                        case SDL_Keycode.SDLK_0:
                            keyboard.pressed_keys[keyboard.key_0] = true;
                            break;
                        case SDL_Keycode.SDLK_F1:
                            keyboard.pressed_keys[keyboard.key_f1] = true;
                            break;
                        case SDL_Keycode.SDLK_F2:
                            keyboard.pressed_keys[keyboard.key_f2] = true;
                            break;
                        case SDL_Keycode.SDLK_F3:
                            keyboard.pressed_keys[keyboard.key_f3] = true;
                            break;
                        case SDL_Keycode.SDLK_F4:
                            keyboard.pressed_keys[keyboard.key_f4] = true;
                            break;
                        case SDL_Keycode.SDLK_F5:
                            keyboard.pressed_keys[keyboard.key_f5] = true;
                            break;
                        case SDL_Keycode.SDLK_F6:
                            keyboard.pressed_keys[keyboard.key_f6] = true;
                            break;
                        case SDL_Keycode.SDLK_F7:
                            keyboard.pressed_keys[keyboard.key_f7] = true;
                            break;
                        case SDL_Keycode.SDLK_F8:
                            keyboard.pressed_keys[keyboard.key_f8] = true;
                            break;
                        case SDL_Keycode.SDLK_F9:
                            keyboard.pressed_keys[keyboard.key_f9] = true;
                            break;
                        case SDL_Keycode.SDLK_F10:
                            keyboard.pressed_keys[keyboard.key_f10] = true;
                            break;
                        case SDL_Keycode.SDLK_F11:
                            keyboard.pressed_keys[keyboard.key_f11] = true;
                            break;
                        case SDL_Keycode.SDLK_F12:
                            keyboard.pressed_keys[keyboard.key_f12] = true;
                            break;
                        case SDL_Keycode.SDLK_ESCAPE:
                            keyboard.pressed_keys[keyboard.key_escape] = true;
                            break;
                        case SDL_Keycode.SDLK_BACKQUOTE:
                            keyboard.pressed_keys[keyboard.key_backquote] = true;
                            break;
                        case SDL_Keycode.SDLK_MINUS:
                            keyboard.pressed_keys[keyboard.key_minus] = true;
                            break;
                        case SDL_Keycode.SDLK_EQUALS:
                            keyboard.pressed_keys[keyboard.key_equals] = true;
                            break;
                        case SDL_Keycode.SDLK_BACKSPACE:
                            keyboard.pressed_keys[keyboard.key_backspace] = true;
                            break;
                        case SDL_Keycode.SDLK_TAB:
                            keyboard.pressed_keys[keyboard.key_tab] = true;
                            break;
                        case SDL_Keycode.SDLK_LEFTBRACKET:
                            keyboard.pressed_keys[keyboard.key_leftbracket] = true;
                            break;
                        case SDL_Keycode.SDLK_RIGHTBRACKET:
                            keyboard.pressed_keys[keyboard.key_rightbracket] = true;
                            break;
                        case SDL_Keycode.SDLK_BACKSLASH:
                            keyboard.pressed_keys[keyboard.key_backslash] = true;
                            break;
                        case SDL_Keycode.SDLK_CAPSLOCK:
                            keyboard.pressed_keys[keyboard.key_capslock] = true;
                            break;
                        case SDL_Keycode.SDLK_SEMICOLON:
                            keyboard.pressed_keys[keyboard.key_semicolon] = true;
                            break;
                        case SDL_Keycode.SDLK_QUOTE:
                            keyboard.pressed_keys[keyboard.key_quote] = true;
                            break;
                        case SDL_Keycode.SDLK_RETURN:
                            keyboard.pressed_keys[keyboard.key_return] = true;
                            break;
                        case SDL_Keycode.SDLK_LSHIFT:
                            keyboard.pressed_keys[keyboard.key_lshift] = true;
                            break;
                        case SDL_Keycode.SDLK_COMMA:
                            keyboard.pressed_keys[keyboard.key_comma] = true;
                            break;
                        case SDL_Keycode.SDLK_PERIOD:
                            keyboard.pressed_keys[keyboard.key_period] = true;
                            break;
                        case SDL_Keycode.SDLK_SLASH:
                            keyboard.pressed_keys[keyboard.key_slash] = true;
                            break;
                        case SDL_Keycode.SDLK_RSHIFT:
                            keyboard.pressed_keys[keyboard.key_rshift] = true;
                            break;
                        case SDL_Keycode.SDLK_LCTRL:
                            keyboard.pressed_keys[keyboard.key_lctrl] = true;
                            break;
                        case SDL_Keycode.SDLK_LALT:
                            keyboard.pressed_keys[keyboard.key_lalt] = true;
                            break;
                        case SDL_Keycode.SDLK_SPACE:
                            keyboard.pressed_keys[keyboard.key_space] = true;
                            break;
                        case SDL_Keycode.SDLK_RALT:
                            keyboard.pressed_keys[keyboard.key_ralt] = true;
                            break;
                        case SDL_Keycode.SDLK_APPLICATION:
                            keyboard.pressed_keys[keyboard.key_application] = true;
                            break;
                        case SDL_Keycode.SDLK_RCTRL:
                            keyboard.pressed_keys[keyboard.key_rctrl] = true;
                            break;
                        case SDL_Keycode.SDLK_UP:
                            keyboard.pressed_keys[keyboard.key_up] = true;
                            break;
                        case SDL_Keycode.SDLK_DOWN:
                            keyboard.pressed_keys[keyboard.key_down] = true;
                            break;
                        case SDL_Keycode.SDLK_LEFT:
                            keyboard.pressed_keys[keyboard.key_left] = true;
                            break;
                        case SDL_Keycode.SDLK_RIGHT:
                            keyboard.pressed_keys[keyboard.key_right] = true;
                            break;
                    }
                    break;
                case SDL_EventType.SDL_KEYUP:
                    switch(events.key.keysym.sym) {
                        case SDL_Keycode.SDLK_a:
                            keyboard.pressed_keys[keyboard.key_a] = false;
                            break;
                        case SDL_Keycode.SDLK_b:
                            keyboard.pressed_keys[keyboard.key_b] = false;
                            break;
                        case SDL_Keycode.SDLK_c:
                            keyboard.pressed_keys[keyboard.key_c] = false;
                            break;
                        case SDL_Keycode.SDLK_d:
                            keyboard.pressed_keys[keyboard.key_d] = false;
                            break;
                        case SDL_Keycode.SDLK_e:
                            keyboard.pressed_keys[keyboard.key_e] = false;
                            break;
                        case SDL_Keycode.SDLK_f:
                            keyboard.pressed_keys[keyboard.key_f] = false;
                            break;
                        case SDL_Keycode.SDLK_g:
                            keyboard.pressed_keys[keyboard.key_g] = false;
                            break;
                        case SDL_Keycode.SDLK_h:
                            keyboard.pressed_keys[keyboard.key_h] = false;
                            break;
                        case SDL_Keycode.SDLK_i:
                            keyboard.pressed_keys[keyboard.key_i] = false;
                            break;
                        case SDL_Keycode.SDLK_j:
                            keyboard.pressed_keys[keyboard.key_j] = false;
                            break;
                        case SDL_Keycode.SDLK_k:
                            keyboard.pressed_keys[keyboard.key_k] = false;
                            break;
                        case SDL_Keycode.SDLK_l:
                            keyboard.pressed_keys[keyboard.key_l] = false;
                            break;
                        case SDL_Keycode.SDLK_m:
                            keyboard.pressed_keys[keyboard.key_m] = false;
                            break;
                        case SDL_Keycode.SDLK_n:
                            keyboard.pressed_keys[keyboard.key_n] = false;
                            break;
                        case SDL_Keycode.SDLK_o:
                            keyboard.pressed_keys[keyboard.key_o] = false;
                            break;
                        case SDL_Keycode.SDLK_p:
                            keyboard.pressed_keys[keyboard.key_p] = false;
                            break;
                        case SDL_Keycode.SDLK_q:
                            keyboard.pressed_keys[keyboard.key_q] = false;
                            break;
                        case SDL_Keycode.SDLK_r:
                            keyboard.pressed_keys[keyboard.key_r] = false;
                            break;
                        case SDL_Keycode.SDLK_s:
                            keyboard.pressed_keys[keyboard.key_s] = false;
                            break;
                        case SDL_Keycode.SDLK_t:
                            keyboard.pressed_keys[keyboard.key_t] = false;
                            break;
                        case SDL_Keycode.SDLK_u:
                            keyboard.pressed_keys[keyboard.key_u] = false;
                            break;
                        case SDL_Keycode.SDLK_v:
                            keyboard.pressed_keys[keyboard.key_v] = false;
                            break;
                        case SDL_Keycode.SDLK_w:
                            keyboard.pressed_keys[keyboard.key_w] = false;
                            break;
                        case SDL_Keycode.SDLK_x:
                            keyboard.pressed_keys[keyboard.key_x] = false;
                            break;
                        case SDL_Keycode.SDLK_y:
                            keyboard.pressed_keys[keyboard.key_y] = false;
                            break;
                        case SDL_Keycode.SDLK_z:
                            keyboard.pressed_keys[keyboard.key_z] = false;
                            break;
                        case SDL_Keycode.SDLK_1:
                            keyboard.pressed_keys[keyboard.key_1] = false;
                            break;
                        case SDL_Keycode.SDLK_2:
                            keyboard.pressed_keys[keyboard.key_2] = false;
                            break;
                        case SDL_Keycode.SDLK_3:
                            keyboard.pressed_keys[keyboard.key_3] = false;
                            break;
                        case SDL_Keycode.SDLK_4:
                            keyboard.pressed_keys[keyboard.key_4] = false;
                            break;
                        case SDL_Keycode.SDLK_5:
                            keyboard.pressed_keys[keyboard.key_5] = false;
                            break;
                        case SDL_Keycode.SDLK_6:
                            keyboard.pressed_keys[keyboard.key_6] = false;
                            break;
                        case SDL_Keycode.SDLK_7:
                            keyboard.pressed_keys[keyboard.key_7] = false;
                            break;
                        case SDL_Keycode.SDLK_8:
                            keyboard.pressed_keys[keyboard.key_8] = false;
                            break;
                        case SDL_Keycode.SDLK_9:
                            keyboard.pressed_keys[keyboard.key_9] = false;
                            break;
                        case SDL_Keycode.SDLK_0:
                            keyboard.pressed_keys[keyboard.key_0] = false;
                            break;
                        case SDL_Keycode.SDLK_F1:
                            keyboard.pressed_keys[keyboard.key_f1] = false;
                            break;
                        case SDL_Keycode.SDLK_F2:
                            keyboard.pressed_keys[keyboard.key_f2] = false;
                            break;
                        case SDL_Keycode.SDLK_F3:
                            keyboard.pressed_keys[keyboard.key_f3] = false;
                            break;
                        case SDL_Keycode.SDLK_F4:
                            keyboard.pressed_keys[keyboard.key_f4] = false;
                            break;
                        case SDL_Keycode.SDLK_F5:
                            keyboard.pressed_keys[keyboard.key_f5] = false;
                            break;
                        case SDL_Keycode.SDLK_F6:
                            keyboard.pressed_keys[keyboard.key_f6] = false;
                            break;
                        case SDL_Keycode.SDLK_F7:
                            keyboard.pressed_keys[keyboard.key_f7] = false;
                            break;
                        case SDL_Keycode.SDLK_F8:
                            keyboard.pressed_keys[keyboard.key_f8] = false;
                            break;
                        case SDL_Keycode.SDLK_F9:
                            keyboard.pressed_keys[keyboard.key_f9] = false;
                            break;
                        case SDL_Keycode.SDLK_F10:
                            keyboard.pressed_keys[keyboard.key_f10] = false;
                            break;
                        case SDL_Keycode.SDLK_F11:
                            keyboard.pressed_keys[keyboard.key_f11] = false;
                            break;
                        case SDL_Keycode.SDLK_F12:
                            keyboard.pressed_keys[keyboard.key_f12] = false;
                            break;
                        case SDL_Keycode.SDLK_ESCAPE:
                            keyboard.pressed_keys[keyboard.key_escape] = false;
                            break;
                        case SDL_Keycode.SDLK_BACKQUOTE:
                            keyboard.pressed_keys[keyboard.key_backquote] = false;
                            break;
                        case SDL_Keycode.SDLK_MINUS:
                            keyboard.pressed_keys[keyboard.key_minus] = false;
                            break;
                        case SDL_Keycode.SDLK_EQUALS:
                            keyboard.pressed_keys[keyboard.key_equals] = false;
                            break;
                        case SDL_Keycode.SDLK_BACKSPACE:
                            keyboard.pressed_keys[keyboard.key_backspace] = false;
                            break;
                        case SDL_Keycode.SDLK_TAB:
                            keyboard.pressed_keys[keyboard.key_tab] = false;
                            break;
                        case SDL_Keycode.SDLK_LEFTBRACKET:
                            keyboard.pressed_keys[keyboard.key_leftbracket] = false;
                            break;
                        case SDL_Keycode.SDLK_RIGHTBRACKET:
                            keyboard.pressed_keys[keyboard.key_rightbracket] = false;
                            break;
                        case SDL_Keycode.SDLK_BACKSLASH:
                            keyboard.pressed_keys[keyboard.key_backslash] = false;
                            break;
                        case SDL_Keycode.SDLK_CAPSLOCK:
                            keyboard.pressed_keys[keyboard.key_capslock] = false;
                            break;
                        case SDL_Keycode.SDLK_SEMICOLON:
                            keyboard.pressed_keys[keyboard.key_semicolon] = false;
                            break;
                        case SDL_Keycode.SDLK_QUOTE:
                            keyboard.pressed_keys[keyboard.key_quote] = false;
                            break;
                        case SDL_Keycode.SDLK_RETURN:
                            keyboard.pressed_keys[keyboard.key_return] = false;
                            break;
                        case SDL_Keycode.SDLK_LSHIFT:
                            keyboard.pressed_keys[keyboard.key_lshift] = false;
                            break;
                        case SDL_Keycode.SDLK_COMMA:
                            keyboard.pressed_keys[keyboard.key_comma] = false;
                            break;
                        case SDL_Keycode.SDLK_PERIOD:
                            keyboard.pressed_keys[keyboard.key_period] = false;
                            break;
                        case SDL_Keycode.SDLK_SLASH:
                            keyboard.pressed_keys[keyboard.key_slash] = false;
                            break;
                        case SDL_Keycode.SDLK_RSHIFT:
                            keyboard.pressed_keys[keyboard.key_rshift] = false;
                            break;
                        case SDL_Keycode.SDLK_LCTRL:
                            keyboard.pressed_keys[keyboard.key_lctrl] = false;
                            break;
                        case SDL_Keycode.SDLK_LALT:
                            keyboard.pressed_keys[keyboard.key_lalt] = false;
                            break;
                        case SDL_Keycode.SDLK_SPACE:
                            keyboard.pressed_keys[keyboard.key_space] = false;
                            break;
                        case SDL_Keycode.SDLK_RALT:
                            keyboard.pressed_keys[keyboard.key_ralt] = false;
                            break;
                        case SDL_Keycode.SDLK_APPLICATION:
                            keyboard.pressed_keys[keyboard.key_application] = false;
                            break;
                        case SDL_Keycode.SDLK_RCTRL:
                            keyboard.pressed_keys[keyboard.key_rctrl] = false;
                            break;
                        case SDL_Keycode.SDLK_UP:
                            keyboard.pressed_keys[keyboard.key_up] = false;
                            break;
                        case SDL_Keycode.SDLK_DOWN:
                            keyboard.pressed_keys[keyboard.key_down] = false;
                            break;
                        case SDL_Keycode.SDLK_LEFT:
                            keyboard.pressed_keys[keyboard.key_left] = false;
                            break;
                        case SDL_Keycode.SDLK_RIGHT:
                            keyboard.pressed_keys[keyboard.key_right] = false;
                            break;
                    }
                    break;
                #endregion
                }
            }
        }
    }
}