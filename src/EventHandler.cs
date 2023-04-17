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
            while (SDL_PollEvent(out SDL_Event e) != 0)
            {
                switch (e.type)
                {
                    case SDL_EventType.SDL_QUIT:
                        Game.Stop();
                        break;
                    case SDL_EventType.SDL_KEYDOWN:
                        if(FUI.selectedTextField != null && FUI.selectedTextFieldValue != null && FUI.selectedTextFieldOnChange != null && FUI.selectedTextFieldOnSumbit != null) {
                            if(e.key.keysym.sym == SDL_Keycode.SDLK_BACKSPACE) {
                                if(FUI.selectedTextFieldValue.Length > 0)
                                    FUI.selectedTextFieldOnChange(FUI.selectedTextFieldValue.Remove(FUI.selectedTextFieldValue.Length - 1));
                            } else if(e.key.keysym.sym == SDL_Keycode.SDLK_RETURN)
                            {
                                if(FUI.selectedTextFieldValue.Length > 0)
                                {
                                    FUI.selectedTextFieldOnSumbit(FUI.selectedTextFieldValue);
                                }
                            }
                            break;
                        }
                        
                        switch (e.key.keysym.sym)
                        {
                            case SDL_Keycode.SDLK_0:
                                GlobalKeyboard.AddKey(Key.N0);
                                break;
                            case SDL_Keycode.SDLK_1:
                                GlobalKeyboard.AddKey(Key.N1);
                                break;
                            case SDL_Keycode.SDLK_2:
                                GlobalKeyboard.AddKey(Key.N);
                                break;
                            case SDL_Keycode.SDLK_3:
                                GlobalKeyboard.AddKey(Key.N3);
                                break;
                            case SDL_Keycode.SDLK_4:
                                GlobalKeyboard.AddKey(Key.N4);
                                break;
                            case SDL_Keycode.SDLK_5:
                                GlobalKeyboard.AddKey(Key.N5);
                                break;
                            case SDL_Keycode.SDLK_6:
                                GlobalKeyboard.AddKey(Key.N6);
                                break;
                            case SDL_Keycode.SDLK_7:
                                GlobalKeyboard.AddKey(Key.N7);
                                break;
                            case SDL_Keycode.SDLK_8:
                                GlobalKeyboard.AddKey(Key.N8);
                                break;
                            case SDL_Keycode.SDLK_9:
                                GlobalKeyboard.AddKey(Key.N9);
                                break;
                            case SDL_Keycode.SDLK_a:
                                GlobalKeyboard.AddKey(Key.A);
                                break;
                            case SDL_Keycode.SDLK_AC_BACK:
                                GlobalKeyboard.AddKey(Key.AC_BACK);
                                break;
                            case SDL_Keycode.SDLK_AC_BOOKMARKS:
                                GlobalKeyboard.AddKey(Key.AC_BOOKMARKS);
                                break;
                            case SDL_Keycode.SDLK_KP_DBLAMPERSAND:
                                GlobalKeyboard.AddKey(Key.KP_DBLAMPERSAND);
                                break;
                            case SDL_Keycode.SDLK_AC_FORWARD:
                                GlobalKeyboard.AddKey(Key.AC_FORWARD);
                                break;
                            case SDL_Keycode.SDLK_AC_HOME:
                                GlobalKeyboard.AddKey(Key.AC_HOME);
                                break;
                            case SDL_Keycode.SDLK_AC_REFRESH:
                                GlobalKeyboard.AddKey(Key.AC_REFRESH);
                                break;
                            case SDL_Keycode.SDLK_AC_SEARCH:
                                GlobalKeyboard.AddKey(Key.AC_SEARCH);
                                break;
                            case SDL_Keycode.SDLK_AC_STOP:
                                GlobalKeyboard.AddKey(Key.AC_STOP);
                                break;
                            case SDL_Keycode.SDLK_AGAIN:
                                GlobalKeyboard.AddKey(Key.AGAIN);
                                break;
                            case SDL_Keycode.SDLK_ALTERASE:
                                GlobalKeyboard.AddKey(Key.ALTERASE);
                                break;
                            case SDL_Keycode.SDLK_QUOTE:
                                GlobalKeyboard.AddKey(Key.QUOTE);
                                break;
                            case SDL_Keycode.SDLK_APPLICATION:
                                GlobalKeyboard.AddKey(Key.APPLICATION);
                                break;
                            case SDL_Keycode.SDLK_AUDIOMUTE:
                                GlobalKeyboard.AddKey(Key.AUDIOMUTE);
                                break;
                            case SDL_Keycode.SDLK_AUDIONEXT:
                                GlobalKeyboard.AddKey(Key.AUDIONEXT);
                                break;
                            case SDL_Keycode.SDLK_AUDIOPLAY:
                                GlobalKeyboard.AddKey(Key.AUDIOPLAY);
                                break;
                            case SDL_Keycode.SDLK_AUDIOPREV:
                                GlobalKeyboard.AddKey(Key.AUDIOPREV);
                                break;
                            case SDL_Keycode.SDLK_AUDIOSTOP:
                                GlobalKeyboard.AddKey(Key.AUDIOSTOP);
                                break;
                            case SDL_Keycode.SDLK_b:
                                GlobalKeyboard.AddKey(Key.B);
                                break;
                            case SDL_Keycode.SDLK_BACKSLASH:
                                GlobalKeyboard.AddKey(Key.BACKSLASH);
                                break;
                            case SDL_Keycode.SDLK_BACKSPACE:
                                GlobalKeyboard.AddKey(Key.BACKSPACE);
                                break;
                            case SDL_Keycode.SDLK_BRIGHTNESSDOWN:
                                GlobalKeyboard.AddKey(Key.BRIGHTNESSDOWN);
                                break;
                            case SDL_Keycode.SDLK_BRIGHTNESSUP:
                                GlobalKeyboard.AddKey(Key.BRIGHTNESSUP);
                                break;
                            case SDL_Keycode.SDLK_c:
                                GlobalKeyboard.AddKey(Key.C);
                                break;
                            case SDL_Keycode.SDLK_CALCULATOR:
                                GlobalKeyboard.AddKey(Key.CALCULATOR);
                                break;
                            case SDL_Keycode.SDLK_CANCEL:
                                GlobalKeyboard.AddKey(Key.CANCEL);
                                break;
                            case SDL_Keycode.SDLK_CAPSLOCK:
                                GlobalKeyboard.AddKey(Key.CAPSLOCK);
                                break;
                            case SDL_Keycode.SDLK_CLEAR:
                                GlobalKeyboard.AddKey(Key.CLEAR);
                                break;
                            case SDL_Keycode.SDLK_CLEARAGAIN:
                                GlobalKeyboard.AddKey(Key.CLEARAGAIN);
                                break;
                            case SDL_Keycode.SDLK_COMMA:
                                GlobalKeyboard.AddKey(Key.COMMA);
                                break;
                            case SDL_Keycode.SDLK_COMPUTER:
                                GlobalKeyboard.AddKey(Key.COMPUTER);
                                break;
                            case SDL_Keycode.SDLK_COPY:
                                GlobalKeyboard.AddKey(Key.COPY);
                                break;
                            case SDL_Keycode.SDLK_CRSEL:
                                GlobalKeyboard.AddKey(Key.CRSEL);
                                break;
                            case SDL_Keycode.SDLK_CURRENCYSUBUNIT:
                                GlobalKeyboard.AddKey(Key.CURRENCYSUBUNIT);
                                break;
                            case SDL_Keycode.SDLK_CURRENCYUNIT:
                                GlobalKeyboard.AddKey(Key.CURRENCYUNIT);
                                break;
                            case SDL_Keycode.SDLK_CUT:
                                GlobalKeyboard.AddKey(Key.CUT);
                                break;
                            case SDL_Keycode.SDLK_d:
                                GlobalKeyboard.AddKey(Key.D);
                                break;
                            case SDL_Keycode.SDLK_DECIMALSEPARATOR:
                                GlobalKeyboard.AddKey(Key.DECIMALSEPARATOR);
                                break;
                            case SDL_Keycode.SDLK_DELETE:
                                GlobalKeyboard.AddKey(Key.DELETE);
                                break;
                            case SDL_Keycode.SDLK_DISPLAYSWITCH:
                                GlobalKeyboard.AddKey(Key.DISPLAYSWITCH);
                                break;
                            case SDL_Keycode.SDLK_DOWN:
                                GlobalKeyboard.AddKey(Key.DOWN);
                                break;
                            case SDL_Keycode.SDLK_e:
                                GlobalKeyboard.AddKey(Key.E);
                                break;
                            case SDL_Keycode.SDLK_EJECT:
                                GlobalKeyboard.AddKey(Key.EJECT);
                                break;
                            case SDL_Keycode.SDLK_END:
                                GlobalKeyboard.AddKey(Key.END);
                                break;
                            case SDL_Keycode.SDLK_EQUALS:
                                GlobalKeyboard.AddKey(Key.EQUALS);
                                break;
                            case SDL_Keycode.SDLK_ESCAPE:
                                GlobalKeyboard.AddKey(Key.ESCAPE);
                                break;
                            case SDL_Keycode.SDLK_EXECUTE:
                                GlobalKeyboard.AddKey(Key.EXECUTE);
                                break;
                            case SDL_Keycode.SDLK_EXSEL:
                                GlobalKeyboard.AddKey(Key.EXSEL);
                                break;
                            case SDL_Keycode.SDLK_f:
                                GlobalKeyboard.AddKey(Key.F);
                                break;
                            case SDL_Keycode.SDLK_F1:
                                GlobalKeyboard.AddKey(Key.F1);
                                break;
                            case SDL_Keycode.SDLK_F10:
                                GlobalKeyboard.AddKey(Key.F10);
                                break;
                            case SDL_Keycode.SDLK_F11:
                                GlobalKeyboard.AddKey(Key.F11);
                                break;
                            case SDL_Keycode.SDLK_F12:
                                GlobalKeyboard.AddKey(Key.F12);
                                break;
                            case SDL_Keycode.SDLK_F13:
                                GlobalKeyboard.AddKey(Key.F13);
                                break;
                            case SDL_Keycode.SDLK_F14:
                                GlobalKeyboard.AddKey(Key.F14);
                                break;
                            case SDL_Keycode.SDLK_F15:
                                GlobalKeyboard.AddKey(Key.F15);
                                break;
                            case SDL_Keycode.SDLK_F16:
                                GlobalKeyboard.AddKey(Key.F16);
                                break;
                            case SDL_Keycode.SDLK_F17:
                                GlobalKeyboard.AddKey(Key.F17);
                                break;
                            case SDL_Keycode.SDLK_F18:
                                GlobalKeyboard.AddKey(Key.F18);
                                break;
                            case SDL_Keycode.SDLK_F19:
                                GlobalKeyboard.AddKey(Key.F19);
                                break;
                            case SDL_Keycode.SDLK_F2:
                                GlobalKeyboard.AddKey(Key.F2);
                                break;
                            case SDL_Keycode.SDLK_F20:
                                GlobalKeyboard.AddKey(Key.F20);
                                break;
                            case SDL_Keycode.SDLK_F21:
                                GlobalKeyboard.AddKey(Key.F21);
                                break;
                            case SDL_Keycode.SDLK_F22:
                                GlobalKeyboard.AddKey(Key.F22);
                                break;
                            case SDL_Keycode.SDLK_F23:
                                GlobalKeyboard.AddKey(Key.F23);
                                break;
                            case SDL_Keycode.SDLK_F24:
                                GlobalKeyboard.AddKey(Key.F24);
                                break;
                            case SDL_Keycode.SDLK_F3:
                                GlobalKeyboard.AddKey(Key.F3);
                                break;
                            case SDL_Keycode.SDLK_F4:
                                GlobalKeyboard.AddKey(Key.F4);
                                break;
                            case SDL_Keycode.SDLK_F5:
                                GlobalKeyboard.AddKey(Key.F5);
                                break;
                            case SDL_Keycode.SDLK_F6:
                                GlobalKeyboard.AddKey(Key.F6);
                                break;
                            case SDL_Keycode.SDLK_F7:
                                GlobalKeyboard.AddKey(Key.F7);
                                break;
                            case SDL_Keycode.SDLK_F8:
                                GlobalKeyboard.AddKey(Key.F8);
                                break;
                            case SDL_Keycode.SDLK_F9:
                                GlobalKeyboard.AddKey(Key.F9);
                                break;
                            case SDL_Keycode.SDLK_FIND:
                                GlobalKeyboard.AddKey(Key.FIND);
                                break;
                            case SDL_Keycode.SDLK_g:
                                GlobalKeyboard.AddKey(Key.G);
                                break;
                            case SDL_Keycode.SDLK_BACKQUOTE:
                                GlobalKeyboard.AddKey(Key.BACKQUOTE);
                                break;
                            case SDL_Keycode.SDLK_h:
                                GlobalKeyboard.AddKey(Key.H);
                                break;
                            case SDL_Keycode.SDLK_HELP:
                                GlobalKeyboard.AddKey(Key.HELP);
                                break;
                            case SDL_Keycode.SDLK_HOME:
                                GlobalKeyboard.AddKey(Key.HOME);
                                break;
                            case SDL_Keycode.SDLK_i:
                                GlobalKeyboard.AddKey(Key.I);
                                break;
                            case SDL_Keycode.SDLK_INSERT:
                                GlobalKeyboard.AddKey(Key.INSERT);
                                break;
                            case SDL_Keycode.SDLK_j:
                                GlobalKeyboard.AddKey(Key.J);
                                break;
                            case SDL_Keycode.SDLK_k:
                                GlobalKeyboard.AddKey(Key.K);
                                break;
                            case SDL_Keycode.SDLK_KBDILLUMDOWN:
                                GlobalKeyboard.AddKey(Key.KBDILLUMDOWN);
                                break;
                            case SDL_Keycode.SDLK_KBDILLUMTOGGLE:
                                GlobalKeyboard.AddKey(Key.KBDILLUMTOGGLE);
                                break;
                            case SDL_Keycode.SDLK_KBDILLUMUP:
                                GlobalKeyboard.AddKey(Key.KBDILLUMUP);
                                break;
                            case SDL_Keycode.SDLK_KP_0:
                                GlobalKeyboard.AddKey(Key.KP_0);
                                break;
                            case SDL_Keycode.SDLK_KP_00:
                                GlobalKeyboard.AddKey(Key.KP_00);
                                break;
                            case SDL_Keycode.SDLK_KP_000:
                                GlobalKeyboard.AddKey(Key.KP_000);
                                break;
                            case SDL_Keycode.SDLK_KP_1:
                                GlobalKeyboard.AddKey(Key.KP_1);
                                break;
                            case SDL_Keycode.SDLK_KP_2:
                                GlobalKeyboard.AddKey(Key.KP_2);
                                break;
                            case SDL_Keycode.SDLK_KP_3:
                                GlobalKeyboard.AddKey(Key.KP_3);
                                break;
                            case SDL_Keycode.SDLK_KP_4:
                                GlobalKeyboard.AddKey(Key.KP_4);
                                break;
                            case SDL_Keycode.SDLK_KP_5:
                                GlobalKeyboard.AddKey(Key.KP_5);
                                break;
                            case SDL_Keycode.SDLK_KP_6:
                                GlobalKeyboard.AddKey(Key.KP_6);
                                break;
                            case SDL_Keycode.SDLK_KP_7:
                                GlobalKeyboard.AddKey(Key.KP_7);
                                break;
                            case SDL_Keycode.SDLK_KP_8:
                                GlobalKeyboard.AddKey(Key.KP_8);
                                break;
                            case SDL_Keycode.SDLK_KP_9:
                                GlobalKeyboard.AddKey(Key.KP_9);
                                break;
                            case SDL_Keycode.SDLK_KP_A:
                                GlobalKeyboard.AddKey(Key.KP_A);
                                break;
                            case SDL_Keycode.SDLK_KP_AMPERSAND:
                                GlobalKeyboard.AddKey(Key.KP_AMPERSAND);
                                break;
                            case SDL_Keycode.SDLK_KP_AT:
                                GlobalKeyboard.AddKey(Key.KP_AT);
                                break;
                            case SDL_Keycode.SDLK_KP_B:
                                GlobalKeyboard.AddKey(Key.KP_B);
                                break;
                            case SDL_Keycode.SDLK_KP_BACKSPACE:
                                GlobalKeyboard.AddKey(Key.KP_BACKSPACE);
                                break;
                            case SDL_Keycode.SDLK_KP_BINARY:
                                GlobalKeyboard.AddKey(Key.KP_BINARY);
                                break;
                            case SDL_Keycode.SDLK_KP_C:
                                GlobalKeyboard.AddKey(Key.KP_C);
                                break;
                            case SDL_Keycode.SDLK_KP_CLEAR:
                                GlobalKeyboard.AddKey(Key.KP_CLEAR);
                                break;
                            case SDL_Keycode.SDLK_KP_CLEARENTRY:
                                GlobalKeyboard.AddKey(Key.KP_CLEARENTRY);
                                break;
                            case SDL_Keycode.SDLK_KP_COLON:
                                GlobalKeyboard.AddKey(Key.KP_COLON);
                                break;
                            case SDL_Keycode.SDLK_KP_COMMA:
                                GlobalKeyboard.AddKey(Key.KP_COMMA);
                                break;
                            case SDL_Keycode.SDLK_KP_D:
                                GlobalKeyboard.AddKey(Key.KP_D);
                                break;
                            case SDL_Keycode.SDLK_KP_DBLVERTICALBAR:
                                GlobalKeyboard.AddKey(Key.KP_DBLVERTICALBAR);
                                break;
                            case SDL_Keycode.SDLK_KP_DECIMAL:
                                GlobalKeyboard.AddKey(Key.KP_DECIMAL);
                                break;
                            case SDL_Keycode.SDLK_KP_DIVIDE:
                                GlobalKeyboard.AddKey(Key.KP_DIVIDE);
                                break;
                            case SDL_Keycode.SDLK_KP_E:
                                GlobalKeyboard.AddKey(Key.KP_E);
                                break;
                            case SDL_Keycode.SDLK_KP_ENTER:
                                GlobalKeyboard.AddKey(Key.KP_ENTER);
                                break;
                            case SDL_Keycode.SDLK_KP_EQUALS:
                                GlobalKeyboard.AddKey(Key.KP_EQUALS);
                                break;
                            case SDL_Keycode.SDLK_KP_EQUALSAS400:
                                GlobalKeyboard.AddKey(Key.KP_EQUALSAS400);
                                break;
                            case SDL_Keycode.SDLK_KP_EXCLAM:
                                GlobalKeyboard.AddKey(Key.KP_EXCLAM);
                                break;
                            case SDL_Keycode.SDLK_KP_F:
                                GlobalKeyboard.AddKey(Key.KP_F);
                                break;
                            case SDL_Keycode.SDLK_KP_GREATER:
                                GlobalKeyboard.AddKey(Key.KP_GREATER);
                                break;
                            case SDL_Keycode.SDLK_KP_HASH:
                                GlobalKeyboard.AddKey(Key.KP_HASH);
                                break;
                            case SDL_Keycode.SDLK_KP_HEXADECIMAL:
                                GlobalKeyboard.AddKey(Key.KP_HEXADECIMAL);
                                break;
                            case SDL_Keycode.SDLK_KP_LEFTBRACE:
                                GlobalKeyboard.AddKey(Key.KP_LEFTBRACE);
                                break;
                            case SDL_Keycode.SDLK_KP_LEFTPAREN:
                                GlobalKeyboard.AddKey(Key.KP_LEFTPAREN);
                                break;
                            case SDL_Keycode.SDLK_KP_LESS:
                                GlobalKeyboard.AddKey(Key.KP_LESS);
                                break;
                            case SDL_Keycode.SDLK_KP_MEMADD:
                                GlobalKeyboard.AddKey(Key.KP_MEMADD);
                                break;
                            case SDL_Keycode.SDLK_KP_MEMCLEAR:
                                GlobalKeyboard.AddKey(Key.KP_MEMCLEAR);
                                break;
                            case SDL_Keycode.SDLK_KP_MEMDIVIDE:
                                GlobalKeyboard.AddKey(Key.KP_MEMDIVIDE);
                                break;
                            case SDL_Keycode.SDLK_KP_MEMMULTIPLY:
                                GlobalKeyboard.AddKey(Key.KP_MEMMULTIPLY);
                                break;
                            case SDL_Keycode.SDLK_KP_MEMRECALL:
                                GlobalKeyboard.AddKey(Key.KP_MEMRECALL);
                                break;
                            case SDL_Keycode.SDLK_KP_MEMSTORE:
                                GlobalKeyboard.AddKey(Key.KP_MEMSTORE);
                                break;
                            case SDL_Keycode.SDLK_KP_MEMSUBTRACT:
                                GlobalKeyboard.AddKey(Key.KP_MEMSUBTRACT);
                                break;
                            case SDL_Keycode.SDLK_KP_MINUS:
                                GlobalKeyboard.AddKey(Key.KP_MINUS);
                                break;
                            case SDL_Keycode.SDLK_KP_MULTIPLY:
                                GlobalKeyboard.AddKey(Key.KP_MULTIPLY);
                                break;
                            case SDL_Keycode.SDLK_KP_OCTAL:
                                GlobalKeyboard.AddKey(Key.KP_OCTAL);
                                break;
                            case SDL_Keycode.SDLK_KP_PERCENT:
                                GlobalKeyboard.AddKey(Key.KP_PERCENT);
                                break;
                            case SDL_Keycode.SDLK_KP_PERIOD:
                                GlobalKeyboard.AddKey(Key.KP_PERIOD);
                                break;
                            case SDL_Keycode.SDLK_KP_PLUS:
                                GlobalKeyboard.AddKey(Key.KP_PLUS);
                                break;
                            case SDL_Keycode.SDLK_KP_PLUSMINUS:
                                GlobalKeyboard.AddKey(Key.KP_PLUSMINUS);
                                break;
                            case SDL_Keycode.SDLK_KP_POWER:
                                GlobalKeyboard.AddKey(Key.KP_POWER);
                                break;
                            case SDL_Keycode.SDLK_KP_RIGHTBRACE:
                                GlobalKeyboard.AddKey(Key.KP_RIGHTBRACE);
                                break;
                            case SDL_Keycode.SDLK_KP_RIGHTPAREN:
                                GlobalKeyboard.AddKey(Key.KP_RIGHTPAREN);
                                break;
                            case SDL_Keycode.SDLK_KP_SPACE:
                                GlobalKeyboard.AddKey(Key.KP_SPACE);
                                break;
                            case SDL_Keycode.SDLK_KP_TAB:
                                GlobalKeyboard.AddKey(Key.KP_TAB);
                                break;
                            case SDL_Keycode.SDLK_KP_VERTICALBAR:
                                GlobalKeyboard.AddKey(Key.KP_VERTICALBAR);
                                break;
                            case SDL_Keycode.SDLK_KP_XOR:
                                GlobalKeyboard.AddKey(Key.KP_XOR);
                                break;
                            case SDL_Keycode.SDLK_l:
                                GlobalKeyboard.AddKey(Key.L);
                                break;
                            case SDL_Keycode.SDLK_LEFT:
                                GlobalKeyboard.AddKey(Key.LEFT);
                                break;
                            case SDL_Keycode.SDLK_LEFTBRACKET:
                                GlobalKeyboard.AddKey(Key.LEFTBRACKET);
                                break;
                            case SDL_Keycode.SDLK_LGUI:
                                GlobalKeyboard.AddKey(Key.LGUI);
                                break;
                            case SDL_Keycode.SDLK_m:
                                GlobalKeyboard.AddKey(Key.M);
                                break;
                            case SDL_Keycode.SDLK_MAIL:
                                GlobalKeyboard.AddKey(Key.MAIL);
                                break;
                            case SDL_Keycode.SDLK_MEDIASELECT:
                                GlobalKeyboard.AddKey(Key.MEDIASELECT);
                                break;
                            case SDL_Keycode.SDLK_MENU:
                                GlobalKeyboard.AddKey(Key.MENU);
                                break;
                            case SDL_Keycode.SDLK_MINUS:
                                GlobalKeyboard.AddKey(Key.MINUS);
                                break;
                            case SDL_Keycode.SDLK_MODE:
                                GlobalKeyboard.AddKey(Key.MODE);
                                break;
                            case SDL_Keycode.SDLK_MUTE:
                                GlobalKeyboard.AddKey(Key.MUTE);
                                break;
                            case SDL_Keycode.SDLK_n:
                                GlobalKeyboard.AddKey(Key.N);
                                break;
                            case SDL_Keycode.SDLK_NUMLOCKCLEAR:
                                GlobalKeyboard.AddKey(Key.NUMLOCKCLEAR);
                                break;
                            case SDL_Keycode.SDLK_o:
                                GlobalKeyboard.AddKey(Key.O);
                                break;
                            case SDL_Keycode.SDLK_OPER:
                                GlobalKeyboard.AddKey(Key.OPER);
                                break;
                            case SDL_Keycode.SDLK_OUT:
                                GlobalKeyboard.AddKey(Key.OUT);
                                break;
                            case SDL_Keycode.SDLK_p:
                                GlobalKeyboard.AddKey(Key.P);
                                break;
                            case SDL_Keycode.SDLK_PAGEDOWN:
                                GlobalKeyboard.AddKey(Key.PAGEDOWN);
                                break;
                            case SDL_Keycode.SDLK_PAGEUP:
                                GlobalKeyboard.AddKey(Key.PAGEUP);
                                break;
                            case SDL_Keycode.SDLK_PASTE:
                                GlobalKeyboard.AddKey(Key.PASTE);
                                break;
                            case SDL_Keycode.SDLK_PAUSE:
                                GlobalKeyboard.AddKey(Key.PAUSE);
                                break;
                            case SDL_Keycode.SDLK_PERIOD:
                                GlobalKeyboard.AddKey(Key.PERIOD);
                                break;
                            case SDL_Keycode.SDLK_POWER:
                                GlobalKeyboard.AddKey(Key.POWER);
                                break;
                            case SDL_Keycode.SDLK_PRINTSCREEN:
                                GlobalKeyboard.AddKey(Key.PRINTSCREEN);
                                break;
                            case SDL_Keycode.SDLK_PRIOR:
                                GlobalKeyboard.AddKey(Key.PRIOR);
                                break;
                            case SDL_Keycode.SDLK_q:
                                GlobalKeyboard.AddKey(Key.Q);
                                break;
                            case SDL_Keycode.SDLK_r:
                                GlobalKeyboard.AddKey(Key.R);
                                break;
                            case SDL_Keycode.SDLK_RETURN:
                                GlobalKeyboard.AddKey(Key.RETURN);
                                break;
                            case SDL_Keycode.SDLK_RETURN2:
                                GlobalKeyboard.AddKey(Key.RETURN2);
                                break;
                            case SDL_Keycode.SDLK_RGUI:
                                GlobalKeyboard.AddKey(Key.RGUI);
                                break;
                            case SDL_Keycode.SDLK_RIGHT:
                                GlobalKeyboard.AddKey(Key.RIGHT);
                                break;
                            case SDL_Keycode.SDLK_RIGHTBRACKET:
                                GlobalKeyboard.AddKey(Key.RIGHTBRACKET);
                                break;
                            case SDL_Keycode.SDLK_s:
                                GlobalKeyboard.AddKey(Key.S);
                                break;
                            case SDL_Keycode.SDLK_SCROLLLOCK:
                                GlobalKeyboard.AddKey(Key.SCROLLLOCK);
                                break;
                            case SDL_Keycode.SDLK_SELECT:
                                GlobalKeyboard.AddKey(Key.SELECT);
                                break;
                            case SDL_Keycode.SDLK_SEMICOLON:
                                GlobalKeyboard.AddKey(Key.SEMICOLON);
                                break;
                            case SDL_Keycode.SDLK_SEPARATOR:
                                GlobalKeyboard.AddKey(Key.SEPARATOR);
                                break;
                            case SDL_Keycode.SDLK_SLASH:
                                GlobalKeyboard.AddKey(Key.SLASH);
                                break;
                            case SDL_Keycode.SDLK_SLEEP:
                                GlobalKeyboard.AddKey(Key.SLEEP);
                                break;
                            case SDL_Keycode.SDLK_SPACE:
                                GlobalKeyboard.AddKey(Key.SPACE);
                                break;
                            case SDL_Keycode.SDLK_STOP:
                                GlobalKeyboard.AddKey(Key.STOP);
                                break;
                            case SDL_Keycode.SDLK_SYSREQ:
                                GlobalKeyboard.AddKey(Key.SYSREQ);
                                break;
                            case SDL_Keycode.SDLK_t:
                                GlobalKeyboard.AddKey(Key.T);
                                break;
                            case SDL_Keycode.SDLK_TAB:
                                GlobalKeyboard.AddKey(Key.TAB);
                                break;
                            case SDL_Keycode.SDLK_THOUSANDSSEPARATOR:
                                GlobalKeyboard.AddKey(Key.THOUSANDSSEPARATOR);
                                break;
                            case SDL_Keycode.SDLK_u:
                                GlobalKeyboard.AddKey(Key.U);
                                break;
                            case SDL_Keycode.SDLK_UNDO:
                                GlobalKeyboard.AddKey(Key.UNDO);
                                break;
                            case SDL_Keycode.SDLK_UNKNOWN:
                                GlobalKeyboard.AddKey(Key.UNKNOWN);
                                break;
                            case SDL_Keycode.SDLK_UP:
                                GlobalKeyboard.AddKey(Key.UP);
                                break;
                            case SDL_Keycode.SDLK_v:
                                GlobalKeyboard.AddKey(Key.V);
                                break;
                            case SDL_Keycode.SDLK_VOLUMEDOWN:
                                GlobalKeyboard.AddKey(Key.VOLUMEDOWN);
                                break;
                            case SDL_Keycode.SDLK_VOLUMEUP:
                                GlobalKeyboard.AddKey(Key.VOLUMEUP);
                                break;
                            case SDL_Keycode.SDLK_w:
                                GlobalKeyboard.AddKey(Key.W);
                                break;
                            case SDL_Keycode.SDLK_WWW:
                                GlobalKeyboard.AddKey(Key.WWW);
                                break;
                            case SDL_Keycode.SDLK_x:
                                GlobalKeyboard.AddKey(Key.X);
                                break;
                            case SDL_Keycode.SDLK_y:
                                GlobalKeyboard.AddKey(Key.Y);
                                break;
                            case SDL_Keycode.SDLK_z:
                                GlobalKeyboard.AddKey(Key.Z);
                                break;
                            case SDL_Keycode.SDLK_AMPERSAND:
                                GlobalKeyboard.AddKey(Key.AMPERSAND);
                                break;
                            case SDL_Keycode.SDLK_ASTERISK:
                                GlobalKeyboard.AddKey(Key.ASTERISK);
                                break;
                            case SDL_Keycode.SDLK_AT:
                                GlobalKeyboard.AddKey(Key.AT);
                                break;
                            case SDL_Keycode.SDLK_CARET:
                                GlobalKeyboard.AddKey(Key.CARET);
                                break;
                            case SDL_Keycode.SDLK_COLON:
                                GlobalKeyboard.AddKey(Key.COLON);
                                break;
                            case SDL_Keycode.SDLK_DOLLAR:
                                GlobalKeyboard.AddKey(Key.DOLLAR);
                                break;
                            case SDL_Keycode.SDLK_EXCLAIM:
                                GlobalKeyboard.AddKey(Key.EXCLAIM);
                                break;
                            case SDL_Keycode.SDLK_GREATER:
                                GlobalKeyboard.AddKey(Key.GREATER);
                                break;
                            case SDL_Keycode.SDLK_HASH:
                                GlobalKeyboard.AddKey(Key.HASH);
                                break;
                            case SDL_Keycode.SDLK_LEFTPAREN:
                                GlobalKeyboard.AddKey(Key.LEFTPAREN);
                                break;
                            case SDL_Keycode.SDLK_LESS:
                                GlobalKeyboard.AddKey(Key.LESS);
                                break;
                            case SDL_Keycode.SDLK_PERCENT:
                                GlobalKeyboard.AddKey(Key.PERCENT);
                                break;
                            case SDL_Keycode.SDLK_PLUS:
                                GlobalKeyboard.AddKey(Key.PLUS);
                                break;
                            case SDL_Keycode.SDLK_QUESTION:
                                GlobalKeyboard.AddKey(Key.QUESTION);
                                break;
                            case SDL_Keycode.SDLK_QUOTEDBL:
                                GlobalKeyboard.AddKey(Key.QUOTEDBL);
                                break;
                            case SDL_Keycode.SDLK_RIGHTPAREN:
                                GlobalKeyboard.AddKey(Key.RIGHTPAREN);
                                break;
                            case SDL_Keycode.SDLK_UNDERSCORE:
                                GlobalKeyboard.AddKey(Key.UNDERSCORE);
                                break;
                            case SDL_Keycode.SDLK_LCTRL:
                                if(!GlobalKeyboard.pressedModifiers.Contains(Mod.LCtrl))
                                    GlobalKeyboard.pressedModifiers.Add(Mod.LCtrl);
                                break;
                            case SDL_Keycode.SDLK_LSHIFT:
                                if (!GlobalKeyboard.pressedModifiers.Contains(Mod.LShift))
                                    GlobalKeyboard.pressedModifiers.Add(Mod.LShift);
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
                                GlobalKeyboard.downKeys.Remove(Key.N0);
                                break;
                            case SDL_Keycode.SDLK_1:
                                GlobalKeyboard.downKeys.Remove(Key.N1);
                                break;
                            case SDL_Keycode.SDLK_2:
                                GlobalKeyboard.downKeys.Remove(Key.N2);
                                break;
                            case SDL_Keycode.SDLK_3:
                                GlobalKeyboard.downKeys.Remove(Key.N3);
                                break;
                            case SDL_Keycode.SDLK_4:
                                GlobalKeyboard.downKeys.Remove(Key.N4);
                                break;
                            case SDL_Keycode.SDLK_5:
                                GlobalKeyboard.downKeys.Remove(Key.N5);
                                break;
                            case SDL_Keycode.SDLK_6:
                                GlobalKeyboard.downKeys.Remove(Key.N6);
                                break;
                            case SDL_Keycode.SDLK_7:
                                GlobalKeyboard.downKeys.Remove(Key.N7);
                                break;
                            case SDL_Keycode.SDLK_8:
                                GlobalKeyboard.downKeys.Remove(Key.N8);
                                break;
                            case SDL_Keycode.SDLK_9:
                                GlobalKeyboard.downKeys.Remove(Key.N9);
                                break;
                            case SDL_Keycode.SDLK_a:
                                GlobalKeyboard.downKeys.Remove(Key.A);
                                break;
                            case SDL_Keycode.SDLK_AC_BACK:
                                GlobalKeyboard.downKeys.Remove(Key.AC_BACK);
                                break;
                            case SDL_Keycode.SDLK_AC_BOOKMARKS:
                                GlobalKeyboard.downKeys.Remove(Key.AC_BOOKMARKS);
                                break;
                            case SDL_Keycode.SDLK_AC_FORWARD:
                                GlobalKeyboard.downKeys.Remove(Key.AC_FORWARD);
                                break;
                            case SDL_Keycode.SDLK_AC_HOME:
                                GlobalKeyboard.downKeys.Remove(Key.AC_HOME);
                                break;
                            case SDL_Keycode.SDLK_AC_REFRESH:
                                GlobalKeyboard.downKeys.Remove(Key.AC_REFRESH);
                                break;
                            case SDL_Keycode.SDLK_AC_SEARCH:
                                GlobalKeyboard.downKeys.Remove(Key.AC_SEARCH);
                                break;
                            case SDL_Keycode.SDLK_AC_STOP:
                                GlobalKeyboard.downKeys.Remove(Key.AC_STOP);
                                break;
                            case SDL_Keycode.SDLK_AGAIN:
                                GlobalKeyboard.downKeys.Remove(Key.AGAIN);
                                break;
                            case SDL_Keycode.SDLK_ALTERASE:
                                GlobalKeyboard.downKeys.Remove(Key.ALTERASE);
                                break;
                            case SDL_Keycode.SDLK_QUOTE:
                                GlobalKeyboard.downKeys.Remove(Key.QUOTE);
                                break;
                            case SDL_Keycode.SDLK_APPLICATION:
                                GlobalKeyboard.downKeys.Remove(Key.APPLICATION);
                                break;
                            case SDL_Keycode.SDLK_AUDIOMUTE:
                                GlobalKeyboard.downKeys.Remove(Key.AUDIOMUTE);
                                break;
                            case SDL_Keycode.SDLK_AUDIONEXT:
                                GlobalKeyboard.downKeys.Remove(Key.AUDIONEXT);
                                break;
                            case SDL_Keycode.SDLK_AUDIOPLAY:
                                GlobalKeyboard.downKeys.Remove(Key.AUDIOPLAY);
                                break;
                            case SDL_Keycode.SDLK_AUDIOPREV:
                                GlobalKeyboard.downKeys.Remove(Key.AUDIOPREV);
                                break;
                            case SDL_Keycode.SDLK_AUDIOSTOP:
                                GlobalKeyboard.downKeys.Remove(Key.AUDIOSTOP);
                                break;
                            case SDL_Keycode.SDLK_b:
                                GlobalKeyboard.downKeys.Remove(Key.B);
                                break;
                            case SDL_Keycode.SDLK_BACKSLASH:
                                GlobalKeyboard.downKeys.Remove(Key.BACKSLASH);
                                break;
                            case SDL_Keycode.SDLK_BACKSPACE:
                                GlobalKeyboard.downKeys.Remove(Key.BACKSPACE);
                                break;
                            case SDL_Keycode.SDLK_BRIGHTNESSDOWN:
                                GlobalKeyboard.downKeys.Remove(Key.BRIGHTNESSDOWN);
                                break;
                            case SDL_Keycode.SDLK_BRIGHTNESSUP:
                                GlobalKeyboard.downKeys.Remove(Key.BRIGHTNESSUP);
                                break;
                            case SDL_Keycode.SDLK_c:
                                GlobalKeyboard.downKeys.Remove(Key.C);
                                break;
                            case SDL_Keycode.SDLK_CALCULATOR:
                                GlobalKeyboard.downKeys.Remove(Key.CALCULATOR);
                                break;
                            case SDL_Keycode.SDLK_CANCEL:
                                GlobalKeyboard.downKeys.Remove(Key.CANCEL);
                                break;
                            case SDL_Keycode.SDLK_CAPSLOCK:
                                GlobalKeyboard.downKeys.Remove(Key.CAPSLOCK);
                                break;
                            case SDL_Keycode.SDLK_CLEAR:
                                GlobalKeyboard.downKeys.Remove(Key.CLEAR);
                                break;
                            case SDL_Keycode.SDLK_CLEARAGAIN:
                                GlobalKeyboard.downKeys.Remove(Key.CLEARAGAIN);
                                break;
                            case SDL_Keycode.SDLK_COMMA:
                                GlobalKeyboard.downKeys.Remove(Key.COMMA);
                                break;
                            case SDL_Keycode.SDLK_COMPUTER:
                                GlobalKeyboard.downKeys.Remove(Key.COMPUTER);
                                break;
                            case SDL_Keycode.SDLK_COPY:
                                GlobalKeyboard.downKeys.Remove(Key.COPY);
                                break;
                            case SDL_Keycode.SDLK_CRSEL:
                                GlobalKeyboard.downKeys.Remove(Key.CRSEL);
                                break;
                            case SDL_Keycode.SDLK_CURRENCYSUBUNIT:
                                GlobalKeyboard.downKeys.Remove(Key.CURRENCYSUBUNIT);
                                break;
                            case SDL_Keycode.SDLK_CURRENCYUNIT:
                                GlobalKeyboard.downKeys.Remove(Key.CURRENCYUNIT);
                                break;
                            case SDL_Keycode.SDLK_CUT:
                                GlobalKeyboard.downKeys.Remove(Key.CUT);
                                break;
                            case SDL_Keycode.SDLK_d:
                                GlobalKeyboard.downKeys.Remove(Key.D);
                                break;
                            case SDL_Keycode.SDLK_DECIMALSEPARATOR:
                                GlobalKeyboard.downKeys.Remove(Key.DECIMALSEPARATOR);
                                break;
                            case SDL_Keycode.SDLK_DELETE:
                                GlobalKeyboard.downKeys.Remove(Key.DELETE);
                                break;
                            case SDL_Keycode.SDLK_DISPLAYSWITCH:
                                GlobalKeyboard.downKeys.Remove(Key.DISPLAYSWITCH);
                                break;
                            case SDL_Keycode.SDLK_DOWN:
                                GlobalKeyboard.downKeys.Remove(Key.DOWN);
                                break;
                            case SDL_Keycode.SDLK_e:
                                GlobalKeyboard.downKeys.Remove(Key.E);
                                break;
                            case SDL_Keycode.SDLK_EJECT:
                                GlobalKeyboard.downKeys.Remove(Key.EJECT);
                                break;
                            case SDL_Keycode.SDLK_END:
                                GlobalKeyboard.downKeys.Remove(Key.END);
                                break;
                            case SDL_Keycode.SDLK_EQUALS:
                                GlobalKeyboard.downKeys.Remove(Key.EQUALS);
                                break;
                            case SDL_Keycode.SDLK_ESCAPE:
                                GlobalKeyboard.downKeys.Remove(Key.ESCAPE);
                                break;
                            case SDL_Keycode.SDLK_EXECUTE:
                                GlobalKeyboard.downKeys.Remove(Key.EXECUTE);
                                break;
                            case SDL_Keycode.SDLK_EXSEL:
                                GlobalKeyboard.downKeys.Remove(Key.EXSEL);
                                break;
                            case SDL_Keycode.SDLK_f:
                                GlobalKeyboard.downKeys.Remove(Key.F);
                                break;
                            case SDL_Keycode.SDLK_F1:
                                GlobalKeyboard.downKeys.Remove(Key.F1);
                                break;
                            case SDL_Keycode.SDLK_F10:
                                GlobalKeyboard.downKeys.Remove(Key.F10);
                                break;
                            case SDL_Keycode.SDLK_F11:
                                GlobalKeyboard.downKeys.Remove(Key.F11);
                                break;
                            case SDL_Keycode.SDLK_F12:
                                GlobalKeyboard.downKeys.Remove(Key.F12);
                                break;
                            case SDL_Keycode.SDLK_F13:
                                GlobalKeyboard.downKeys.Remove(Key.F13);
                                break;
                            case SDL_Keycode.SDLK_F14:
                                GlobalKeyboard.downKeys.Remove(Key.F14);
                                break;
                            case SDL_Keycode.SDLK_F15:
                                GlobalKeyboard.downKeys.Remove(Key.F15);
                                break;
                            case SDL_Keycode.SDLK_F16:
                                GlobalKeyboard.downKeys.Remove(Key.F16);
                                break;
                            case SDL_Keycode.SDLK_F17:
                                GlobalKeyboard.downKeys.Remove(Key.F17);
                                break;
                            case SDL_Keycode.SDLK_F18:
                                GlobalKeyboard.downKeys.Remove(Key.F18);
                                break;
                            case SDL_Keycode.SDLK_F19:
                                GlobalKeyboard.downKeys.Remove(Key.F19);
                                break;
                            case SDL_Keycode.SDLK_F2:
                                GlobalKeyboard.downKeys.Remove(Key.F2);
                                break;
                            case SDL_Keycode.SDLK_F20:
                                GlobalKeyboard.downKeys.Remove(Key.F20);
                                break;
                            case SDL_Keycode.SDLK_F21:
                                GlobalKeyboard.downKeys.Remove(Key.F21);
                                break;
                            case SDL_Keycode.SDLK_F22:
                                GlobalKeyboard.downKeys.Remove(Key.F22);
                                break;
                            case SDL_Keycode.SDLK_F23:
                                GlobalKeyboard.downKeys.Remove(Key.F23);
                                break;
                            case SDL_Keycode.SDLK_F24:
                                GlobalKeyboard.downKeys.Remove(Key.F24);
                                break;
                            case SDL_Keycode.SDLK_F3:
                                GlobalKeyboard.downKeys.Remove(Key.F3);
                                break;
                            case SDL_Keycode.SDLK_F4:
                                GlobalKeyboard.downKeys.Remove(Key.F4);
                                break;
                            case SDL_Keycode.SDLK_F5:
                                GlobalKeyboard.downKeys.Remove(Key.F5);
                                break;
                            case SDL_Keycode.SDLK_F6:
                                GlobalKeyboard.downKeys.Remove(Key.F6);
                                break;
                            case SDL_Keycode.SDLK_F7:
                                GlobalKeyboard.downKeys.Remove(Key.F7);
                                break;
                            case SDL_Keycode.SDLK_F8:
                                GlobalKeyboard.downKeys.Remove(Key.F8);
                                break;
                            case SDL_Keycode.SDLK_F9:
                                GlobalKeyboard.downKeys.Remove(Key.F9);
                                break;
                            case SDL_Keycode.SDLK_FIND:
                                GlobalKeyboard.downKeys.Remove(Key.FIND);
                                break;
                            case SDL_Keycode.SDLK_g:
                                GlobalKeyboard.downKeys.Remove(Key.G);
                                break;
                            case SDL_Keycode.SDLK_BACKQUOTE:
                                GlobalKeyboard.downKeys.Remove(Key.BACKQUOTE);
                                break;
                            case SDL_Keycode.SDLK_h:
                                GlobalKeyboard.downKeys.Remove(Key.H);
                                break;
                            case SDL_Keycode.SDLK_HELP:
                                GlobalKeyboard.downKeys.Remove(Key.HELP);
                                break;
                            case SDL_Keycode.SDLK_HOME:
                                GlobalKeyboard.downKeys.Remove(Key.HOME);
                                break;
                            case SDL_Keycode.SDLK_i:
                                GlobalKeyboard.downKeys.Remove(Key.I);
                                break;
                            case SDL_Keycode.SDLK_INSERT:
                                GlobalKeyboard.downKeys.Remove(Key.INSERT);
                                break;
                            case SDL_Keycode.SDLK_j:
                                GlobalKeyboard.downKeys.Remove(Key.J);
                                break;
                            case SDL_Keycode.SDLK_k:
                                GlobalKeyboard.downKeys.Remove(Key.K);
                                break;
                            case SDL_Keycode.SDLK_KBDILLUMDOWN:
                                GlobalKeyboard.downKeys.Remove(Key.KBDILLUMDOWN);
                                break;
                            case SDL_Keycode.SDLK_KBDILLUMTOGGLE:
                                GlobalKeyboard.downKeys.Remove(Key.KBDILLUMTOGGLE);
                                break;
                            case SDL_Keycode.SDLK_KBDILLUMUP:
                                GlobalKeyboard.downKeys.Remove(Key.KBDILLUMUP);
                                break;
                            case SDL_Keycode.SDLK_KP_0:
                                GlobalKeyboard.downKeys.Remove(Key.KP_0);
                                break;
                            case SDL_Keycode.SDLK_KP_00:
                                GlobalKeyboard.downKeys.Remove(Key.KP_00);
                                break;
                            case SDL_Keycode.SDLK_KP_000:
                                GlobalKeyboard.downKeys.Remove(Key.KP_000);
                                break;
                            case SDL_Keycode.SDLK_KP_1:
                                GlobalKeyboard.downKeys.Remove(Key.KP_1);
                                break;
                            case SDL_Keycode.SDLK_KP_2:
                                GlobalKeyboard.downKeys.Remove(Key.KP_2);
                                break;
                            case SDL_Keycode.SDLK_KP_3:
                                GlobalKeyboard.downKeys.Remove(Key.KP_3);
                                break;
                            case SDL_Keycode.SDLK_KP_4:
                                GlobalKeyboard.downKeys.Remove(Key.KP_4);
                                break;
                            case SDL_Keycode.SDLK_KP_5:
                                GlobalKeyboard.downKeys.Remove(Key.KP_5);
                                break;
                            case SDL_Keycode.SDLK_KP_6:
                                GlobalKeyboard.downKeys.Remove(Key.KP_6);
                                break;
                            case SDL_Keycode.SDLK_KP_7:
                                GlobalKeyboard.downKeys.Remove(Key.KP_7);
                                break;
                            case SDL_Keycode.SDLK_KP_8:
                                GlobalKeyboard.downKeys.Remove(Key.KP_8);
                                break;
                            case SDL_Keycode.SDLK_KP_9:
                                GlobalKeyboard.downKeys.Remove(Key.KP_9);
                                break;
                            case SDL_Keycode.SDLK_KP_A:
                                GlobalKeyboard.downKeys.Remove(Key.KP_A);
                                break;
                            case SDL_Keycode.SDLK_KP_AMPERSAND:
                                GlobalKeyboard.downKeys.Remove(Key.KP_AMPERSAND);
                                break;
                            case SDL_Keycode.SDLK_KP_AT:
                                GlobalKeyboard.downKeys.Remove(Key.KP_AT);
                                break;
                            case SDL_Keycode.SDLK_KP_B:
                                GlobalKeyboard.downKeys.Remove(Key.KP_B);
                                break;
                            case SDL_Keycode.SDLK_KP_BACKSPACE:
                                GlobalKeyboard.downKeys.Remove(Key.KP_BACKSPACE);
                                break;
                            case SDL_Keycode.SDLK_KP_BINARY:
                                GlobalKeyboard.downKeys.Remove(Key.KP_BINARY);
                                break;
                            case SDL_Keycode.SDLK_KP_C:
                                GlobalKeyboard.downKeys.Remove(Key.KP_C);
                                break;
                            case SDL_Keycode.SDLK_KP_CLEAR:
                                GlobalKeyboard.downKeys.Remove(Key.KP_CLEAR);
                                break;
                            case SDL_Keycode.SDLK_KP_CLEARENTRY:
                                GlobalKeyboard.downKeys.Remove(Key.KP_CLEARENTRY);
                                break;
                            case SDL_Keycode.SDLK_KP_COLON:
                                GlobalKeyboard.downKeys.Remove(Key.KP_COLON);
                                break;
                            case SDL_Keycode.SDLK_KP_COMMA:
                                GlobalKeyboard.downKeys.Remove(Key.KP_COMMA);
                                break;
                            case SDL_Keycode.SDLK_KP_D:
                                GlobalKeyboard.downKeys.Remove(Key.KP_D);
                                break;
                            case SDL_Keycode.SDLK_KP_DBLAMPERSAND:
                                GlobalKeyboard.downKeys.Remove(Key.KP_DBLAMPERSAND);
                                break;
                            case SDL_Keycode.SDLK_KP_DBLVERTICALBAR:
                                GlobalKeyboard.downKeys.Remove(Key.KP_DBLVERTICALBAR);
                                break;
                            case SDL_Keycode.SDLK_KP_DECIMAL:
                                GlobalKeyboard.downKeys.Remove(Key.KP_DECIMAL);
                                break;
                            case SDL_Keycode.SDLK_KP_DIVIDE:
                                GlobalKeyboard.downKeys.Remove(Key.KP_DIVIDE);
                                break;
                            case SDL_Keycode.SDLK_KP_E:
                                GlobalKeyboard.downKeys.Remove(Key.KP_E);
                                break;
                            case SDL_Keycode.SDLK_KP_ENTER:
                                GlobalKeyboard.downKeys.Remove(Key.KP_ENTER);
                                break;
                            case SDL_Keycode.SDLK_KP_EQUALS:
                                GlobalKeyboard.downKeys.Remove(Key.KP_EQUALS);
                                break;
                            case SDL_Keycode.SDLK_KP_EQUALSAS400:
                                GlobalKeyboard.downKeys.Remove(Key.KP_EQUALSAS400);
                                break;
                            case SDL_Keycode.SDLK_KP_EXCLAM:
                                GlobalKeyboard.downKeys.Remove(Key.KP_EXCLAM);
                                break;
                            case SDL_Keycode.SDLK_KP_F:
                                GlobalKeyboard.downKeys.Remove(Key.KP_F);
                                break;
                            case SDL_Keycode.SDLK_KP_GREATER:
                                GlobalKeyboard.downKeys.Remove(Key.KP_GREATER);
                                break;
                            case SDL_Keycode.SDLK_KP_HASH:
                                GlobalKeyboard.downKeys.Remove(Key.KP_HASH);
                                break;
                            case SDL_Keycode.SDLK_KP_HEXADECIMAL:
                                GlobalKeyboard.downKeys.Remove(Key.KP_HEXADECIMAL);
                                break;
                            case SDL_Keycode.SDLK_KP_LEFTBRACE:
                                GlobalKeyboard.downKeys.Remove(Key.KP_LEFTBRACE);
                                break;
                            case SDL_Keycode.SDLK_KP_LEFTPAREN:
                                GlobalKeyboard.downKeys.Remove(Key.KP_LEFTPAREN);
                                break;
                            case SDL_Keycode.SDLK_KP_LESS:
                                GlobalKeyboard.downKeys.Remove(Key.KP_LESS);
                                break;
                            case SDL_Keycode.SDLK_KP_MEMADD:
                                GlobalKeyboard.downKeys.Remove(Key.KP_MEMADD);
                                break;
                            case SDL_Keycode.SDLK_KP_MEMCLEAR:
                                GlobalKeyboard.downKeys.Remove(Key.KP_MEMCLEAR);
                                break;
                            case SDL_Keycode.SDLK_KP_MEMDIVIDE:
                                GlobalKeyboard.downKeys.Remove(Key.KP_MEMDIVIDE);
                                break;
                            case SDL_Keycode.SDLK_KP_MEMMULTIPLY:
                                GlobalKeyboard.downKeys.Remove(Key.KP_MEMMULTIPLY);
                                break;
                            case SDL_Keycode.SDLK_KP_MEMRECALL:
                                GlobalKeyboard.downKeys.Remove(Key.KP_MEMRECALL);
                                break;
                            case SDL_Keycode.SDLK_KP_MEMSTORE:
                                GlobalKeyboard.downKeys.Remove(Key.KP_MEMSTORE);
                                break;
                            case SDL_Keycode.SDLK_KP_MEMSUBTRACT:
                                GlobalKeyboard.downKeys.Remove(Key.KP_MEMSUBTRACT);
                                break;
                            case SDL_Keycode.SDLK_KP_MINUS:
                                GlobalKeyboard.downKeys.Remove(Key.KP_MINUS);
                                break;
                            case SDL_Keycode.SDLK_KP_MULTIPLY:
                                GlobalKeyboard.downKeys.Remove(Key.KP_MULTIPLY);
                                break;
                            case SDL_Keycode.SDLK_KP_OCTAL:
                                GlobalKeyboard.downKeys.Remove(Key.KP_OCTAL);
                                break;
                            case SDL_Keycode.SDLK_KP_PERCENT:
                                GlobalKeyboard.downKeys.Remove(Key.KP_PERCENT);
                                break;
                            case SDL_Keycode.SDLK_KP_PERIOD:
                                GlobalKeyboard.downKeys.Remove(Key.KP_PERIOD);
                                break;
                            case SDL_Keycode.SDLK_KP_PLUS:
                                GlobalKeyboard.downKeys.Remove(Key.KP_PLUS);
                                break;
                            case SDL_Keycode.SDLK_KP_PLUSMINUS:
                                GlobalKeyboard.downKeys.Remove(Key.KP_PLUSMINUS);
                                break;
                            case SDL_Keycode.SDLK_KP_POWER:
                                GlobalKeyboard.downKeys.Remove(Key.KP_POWER);
                                break;
                            case SDL_Keycode.SDLK_KP_RIGHTBRACE:
                                GlobalKeyboard.downKeys.Remove(Key.KP_RIGHTBRACE);
                                break;
                            case SDL_Keycode.SDLK_KP_RIGHTPAREN:
                                GlobalKeyboard.downKeys.Remove(Key.KP_RIGHTPAREN);
                                break;
                            case SDL_Keycode.SDLK_KP_SPACE:
                                GlobalKeyboard.downKeys.Remove(Key.KP_SPACE);
                                break;
                            case SDL_Keycode.SDLK_KP_TAB:
                                GlobalKeyboard.downKeys.Remove(Key.KP_TAB);
                                break;
                            case SDL_Keycode.SDLK_KP_VERTICALBAR:
                                GlobalKeyboard.downKeys.Remove(Key.KP_VERTICALBAR);
                                break;
                            case SDL_Keycode.SDLK_KP_XOR:
                                GlobalKeyboard.downKeys.Remove(Key.KP_XOR);
                                break;
                            case SDL_Keycode.SDLK_l:
                                GlobalKeyboard.downKeys.Remove(Key.L);
                                break;
                            case SDL_Keycode.SDLK_LEFT:
                                GlobalKeyboard.downKeys.Remove(Key.LEFT);
                                break;
                            case SDL_Keycode.SDLK_LEFTBRACKET:
                                GlobalKeyboard.downKeys.Remove(Key.LEFTBRACKET);
                                break;
                            case SDL_Keycode.SDLK_LGUI:
                                GlobalKeyboard.downKeys.Remove(Key.LGUI);
                                break;
                            case SDL_Keycode.SDLK_m:
                                GlobalKeyboard.downKeys.Remove(Key.M);
                                break;
                            case SDL_Keycode.SDLK_MAIL:
                                GlobalKeyboard.downKeys.Remove(Key.MAIL);
                                break;
                            case SDL_Keycode.SDLK_MEDIASELECT:
                                GlobalKeyboard.downKeys.Remove(Key.MEDIASELECT);
                                break;
                            case SDL_Keycode.SDLK_MENU:
                                GlobalKeyboard.downKeys.Remove(Key.MENU);
                                break;
                            case SDL_Keycode.SDLK_MINUS:
                                GlobalKeyboard.downKeys.Remove(Key.MINUS);
                                break;
                            case SDL_Keycode.SDLK_MODE:
                                GlobalKeyboard.downKeys.Remove(Key.MODE);
                                break;
                            case SDL_Keycode.SDLK_MUTE:
                                GlobalKeyboard.downKeys.Remove(Key.MUTE);
                                break;
                            case SDL_Keycode.SDLK_n:
                                GlobalKeyboard.downKeys.Remove(Key.N);
                                break;
                            case SDL_Keycode.SDLK_NUMLOCKCLEAR:
                                GlobalKeyboard.downKeys.Remove(Key.NUMLOCKCLEAR);
                                break;
                            case SDL_Keycode.SDLK_o:
                                GlobalKeyboard.downKeys.Remove(Key.O);
                                break;
                            case SDL_Keycode.SDLK_OPER:
                                GlobalKeyboard.downKeys.Remove(Key.OPER);
                                break;
                            case SDL_Keycode.SDLK_OUT:
                                GlobalKeyboard.downKeys.Remove(Key.OUT);
                                break;
                            case SDL_Keycode.SDLK_p:
                                GlobalKeyboard.downKeys.Remove(Key.P);
                                break;
                            case SDL_Keycode.SDLK_PAGEDOWN:
                                GlobalKeyboard.downKeys.Remove(Key.PAGEDOWN);
                                break;
                            case SDL_Keycode.SDLK_PAGEUP:
                                GlobalKeyboard.downKeys.Remove(Key.PAGEUP);
                                break;
                            case SDL_Keycode.SDLK_PASTE:
                                GlobalKeyboard.downKeys.Remove(Key.PASTE);
                                break;
                            case SDL_Keycode.SDLK_PAUSE:
                                GlobalKeyboard.downKeys.Remove(Key.PAUSE);
                                break;
                            case SDL_Keycode.SDLK_PERIOD:
                                GlobalKeyboard.downKeys.Remove(Key.PERIOD);
                                break;
                            case SDL_Keycode.SDLK_POWER:
                                GlobalKeyboard.downKeys.Remove(Key.POWER);
                                break;
                            case SDL_Keycode.SDLK_PRINTSCREEN:
                                GlobalKeyboard.downKeys.Remove(Key.PRINTSCREEN);
                                break;
                            case SDL_Keycode.SDLK_PRIOR:
                                GlobalKeyboard.downKeys.Remove(Key.PRIOR);
                                break;
                            case SDL_Keycode.SDLK_q:
                                GlobalKeyboard.downKeys.Remove(Key.Q);
                                break;
                            case SDL_Keycode.SDLK_r:
                                GlobalKeyboard.downKeys.Remove(Key.R);
                                break;
                            case SDL_Keycode.SDLK_RETURN:
                                GlobalKeyboard.downKeys.Remove(Key.RETURN);
                                break;
                            case SDL_Keycode.SDLK_RETURN2:
                                GlobalKeyboard.downKeys.Remove(Key.RETURN2);
                                break;
                            case SDL_Keycode.SDLK_RGUI:
                                GlobalKeyboard.downKeys.Remove(Key.RGUI);
                                break;
                            case SDL_Keycode.SDLK_RIGHT:
                                GlobalKeyboard.downKeys.Remove(Key.RIGHT);
                                break;
                            case SDL_Keycode.SDLK_RIGHTBRACKET:
                                GlobalKeyboard.downKeys.Remove(Key.RIGHTBRACKET);
                                break;
                            case SDL_Keycode.SDLK_s:
                                GlobalKeyboard.downKeys.Remove(Key.S);
                                break;
                            case SDL_Keycode.SDLK_SCROLLLOCK:
                                GlobalKeyboard.downKeys.Remove(Key.SCROLLLOCK);
                                break;
                            case SDL_Keycode.SDLK_SELECT:
                                GlobalKeyboard.downKeys.Remove(Key.SELECT);
                                break;
                            case SDL_Keycode.SDLK_SEMICOLON:
                                GlobalKeyboard.downKeys.Remove(Key.SEMICOLON);
                                break;
                            case SDL_Keycode.SDLK_SEPARATOR:
                                GlobalKeyboard.downKeys.Remove(Key.SEPARATOR);
                                break;
                            case SDL_Keycode.SDLK_SLASH:
                                GlobalKeyboard.downKeys.Remove(Key.SLASH);
                                break;
                            case SDL_Keycode.SDLK_SLEEP:
                                GlobalKeyboard.downKeys.Remove(Key.SLEEP);
                                break;
                            case SDL_Keycode.SDLK_SPACE:
                                GlobalKeyboard.downKeys.Remove(Key.SPACE);
                                break;
                            case SDL_Keycode.SDLK_STOP:
                                GlobalKeyboard.downKeys.Remove(Key.STOP);
                                break;
                            case SDL_Keycode.SDLK_SYSREQ:
                                GlobalKeyboard.downKeys.Remove(Key.SYSREQ);
                                break;
                            case SDL_Keycode.SDLK_t:
                                GlobalKeyboard.downKeys.Remove(Key.T);
                                break;
                            case SDL_Keycode.SDLK_TAB:
                                GlobalKeyboard.downKeys.Remove(Key.TAB);
                                break;
                            case SDL_Keycode.SDLK_THOUSANDSSEPARATOR:
                                GlobalKeyboard.downKeys.Remove(Key.THOUSANDSSEPARATOR);
                                break;
                            case SDL_Keycode.SDLK_u:
                                GlobalKeyboard.downKeys.Remove(Key.U);
                                break;
                            case SDL_Keycode.SDLK_UNDO:
                                GlobalKeyboard.downKeys.Remove(Key.UNDO);
                                break;
                            case SDL_Keycode.SDLK_UNKNOWN:
                                GlobalKeyboard.downKeys.Remove(Key.UNKNOWN);
                                break;
                            case SDL_Keycode.SDLK_UP:
                                GlobalKeyboard.downKeys.Remove(Key.UP);
                                break;
                            case SDL_Keycode.SDLK_v:
                                GlobalKeyboard.downKeys.Remove(Key.V);
                                break;
                            case SDL_Keycode.SDLK_VOLUMEDOWN:
                                GlobalKeyboard.downKeys.Remove(Key.VOLUMEDOWN);
                                break;
                            case SDL_Keycode.SDLK_VOLUMEUP:
                                GlobalKeyboard.downKeys.Remove(Key.VOLUMEUP);
                                break;
                            case SDL_Keycode.SDLK_w:
                                GlobalKeyboard.downKeys.Remove(Key.W);
                                break;
                            case SDL_Keycode.SDLK_WWW:
                                GlobalKeyboard.downKeys.Remove(Key.WWW);
                                break;
                            case SDL_Keycode.SDLK_x:
                                GlobalKeyboard.downKeys.Remove(Key.X);
                                break;
                            case SDL_Keycode.SDLK_y:
                                GlobalKeyboard.downKeys.Remove(Key.Y);
                                break;
                            case SDL_Keycode.SDLK_z:
                                GlobalKeyboard.downKeys.Remove(Key.Z);
                                break;
                            case SDL_Keycode.SDLK_AMPERSAND:
                                GlobalKeyboard.downKeys.Remove(Key.AMPERSAND);
                                break;
                            case SDL_Keycode.SDLK_ASTERISK:
                                GlobalKeyboard.downKeys.Remove(Key.ASTERISK);
                                break;
                            case SDL_Keycode.SDLK_AT:
                                GlobalKeyboard.downKeys.Remove(Key.AT);
                                break;
                            case SDL_Keycode.SDLK_CARET:
                                GlobalKeyboard.downKeys.Remove(Key.CARET);
                                break;
                            case SDL_Keycode.SDLK_COLON:
                                GlobalKeyboard.downKeys.Remove(Key.COLON);
                                break;
                            case SDL_Keycode.SDLK_DOLLAR:
                                GlobalKeyboard.downKeys.Remove(Key.DOLLAR);
                                break;
                            case SDL_Keycode.SDLK_EXCLAIM:
                                GlobalKeyboard.downKeys.Remove(Key.EXCLAIM);
                                break;
                            case SDL_Keycode.SDLK_GREATER:
                                GlobalKeyboard.downKeys.Remove(Key.GREATER);
                                break;
                            case SDL_Keycode.SDLK_HASH:
                                GlobalKeyboard.downKeys.Remove(Key.HASH);
                                break;
                            case SDL_Keycode.SDLK_LEFTPAREN:
                                GlobalKeyboard.downKeys.Remove(Key.LEFTPAREN);
                                break;
                            case SDL_Keycode.SDLK_LESS:
                                GlobalKeyboard.downKeys.Remove(Key.LESS);
                                break;
                            case SDL_Keycode.SDLK_PERCENT:
                                GlobalKeyboard.downKeys.Remove(Key.PERCENT);
                                break;
                            case SDL_Keycode.SDLK_PLUS:
                                GlobalKeyboard.downKeys.Remove(Key.PLUS);
                                break;
                            case SDL_Keycode.SDLK_QUESTION:
                                GlobalKeyboard.downKeys.Remove(Key.QUESTION);
                                break;
                            case SDL_Keycode.SDLK_QUOTEDBL:
                                GlobalKeyboard.downKeys.Remove(Key.QUOTEDBL);
                                break;
                            case SDL_Keycode.SDLK_RIGHTPAREN:
                                GlobalKeyboard.downKeys.Remove(Key.RIGHTPAREN);
                                break;
                            case SDL_Keycode.SDLK_UNDERSCORE:
                                GlobalKeyboard.downKeys.Remove(Key.UNDERSCORE);
                                break;
                            case SDL_Keycode.SDLK_LCTRL:
                                GlobalKeyboard.pressedModifiers.Remove(Mod.LCtrl);
                                break;
                            case SDL_Keycode.SDLK_LSHIFT:
                                GlobalKeyboard.pressedModifiers.Remove(Mod.LShift);
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
                            if(FUI.selectedTextFieldOnChange != null)
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
                        GlobalMouse.Position = new Vector2(_x, _y);
                        GlobalMouse.RelativePosition = new Vector2((float)_x / (float)Game.Window.Width, (float)_y / (float)Game.Window.Height);
                        break;
                    case SDL_EventType.SDL_MOUSEBUTTONDOWN:
                        switch(e.button.button) {
                            case (byte)SDL_BUTTON_LEFT: {
                                if (!GlobalMouse.Down(MB.Left))
                                    GlobalMouse.pressedKeys[MB.Left] = true;
                                else
                                    GlobalMouse.pressedKeys[MB.Left] = false;
                                GlobalMouse.downKeys[MB.Left] = true;
                            } break;
                            case (byte)SDL_BUTTON_RIGHT: {
                                if (!GlobalMouse.Down(MB.Right))
                                    GlobalMouse.pressedKeys[MB.Right] = true;
                                else
                                    GlobalMouse.pressedKeys[MB.Right] = false;
                                GlobalMouse.downKeys[MB.Right] = true;
                            } break;
                        }
                        break;
                    case SDL_EventType.SDL_MOUSEBUTTONUP:
                        switch(e.button.button) {
                            case (byte)SDL_BUTTON_LEFT: {
                                GlobalMouse.downKeys[MB.Left] = false;
                            } break;
                            case (byte)SDL_BUTTON_RIGHT: {
                                GlobalMouse.downKeys[MB.Right] = false;
                            } break;
                        }
                        break;
                    case SDL_EventType.SDL_MOUSEWHEEL:
                        if(e.wheel.y > 0) // scroll up
                        {
                            if (!GlobalMouse.Down(MB.ScrollUp))
                                GlobalMouse.pressedKeys[MB.ScrollUp] = true;
                            else
                                GlobalMouse.pressedKeys[MB.ScrollUp] = false;
                            GlobalMouse.downKeys[MB.ScrollUp] = true;
                        }
                        else if(e.wheel.y < 0) // scroll down
                        {
                            if (!GlobalMouse.Down(MB.ScrollDown))
                                GlobalMouse.pressedKeys[MB.ScrollDown] = true;
                            else
                                GlobalMouse.pressedKeys[MB.ScrollDown] = false;
                            GlobalMouse.downKeys[MB.ScrollDown] = true;
                        }

                        if(e.wheel.x > 0) // scroll right
                        {
                            if (!GlobalMouse.Down(MB.ScrollRight))
                                GlobalMouse.pressedKeys[MB.ScrollRight] = true;
                            else
                                GlobalMouse.pressedKeys[MB.ScrollRight] = false;
                            GlobalMouse.downKeys[MB.ScrollRight] = true;
                        }
                        else if(e.wheel.x < 0) // scroll left
                        {
                            if (!GlobalMouse.Down(MB.ScrollLeft))
                                GlobalMouse.pressedKeys[MB.ScrollLeft] = true;
                            else
                                GlobalMouse.pressedKeys[MB.ScrollLeft] = false;
                            GlobalMouse.downKeys[MB.ScrollLeft] = true;
                        }
                        break;
                }
            }
        }
    }
}
