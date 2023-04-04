using Fjord.Input;
using Fjord.Ui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static SDL2.SDL;

namespace Fjord
{
    internal static class EventHandler
    {
        internal static void HandleEvents()
        {
            Mouse.ScrollDown = false;
            Mouse.ScrollLeft = false;
            Mouse.ScrollRight = false;
            Mouse.ScrollUp = false;
            while (SDL_PollEvent(out SDL_Event e) != 0)
            {
                switch (e.type)
                {
                    case SDL_EventType.SDL_QUIT:
                        Game.Stop();
                        break;
                    case SDL_EventType.SDL_KEYDOWN:
                        if(FUI.selectedTextField != null) {
                            if(e.key.keysym.sym == SDL_Keycode.SDLK_BACKSPACE) {
                                if(FUI.selectedTextFieldValue.Length > 0)
                                    FUI.selectedTextFieldOnChange(FUI.selectedTextFieldValue.Remove(FUI.selectedTextFieldValue.Length - 1));
                            }
                            break;
                        }
                        
                        switch (e.key.keysym.sym)
                        {
                            case SDL_Keycode.SDLK_0:
                                Keyboard.AddKey(Key.N0);
                                break;
                            case SDL_Keycode.SDLK_1:
                                Keyboard.AddKey(Key.N1);
                                break;
                            case SDL_Keycode.SDLK_2:
                                Keyboard.AddKey(Key.N);
                                break;
                            case SDL_Keycode.SDLK_3:
                                Keyboard.AddKey(Key.N3);
                                break;
                            case SDL_Keycode.SDLK_4:
                                Keyboard.AddKey(Key.N4);
                                break;
                            case SDL_Keycode.SDLK_5:
                                Keyboard.AddKey(Key.N5);
                                break;
                            case SDL_Keycode.SDLK_6:
                                Keyboard.AddKey(Key.N6);
                                break;
                            case SDL_Keycode.SDLK_7:
                                Keyboard.AddKey(Key.N7);
                                break;
                            case SDL_Keycode.SDLK_8:
                                Keyboard.AddKey(Key.N8);
                                break;
                            case SDL_Keycode.SDLK_9:
                                Keyboard.AddKey(Key.N9);
                                break;
                            case SDL_Keycode.SDLK_a:
                                Keyboard.AddKey(Key.A);
                                break;
                            case SDL_Keycode.SDLK_AC_BACK:
                                Keyboard.AddKey(Key.AC_BACK);
                                break;
                            case SDL_Keycode.SDLK_AC_BOOKMARKS:
                                Keyboard.AddKey(Key.AC_BOOKMARKS);
                                break;
                            case SDL_Keycode.SDLK_KP_DBLAMPERSAND:
                                Keyboard.AddKey(Key.KP_DBLAMPERSAND);
                                break;
                            case SDL_Keycode.SDLK_AC_FORWARD:
                                Keyboard.AddKey(Key.AC_FORWARD);
                                break;
                            case SDL_Keycode.SDLK_AC_HOME:
                                Keyboard.AddKey(Key.AC_HOME);
                                break;
                            case SDL_Keycode.SDLK_AC_REFRESH:
                                Keyboard.AddKey(Key.AC_REFRESH);
                                break;
                            case SDL_Keycode.SDLK_AC_SEARCH:
                                Keyboard.AddKey(Key.AC_SEARCH);
                                break;
                            case SDL_Keycode.SDLK_AC_STOP:
                                Keyboard.AddKey(Key.AC_STOP);
                                break;
                            case SDL_Keycode.SDLK_AGAIN:
                                Keyboard.AddKey(Key.AGAIN);
                                break;
                            case SDL_Keycode.SDLK_ALTERASE:
                                Keyboard.AddKey(Key.ALTERASE);
                                break;
                            case SDL_Keycode.SDLK_QUOTE:
                                Keyboard.AddKey(Key.QUOTE);
                                break;
                            case SDL_Keycode.SDLK_APPLICATION:
                                Keyboard.AddKey(Key.APPLICATION);
                                break;
                            case SDL_Keycode.SDLK_AUDIOMUTE:
                                Keyboard.AddKey(Key.AUDIOMUTE);
                                break;
                            case SDL_Keycode.SDLK_AUDIONEXT:
                                Keyboard.AddKey(Key.AUDIONEXT);
                                break;
                            case SDL_Keycode.SDLK_AUDIOPLAY:
                                Keyboard.AddKey(Key.AUDIOPLAY);
                                break;
                            case SDL_Keycode.SDLK_AUDIOPREV:
                                Keyboard.AddKey(Key.AUDIOPREV);
                                break;
                            case SDL_Keycode.SDLK_AUDIOSTOP:
                                Keyboard.AddKey(Key.AUDIOSTOP);
                                break;
                            case SDL_Keycode.SDLK_b:
                                Keyboard.AddKey(Key.B);
                                break;
                            case SDL_Keycode.SDLK_BACKSLASH:
                                Keyboard.AddKey(Key.BACKSLASH);
                                break;
                            case SDL_Keycode.SDLK_BACKSPACE:
                                Keyboard.AddKey(Key.BACKSPACE);
                                break;
                            case SDL_Keycode.SDLK_BRIGHTNESSDOWN:
                                Keyboard.AddKey(Key.BRIGHTNESSDOWN);
                                break;
                            case SDL_Keycode.SDLK_BRIGHTNESSUP:
                                Keyboard.AddKey(Key.BRIGHTNESSUP);
                                break;
                            case SDL_Keycode.SDLK_c:
                                Keyboard.AddKey(Key.C);
                                break;
                            case SDL_Keycode.SDLK_CALCULATOR:
                                Keyboard.AddKey(Key.CALCULATOR);
                                break;
                            case SDL_Keycode.SDLK_CANCEL:
                                Keyboard.AddKey(Key.CANCEL);
                                break;
                            case SDL_Keycode.SDLK_CAPSLOCK:
                                Keyboard.AddKey(Key.CAPSLOCK);
                                break;
                            case SDL_Keycode.SDLK_CLEAR:
                                Keyboard.AddKey(Key.CLEAR);
                                break;
                            case SDL_Keycode.SDLK_CLEARAGAIN:
                                Keyboard.AddKey(Key.CLEARAGAIN);
                                break;
                            case SDL_Keycode.SDLK_COMMA:
                                Keyboard.AddKey(Key.COMMA);
                                break;
                            case SDL_Keycode.SDLK_COMPUTER:
                                Keyboard.AddKey(Key.COMPUTER);
                                break;
                            case SDL_Keycode.SDLK_COPY:
                                Keyboard.AddKey(Key.COPY);
                                break;
                            case SDL_Keycode.SDLK_CRSEL:
                                Keyboard.AddKey(Key.CRSEL);
                                break;
                            case SDL_Keycode.SDLK_CURRENCYSUBUNIT:
                                Keyboard.AddKey(Key.CURRENCYSUBUNIT);
                                break;
                            case SDL_Keycode.SDLK_CURRENCYUNIT:
                                Keyboard.AddKey(Key.CURRENCYUNIT);
                                break;
                            case SDL_Keycode.SDLK_CUT:
                                Keyboard.AddKey(Key.CUT);
                                break;
                            case SDL_Keycode.SDLK_d:
                                Keyboard.AddKey(Key.D);
                                break;
                            case SDL_Keycode.SDLK_DECIMALSEPARATOR:
                                Keyboard.AddKey(Key.DECIMALSEPARATOR);
                                break;
                            case SDL_Keycode.SDLK_DELETE:
                                Keyboard.AddKey(Key.DELETE);
                                break;
                            case SDL_Keycode.SDLK_DISPLAYSWITCH:
                                Keyboard.AddKey(Key.DISPLAYSWITCH);
                                break;
                            case SDL_Keycode.SDLK_DOWN:
                                Keyboard.AddKey(Key.DOWN);
                                break;
                            case SDL_Keycode.SDLK_e:
                                Keyboard.AddKey(Key.E);
                                break;
                            case SDL_Keycode.SDLK_EJECT:
                                Keyboard.AddKey(Key.EJECT);
                                break;
                            case SDL_Keycode.SDLK_END:
                                Keyboard.AddKey(Key.END);
                                break;
                            case SDL_Keycode.SDLK_EQUALS:
                                Keyboard.AddKey(Key.EQUALS);
                                break;
                            case SDL_Keycode.SDLK_ESCAPE:
                                Keyboard.AddKey(Key.ESCAPE);
                                break;
                            case SDL_Keycode.SDLK_EXECUTE:
                                Keyboard.AddKey(Key.EXECUTE);
                                break;
                            case SDL_Keycode.SDLK_EXSEL:
                                Keyboard.AddKey(Key.EXSEL);
                                break;
                            case SDL_Keycode.SDLK_f:
                                Keyboard.AddKey(Key.F);
                                break;
                            case SDL_Keycode.SDLK_F1:
                                Keyboard.AddKey(Key.F1);
                                break;
                            case SDL_Keycode.SDLK_F10:
                                Keyboard.AddKey(Key.F10);
                                break;
                            case SDL_Keycode.SDLK_F11:
                                Keyboard.AddKey(Key.F11);
                                break;
                            case SDL_Keycode.SDLK_F12:
                                Keyboard.AddKey(Key.F12);
                                break;
                            case SDL_Keycode.SDLK_F13:
                                Keyboard.AddKey(Key.F13);
                                break;
                            case SDL_Keycode.SDLK_F14:
                                Keyboard.AddKey(Key.F14);
                                break;
                            case SDL_Keycode.SDLK_F15:
                                Keyboard.AddKey(Key.F15);
                                break;
                            case SDL_Keycode.SDLK_F16:
                                Keyboard.AddKey(Key.F16);
                                break;
                            case SDL_Keycode.SDLK_F17:
                                Keyboard.AddKey(Key.F17);
                                break;
                            case SDL_Keycode.SDLK_F18:
                                Keyboard.AddKey(Key.F18);
                                break;
                            case SDL_Keycode.SDLK_F19:
                                Keyboard.AddKey(Key.F19);
                                break;
                            case SDL_Keycode.SDLK_F2:
                                Keyboard.AddKey(Key.F2);
                                break;
                            case SDL_Keycode.SDLK_F20:
                                Keyboard.AddKey(Key.F20);
                                break;
                            case SDL_Keycode.SDLK_F21:
                                Keyboard.AddKey(Key.F21);
                                break;
                            case SDL_Keycode.SDLK_F22:
                                Keyboard.AddKey(Key.F22);
                                break;
                            case SDL_Keycode.SDLK_F23:
                                Keyboard.AddKey(Key.F23);
                                break;
                            case SDL_Keycode.SDLK_F24:
                                Keyboard.AddKey(Key.F24);
                                break;
                            case SDL_Keycode.SDLK_F3:
                                Keyboard.AddKey(Key.F3);
                                break;
                            case SDL_Keycode.SDLK_F4:
                                Keyboard.AddKey(Key.F4);
                                break;
                            case SDL_Keycode.SDLK_F5:
                                Keyboard.AddKey(Key.F5);
                                break;
                            case SDL_Keycode.SDLK_F6:
                                Keyboard.AddKey(Key.F6);
                                break;
                            case SDL_Keycode.SDLK_F7:
                                Keyboard.AddKey(Key.F7);
                                break;
                            case SDL_Keycode.SDLK_F8:
                                Keyboard.AddKey(Key.F8);
                                break;
                            case SDL_Keycode.SDLK_F9:
                                Keyboard.AddKey(Key.F9);
                                break;
                            case SDL_Keycode.SDLK_FIND:
                                Keyboard.AddKey(Key.FIND);
                                break;
                            case SDL_Keycode.SDLK_g:
                                Keyboard.AddKey(Key.G);
                                break;
                            case SDL_Keycode.SDLK_BACKQUOTE:
                                Keyboard.AddKey(Key.BACKQUOTE);
                                break;
                            case SDL_Keycode.SDLK_h:
                                Keyboard.AddKey(Key.H);
                                break;
                            case SDL_Keycode.SDLK_HELP:
                                Keyboard.AddKey(Key.HELP);
                                break;
                            case SDL_Keycode.SDLK_HOME:
                                Keyboard.AddKey(Key.HOME);
                                break;
                            case SDL_Keycode.SDLK_i:
                                Keyboard.AddKey(Key.I);
                                break;
                            case SDL_Keycode.SDLK_INSERT:
                                Keyboard.AddKey(Key.INSERT);
                                break;
                            case SDL_Keycode.SDLK_j:
                                Keyboard.AddKey(Key.J);
                                break;
                            case SDL_Keycode.SDLK_k:
                                Keyboard.AddKey(Key.K);
                                break;
                            case SDL_Keycode.SDLK_KBDILLUMDOWN:
                                Keyboard.AddKey(Key.KBDILLUMDOWN);
                                break;
                            case SDL_Keycode.SDLK_KBDILLUMTOGGLE:
                                Keyboard.AddKey(Key.KBDILLUMTOGGLE);
                                break;
                            case SDL_Keycode.SDLK_KBDILLUMUP:
                                Keyboard.AddKey(Key.KBDILLUMUP);
                                break;
                            case SDL_Keycode.SDLK_KP_0:
                                Keyboard.AddKey(Key.KP_0);
                                break;
                            case SDL_Keycode.SDLK_KP_00:
                                Keyboard.AddKey(Key.KP_00);
                                break;
                            case SDL_Keycode.SDLK_KP_000:
                                Keyboard.AddKey(Key.KP_000);
                                break;
                            case SDL_Keycode.SDLK_KP_1:
                                Keyboard.AddKey(Key.KP_1);
                                break;
                            case SDL_Keycode.SDLK_KP_2:
                                Keyboard.AddKey(Key.KP_2);
                                break;
                            case SDL_Keycode.SDLK_KP_3:
                                Keyboard.AddKey(Key.KP_3);
                                break;
                            case SDL_Keycode.SDLK_KP_4:
                                Keyboard.AddKey(Key.KP_4);
                                break;
                            case SDL_Keycode.SDLK_KP_5:
                                Keyboard.AddKey(Key.KP_5);
                                break;
                            case SDL_Keycode.SDLK_KP_6:
                                Keyboard.AddKey(Key.KP_6);
                                break;
                            case SDL_Keycode.SDLK_KP_7:
                                Keyboard.AddKey(Key.KP_7);
                                break;
                            case SDL_Keycode.SDLK_KP_8:
                                Keyboard.AddKey(Key.KP_8);
                                break;
                            case SDL_Keycode.SDLK_KP_9:
                                Keyboard.AddKey(Key.KP_9);
                                break;
                            case SDL_Keycode.SDLK_KP_A:
                                Keyboard.AddKey(Key.KP_A);
                                break;
                            case SDL_Keycode.SDLK_KP_AMPERSAND:
                                Keyboard.AddKey(Key.KP_AMPERSAND);
                                break;
                            case SDL_Keycode.SDLK_KP_AT:
                                Keyboard.AddKey(Key.KP_AT);
                                break;
                            case SDL_Keycode.SDLK_KP_B:
                                Keyboard.AddKey(Key.KP_B);
                                break;
                            case SDL_Keycode.SDLK_KP_BACKSPACE:
                                Keyboard.AddKey(Key.KP_BACKSPACE);
                                break;
                            case SDL_Keycode.SDLK_KP_BINARY:
                                Keyboard.AddKey(Key.KP_BINARY);
                                break;
                            case SDL_Keycode.SDLK_KP_C:
                                Keyboard.AddKey(Key.KP_C);
                                break;
                            case SDL_Keycode.SDLK_KP_CLEAR:
                                Keyboard.AddKey(Key.KP_CLEAR);
                                break;
                            case SDL_Keycode.SDLK_KP_CLEARENTRY:
                                Keyboard.AddKey(Key.KP_CLEARENTRY);
                                break;
                            case SDL_Keycode.SDLK_KP_COLON:
                                Keyboard.AddKey(Key.KP_COLON);
                                break;
                            case SDL_Keycode.SDLK_KP_COMMA:
                                Keyboard.AddKey(Key.KP_COMMA);
                                break;
                            case SDL_Keycode.SDLK_KP_D:
                                Keyboard.AddKey(Key.KP_D);
                                break;
                            case SDL_Keycode.SDLK_KP_DBLVERTICALBAR:
                                Keyboard.AddKey(Key.KP_DBLVERTICALBAR);
                                break;
                            case SDL_Keycode.SDLK_KP_DECIMAL:
                                Keyboard.AddKey(Key.KP_DECIMAL);
                                break;
                            case SDL_Keycode.SDLK_KP_DIVIDE:
                                Keyboard.AddKey(Key.KP_DIVIDE);
                                break;
                            case SDL_Keycode.SDLK_KP_E:
                                Keyboard.AddKey(Key.KP_E);
                                break;
                            case SDL_Keycode.SDLK_KP_ENTER:
                                Keyboard.AddKey(Key.KP_ENTER);
                                break;
                            case SDL_Keycode.SDLK_KP_EQUALS:
                                Keyboard.AddKey(Key.KP_EQUALS);
                                break;
                            case SDL_Keycode.SDLK_KP_EQUALSAS400:
                                Keyboard.AddKey(Key.KP_EQUALSAS400);
                                break;
                            case SDL_Keycode.SDLK_KP_EXCLAM:
                                Keyboard.AddKey(Key.KP_EXCLAM);
                                break;
                            case SDL_Keycode.SDLK_KP_F:
                                Keyboard.AddKey(Key.KP_F);
                                break;
                            case SDL_Keycode.SDLK_KP_GREATER:
                                Keyboard.AddKey(Key.KP_GREATER);
                                break;
                            case SDL_Keycode.SDLK_KP_HASH:
                                Keyboard.AddKey(Key.KP_HASH);
                                break;
                            case SDL_Keycode.SDLK_KP_HEXADECIMAL:
                                Keyboard.AddKey(Key.KP_HEXADECIMAL);
                                break;
                            case SDL_Keycode.SDLK_KP_LEFTBRACE:
                                Keyboard.AddKey(Key.KP_LEFTBRACE);
                                break;
                            case SDL_Keycode.SDLK_KP_LEFTPAREN:
                                Keyboard.AddKey(Key.KP_LEFTPAREN);
                                break;
                            case SDL_Keycode.SDLK_KP_LESS:
                                Keyboard.AddKey(Key.KP_LESS);
                                break;
                            case SDL_Keycode.SDLK_KP_MEMADD:
                                Keyboard.AddKey(Key.KP_MEMADD);
                                break;
                            case SDL_Keycode.SDLK_KP_MEMCLEAR:
                                Keyboard.AddKey(Key.KP_MEMCLEAR);
                                break;
                            case SDL_Keycode.SDLK_KP_MEMDIVIDE:
                                Keyboard.AddKey(Key.KP_MEMDIVIDE);
                                break;
                            case SDL_Keycode.SDLK_KP_MEMMULTIPLY:
                                Keyboard.AddKey(Key.KP_MEMMULTIPLY);
                                break;
                            case SDL_Keycode.SDLK_KP_MEMRECALL:
                                Keyboard.AddKey(Key.KP_MEMRECALL);
                                break;
                            case SDL_Keycode.SDLK_KP_MEMSTORE:
                                Keyboard.AddKey(Key.KP_MEMSTORE);
                                break;
                            case SDL_Keycode.SDLK_KP_MEMSUBTRACT:
                                Keyboard.AddKey(Key.KP_MEMSUBTRACT);
                                break;
                            case SDL_Keycode.SDLK_KP_MINUS:
                                Keyboard.AddKey(Key.KP_MINUS);
                                break;
                            case SDL_Keycode.SDLK_KP_MULTIPLY:
                                Keyboard.AddKey(Key.KP_MULTIPLY);
                                break;
                            case SDL_Keycode.SDLK_KP_OCTAL:
                                Keyboard.AddKey(Key.KP_OCTAL);
                                break;
                            case SDL_Keycode.SDLK_KP_PERCENT:
                                Keyboard.AddKey(Key.KP_PERCENT);
                                break;
                            case SDL_Keycode.SDLK_KP_PERIOD:
                                Keyboard.AddKey(Key.KP_PERIOD);
                                break;
                            case SDL_Keycode.SDLK_KP_PLUS:
                                Keyboard.AddKey(Key.KP_PLUS);
                                break;
                            case SDL_Keycode.SDLK_KP_PLUSMINUS:
                                Keyboard.AddKey(Key.KP_PLUSMINUS);
                                break;
                            case SDL_Keycode.SDLK_KP_POWER:
                                Keyboard.AddKey(Key.KP_POWER);
                                break;
                            case SDL_Keycode.SDLK_KP_RIGHTBRACE:
                                Keyboard.AddKey(Key.KP_RIGHTBRACE);
                                break;
                            case SDL_Keycode.SDLK_KP_RIGHTPAREN:
                                Keyboard.AddKey(Key.KP_RIGHTPAREN);
                                break;
                            case SDL_Keycode.SDLK_KP_SPACE:
                                Keyboard.AddKey(Key.KP_SPACE);
                                break;
                            case SDL_Keycode.SDLK_KP_TAB:
                                Keyboard.AddKey(Key.KP_TAB);
                                break;
                            case SDL_Keycode.SDLK_KP_VERTICALBAR:
                                Keyboard.AddKey(Key.KP_VERTICALBAR);
                                break;
                            case SDL_Keycode.SDLK_KP_XOR:
                                Keyboard.AddKey(Key.KP_XOR);
                                break;
                            case SDL_Keycode.SDLK_l:
                                Keyboard.AddKey(Key.L);
                                break;
                            case SDL_Keycode.SDLK_LEFT:
                                Keyboard.AddKey(Key.LEFT);
                                break;
                            case SDL_Keycode.SDLK_LEFTBRACKET:
                                Keyboard.AddKey(Key.LEFTBRACKET);
                                break;
                            case SDL_Keycode.SDLK_LGUI:
                                Keyboard.AddKey(Key.LGUI);
                                break;
                            case SDL_Keycode.SDLK_m:
                                Keyboard.AddKey(Key.M);
                                break;
                            case SDL_Keycode.SDLK_MAIL:
                                Keyboard.AddKey(Key.MAIL);
                                break;
                            case SDL_Keycode.SDLK_MEDIASELECT:
                                Keyboard.AddKey(Key.MEDIASELECT);
                                break;
                            case SDL_Keycode.SDLK_MENU:
                                Keyboard.AddKey(Key.MENU);
                                break;
                            case SDL_Keycode.SDLK_MINUS:
                                Keyboard.AddKey(Key.MINUS);
                                break;
                            case SDL_Keycode.SDLK_MODE:
                                Keyboard.AddKey(Key.MODE);
                                break;
                            case SDL_Keycode.SDLK_MUTE:
                                Keyboard.AddKey(Key.MUTE);
                                break;
                            case SDL_Keycode.SDLK_n:
                                Keyboard.AddKey(Key.N);
                                break;
                            case SDL_Keycode.SDLK_NUMLOCKCLEAR:
                                Keyboard.AddKey(Key.NUMLOCKCLEAR);
                                break;
                            case SDL_Keycode.SDLK_o:
                                Keyboard.AddKey(Key.O);
                                break;
                            case SDL_Keycode.SDLK_OPER:
                                Keyboard.AddKey(Key.OPER);
                                break;
                            case SDL_Keycode.SDLK_OUT:
                                Keyboard.AddKey(Key.OUT);
                                break;
                            case SDL_Keycode.SDLK_p:
                                Keyboard.AddKey(Key.P);
                                break;
                            case SDL_Keycode.SDLK_PAGEDOWN:
                                Keyboard.AddKey(Key.PAGEDOWN);
                                break;
                            case SDL_Keycode.SDLK_PAGEUP:
                                Keyboard.AddKey(Key.PAGEUP);
                                break;
                            case SDL_Keycode.SDLK_PASTE:
                                Keyboard.AddKey(Key.PASTE);
                                break;
                            case SDL_Keycode.SDLK_PAUSE:
                                Keyboard.AddKey(Key.PAUSE);
                                break;
                            case SDL_Keycode.SDLK_PERIOD:
                                Keyboard.AddKey(Key.PERIOD);
                                break;
                            case SDL_Keycode.SDLK_POWER:
                                Keyboard.AddKey(Key.POWER);
                                break;
                            case SDL_Keycode.SDLK_PRINTSCREEN:
                                Keyboard.AddKey(Key.PRINTSCREEN);
                                break;
                            case SDL_Keycode.SDLK_PRIOR:
                                Keyboard.AddKey(Key.PRIOR);
                                break;
                            case SDL_Keycode.SDLK_q:
                                Keyboard.AddKey(Key.Q);
                                break;
                            case SDL_Keycode.SDLK_r:
                                Keyboard.AddKey(Key.R);
                                break;
                            case SDL_Keycode.SDLK_RETURN:
                                Keyboard.AddKey(Key.RETURN);
                                break;
                            case SDL_Keycode.SDLK_RETURN2:
                                Keyboard.AddKey(Key.RETURN2);
                                break;
                            case SDL_Keycode.SDLK_RGUI:
                                Keyboard.AddKey(Key.RGUI);
                                break;
                            case SDL_Keycode.SDLK_RIGHT:
                                Keyboard.AddKey(Key.RIGHT);
                                break;
                            case SDL_Keycode.SDLK_RIGHTBRACKET:
                                Keyboard.AddKey(Key.RIGHTBRACKET);
                                break;
                            case SDL_Keycode.SDLK_s:
                                Keyboard.AddKey(Key.S);
                                break;
                            case SDL_Keycode.SDLK_SCROLLLOCK:
                                Keyboard.AddKey(Key.SCROLLLOCK);
                                break;
                            case SDL_Keycode.SDLK_SELECT:
                                Keyboard.AddKey(Key.SELECT);
                                break;
                            case SDL_Keycode.SDLK_SEMICOLON:
                                Keyboard.AddKey(Key.SEMICOLON);
                                break;
                            case SDL_Keycode.SDLK_SEPARATOR:
                                Keyboard.AddKey(Key.SEPARATOR);
                                break;
                            case SDL_Keycode.SDLK_SLASH:
                                Keyboard.AddKey(Key.SLASH);
                                break;
                            case SDL_Keycode.SDLK_SLEEP:
                                Keyboard.AddKey(Key.SLEEP);
                                break;
                            case SDL_Keycode.SDLK_SPACE:
                                Keyboard.AddKey(Key.SPACE);
                                break;
                            case SDL_Keycode.SDLK_STOP:
                                Keyboard.AddKey(Key.STOP);
                                break;
                            case SDL_Keycode.SDLK_SYSREQ:
                                Keyboard.AddKey(Key.SYSREQ);
                                break;
                            case SDL_Keycode.SDLK_t:
                                Keyboard.AddKey(Key.T);
                                break;
                            case SDL_Keycode.SDLK_TAB:
                                Keyboard.AddKey(Key.TAB);
                                break;
                            case SDL_Keycode.SDLK_THOUSANDSSEPARATOR:
                                Keyboard.AddKey(Key.THOUSANDSSEPARATOR);
                                break;
                            case SDL_Keycode.SDLK_u:
                                Keyboard.AddKey(Key.U);
                                break;
                            case SDL_Keycode.SDLK_UNDO:
                                Keyboard.AddKey(Key.UNDO);
                                break;
                            case SDL_Keycode.SDLK_UNKNOWN:
                                Keyboard.AddKey(Key.UNKNOWN);
                                break;
                            case SDL_Keycode.SDLK_UP:
                                Keyboard.AddKey(Key.UP);
                                break;
                            case SDL_Keycode.SDLK_v:
                                Keyboard.AddKey(Key.V);
                                break;
                            case SDL_Keycode.SDLK_VOLUMEDOWN:
                                Keyboard.AddKey(Key.VOLUMEDOWN);
                                break;
                            case SDL_Keycode.SDLK_VOLUMEUP:
                                Keyboard.AddKey(Key.VOLUMEUP);
                                break;
                            case SDL_Keycode.SDLK_w:
                                Keyboard.AddKey(Key.W);
                                break;
                            case SDL_Keycode.SDLK_WWW:
                                Keyboard.AddKey(Key.WWW);
                                break;
                            case SDL_Keycode.SDLK_x:
                                Keyboard.AddKey(Key.X);
                                break;
                            case SDL_Keycode.SDLK_y:
                                Keyboard.AddKey(Key.Y);
                                break;
                            case SDL_Keycode.SDLK_z:
                                Keyboard.AddKey(Key.Z);
                                break;
                            case SDL_Keycode.SDLK_AMPERSAND:
                                Keyboard.AddKey(Key.AMPERSAND);
                                break;
                            case SDL_Keycode.SDLK_ASTERISK:
                                Keyboard.AddKey(Key.ASTERISK);
                                break;
                            case SDL_Keycode.SDLK_AT:
                                Keyboard.AddKey(Key.AT);
                                break;
                            case SDL_Keycode.SDLK_CARET:
                                Keyboard.AddKey(Key.CARET);
                                break;
                            case SDL_Keycode.SDLK_COLON:
                                Keyboard.AddKey(Key.COLON);
                                break;
                            case SDL_Keycode.SDLK_DOLLAR:
                                Keyboard.AddKey(Key.DOLLAR);
                                break;
                            case SDL_Keycode.SDLK_EXCLAIM:
                                Keyboard.AddKey(Key.EXCLAIM);
                                break;
                            case SDL_Keycode.SDLK_GREATER:
                                Keyboard.AddKey(Key.GREATER);
                                break;
                            case SDL_Keycode.SDLK_HASH:
                                Keyboard.AddKey(Key.HASH);
                                break;
                            case SDL_Keycode.SDLK_LEFTPAREN:
                                Keyboard.AddKey(Key.LEFTPAREN);
                                break;
                            case SDL_Keycode.SDLK_LESS:
                                Keyboard.AddKey(Key.LESS);
                                break;
                            case SDL_Keycode.SDLK_PERCENT:
                                Keyboard.AddKey(Key.PERCENT);
                                break;
                            case SDL_Keycode.SDLK_PLUS:
                                Keyboard.AddKey(Key.PLUS);
                                break;
                            case SDL_Keycode.SDLK_QUESTION:
                                Keyboard.AddKey(Key.QUESTION);
                                break;
                            case SDL_Keycode.SDLK_QUOTEDBL:
                                Keyboard.AddKey(Key.QUOTEDBL);
                                break;
                            case SDL_Keycode.SDLK_RIGHTPAREN:
                                Keyboard.AddKey(Key.RIGHTPAREN);
                                break;
                            case SDL_Keycode.SDLK_UNDERSCORE:
                                Keyboard.AddKey(Key.UNDERSCORE);
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
                        if(FUI.selectedTextField != null) {
                            break;
                        }

                        switch (e.key.keysym.sym)
                        {
                            case SDL_Keycode.SDLK_0:
                                Keyboard.downKeys.Remove(Key.N0);
                                break;
                            case SDL_Keycode.SDLK_1:
                                Keyboard.downKeys.Remove(Key.N1);
                                break;
                            case SDL_Keycode.SDLK_2:
                                Keyboard.downKeys.Remove(Key.N2);
                                break;
                            case SDL_Keycode.SDLK_3:
                                Keyboard.downKeys.Remove(Key.N3);
                                break;
                            case SDL_Keycode.SDLK_4:
                                Keyboard.downKeys.Remove(Key.N4);
                                break;
                            case SDL_Keycode.SDLK_5:
                                Keyboard.downKeys.Remove(Key.N5);
                                break;
                            case SDL_Keycode.SDLK_6:
                                Keyboard.downKeys.Remove(Key.N6);
                                break;
                            case SDL_Keycode.SDLK_7:
                                Keyboard.downKeys.Remove(Key.N7);
                                break;
                            case SDL_Keycode.SDLK_8:
                                Keyboard.downKeys.Remove(Key.N8);
                                break;
                            case SDL_Keycode.SDLK_9:
                                Keyboard.downKeys.Remove(Key.N9);
                                break;
                            case SDL_Keycode.SDLK_a:
                                Keyboard.downKeys.Remove(Key.A);
                                break;
                            case SDL_Keycode.SDLK_AC_BACK:
                                Keyboard.downKeys.Remove(Key.AC_BACK);
                                break;
                            case SDL_Keycode.SDLK_AC_BOOKMARKS:
                                Keyboard.downKeys.Remove(Key.AC_BOOKMARKS);
                                break;
                            case SDL_Keycode.SDLK_AC_FORWARD:
                                Keyboard.downKeys.Remove(Key.AC_FORWARD);
                                break;
                            case SDL_Keycode.SDLK_AC_HOME:
                                Keyboard.downKeys.Remove(Key.AC_HOME);
                                break;
                            case SDL_Keycode.SDLK_AC_REFRESH:
                                Keyboard.downKeys.Remove(Key.AC_REFRESH);
                                break;
                            case SDL_Keycode.SDLK_AC_SEARCH:
                                Keyboard.downKeys.Remove(Key.AC_SEARCH);
                                break;
                            case SDL_Keycode.SDLK_AC_STOP:
                                Keyboard.downKeys.Remove(Key.AC_STOP);
                                break;
                            case SDL_Keycode.SDLK_AGAIN:
                                Keyboard.downKeys.Remove(Key.AGAIN);
                                break;
                            case SDL_Keycode.SDLK_ALTERASE:
                                Keyboard.downKeys.Remove(Key.ALTERASE);
                                break;
                            case SDL_Keycode.SDLK_QUOTE:
                                Keyboard.downKeys.Remove(Key.QUOTE);
                                break;
                            case SDL_Keycode.SDLK_APPLICATION:
                                Keyboard.downKeys.Remove(Key.APPLICATION);
                                break;
                            case SDL_Keycode.SDLK_AUDIOMUTE:
                                Keyboard.downKeys.Remove(Key.AUDIOMUTE);
                                break;
                            case SDL_Keycode.SDLK_AUDIONEXT:
                                Keyboard.downKeys.Remove(Key.AUDIONEXT);
                                break;
                            case SDL_Keycode.SDLK_AUDIOPLAY:
                                Keyboard.downKeys.Remove(Key.AUDIOPLAY);
                                break;
                            case SDL_Keycode.SDLK_AUDIOPREV:
                                Keyboard.downKeys.Remove(Key.AUDIOPREV);
                                break;
                            case SDL_Keycode.SDLK_AUDIOSTOP:
                                Keyboard.downKeys.Remove(Key.AUDIOSTOP);
                                break;
                            case SDL_Keycode.SDLK_b:
                                Keyboard.downKeys.Remove(Key.B);
                                break;
                            case SDL_Keycode.SDLK_BACKSLASH:
                                Keyboard.downKeys.Remove(Key.BACKSLASH);
                                break;
                            case SDL_Keycode.SDLK_BACKSPACE:
                                Keyboard.downKeys.Remove(Key.BACKSPACE);
                                break;
                            case SDL_Keycode.SDLK_BRIGHTNESSDOWN:
                                Keyboard.downKeys.Remove(Key.BRIGHTNESSDOWN);
                                break;
                            case SDL_Keycode.SDLK_BRIGHTNESSUP:
                                Keyboard.downKeys.Remove(Key.BRIGHTNESSUP);
                                break;
                            case SDL_Keycode.SDLK_c:
                                Keyboard.downKeys.Remove(Key.C);
                                break;
                            case SDL_Keycode.SDLK_CALCULATOR:
                                Keyboard.downKeys.Remove(Key.CALCULATOR);
                                break;
                            case SDL_Keycode.SDLK_CANCEL:
                                Keyboard.downKeys.Remove(Key.CANCEL);
                                break;
                            case SDL_Keycode.SDLK_CAPSLOCK:
                                Keyboard.downKeys.Remove(Key.CAPSLOCK);
                                break;
                            case SDL_Keycode.SDLK_CLEAR:
                                Keyboard.downKeys.Remove(Key.CLEAR);
                                break;
                            case SDL_Keycode.SDLK_CLEARAGAIN:
                                Keyboard.downKeys.Remove(Key.CLEARAGAIN);
                                break;
                            case SDL_Keycode.SDLK_COMMA:
                                Keyboard.downKeys.Remove(Key.COMMA);
                                break;
                            case SDL_Keycode.SDLK_COMPUTER:
                                Keyboard.downKeys.Remove(Key.COMPUTER);
                                break;
                            case SDL_Keycode.SDLK_COPY:
                                Keyboard.downKeys.Remove(Key.COPY);
                                break;
                            case SDL_Keycode.SDLK_CRSEL:
                                Keyboard.downKeys.Remove(Key.CRSEL);
                                break;
                            case SDL_Keycode.SDLK_CURRENCYSUBUNIT:
                                Keyboard.downKeys.Remove(Key.CURRENCYSUBUNIT);
                                break;
                            case SDL_Keycode.SDLK_CURRENCYUNIT:
                                Keyboard.downKeys.Remove(Key.CURRENCYUNIT);
                                break;
                            case SDL_Keycode.SDLK_CUT:
                                Keyboard.downKeys.Remove(Key.CUT);
                                break;
                            case SDL_Keycode.SDLK_d:
                                Keyboard.downKeys.Remove(Key.D);
                                break;
                            case SDL_Keycode.SDLK_DECIMALSEPARATOR:
                                Keyboard.downKeys.Remove(Key.DECIMALSEPARATOR);
                                break;
                            case SDL_Keycode.SDLK_DELETE:
                                Keyboard.downKeys.Remove(Key.DELETE);
                                break;
                            case SDL_Keycode.SDLK_DISPLAYSWITCH:
                                Keyboard.downKeys.Remove(Key.DISPLAYSWITCH);
                                break;
                            case SDL_Keycode.SDLK_DOWN:
                                Keyboard.downKeys.Remove(Key.DOWN);
                                break;
                            case SDL_Keycode.SDLK_e:
                                Keyboard.downKeys.Remove(Key.E);
                                break;
                            case SDL_Keycode.SDLK_EJECT:
                                Keyboard.downKeys.Remove(Key.EJECT);
                                break;
                            case SDL_Keycode.SDLK_END:
                                Keyboard.downKeys.Remove(Key.END);
                                break;
                            case SDL_Keycode.SDLK_EQUALS:
                                Keyboard.downKeys.Remove(Key.EQUALS);
                                break;
                            case SDL_Keycode.SDLK_ESCAPE:
                                Keyboard.downKeys.Remove(Key.ESCAPE);
                                break;
                            case SDL_Keycode.SDLK_EXECUTE:
                                Keyboard.downKeys.Remove(Key.EXECUTE);
                                break;
                            case SDL_Keycode.SDLK_EXSEL:
                                Keyboard.downKeys.Remove(Key.EXSEL);
                                break;
                            case SDL_Keycode.SDLK_f:
                                Keyboard.downKeys.Remove(Key.F);
                                break;
                            case SDL_Keycode.SDLK_F1:
                                Keyboard.downKeys.Remove(Key.F1);
                                break;
                            case SDL_Keycode.SDLK_F10:
                                Keyboard.downKeys.Remove(Key.F10);
                                break;
                            case SDL_Keycode.SDLK_F11:
                                Keyboard.downKeys.Remove(Key.F11);
                                break;
                            case SDL_Keycode.SDLK_F12:
                                Keyboard.downKeys.Remove(Key.F12);
                                break;
                            case SDL_Keycode.SDLK_F13:
                                Keyboard.downKeys.Remove(Key.F13);
                                break;
                            case SDL_Keycode.SDLK_F14:
                                Keyboard.downKeys.Remove(Key.F14);
                                break;
                            case SDL_Keycode.SDLK_F15:
                                Keyboard.downKeys.Remove(Key.F15);
                                break;
                            case SDL_Keycode.SDLK_F16:
                                Keyboard.downKeys.Remove(Key.F16);
                                break;
                            case SDL_Keycode.SDLK_F17:
                                Keyboard.downKeys.Remove(Key.F17);
                                break;
                            case SDL_Keycode.SDLK_F18:
                                Keyboard.downKeys.Remove(Key.F18);
                                break;
                            case SDL_Keycode.SDLK_F19:
                                Keyboard.downKeys.Remove(Key.F19);
                                break;
                            case SDL_Keycode.SDLK_F2:
                                Keyboard.downKeys.Remove(Key.F2);
                                break;
                            case SDL_Keycode.SDLK_F20:
                                Keyboard.downKeys.Remove(Key.F20);
                                break;
                            case SDL_Keycode.SDLK_F21:
                                Keyboard.downKeys.Remove(Key.F21);
                                break;
                            case SDL_Keycode.SDLK_F22:
                                Keyboard.downKeys.Remove(Key.F22);
                                break;
                            case SDL_Keycode.SDLK_F23:
                                Keyboard.downKeys.Remove(Key.F23);
                                break;
                            case SDL_Keycode.SDLK_F24:
                                Keyboard.downKeys.Remove(Key.F24);
                                break;
                            case SDL_Keycode.SDLK_F3:
                                Keyboard.downKeys.Remove(Key.F3);
                                break;
                            case SDL_Keycode.SDLK_F4:
                                Keyboard.downKeys.Remove(Key.F4);
                                break;
                            case SDL_Keycode.SDLK_F5:
                                Keyboard.downKeys.Remove(Key.F5);
                                break;
                            case SDL_Keycode.SDLK_F6:
                                Keyboard.downKeys.Remove(Key.F6);
                                break;
                            case SDL_Keycode.SDLK_F7:
                                Keyboard.downKeys.Remove(Key.F7);
                                break;
                            case SDL_Keycode.SDLK_F8:
                                Keyboard.downKeys.Remove(Key.F8);
                                break;
                            case SDL_Keycode.SDLK_F9:
                                Keyboard.downKeys.Remove(Key.F9);
                                break;
                            case SDL_Keycode.SDLK_FIND:
                                Keyboard.downKeys.Remove(Key.FIND);
                                break;
                            case SDL_Keycode.SDLK_g:
                                Keyboard.downKeys.Remove(Key.G);
                                break;
                            case SDL_Keycode.SDLK_BACKQUOTE:
                                Keyboard.downKeys.Remove(Key.BACKQUOTE);
                                break;
                            case SDL_Keycode.SDLK_h:
                                Keyboard.downKeys.Remove(Key.H);
                                break;
                            case SDL_Keycode.SDLK_HELP:
                                Keyboard.downKeys.Remove(Key.HELP);
                                break;
                            case SDL_Keycode.SDLK_HOME:
                                Keyboard.downKeys.Remove(Key.HOME);
                                break;
                            case SDL_Keycode.SDLK_i:
                                Keyboard.downKeys.Remove(Key.I);
                                break;
                            case SDL_Keycode.SDLK_INSERT:
                                Keyboard.downKeys.Remove(Key.INSERT);
                                break;
                            case SDL_Keycode.SDLK_j:
                                Keyboard.downKeys.Remove(Key.J);
                                break;
                            case SDL_Keycode.SDLK_k:
                                Keyboard.downKeys.Remove(Key.K);
                                break;
                            case SDL_Keycode.SDLK_KBDILLUMDOWN:
                                Keyboard.downKeys.Remove(Key.KBDILLUMDOWN);
                                break;
                            case SDL_Keycode.SDLK_KBDILLUMTOGGLE:
                                Keyboard.downKeys.Remove(Key.KBDILLUMTOGGLE);
                                break;
                            case SDL_Keycode.SDLK_KBDILLUMUP:
                                Keyboard.downKeys.Remove(Key.KBDILLUMUP);
                                break;
                            case SDL_Keycode.SDLK_KP_0:
                                Keyboard.downKeys.Remove(Key.KP_0);
                                break;
                            case SDL_Keycode.SDLK_KP_00:
                                Keyboard.downKeys.Remove(Key.KP_00);
                                break;
                            case SDL_Keycode.SDLK_KP_000:
                                Keyboard.downKeys.Remove(Key.KP_000);
                                break;
                            case SDL_Keycode.SDLK_KP_1:
                                Keyboard.downKeys.Remove(Key.KP_1);
                                break;
                            case SDL_Keycode.SDLK_KP_2:
                                Keyboard.downKeys.Remove(Key.KP_2);
                                break;
                            case SDL_Keycode.SDLK_KP_3:
                                Keyboard.downKeys.Remove(Key.KP_3);
                                break;
                            case SDL_Keycode.SDLK_KP_4:
                                Keyboard.downKeys.Remove(Key.KP_4);
                                break;
                            case SDL_Keycode.SDLK_KP_5:
                                Keyboard.downKeys.Remove(Key.KP_5);
                                break;
                            case SDL_Keycode.SDLK_KP_6:
                                Keyboard.downKeys.Remove(Key.KP_6);
                                break;
                            case SDL_Keycode.SDLK_KP_7:
                                Keyboard.downKeys.Remove(Key.KP_7);
                                break;
                            case SDL_Keycode.SDLK_KP_8:
                                Keyboard.downKeys.Remove(Key.KP_8);
                                break;
                            case SDL_Keycode.SDLK_KP_9:
                                Keyboard.downKeys.Remove(Key.KP_9);
                                break;
                            case SDL_Keycode.SDLK_KP_A:
                                Keyboard.downKeys.Remove(Key.KP_A);
                                break;
                            case SDL_Keycode.SDLK_KP_AMPERSAND:
                                Keyboard.downKeys.Remove(Key.KP_AMPERSAND);
                                break;
                            case SDL_Keycode.SDLK_KP_AT:
                                Keyboard.downKeys.Remove(Key.KP_AT);
                                break;
                            case SDL_Keycode.SDLK_KP_B:
                                Keyboard.downKeys.Remove(Key.KP_B);
                                break;
                            case SDL_Keycode.SDLK_KP_BACKSPACE:
                                Keyboard.downKeys.Remove(Key.KP_BACKSPACE);
                                break;
                            case SDL_Keycode.SDLK_KP_BINARY:
                                Keyboard.downKeys.Remove(Key.KP_BINARY);
                                break;
                            case SDL_Keycode.SDLK_KP_C:
                                Keyboard.downKeys.Remove(Key.KP_C);
                                break;
                            case SDL_Keycode.SDLK_KP_CLEAR:
                                Keyboard.downKeys.Remove(Key.KP_CLEAR);
                                break;
                            case SDL_Keycode.SDLK_KP_CLEARENTRY:
                                Keyboard.downKeys.Remove(Key.KP_CLEARENTRY);
                                break;
                            case SDL_Keycode.SDLK_KP_COLON:
                                Keyboard.downKeys.Remove(Key.KP_COLON);
                                break;
                            case SDL_Keycode.SDLK_KP_COMMA:
                                Keyboard.downKeys.Remove(Key.KP_COMMA);
                                break;
                            case SDL_Keycode.SDLK_KP_D:
                                Keyboard.downKeys.Remove(Key.KP_D);
                                break;
                            case SDL_Keycode.SDLK_KP_DBLAMPERSAND:
                                Keyboard.downKeys.Remove(Key.KP_DBLAMPERSAND);
                                break;
                            case SDL_Keycode.SDLK_KP_DBLVERTICALBAR:
                                Keyboard.downKeys.Remove(Key.KP_DBLVERTICALBAR);
                                break;
                            case SDL_Keycode.SDLK_KP_DECIMAL:
                                Keyboard.downKeys.Remove(Key.KP_DECIMAL);
                                break;
                            case SDL_Keycode.SDLK_KP_DIVIDE:
                                Keyboard.downKeys.Remove(Key.KP_DIVIDE);
                                break;
                            case SDL_Keycode.SDLK_KP_E:
                                Keyboard.downKeys.Remove(Key.KP_E);
                                break;
                            case SDL_Keycode.SDLK_KP_ENTER:
                                Keyboard.downKeys.Remove(Key.KP_ENTER);
                                break;
                            case SDL_Keycode.SDLK_KP_EQUALS:
                                Keyboard.downKeys.Remove(Key.KP_EQUALS);
                                break;
                            case SDL_Keycode.SDLK_KP_EQUALSAS400:
                                Keyboard.downKeys.Remove(Key.KP_EQUALSAS400);
                                break;
                            case SDL_Keycode.SDLK_KP_EXCLAM:
                                Keyboard.downKeys.Remove(Key.KP_EXCLAM);
                                break;
                            case SDL_Keycode.SDLK_KP_F:
                                Keyboard.downKeys.Remove(Key.KP_F);
                                break;
                            case SDL_Keycode.SDLK_KP_GREATER:
                                Keyboard.downKeys.Remove(Key.KP_GREATER);
                                break;
                            case SDL_Keycode.SDLK_KP_HASH:
                                Keyboard.downKeys.Remove(Key.KP_HASH);
                                break;
                            case SDL_Keycode.SDLK_KP_HEXADECIMAL:
                                Keyboard.downKeys.Remove(Key.KP_HEXADECIMAL);
                                break;
                            case SDL_Keycode.SDLK_KP_LEFTBRACE:
                                Keyboard.downKeys.Remove(Key.KP_LEFTBRACE);
                                break;
                            case SDL_Keycode.SDLK_KP_LEFTPAREN:
                                Keyboard.downKeys.Remove(Key.KP_LEFTPAREN);
                                break;
                            case SDL_Keycode.SDLK_KP_LESS:
                                Keyboard.downKeys.Remove(Key.KP_LESS);
                                break;
                            case SDL_Keycode.SDLK_KP_MEMADD:
                                Keyboard.downKeys.Remove(Key.KP_MEMADD);
                                break;
                            case SDL_Keycode.SDLK_KP_MEMCLEAR:
                                Keyboard.downKeys.Remove(Key.KP_MEMCLEAR);
                                break;
                            case SDL_Keycode.SDLK_KP_MEMDIVIDE:
                                Keyboard.downKeys.Remove(Key.KP_MEMDIVIDE);
                                break;
                            case SDL_Keycode.SDLK_KP_MEMMULTIPLY:
                                Keyboard.downKeys.Remove(Key.KP_MEMMULTIPLY);
                                break;
                            case SDL_Keycode.SDLK_KP_MEMRECALL:
                                Keyboard.downKeys.Remove(Key.KP_MEMRECALL);
                                break;
                            case SDL_Keycode.SDLK_KP_MEMSTORE:
                                Keyboard.downKeys.Remove(Key.KP_MEMSTORE);
                                break;
                            case SDL_Keycode.SDLK_KP_MEMSUBTRACT:
                                Keyboard.downKeys.Remove(Key.KP_MEMSUBTRACT);
                                break;
                            case SDL_Keycode.SDLK_KP_MINUS:
                                Keyboard.downKeys.Remove(Key.KP_MINUS);
                                break;
                            case SDL_Keycode.SDLK_KP_MULTIPLY:
                                Keyboard.downKeys.Remove(Key.KP_MULTIPLY);
                                break;
                            case SDL_Keycode.SDLK_KP_OCTAL:
                                Keyboard.downKeys.Remove(Key.KP_OCTAL);
                                break;
                            case SDL_Keycode.SDLK_KP_PERCENT:
                                Keyboard.downKeys.Remove(Key.KP_PERCENT);
                                break;
                            case SDL_Keycode.SDLK_KP_PERIOD:
                                Keyboard.downKeys.Remove(Key.KP_PERIOD);
                                break;
                            case SDL_Keycode.SDLK_KP_PLUS:
                                Keyboard.downKeys.Remove(Key.KP_PLUS);
                                break;
                            case SDL_Keycode.SDLK_KP_PLUSMINUS:
                                Keyboard.downKeys.Remove(Key.KP_PLUSMINUS);
                                break;
                            case SDL_Keycode.SDLK_KP_POWER:
                                Keyboard.downKeys.Remove(Key.KP_POWER);
                                break;
                            case SDL_Keycode.SDLK_KP_RIGHTBRACE:
                                Keyboard.downKeys.Remove(Key.KP_RIGHTBRACE);
                                break;
                            case SDL_Keycode.SDLK_KP_RIGHTPAREN:
                                Keyboard.downKeys.Remove(Key.KP_RIGHTPAREN);
                                break;
                            case SDL_Keycode.SDLK_KP_SPACE:
                                Keyboard.downKeys.Remove(Key.KP_SPACE);
                                break;
                            case SDL_Keycode.SDLK_KP_TAB:
                                Keyboard.downKeys.Remove(Key.KP_TAB);
                                break;
                            case SDL_Keycode.SDLK_KP_VERTICALBAR:
                                Keyboard.downKeys.Remove(Key.KP_VERTICALBAR);
                                break;
                            case SDL_Keycode.SDLK_KP_XOR:
                                Keyboard.downKeys.Remove(Key.KP_XOR);
                                break;
                            case SDL_Keycode.SDLK_l:
                                Keyboard.downKeys.Remove(Key.L);
                                break;
                            case SDL_Keycode.SDLK_LEFT:
                                Keyboard.downKeys.Remove(Key.LEFT);
                                break;
                            case SDL_Keycode.SDLK_LEFTBRACKET:
                                Keyboard.downKeys.Remove(Key.LEFTBRACKET);
                                break;
                            case SDL_Keycode.SDLK_LGUI:
                                Keyboard.downKeys.Remove(Key.LGUI);
                                break;
                            case SDL_Keycode.SDLK_m:
                                Keyboard.downKeys.Remove(Key.M);
                                break;
                            case SDL_Keycode.SDLK_MAIL:
                                Keyboard.downKeys.Remove(Key.MAIL);
                                break;
                            case SDL_Keycode.SDLK_MEDIASELECT:
                                Keyboard.downKeys.Remove(Key.MEDIASELECT);
                                break;
                            case SDL_Keycode.SDLK_MENU:
                                Keyboard.downKeys.Remove(Key.MENU);
                                break;
                            case SDL_Keycode.SDLK_MINUS:
                                Keyboard.downKeys.Remove(Key.MINUS);
                                break;
                            case SDL_Keycode.SDLK_MODE:
                                Keyboard.downKeys.Remove(Key.MODE);
                                break;
                            case SDL_Keycode.SDLK_MUTE:
                                Keyboard.downKeys.Remove(Key.MUTE);
                                break;
                            case SDL_Keycode.SDLK_n:
                                Keyboard.downKeys.Remove(Key.N);
                                break;
                            case SDL_Keycode.SDLK_NUMLOCKCLEAR:
                                Keyboard.downKeys.Remove(Key.NUMLOCKCLEAR);
                                break;
                            case SDL_Keycode.SDLK_o:
                                Keyboard.downKeys.Remove(Key.O);
                                break;
                            case SDL_Keycode.SDLK_OPER:
                                Keyboard.downKeys.Remove(Key.OPER);
                                break;
                            case SDL_Keycode.SDLK_OUT:
                                Keyboard.downKeys.Remove(Key.OUT);
                                break;
                            case SDL_Keycode.SDLK_p:
                                Keyboard.downKeys.Remove(Key.P);
                                break;
                            case SDL_Keycode.SDLK_PAGEDOWN:
                                Keyboard.downKeys.Remove(Key.PAGEDOWN);
                                break;
                            case SDL_Keycode.SDLK_PAGEUP:
                                Keyboard.downKeys.Remove(Key.PAGEUP);
                                break;
                            case SDL_Keycode.SDLK_PASTE:
                                Keyboard.downKeys.Remove(Key.PASTE);
                                break;
                            case SDL_Keycode.SDLK_PAUSE:
                                Keyboard.downKeys.Remove(Key.PAUSE);
                                break;
                            case SDL_Keycode.SDLK_PERIOD:
                                Keyboard.downKeys.Remove(Key.PERIOD);
                                break;
                            case SDL_Keycode.SDLK_POWER:
                                Keyboard.downKeys.Remove(Key.POWER);
                                break;
                            case SDL_Keycode.SDLK_PRINTSCREEN:
                                Keyboard.downKeys.Remove(Key.PRINTSCREEN);
                                break;
                            case SDL_Keycode.SDLK_PRIOR:
                                Keyboard.downKeys.Remove(Key.PRIOR);
                                break;
                            case SDL_Keycode.SDLK_q:
                                Keyboard.downKeys.Remove(Key.Q);
                                break;
                            case SDL_Keycode.SDLK_r:
                                Keyboard.downKeys.Remove(Key.R);
                                break;
                            case SDL_Keycode.SDLK_RETURN:
                                Keyboard.downKeys.Remove(Key.RETURN);
                                break;
                            case SDL_Keycode.SDLK_RETURN2:
                                Keyboard.downKeys.Remove(Key.RETURN2);
                                break;
                            case SDL_Keycode.SDLK_RGUI:
                                Keyboard.downKeys.Remove(Key.RGUI);
                                break;
                            case SDL_Keycode.SDLK_RIGHT:
                                Keyboard.downKeys.Remove(Key.RIGHT);
                                break;
                            case SDL_Keycode.SDLK_RIGHTBRACKET:
                                Keyboard.downKeys.Remove(Key.RIGHTBRACKET);
                                break;
                            case SDL_Keycode.SDLK_s:
                                Keyboard.downKeys.Remove(Key.S);
                                break;
                            case SDL_Keycode.SDLK_SCROLLLOCK:
                                Keyboard.downKeys.Remove(Key.SCROLLLOCK);
                                break;
                            case SDL_Keycode.SDLK_SELECT:
                                Keyboard.downKeys.Remove(Key.SELECT);
                                break;
                            case SDL_Keycode.SDLK_SEMICOLON:
                                Keyboard.downKeys.Remove(Key.SEMICOLON);
                                break;
                            case SDL_Keycode.SDLK_SEPARATOR:
                                Keyboard.downKeys.Remove(Key.SEPARATOR);
                                break;
                            case SDL_Keycode.SDLK_SLASH:
                                Keyboard.downKeys.Remove(Key.SLASH);
                                break;
                            case SDL_Keycode.SDLK_SLEEP:
                                Keyboard.downKeys.Remove(Key.SLEEP);
                                break;
                            case SDL_Keycode.SDLK_SPACE:
                                Keyboard.downKeys.Remove(Key.SPACE);
                                break;
                            case SDL_Keycode.SDLK_STOP:
                                Keyboard.downKeys.Remove(Key.STOP);
                                break;
                            case SDL_Keycode.SDLK_SYSREQ:
                                Keyboard.downKeys.Remove(Key.SYSREQ);
                                break;
                            case SDL_Keycode.SDLK_t:
                                Keyboard.downKeys.Remove(Key.T);
                                break;
                            case SDL_Keycode.SDLK_TAB:
                                Keyboard.downKeys.Remove(Key.TAB);
                                break;
                            case SDL_Keycode.SDLK_THOUSANDSSEPARATOR:
                                Keyboard.downKeys.Remove(Key.THOUSANDSSEPARATOR);
                                break;
                            case SDL_Keycode.SDLK_u:
                                Keyboard.downKeys.Remove(Key.U);
                                break;
                            case SDL_Keycode.SDLK_UNDO:
                                Keyboard.downKeys.Remove(Key.UNDO);
                                break;
                            case SDL_Keycode.SDLK_UNKNOWN:
                                Keyboard.downKeys.Remove(Key.UNKNOWN);
                                break;
                            case SDL_Keycode.SDLK_UP:
                                Keyboard.downKeys.Remove(Key.UP);
                                break;
                            case SDL_Keycode.SDLK_v:
                                Keyboard.downKeys.Remove(Key.V);
                                break;
                            case SDL_Keycode.SDLK_VOLUMEDOWN:
                                Keyboard.downKeys.Remove(Key.VOLUMEDOWN);
                                break;
                            case SDL_Keycode.SDLK_VOLUMEUP:
                                Keyboard.downKeys.Remove(Key.VOLUMEUP);
                                break;
                            case SDL_Keycode.SDLK_w:
                                Keyboard.downKeys.Remove(Key.W);
                                break;
                            case SDL_Keycode.SDLK_WWW:
                                Keyboard.downKeys.Remove(Key.WWW);
                                break;
                            case SDL_Keycode.SDLK_x:
                                Keyboard.downKeys.Remove(Key.X);
                                break;
                            case SDL_Keycode.SDLK_y:
                                Keyboard.downKeys.Remove(Key.Y);
                                break;
                            case SDL_Keycode.SDLK_z:
                                Keyboard.downKeys.Remove(Key.Z);
                                break;
                            case SDL_Keycode.SDLK_AMPERSAND:
                                Keyboard.downKeys.Remove(Key.AMPERSAND);
                                break;
                            case SDL_Keycode.SDLK_ASTERISK:
                                Keyboard.downKeys.Remove(Key.ASTERISK);
                                break;
                            case SDL_Keycode.SDLK_AT:
                                Keyboard.downKeys.Remove(Key.AT);
                                break;
                            case SDL_Keycode.SDLK_CARET:
                                Keyboard.downKeys.Remove(Key.CARET);
                                break;
                            case SDL_Keycode.SDLK_COLON:
                                Keyboard.downKeys.Remove(Key.COLON);
                                break;
                            case SDL_Keycode.SDLK_DOLLAR:
                                Keyboard.downKeys.Remove(Key.DOLLAR);
                                break;
                            case SDL_Keycode.SDLK_EXCLAIM:
                                Keyboard.downKeys.Remove(Key.EXCLAIM);
                                break;
                            case SDL_Keycode.SDLK_GREATER:
                                Keyboard.downKeys.Remove(Key.GREATER);
                                break;
                            case SDL_Keycode.SDLK_HASH:
                                Keyboard.downKeys.Remove(Key.HASH);
                                break;
                            case SDL_Keycode.SDLK_LEFTPAREN:
                                Keyboard.downKeys.Remove(Key.LEFTPAREN);
                                break;
                            case SDL_Keycode.SDLK_LESS:
                                Keyboard.downKeys.Remove(Key.LESS);
                                break;
                            case SDL_Keycode.SDLK_PERCENT:
                                Keyboard.downKeys.Remove(Key.PERCENT);
                                break;
                            case SDL_Keycode.SDLK_PLUS:
                                Keyboard.downKeys.Remove(Key.PLUS);
                                break;
                            case SDL_Keycode.SDLK_QUESTION:
                                Keyboard.downKeys.Remove(Key.QUESTION);
                                break;
                            case SDL_Keycode.SDLK_QUOTEDBL:
                                Keyboard.downKeys.Remove(Key.QUOTEDBL);
                                break;
                            case SDL_Keycode.SDLK_RIGHTPAREN:
                                Keyboard.downKeys.Remove(Key.RIGHTPAREN);
                                break;
                            case SDL_Keycode.SDLK_UNDERSCORE:
                                Keyboard.downKeys.Remove(Key.UNDERSCORE);
                                break;
                            case SDL_Keycode.SDLK_LCTRL:
                                Keyboard.pressedModifiers.Remove(Mod.LCtrl);
                                break;
                            case SDL_Keycode.SDLK_LSHIFT:
                                Keyboard.pressedModifiers.Remove(Mod.LShift);
                                break;
                        }

                        break;
                    case SDL_EventType.SDL_TEXTINPUT:
                        if(FUI.selectedTextField == null) {
                            break;
                        }

                        unsafe {
                            byte[] arr = new byte[1];
                            Marshal.Copy((IntPtr)e.text.text, arr, 0, 1);
                            // Console.WriteLine(FUI.selectedTextFieldValue);
                            FUI.selectedTextFieldOnChange(FUI.selectedTextFieldValue + Encoding.UTF8.GetString(arr));
                        }
                        break;
                    case SDL_EventType.SDL_TEXTEDITING:
                        if(FUI.selectedTextField == null) {
                            break;
                        }
                        break;
                    case SDL_EventType.SDL_MOUSEMOTION:
                        SDL_GetMouseState(out int _x, out int _y);
                        Mouse.Position = new Vector2(_x, _y);
                        Mouse.RelativePosition = new Vector2((float)_x / (float)Game.Window.Width, (float)_y / (float)Game.Window.Height);
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
                    case SDL_EventType.SDL_MOUSEWHEEL:
                        if(e.wheel.y > 0) // scroll up
                        {
                            Mouse.ScrollUp = true;
                        }
                        else if(e.wheel.y < 0) // scroll down
                        {
                            Mouse.ScrollDown = true;
                        }

                        if(e.wheel.x > 0) // scroll right
                        {
                            Mouse.ScrollRight = true;
                        }
                        else if(e.wheel.x < 0) // scroll left
                        {
                            Mouse.ScrollLeft = true;
                        }
                        break;
                }
            }
        }
    }
}
