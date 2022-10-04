using static SDL2.SDL;
using Fjord.Modules.Input;

namespace Fjord;

internal static class EventHandler {
    public static void PollEvents() {
        while (SDL_PollEvent(out SDL_Event events) != 0) {
            switch(events.type) {
            case SDL_EventType.SDL_QUIT:
                Game.Stop();
                break;    
            case SDL_EventType.SDL_KEYDOWN:
                switch(events.key.keysym.sym) {
                case SDL_Keycode.SDLK_UNKNOWN:
                    Keyboard._PressedKeys[(int)Key.UNKNOWN] = true;
                    break;
                case SDL_Keycode.SDLK_RETURN:
                    Keyboard._PressedKeys[(int)Key.RETURN] = true;
                    break;
                case SDL_Keycode.SDLK_ESCAPE:
                    Keyboard._PressedKeys[(int)Key.ESCAPE] = true;
                    break;
                case SDL_Keycode.SDLK_BACKSPACE:
                    Keyboard._PressedKeys[(int)Key.BACKSPACE] = true;
                    break;
                case SDL_Keycode.SDLK_TAB:
                    Keyboard._PressedKeys[(int)Key.TAB] = true;
                    break;
                case SDL_Keycode.SDLK_SPACE:
                    Keyboard._PressedKeys[(int)Key.SPACE] = true;
                    break;
                case SDL_Keycode.SDLK_EXCLAIM:
                    Keyboard._PressedKeys[(int)Key.EXCLAIM] = true;
                    break;
                case SDL_Keycode.SDLK_QUOTEDBL:
                    Keyboard._PressedKeys[(int)Key.QUOTEDBL] = true;
                    break;
                case SDL_Keycode.SDLK_HASH:
                    Keyboard._PressedKeys[(int)Key.HASH] = true;
                    break;
                case SDL_Keycode.SDLK_PERCENT:
                    Keyboard._PressedKeys[(int)Key.PERCENT] = true;
                    break;
                case SDL_Keycode.SDLK_DOLLAR:
                    Keyboard._PressedKeys[(int)Key.DOLLAR] = true;
                    break;
                case SDL_Keycode.SDLK_AMPERSAND:
                    Keyboard._PressedKeys[(int)Key.AMPERSAND] = true;
                    break;
                case SDL_Keycode.SDLK_QUOTE:
                    Keyboard._PressedKeys[(int)Key.QUOTE] = true;
                    break;
                case SDL_Keycode.SDLK_LEFTPAREN:
                    Keyboard._PressedKeys[(int)Key.LEFTPAREN] = true;
                    break;
                case SDL_Keycode.SDLK_RIGHTPAREN:
                    Keyboard._PressedKeys[(int)Key.RIGHTPAREN] = true;
                    break;
                case SDL_Keycode.SDLK_ASTERISK:
                    Keyboard._PressedKeys[(int)Key.ASTERISK] = true;
                    break;
                case SDL_Keycode.SDLK_PLUS:
                    Keyboard._PressedKeys[(int)Key.PLUS] = true;
                    break;
                case SDL_Keycode.SDLK_COMMA:
                    Keyboard._PressedKeys[(int)Key.COMMA] = true;
                    break;
                case SDL_Keycode.SDLK_MINUS:
                    Keyboard._PressedKeys[(int)Key.MINUS] = true;
                    break;
                case SDL_Keycode.SDLK_PERIOD:
                    Keyboard._PressedKeys[(int)Key.PERIOD] = true;
                    break;
                case SDL_Keycode.SDLK_SLASH:
                    Keyboard._PressedKeys[(int)Key.SLASH] = true;
                    break;
                case SDL_Keycode.SDLK_0:
                    Keyboard._PressedKeys[(int)Key.N0] = true;
                    break;
                case SDL_Keycode.SDLK_1:
                    Keyboard._PressedKeys[(int)Key.N1] = true;
                    break;
                case SDL_Keycode.SDLK_2:
                    Keyboard._PressedKeys[(int)Key.N2] = true;
                    break;
                case SDL_Keycode.SDLK_3:
                    Keyboard._PressedKeys[(int)Key.N3] = true;
                    break;
                case SDL_Keycode.SDLK_4:
                    Keyboard._PressedKeys[(int)Key.N4] = true;
                    break;
                case SDL_Keycode.SDLK_5:
                    Keyboard._PressedKeys[(int)Key.N5] = true;
                    break;
                case SDL_Keycode.SDLK_6:
                    Keyboard._PressedKeys[(int)Key.N6] = true;
                    break;
                case SDL_Keycode.SDLK_7:
                    Keyboard._PressedKeys[(int)Key.N7] = true;
                    break;
                case SDL_Keycode.SDLK_8:
                    Keyboard._PressedKeys[(int)Key.N8] = true;
                    break;
                case SDL_Keycode.SDLK_9:
                    Keyboard._PressedKeys[(int)Key.N9] = true;
                    break;
                case SDL_Keycode.SDLK_COLON:
                    Keyboard._PressedKeys[(int)Key.COLON] = true;
                    break;
                case SDL_Keycode.SDLK_SEMICOLON:
                    Keyboard._PressedKeys[(int)Key.SEMICOLON] = true;
                    break;
                case SDL_Keycode.SDLK_LESS:
                    Keyboard._PressedKeys[(int)Key.LESS] = true;
                    break;
                case SDL_Keycode.SDLK_EQUALS:
                    Keyboard._PressedKeys[(int)Key.EQUALS] = true;
                    break;
                case SDL_Keycode.SDLK_GREATER:
                    Keyboard._PressedKeys[(int)Key.GREATER] = true;
                    break;
                case SDL_Keycode.SDLK_QUESTION:
                    Keyboard._PressedKeys[(int)Key.QUESTION] = true;
                    break;
                case SDL_Keycode.SDLK_AT:
                    Keyboard._PressedKeys[(int)Key.AT] = true;
                    break;
                case SDL_Keycode.SDLK_LEFTBRACKET:
                    Keyboard._PressedKeys[(int)Key.LEFTBRACKET] = true;
                    break;
                case SDL_Keycode.SDLK_BACKSLASH:
                    Keyboard._PressedKeys[(int)Key.BACKSLASH] = true;
                    break;
                case SDL_Keycode.SDLK_RIGHTBRACKET:
                    Keyboard._PressedKeys[(int)Key.RIGHTBRACKET] = true;
                    break;
                case SDL_Keycode.SDLK_CARET:
                    Keyboard._PressedKeys[(int)Key.CARET] = true;
                    break;
                case SDL_Keycode.SDLK_UNDERSCORE:
                    Keyboard._PressedKeys[(int)Key.UNDERSCORE] = true;
                    break;
                case SDL_Keycode.SDLK_BACKQUOTE:
                    Keyboard._PressedKeys[(int)Key.BACKQUOTE] = true;
                    break;
                case SDL_Keycode.SDLK_a:
                    Keyboard._PressedKeys[(int)Key.a] = true;
                    break;
                case SDL_Keycode.SDLK_b:
                    Keyboard._PressedKeys[(int)Key.b] = true;
                    break;
                case SDL_Keycode.SDLK_c:
                    Keyboard._PressedKeys[(int)Key.c] = true;
                    break;
                case SDL_Keycode.SDLK_d:
                    Keyboard._PressedKeys[(int)Key.d] = true;
                    break;
                case SDL_Keycode.SDLK_e:
                    Keyboard._PressedKeys[(int)Key.e] = true;
                    break;
                case SDL_Keycode.SDLK_f:
                    Keyboard._PressedKeys[(int)Key.f] = true;
                    break;
                case SDL_Keycode.SDLK_g:
                    Keyboard._PressedKeys[(int)Key.g] = true;
                    break;
                case SDL_Keycode.SDLK_h:
                    Keyboard._PressedKeys[(int)Key.h] = true;
                    break;
                case SDL_Keycode.SDLK_i:
                    Keyboard._PressedKeys[(int)Key.i] = true;
                    break;
                case SDL_Keycode.SDLK_j:
                    Keyboard._PressedKeys[(int)Key.j] = true;
                    break;
                case SDL_Keycode.SDLK_k:
                    Keyboard._PressedKeys[(int)Key.k] = true;
                    break;
                case SDL_Keycode.SDLK_l:
                    Keyboard._PressedKeys[(int)Key.l] = true;
                    break;
                case SDL_Keycode.SDLK_m:
                    Keyboard._PressedKeys[(int)Key.m] = true;
                    break;
                case SDL_Keycode.SDLK_n:
                    Keyboard._PressedKeys[(int)Key.n] = true;
                    break;
                case SDL_Keycode.SDLK_o:
                    Keyboard._PressedKeys[(int)Key.o] = true;
                    break;
                case SDL_Keycode.SDLK_p:
                    Keyboard._PressedKeys[(int)Key.p] = true;
                    break;
                case SDL_Keycode.SDLK_q:
                    Keyboard._PressedKeys[(int)Key.q] = true;
                    break;
                case SDL_Keycode.SDLK_r:
                    Keyboard._PressedKeys[(int)Key.r] = true;
                    break;
                case SDL_Keycode.SDLK_s:
                    Keyboard._PressedKeys[(int)Key.s] = true;
                    break;
                case SDL_Keycode.SDLK_t:
                    Keyboard._PressedKeys[(int)Key.t] = true;
                    break;
                case SDL_Keycode.SDLK_u:
                    Keyboard._PressedKeys[(int)Key.u] = true;
                    break;
                case SDL_Keycode.SDLK_v:
                    Keyboard._PressedKeys[(int)Key.v] = true;
                    break;
                case SDL_Keycode.SDLK_w:
                    Keyboard._PressedKeys[(int)Key.w] = true;
                    break;
                case SDL_Keycode.SDLK_x:
                    Keyboard._PressedKeys[(int)Key.x] = true;
                    break;
                case SDL_Keycode.SDLK_y:
                    Keyboard._PressedKeys[(int)Key.y] = true;
                    break;
                case SDL_Keycode.SDLK_z:
                    Keyboard._PressedKeys[(int)Key.z] = true;
                    break;
                case SDL_Keycode.SDLK_CAPSLOCK:
                    Keyboard._PressedKeys[(int)Key.CAPSLOCK] = true;
                    break;
                case SDL_Keycode.SDLK_F1:
                    Keyboard._PressedKeys[(int)Key.F1] = true;
                    break;
                case SDL_Keycode.SDLK_F2:
                    Keyboard._PressedKeys[(int)Key.F2] = true;
                    break;
                case SDL_Keycode.SDLK_F3:
                    Keyboard._PressedKeys[(int)Key.F3] = true;
                    break;
                case SDL_Keycode.SDLK_F4:
                    Keyboard._PressedKeys[(int)Key.F4] = true;
                    break;
                case SDL_Keycode.SDLK_F5:
                    Keyboard._PressedKeys[(int)Key.F5] = true;
                    break;
                case SDL_Keycode.SDLK_F6:
                    Keyboard._PressedKeys[(int)Key.F6] = true;
                    break;
                case SDL_Keycode.SDLK_F7:
                    Keyboard._PressedKeys[(int)Key.F7] = true;
                    break;
                case SDL_Keycode.SDLK_F8:
                    Keyboard._PressedKeys[(int)Key.F8] = true;
                    break;
                case SDL_Keycode.SDLK_F9:
                    Keyboard._PressedKeys[(int)Key.F9] = true;
                    break;
                case SDL_Keycode.SDLK_F10:
                    Keyboard._PressedKeys[(int)Key.F10] = true;
                    break;
                case SDL_Keycode.SDLK_F11:
                    Keyboard._PressedKeys[(int)Key.F11] = true;
                    break;
                case SDL_Keycode.SDLK_F12:
                    Keyboard._PressedKeys[(int)Key.F12] = true;
                    break;
                case SDL_Keycode.SDLK_PRINTSCREEN:
                    Keyboard._PressedKeys[(int)Key.PRINTSCREEN] = true;
                    break;
                case SDL_Keycode.SDLK_SCROLLLOCK:
                    Keyboard._PressedKeys[(int)Key.SCROLLLOCK] = true;
                    break;
                case SDL_Keycode.SDLK_PAUSE:
                    Keyboard._PressedKeys[(int)Key.PAUSE] = true;
                    break;
                case SDL_Keycode.SDLK_INSERT:
                    Keyboard._PressedKeys[(int)Key.INSERT] = true;
                    break;
                case SDL_Keycode.SDLK_HOME:
                    Keyboard._PressedKeys[(int)Key.HOME] = true;
                    break;
                case SDL_Keycode.SDLK_PAGEUP:
                    Keyboard._PressedKeys[(int)Key.PAGEUP] = true;
                    break;
                case SDL_Keycode.SDLK_DELETE:
                    Keyboard._PressedKeys[(int)Key.DELETE] = true;
                    break;
                case SDL_Keycode.SDLK_END:
                    Keyboard._PressedKeys[(int)Key.END] = true;
                    break;
                case SDL_Keycode.SDLK_PAGEDOWN:
                    Keyboard._PressedKeys[(int)Key.PAGEDOWN] = true;
                    break;
                case SDL_Keycode.SDLK_RIGHT:
                    Keyboard._PressedKeys[(int)Key.RIGHT] = true;
                    break;
                case SDL_Keycode.SDLK_LEFT:
                    Keyboard._PressedKeys[(int)Key.LEFT] = true;
                    break;
                case SDL_Keycode.SDLK_DOWN:
                    Keyboard._PressedKeys[(int)Key.DOWN] = true;
                    break;
                case SDL_Keycode.SDLK_UP:
                    Keyboard._PressedKeys[(int)Key.UP] = true;
                    break;
                case SDL_Keycode.SDLK_NUMLOCKCLEAR:
                    Keyboard._PressedKeys[(int)Key.NUMLOCKCLEAR] = true;
                    break;
                case SDL_Keycode.SDLK_KP_DIVIDE:
                    Keyboard._PressedKeys[(int)Key.KP_DIVIDE] = true;
                    break;
                case SDL_Keycode.SDLK_KP_MULTIPLY:
                    Keyboard._PressedKeys[(int)Key.KP_MULTIPLY] = true;
                    break;
                case SDL_Keycode.SDLK_KP_MINUS:
                    Keyboard._PressedKeys[(int)Key.KP_MINUS] = true;
                    break;
                case SDL_Keycode.SDLK_KP_PLUS:
                    Keyboard._PressedKeys[(int)Key.KP_PLUS] = true;
                    break;
                case SDL_Keycode.SDLK_KP_ENTER:
                    Keyboard._PressedKeys[(int)Key.KP_ENTER] = true;
                    break;
                case SDL_Keycode.SDLK_KP_1:
                    Keyboard._PressedKeys[(int)Key.KP_1] = true;
                    break;
                case SDL_Keycode.SDLK_KP_2:
                    Keyboard._PressedKeys[(int)Key.KP_2] = true;
                    break;
                case SDL_Keycode.SDLK_KP_3:
                    Keyboard._PressedKeys[(int)Key.KP_3] = true;
                    break;
                case SDL_Keycode.SDLK_KP_4:
                    Keyboard._PressedKeys[(int)Key.KP_4] = true;
                    break;
                case SDL_Keycode.SDLK_KP_5:
                    Keyboard._PressedKeys[(int)Key.KP_5] = true;
                    break;
                case SDL_Keycode.SDLK_KP_6:
                    Keyboard._PressedKeys[(int)Key.KP_6] = true;
                    break;
                case SDL_Keycode.SDLK_KP_7:
                    Keyboard._PressedKeys[(int)Key.KP_7] = true;
                    break;
                case SDL_Keycode.SDLK_KP_8:
                    Keyboard._PressedKeys[(int)Key.KP_8] = true;
                    break;
                case SDL_Keycode.SDLK_KP_9:
                    Keyboard._PressedKeys[(int)Key.KP_9] = true;
                    break;
                case SDL_Keycode.SDLK_KP_0:
                    Keyboard._PressedKeys[(int)Key.KP_0] = true;
                    break;
                case SDL_Keycode.SDLK_KP_PERIOD:
                    Keyboard._PressedKeys[(int)Key.KP_PERIOD] = true;
                    break;
                case SDL_Keycode.SDLK_APPLICATION:
                    Keyboard._PressedKeys[(int)Key.APPLICATION] = true;
                    break;
                case SDL_Keycode.SDLK_POWER:
                    Keyboard._PressedKeys[(int)Key.POWER] = true;
                    break;
                case SDL_Keycode.SDLK_KP_EQUALS:
                    Keyboard._PressedKeys[(int)Key.KP_EQUALS] = true;
                    break;
                case SDL_Keycode.SDLK_F13:
                    Keyboard._PressedKeys[(int)Key.F13] = true;
                    break;
                case SDL_Keycode.SDLK_F14:
                    Keyboard._PressedKeys[(int)Key.F14] = true;
                    break;
                case SDL_Keycode.SDLK_F15:
                    Keyboard._PressedKeys[(int)Key.F15] = true;
                    break;
                case SDL_Keycode.SDLK_F16:
                    Keyboard._PressedKeys[(int)Key.F16] = true;
                    break;
                case SDL_Keycode.SDLK_F17:
                    Keyboard._PressedKeys[(int)Key.F17] = true;
                    break;
                case SDL_Keycode.SDLK_F18:
                    Keyboard._PressedKeys[(int)Key.F18] = true;
                    break;
                case SDL_Keycode.SDLK_F19:
                    Keyboard._PressedKeys[(int)Key.F19] = true;
                    break;
                case SDL_Keycode.SDLK_F20:
                    Keyboard._PressedKeys[(int)Key.F20] = true;
                    break;
                case SDL_Keycode.SDLK_F21:
                    Keyboard._PressedKeys[(int)Key.F21] = true;
                    break;
                case SDL_Keycode.SDLK_F22:
                    Keyboard._PressedKeys[(int)Key.F22] = true;
                    break;
                case SDL_Keycode.SDLK_F23:
                    Keyboard._PressedKeys[(int)Key.F23] = true;
                    break;
                case SDL_Keycode.SDLK_F24:
                    Keyboard._PressedKeys[(int)Key.F24] = true;
                    break;
                case SDL_Keycode.SDLK_EXECUTE:
                    Keyboard._PressedKeys[(int)Key.EXECUTE] = true;
                    break;
                case SDL_Keycode.SDLK_HELP:
                    Keyboard._PressedKeys[(int)Key.HELP] = true;
                    break;
                case SDL_Keycode.SDLK_MENU:
                    Keyboard._PressedKeys[(int)Key.MENU] = true;
                    break;
                case SDL_Keycode.SDLK_SELECT:
                    Keyboard._PressedKeys[(int)Key.SELECT] = true;
                    break;
                case SDL_Keycode.SDLK_STOP:
                    Keyboard._PressedKeys[(int)Key.STOP] = true;
                    break;
                case SDL_Keycode.SDLK_AGAIN:
                    Keyboard._PressedKeys[(int)Key.AGAIN] = true;
                    break;
                case SDL_Keycode.SDLK_UNDO:
                    Keyboard._PressedKeys[(int)Key.UNDO] = true;
                    break;
                case SDL_Keycode.SDLK_CUT:
                    Keyboard._PressedKeys[(int)Key.CUT] = true;
                    break;
                case SDL_Keycode.SDLK_COPY:
                    Keyboard._PressedKeys[(int)Key.COPY] = true;
                    break;
                case SDL_Keycode.SDLK_PASTE:
                    Keyboard._PressedKeys[(int)Key.PASTE] = true;
                    break;
                case SDL_Keycode.SDLK_FIND:
                    Keyboard._PressedKeys[(int)Key.FIND] = true;
                    break;
                case SDL_Keycode.SDLK_MUTE:
                    Keyboard._PressedKeys[(int)Key.MUTE] = true;
                    break;
                case SDL_Keycode.SDLK_VOLUMEUP:
                    Keyboard._PressedKeys[(int)Key.VOLUMEUP] = true;
                    break;
                case SDL_Keycode.SDLK_VOLUMEDOWN:
                    Keyboard._PressedKeys[(int)Key.VOLUMEDOWN] = true;
                    break;
                case SDL_Keycode.SDLK_KP_COMMA:
                    Keyboard._PressedKeys[(int)Key.KP_COMMA] = true;
                    break;
                case SDL_Keycode.SDLK_KP_EQUALSAS400:
                    Keyboard._PressedKeys[(int)Key.KP_EQUALSAS400] = true;
                    break;
                case SDL_Keycode.SDLK_ALTERASE:
                    Keyboard._PressedKeys[(int)Key.ALTERASE] = true;
                    break;
                case SDL_Keycode.SDLK_SYSREQ:
                    Keyboard._PressedKeys[(int)Key.SYSREQ] = true;
                    break;
                case SDL_Keycode.SDLK_CANCEL:
                    Keyboard._PressedKeys[(int)Key.CANCEL] = true;
                    break;
                case SDL_Keycode.SDLK_CLEAR:
                    Keyboard._PressedKeys[(int)Key.CLEAR] = true;
                    break;
                case SDL_Keycode.SDLK_PRIOR:
                    Keyboard._PressedKeys[(int)Key.PRIOR] = true;
                    break;
                case SDL_Keycode.SDLK_RETURN2:
                    Keyboard._PressedKeys[(int)Key.RETURN2] = true;
                    break;
                case SDL_Keycode.SDLK_SEPARATOR:
                    Keyboard._PressedKeys[(int)Key.SEPARATOR] = true;
                    break;
                case SDL_Keycode.SDLK_OUT:
                    Keyboard._PressedKeys[(int)Key.OUT] = true;
                    break;
                case SDL_Keycode.SDLK_OPER:
                    Keyboard._PressedKeys[(int)Key.OPER] = true;
                    break;
                case SDL_Keycode.SDLK_CLEARAGAIN:
                    Keyboard._PressedKeys[(int)Key.CLEARAGAIN] = true;
                    break;
                case SDL_Keycode.SDLK_CRSEL:
                    Keyboard._PressedKeys[(int)Key.CRSEL] = true;
                    break;
                case SDL_Keycode.SDLK_EXSEL:
                    Keyboard._PressedKeys[(int)Key.EXSEL] = true;
                    break;
                case SDL_Keycode.SDLK_KP_00:
                    Keyboard._PressedKeys[(int)Key.KP_00] = true;
                    break;
                case SDL_Keycode.SDLK_KP_000:
                    Keyboard._PressedKeys[(int)Key.KP_000] = true;
                    break;
                case SDL_Keycode.SDLK_THOUSANDSSEPARATOR:
                    Keyboard._PressedKeys[(int)Key.THOUSANDSSEPARATOR] = true;
                    break;
                case SDL_Keycode.SDLK_DECIMALSEPARATOR:
                    Keyboard._PressedKeys[(int)Key.DECIMALSEPARATOR] = true;
                    break;
                case SDL_Keycode.SDLK_CURRENCYUNIT:
                    Keyboard._PressedKeys[(int)Key.CURRENCYUNIT] = true;
                    break;
                case SDL_Keycode.SDLK_CURRENCYSUBUNIT:
                    Keyboard._PressedKeys[(int)Key.CURRENCYSUBUNIT] = true;
                    break;
                case SDL_Keycode.SDLK_KP_LEFTPAREN:
                    Keyboard._PressedKeys[(int)Key.KP_LEFTPAREN] = true;
                    break;
                case SDL_Keycode.SDLK_KP_RIGHTPAREN:
                    Keyboard._PressedKeys[(int)Key.KP_RIGHTPAREN] = true;
                    break;
                case SDL_Keycode.SDLK_KP_LEFTBRACE:
                    Keyboard._PressedKeys[(int)Key.KP_LEFTBRACE] = true;
                    break;
                case SDL_Keycode.SDLK_KP_RIGHTBRACE:
                    Keyboard._PressedKeys[(int)Key.KP_RIGHTBRACE] = true;
                    break;
                case SDL_Keycode.SDLK_KP_TAB:
                    Keyboard._PressedKeys[(int)Key.KP_TAB] = true;
                    break;
                case SDL_Keycode.SDLK_KP_BACKSPACE:
                    Keyboard._PressedKeys[(int)Key.KP_BACKSPACE] = true;
                    break;
                case SDL_Keycode.SDLK_KP_A:
                    Keyboard._PressedKeys[(int)Key.KP_A] = true;
                    break;
                case SDL_Keycode.SDLK_KP_B:
                    Keyboard._PressedKeys[(int)Key.KP_B] = true;
                    break;
                case SDL_Keycode.SDLK_KP_C:
                    Keyboard._PressedKeys[(int)Key.KP_C] = true;
                    break;
                case SDL_Keycode.SDLK_KP_D:
                    Keyboard._PressedKeys[(int)Key.KP_D] = true;
                    break;
                case SDL_Keycode.SDLK_KP_E:
                    Keyboard._PressedKeys[(int)Key.KP_E] = true;
                    break;
                case SDL_Keycode.SDLK_KP_F:
                    Keyboard._PressedKeys[(int)Key.KP_F] = true;
                    break;
                case SDL_Keycode.SDLK_KP_XOR:
                    Keyboard._PressedKeys[(int)Key.KP_XOR] = true;
                    break;
                case SDL_Keycode.SDLK_KP_POWER:
                    Keyboard._PressedKeys[(int)Key.KP_POWER] = true;
                    break;
                case SDL_Keycode.SDLK_KP_PERCENT:
                    Keyboard._PressedKeys[(int)Key.KP_PERCENT] = true;
                    break;
                case SDL_Keycode.SDLK_KP_LESS:
                    Keyboard._PressedKeys[(int)Key.KP_LESS] = true;
                    break;
                case SDL_Keycode.SDLK_KP_GREATER:
                    Keyboard._PressedKeys[(int)Key.KP_GREATER] = true;
                    break;
                case SDL_Keycode.SDLK_KP_AMPERSAND:
                    Keyboard._PressedKeys[(int)Key.KP_AMPERSAND] = true;
                    break;
                case SDL_Keycode.SDLK_KP_DBLAMPERSAND:
                    Keyboard._PressedKeys[(int)Key.KP_DBLAMPERSAND] = true;
                    break;
                case SDL_Keycode.SDLK_KP_VERTICALBAR:
                    Keyboard._PressedKeys[(int)Key.KP_VERTICALBAR] = true;
                    break;
                case SDL_Keycode.SDLK_KP_DBLVERTICALBAR:
                    Keyboard._PressedKeys[(int)Key.KP_DBLVERTICALBAR] = true;
                    break;
                case SDL_Keycode.SDLK_KP_COLON:
                    Keyboard._PressedKeys[(int)Key.KP_COLON] = true;
                    break;
                case SDL_Keycode.SDLK_KP_HASH:
                    Keyboard._PressedKeys[(int)Key.KP_HASH] = true;
                    break;
                case SDL_Keycode.SDLK_KP_SPACE:
                    Keyboard._PressedKeys[(int)Key.KP_SPACE] = true;
                    break;
                case SDL_Keycode.SDLK_KP_AT:
                    Keyboard._PressedKeys[(int)Key.KP_AT] = true;
                    break;
                case SDL_Keycode.SDLK_KP_EXCLAM:
                    Keyboard._PressedKeys[(int)Key.KP_EXCLAM] = true;
                    break;
                case SDL_Keycode.SDLK_KP_MEMSTORE:
                    Keyboard._PressedKeys[(int)Key.KP_MEMSTORE] = true;
                    break;
                case SDL_Keycode.SDLK_KP_MEMRECALL:
                    Keyboard._PressedKeys[(int)Key.KP_MEMRECALL] = true;
                    break;
                case SDL_Keycode.SDLK_KP_MEMCLEAR:
                    Keyboard._PressedKeys[(int)Key.KP_MEMCLEAR] = true;
                    break;
                case SDL_Keycode.SDLK_KP_MEMADD:
                    Keyboard._PressedKeys[(int)Key.KP_MEMADD] = true;
                    break;
                case SDL_Keycode.SDLK_KP_MEMSUBTRACT:
                    Keyboard._PressedKeys[(int)Key.KP_MEMSUBTRACT] = true;
                    break;
                case SDL_Keycode.SDLK_KP_MEMMULTIPLY:
                    Keyboard._PressedKeys[(int)Key.KP_MEMMULTIPLY] = true;
                    break;
                case SDL_Keycode.SDLK_KP_MEMDIVIDE:
                    Keyboard._PressedKeys[(int)Key.KP_MEMDIVIDE] = true;
                    break;
                case SDL_Keycode.SDLK_KP_PLUSMINUS:
                    Keyboard._PressedKeys[(int)Key.KP_PLUSMINUS] = true;
                    break;
                case SDL_Keycode.SDLK_KP_CLEAR:
                    Keyboard._PressedKeys[(int)Key.KP_CLEAR] = true;
                    break;
                case SDL_Keycode.SDLK_KP_CLEARENTRY:
                    Keyboard._PressedKeys[(int)Key.KP_CLEARENTRY] = true;
                    break;
                case SDL_Keycode.SDLK_KP_BINARY:
                    Keyboard._PressedKeys[(int)Key.KP_BINARY] = true;
                    break;
                case SDL_Keycode.SDLK_KP_OCTAL:
                    Keyboard._PressedKeys[(int)Key.KP_OCTAL] = true;
                    break;
                case SDL_Keycode.SDLK_KP_DECIMAL:
                    Keyboard._PressedKeys[(int)Key.KP_DECIMAL] = true;
                    break;
                case SDL_Keycode.SDLK_KP_HEXADECIMAL:
                    Keyboard._PressedKeys[(int)Key.KP_HEXADECIMAL] = true;
                    break;
                case SDL_Keycode.SDLK_LCTRL:
                    Keyboard._PressedKeys[(int)Key.LCTRL] = true;
                    break;
                case SDL_Keycode.SDLK_LSHIFT:
                    Keyboard._PressedKeys[(int)Key.LSHIFT] = true;
                    break;
                case SDL_Keycode.SDLK_LALT:
                    Keyboard._PressedKeys[(int)Key.LALT] = true;
                    break;
                case SDL_Keycode.SDLK_LGUI:
                    Keyboard._PressedKeys[(int)Key.LGUI] = true;
                    break;
                case SDL_Keycode.SDLK_RCTRL:
                    Keyboard._PressedKeys[(int)Key.RCTRL] = true;
                    break;
                case SDL_Keycode.SDLK_RSHIFT:
                    Keyboard._PressedKeys[(int)Key.RSHIFT] = true;
                    break;
                case SDL_Keycode.SDLK_RALT:
                    Keyboard._PressedKeys[(int)Key.RALT] = true;
                    break;
                case SDL_Keycode.SDLK_RGUI:
                    Keyboard._PressedKeys[(int)Key.RGUI] = true;
                    break;
                case SDL_Keycode.SDLK_MODE:
                    Keyboard._PressedKeys[(int)Key.MODE] = true;
                    break;
                case SDL_Keycode.SDLK_AUDIONEXT:
                    Keyboard._PressedKeys[(int)Key.AUDIONEXT] = true;
                    break;
                case SDL_Keycode.SDLK_AUDIOPREV:
                    Keyboard._PressedKeys[(int)Key.AUDIOPREV] = true;
                    break;
                case SDL_Keycode.SDLK_AUDIOSTOP:
                    Keyboard._PressedKeys[(int)Key.AUDIOSTOP] = true;
                    break;
                case SDL_Keycode.SDLK_AUDIOPLAY:
                    Keyboard._PressedKeys[(int)Key.AUDIOPLAY] = true;
                    break;
                case SDL_Keycode.SDLK_AUDIOMUTE:
                    Keyboard._PressedKeys[(int)Key.AUDIOMUTE] = true;
                    break;
                case SDL_Keycode.SDLK_MEDIASELECT:
                    Keyboard._PressedKeys[(int)Key.MEDIASELECT] = true;
                    break;
                case SDL_Keycode.SDLK_WWW:
                    Keyboard._PressedKeys[(int)Key.WWW] = true;
                    break;
                case SDL_Keycode.SDLK_MAIL:
                    Keyboard._PressedKeys[(int)Key.MAIL] = true;
                    break;
                case SDL_Keycode.SDLK_CALCULATOR:
                    Keyboard._PressedKeys[(int)Key.CALCULATOR] = true;
                    break;
                case SDL_Keycode.SDLK_COMPUTER:
                    Keyboard._PressedKeys[(int)Key.COMPUTER] = true;
                    break;
                case SDL_Keycode.SDLK_AC_SEARCH:
                    Keyboard._PressedKeys[(int)Key.AC_SEARCH] = true;
                    break;
                case SDL_Keycode.SDLK_AC_HOME:
                    Keyboard._PressedKeys[(int)Key.AC_HOME] = true;
                    break;
                case SDL_Keycode.SDLK_AC_BACK:
                    Keyboard._PressedKeys[(int)Key.AC_BACK] = true;
                    break;
                case SDL_Keycode.SDLK_AC_FORWARD:
                    Keyboard._PressedKeys[(int)Key.AC_FORWARD] = true;
                    break;
                case SDL_Keycode.SDLK_AC_STOP:
                    Keyboard._PressedKeys[(int)Key.AC_STOP] = true;
                    break;
                case SDL_Keycode.SDLK_AC_REFRESH:
                    Keyboard._PressedKeys[(int)Key.AC_REFRESH] = true;
                    break;
                case SDL_Keycode.SDLK_AC_BOOKMARKS:
                    Keyboard._PressedKeys[(int)Key.AC_BOOKMARKS] = true;
                    break;
                case SDL_Keycode.SDLK_BRIGHTNESSDOWN:
                    Keyboard._PressedKeys[(int)Key.BRIGHTNESSDOWN] = true;
                    break;
                case SDL_Keycode.SDLK_BRIGHTNESSUP:
                    Keyboard._PressedKeys[(int)Key.BRIGHTNESSUP] = true;
                    break;
                case SDL_Keycode.SDLK_DISPLAYSWITCH:
                    Keyboard._PressedKeys[(int)Key.DISPLAYSWITCH] = true;
                    break;
                case SDL_Keycode.SDLK_KBDILLUMTOGGLE:
                    Keyboard._PressedKeys[(int)Key.KBDILLUMTOGGLE] = true;
                    break;
                case SDL_Keycode.SDLK_KBDILLUMDOWN:
                    Keyboard._PressedKeys[(int)Key.KBDILLUMDOWN] = true;
                    break;
                case SDL_Keycode.SDLK_KBDILLUMUP:
                    Keyboard._PressedKeys[(int)Key.KBDILLUMUP] = true;
                    break;
                case SDL_Keycode.SDLK_EJECT:
                    Keyboard._PressedKeys[(int)Key.EJECT] = true;
                    break;
                case SDL_Keycode.SDLK_SLEEP:
                    Keyboard._PressedKeys[(int)Key.SLEEP] = true;
                    break;
                case SDL_Keycode.SDLK_APP1:
                    Keyboard._PressedKeys[(int)Key.APP1] = true;
                    break;
                case SDL_Keycode.SDLK_APP2:
                    Keyboard._PressedKeys[(int)Key.APP2] = true;
                    break;
                case SDL_Keycode.SDLK_AUDIOREWIND:
                    Keyboard._PressedKeys[(int)Key.AUDIOREWIND] = true;
                    break;
                case SDL_Keycode.SDLK_AUDIOFASTFORWARD:
                    Keyboard._PressedKeys[(int)Key.AUDIOFASTFORWARD] = true;
                    break;

                }  
                break;
            }
        }
    }
}


