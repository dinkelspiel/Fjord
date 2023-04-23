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
                                GlobalKeyboard.downKeys[(int)Key.N0] = false;
                                break;
                            case SDL_Keycode.SDLK_1:
                                GlobalKeyboard.downKeys[(int)Key.N1] = false;
                                break;
                            case SDL_Keycode.SDLK_2:
                                GlobalKeyboard.downKeys[(int)Key.N2] = false;
                                break;
                            case SDL_Keycode.SDLK_3:
                                GlobalKeyboard.downKeys[(int)Key.N3] = false;
                                break;
                            case SDL_Keycode.SDLK_4:
                                GlobalKeyboard.downKeys[(int)Key.N4] = false;
                                break;
                            case SDL_Keycode.SDLK_5:
                                GlobalKeyboard.downKeys[(int)Key.N5] = false;
                                break;
                            case SDL_Keycode.SDLK_6:
                                GlobalKeyboard.downKeys[(int)Key.N6] = false;
                                break;
                            case SDL_Keycode.SDLK_7:
                                GlobalKeyboard.downKeys[(int)Key.N7] = false;
                                break;
                            case SDL_Keycode.SDLK_8:
                                GlobalKeyboard.downKeys[(int)Key.N8] = false;
                                break;
                            case SDL_Keycode.SDLK_9:
                                GlobalKeyboard.downKeys[(int)Key.N9] = false;
                                break;
                            case SDL_Keycode.SDLK_a:
                                GlobalKeyboard.downKeys[(int)Key.A] = false;
                                break;
                            case SDL_Keycode.SDLK_AC_BACK:
                                GlobalKeyboard.downKeys[(int)Key.AC_BACK] = false;
                                break;
                            case SDL_Keycode.SDLK_AC_BOOKMARKS:
                                GlobalKeyboard.downKeys[(int)Key.AC_BOOKMARKS] = false;
                                break;
                            case SDL_Keycode.SDLK_AC_FORWARD:
                                GlobalKeyboard.downKeys[(int)Key.AC_FORWARD] = false;
                                break;
                            case SDL_Keycode.SDLK_AC_HOME:
                                GlobalKeyboard.downKeys[(int)Key.AC_HOME] = false;
                                break;
                            case SDL_Keycode.SDLK_AC_REFRESH:
                                GlobalKeyboard.downKeys[(int)Key.AC_REFRESH] = false;
                                break;
                            case SDL_Keycode.SDLK_AC_SEARCH:
                                GlobalKeyboard.downKeys[(int)Key.AC_SEARCH] = false;
                                break;
                            case SDL_Keycode.SDLK_AC_STOP:
                                GlobalKeyboard.downKeys[(int)Key.AC_STOP] = false;
                                break;
                            case SDL_Keycode.SDLK_AGAIN:
                                GlobalKeyboard.downKeys[(int)Key.AGAIN] = false;
                                break;
                            case SDL_Keycode.SDLK_ALTERASE:
                                GlobalKeyboard.downKeys[(int)Key.ALTERASE] = false;
                                break;
                            case SDL_Keycode.SDLK_QUOTE:
                                GlobalKeyboard.downKeys[(int)Key.QUOTE] = false;
                                break;
                            case SDL_Keycode.SDLK_APPLICATION:
                                GlobalKeyboard.downKeys[(int)Key.APPLICATION] = false;
                                break;
                            case SDL_Keycode.SDLK_AUDIOMUTE:
                                GlobalKeyboard.downKeys[(int)Key.AUDIOMUTE] = false;
                                break;
                            case SDL_Keycode.SDLK_AUDIONEXT:
                                GlobalKeyboard.downKeys[(int)Key.AUDIONEXT] = false;
                                break;
                            case SDL_Keycode.SDLK_AUDIOPLAY:
                                GlobalKeyboard.downKeys[(int)Key.AUDIOPLAY] = false;
                                break;
                            case SDL_Keycode.SDLK_AUDIOPREV:
                                GlobalKeyboard.downKeys[(int)Key.AUDIOPREV] = false;
                                break;
                            case SDL_Keycode.SDLK_AUDIOSTOP:
                                GlobalKeyboard.downKeys[(int)Key.AUDIOSTOP] = false;
                                break;
                            case SDL_Keycode.SDLK_b:
                                GlobalKeyboard.downKeys[(int)Key.B] = false;
                                break;
                            case SDL_Keycode.SDLK_BACKSLASH:
                                GlobalKeyboard.downKeys[(int)Key.BACKSLASH] = false;
                                break;
                            case SDL_Keycode.SDLK_BACKSPACE:
                                GlobalKeyboard.downKeys[(int)Key.BACKSPACE] = false;
                                break;
                            case SDL_Keycode.SDLK_BRIGHTNESSDOWN:
                                GlobalKeyboard.downKeys[(int)Key.BRIGHTNESSDOWN] = false;
                                break;
                            case SDL_Keycode.SDLK_BRIGHTNESSUP:
                                GlobalKeyboard.downKeys[(int)Key.BRIGHTNESSUP] = false;
                                break;
                            case SDL_Keycode.SDLK_c:
                                GlobalKeyboard.downKeys[(int)Key.C] = false;
                                break;
                            case SDL_Keycode.SDLK_CALCULATOR:
                                GlobalKeyboard.downKeys[(int)Key.CALCULATOR] = false;
                                break;
                            case SDL_Keycode.SDLK_CANCEL:
                                GlobalKeyboard.downKeys[(int)Key.CANCEL] = false;
                                break;
                            case SDL_Keycode.SDLK_CAPSLOCK:
                                GlobalKeyboard.downKeys[(int)Key.CAPSLOCK] = false;
                                break;
                            case SDL_Keycode.SDLK_CLEAR:
                                GlobalKeyboard.downKeys[(int)Key.CLEAR] = false;
                                break;
                            case SDL_Keycode.SDLK_CLEARAGAIN:
                                GlobalKeyboard.downKeys[(int)Key.CLEARAGAIN] = false;
                                break;
                            case SDL_Keycode.SDLK_COMMA:
                                GlobalKeyboard.downKeys[(int)Key.COMMA] = false;
                                break;
                            case SDL_Keycode.SDLK_COMPUTER:
                                GlobalKeyboard.downKeys[(int)Key.COMPUTER] = false;
                                break;
                            case SDL_Keycode.SDLK_COPY:
                                GlobalKeyboard.downKeys[(int)Key.COPY] = false;
                                break;
                            case SDL_Keycode.SDLK_CRSEL:
                                GlobalKeyboard.downKeys[(int)Key.CRSEL] = false;
                                break;
                            case SDL_Keycode.SDLK_CURRENCYSUBUNIT:
                                GlobalKeyboard.downKeys[(int)Key.CURRENCYSUBUNIT] = false;
                                break;
                            case SDL_Keycode.SDLK_CURRENCYUNIT:
                                GlobalKeyboard.downKeys[(int)Key.CURRENCYUNIT] = false;
                                break;
                            case SDL_Keycode.SDLK_CUT:
                                GlobalKeyboard.downKeys[(int)Key.CUT] = false;
                                break;
                            case SDL_Keycode.SDLK_d:
                                GlobalKeyboard.downKeys[(int)Key.D] = false;
                                break;
                            case SDL_Keycode.SDLK_DECIMALSEPARATOR:
                                GlobalKeyboard.downKeys[(int)Key.DECIMALSEPARATOR] = false;
                                break;
                            case SDL_Keycode.SDLK_DELETE:
                                GlobalKeyboard.downKeys[(int)Key.DELETE] = false;
                                break;
                            case SDL_Keycode.SDLK_DISPLAYSWITCH:
                                GlobalKeyboard.downKeys[(int)Key.DISPLAYSWITCH] = false;
                                break;
                            case SDL_Keycode.SDLK_DOWN:
                                GlobalKeyboard.downKeys[(int)Key.DOWN] = false;
                                break;
                            case SDL_Keycode.SDLK_e:
                                GlobalKeyboard.downKeys[(int)Key.E] = false;
                                break;
                            case SDL_Keycode.SDLK_EJECT:
                                GlobalKeyboard.downKeys[(int)Key.EJECT] = false;
                                break;
                            case SDL_Keycode.SDLK_END:
                                GlobalKeyboard.downKeys[(int)Key.END] = false;
                                break;
                            case SDL_Keycode.SDLK_EQUALS:
                                GlobalKeyboard.downKeys[(int)Key.EQUALS] = false;
                                break;
                            case SDL_Keycode.SDLK_ESCAPE:
                                GlobalKeyboard.downKeys[(int)Key.ESCAPE] = false;
                                break;
                            case SDL_Keycode.SDLK_EXECUTE:
                                GlobalKeyboard.downKeys[(int)Key.EXECUTE] = false;
                                break;
                            case SDL_Keycode.SDLK_EXSEL:
                                GlobalKeyboard.downKeys[(int)Key.EXSEL] = false;
                                break;
                            case SDL_Keycode.SDLK_f:
                                GlobalKeyboard.downKeys[(int)Key.F] = false;
                                break;
                            case SDL_Keycode.SDLK_F1:
                                GlobalKeyboard.downKeys[(int)Key.F1] = false;
                                break;
                            case SDL_Keycode.SDLK_F10:
                                GlobalKeyboard.downKeys[(int)Key.F10] = false;
                                break;
                            case SDL_Keycode.SDLK_F11:
                                GlobalKeyboard.downKeys[(int)Key.F11] = false;
                                break;
                            case SDL_Keycode.SDLK_F12:
                                GlobalKeyboard.downKeys[(int)Key.F12] = false;
                                break;
                            case SDL_Keycode.SDLK_F13:
                                GlobalKeyboard.downKeys[(int)Key.F13] = false;
                                break;
                            case SDL_Keycode.SDLK_F14:
                                GlobalKeyboard.downKeys[(int)Key.F14] = false;
                                break;
                            case SDL_Keycode.SDLK_F15:
                                GlobalKeyboard.downKeys[(int)Key.F15] = false;
                                break;
                            case SDL_Keycode.SDLK_F16:
                                GlobalKeyboard.downKeys[(int)Key.F16] = false;
                                break;
                            case SDL_Keycode.SDLK_F17:
                                GlobalKeyboard.downKeys[(int)Key.F17] = false;
                                break;
                            case SDL_Keycode.SDLK_F18:
                                GlobalKeyboard.downKeys[(int)Key.F18] = false;
                                break;
                            case SDL_Keycode.SDLK_F19:
                                GlobalKeyboard.downKeys[(int)Key.F19] = false;
                                break;
                            case SDL_Keycode.SDLK_F2:
                                GlobalKeyboard.downKeys[(int)Key.F2] = false;
                                break;
                            case SDL_Keycode.SDLK_F20:
                                GlobalKeyboard.downKeys[(int)Key.F20] = false;
                                break;
                            case SDL_Keycode.SDLK_F21:
                                GlobalKeyboard.downKeys[(int)Key.F21] = false;
                                break;
                            case SDL_Keycode.SDLK_F22:
                                GlobalKeyboard.downKeys[(int)Key.F22] = false;
                                break;
                            case SDL_Keycode.SDLK_F23:
                                GlobalKeyboard.downKeys[(int)Key.F23] = false;
                                break;
                            case SDL_Keycode.SDLK_F24:
                                GlobalKeyboard.downKeys[(int)Key.F24] = false;
                                break;
                            case SDL_Keycode.SDLK_F3:
                                GlobalKeyboard.downKeys[(int)Key.F3] = false;
                                break;
                            case SDL_Keycode.SDLK_F4:
                                GlobalKeyboard.downKeys[(int)Key.F4] = false;
                                break;
                            case SDL_Keycode.SDLK_F5:
                                GlobalKeyboard.downKeys[(int)Key.F5] = false;
                                break;
                            case SDL_Keycode.SDLK_F6:
                                GlobalKeyboard.downKeys[(int)Key.F6] = false;
                                break;
                            case SDL_Keycode.SDLK_F7:
                                GlobalKeyboard.downKeys[(int)Key.F7] = false;
                                break;
                            case SDL_Keycode.SDLK_F8:
                                GlobalKeyboard.downKeys[(int)Key.F8] = false;
                                break;
                            case SDL_Keycode.SDLK_F9:
                                GlobalKeyboard.downKeys[(int)Key.F9] = false;
                                break;
                            case SDL_Keycode.SDLK_FIND:
                                GlobalKeyboard.downKeys[(int)Key.FIND] = false;
                                break;
                            case SDL_Keycode.SDLK_g:
                                GlobalKeyboard.downKeys[(int)Key.G] = false;
                                break;
                            case SDL_Keycode.SDLK_BACKQUOTE:
                                GlobalKeyboard.downKeys[(int)Key.BACKQUOTE] = false;
                                break;
                            case SDL_Keycode.SDLK_h:
                                GlobalKeyboard.downKeys[(int)Key.H] = false;
                                break;
                            case SDL_Keycode.SDLK_HELP:
                                GlobalKeyboard.downKeys[(int)Key.HELP] = false;
                                break;
                            case SDL_Keycode.SDLK_HOME:
                                GlobalKeyboard.downKeys[(int)Key.HOME] = false;
                                break;
                            case SDL_Keycode.SDLK_i:
                                GlobalKeyboard.downKeys[(int)Key.I] = false;
                                break;
                            case SDL_Keycode.SDLK_INSERT:
                                GlobalKeyboard.downKeys[(int)Key.INSERT] = false;
                                break;
                            case SDL_Keycode.SDLK_j:
                                GlobalKeyboard.downKeys[(int)Key.J] = false;
                                break;
                            case SDL_Keycode.SDLK_k:
                                GlobalKeyboard.downKeys[(int)Key.K] = false;
                                break;
                            case SDL_Keycode.SDLK_KBDILLUMDOWN:
                                GlobalKeyboard.downKeys[(int)Key.KBDILLUMDOWN] = false;
                                break;
                            case SDL_Keycode.SDLK_KBDILLUMTOGGLE:
                                GlobalKeyboard.downKeys[(int)Key.KBDILLUMTOGGLE] = false;
                                break;
                            case SDL_Keycode.SDLK_KBDILLUMUP:
                                GlobalKeyboard.downKeys[(int)Key.KBDILLUMUP] = false;
                                break;
                            case SDL_Keycode.SDLK_KP_0:
                                GlobalKeyboard.downKeys[(int)Key.KP_0] = false;
                                break;
                            case SDL_Keycode.SDLK_KP_00:
                                GlobalKeyboard.downKeys[(int)Key.KP_00] = false;
                                break;
                            case SDL_Keycode.SDLK_KP_000:
                                GlobalKeyboard.downKeys[(int)Key.KP_000] = false;
                                break;
                            case SDL_Keycode.SDLK_KP_1:
                                GlobalKeyboard.downKeys[(int)Key.KP_1] = false;
                                break;
                            case SDL_Keycode.SDLK_KP_2:
                                GlobalKeyboard.downKeys[(int)Key.KP_2] = false;
                                break;
                            case SDL_Keycode.SDLK_KP_3:
                                GlobalKeyboard.downKeys[(int)Key.KP_3] = false;
                                break;
                            case SDL_Keycode.SDLK_KP_4:
                                GlobalKeyboard.downKeys[(int)Key.KP_4] = false;
                                break;
                            case SDL_Keycode.SDLK_KP_5:
                                GlobalKeyboard.downKeys[(int)Key.KP_5] = false;
                                break;
                            case SDL_Keycode.SDLK_KP_6:
                                GlobalKeyboard.downKeys[(int)Key.KP_6] = false;
                                break;
                            case SDL_Keycode.SDLK_KP_7:
                                GlobalKeyboard.downKeys[(int)Key.KP_7] = false;
                                break;
                            case SDL_Keycode.SDLK_KP_8:
                                GlobalKeyboard.downKeys[(int)Key.KP_8] = false;
                                break;
                            case SDL_Keycode.SDLK_KP_9:
                                GlobalKeyboard.downKeys[(int)Key.KP_9] = false;
                                break;
                            case SDL_Keycode.SDLK_KP_A:
                                GlobalKeyboard.downKeys[(int)Key.KP_A] = false;
                                break;
                            case SDL_Keycode.SDLK_KP_AMPERSAND:
                                GlobalKeyboard.downKeys[(int)Key.KP_AMPERSAND] = false;
                                break;
                            case SDL_Keycode.SDLK_KP_AT:
                                GlobalKeyboard.downKeys[(int)Key.KP_AT] = false;
                                break;
                            case SDL_Keycode.SDLK_KP_B:
                                GlobalKeyboard.downKeys[(int)Key.KP_B] = false;
                                break;
                            case SDL_Keycode.SDLK_KP_BACKSPACE:
                                GlobalKeyboard.downKeys[(int)Key.KP_BACKSPACE] = false;
                                break;
                            case SDL_Keycode.SDLK_KP_BINARY:
                                GlobalKeyboard.downKeys[(int)Key.KP_BINARY] = false;
                                break;
                            case SDL_Keycode.SDLK_KP_C:
                                GlobalKeyboard.downKeys[(int)Key.KP_C] = false;
                                break;
                            case SDL_Keycode.SDLK_KP_CLEAR:
                                GlobalKeyboard.downKeys[(int)Key.KP_CLEAR] = false;
                                break;
                            case SDL_Keycode.SDLK_KP_CLEARENTRY:
                                GlobalKeyboard.downKeys[(int)Key.KP_CLEARENTRY] = false;
                                break;
                            case SDL_Keycode.SDLK_KP_COLON:
                                GlobalKeyboard.downKeys[(int)Key.KP_COLON] = false;
                                break;
                            case SDL_Keycode.SDLK_KP_COMMA:
                                GlobalKeyboard.downKeys[(int)Key.KP_COMMA] = false;
                                break;
                            case SDL_Keycode.SDLK_KP_D:
                                GlobalKeyboard.downKeys[(int)Key.KP_D] = false;
                                break;
                            case SDL_Keycode.SDLK_KP_DBLAMPERSAND:
                                GlobalKeyboard.downKeys[(int)Key.KP_DBLAMPERSAND] = false;
                                break;
                            case SDL_Keycode.SDLK_KP_DBLVERTICALBAR:
                                GlobalKeyboard.downKeys[(int)Key.KP_DBLVERTICALBAR] = false;
                                break;
                            case SDL_Keycode.SDLK_KP_DECIMAL:
                                GlobalKeyboard.downKeys[(int)Key.KP_DECIMAL] = false;
                                break;
                            case SDL_Keycode.SDLK_KP_DIVIDE:
                                GlobalKeyboard.downKeys[(int)Key.KP_DIVIDE] = false;
                                break;
                            case SDL_Keycode.SDLK_KP_E:
                                GlobalKeyboard.downKeys[(int)Key.KP_E] = false;
                                break;
                            case SDL_Keycode.SDLK_KP_ENTER:
                                GlobalKeyboard.downKeys[(int)Key.KP_ENTER] = false;
                                break;
                            case SDL_Keycode.SDLK_KP_EQUALS:
                                GlobalKeyboard.downKeys[(int)Key.KP_EQUALS] = false;
                                break;
                            case SDL_Keycode.SDLK_KP_EQUALSAS400:
                                GlobalKeyboard.downKeys[(int)Key.KP_EQUALSAS400] = false;
                                break;
                            case SDL_Keycode.SDLK_KP_EXCLAM:
                                GlobalKeyboard.downKeys[(int)Key.KP_EXCLAM] = false;
                                break;
                            case SDL_Keycode.SDLK_KP_F:
                                GlobalKeyboard.downKeys[(int)Key.KP_F] = false;
                                break;
                            case SDL_Keycode.SDLK_KP_GREATER:
                                GlobalKeyboard.downKeys[(int)Key.KP_GREATER] = false;
                                break;
                            case SDL_Keycode.SDLK_KP_HASH:
                                GlobalKeyboard.downKeys[(int)Key.KP_HASH] = false;
                                break;
                            case SDL_Keycode.SDLK_KP_HEXADECIMAL:
                                GlobalKeyboard.downKeys[(int)Key.KP_HEXADECIMAL] = false;
                                break;
                            case SDL_Keycode.SDLK_KP_LEFTBRACE:
                                GlobalKeyboard.downKeys[(int)Key.KP_LEFTBRACE] = false;
                                break;
                            case SDL_Keycode.SDLK_KP_LEFTPAREN:
                                GlobalKeyboard.downKeys[(int)Key.KP_LEFTPAREN] = false;
                                break;
                            case SDL_Keycode.SDLK_KP_LESS:
                                GlobalKeyboard.downKeys[(int)Key.KP_LESS] = false;
                                break;
                            case SDL_Keycode.SDLK_KP_MEMADD:
                                GlobalKeyboard.downKeys[(int)Key.KP_MEMADD] = false;
                                break;
                            case SDL_Keycode.SDLK_KP_MEMCLEAR:
                                GlobalKeyboard.downKeys[(int)Key.KP_MEMCLEAR] = false;
                                break;
                            case SDL_Keycode.SDLK_KP_MEMDIVIDE:
                                GlobalKeyboard.downKeys[(int)Key.KP_MEMDIVIDE] = false;
                                break;
                            case SDL_Keycode.SDLK_KP_MEMMULTIPLY:
                                GlobalKeyboard.downKeys[(int)Key.KP_MEMMULTIPLY] = false;
                                break;
                            case SDL_Keycode.SDLK_KP_MEMRECALL:
                                GlobalKeyboard.downKeys[(int)Key.KP_MEMRECALL] = false;
                                break;
                            case SDL_Keycode.SDLK_KP_MEMSTORE:
                                GlobalKeyboard.downKeys[(int)Key.KP_MEMSTORE] = false;
                                break;
                            case SDL_Keycode.SDLK_KP_MEMSUBTRACT:
                                GlobalKeyboard.downKeys[(int)Key.KP_MEMSUBTRACT] = false;
                                break;
                            case SDL_Keycode.SDLK_KP_MINUS:
                                GlobalKeyboard.downKeys[(int)Key.KP_MINUS] = false;
                                break;
                            case SDL_Keycode.SDLK_KP_MULTIPLY:
                                GlobalKeyboard.downKeys[(int)Key.KP_MULTIPLY] = false;
                                break;
                            case SDL_Keycode.SDLK_KP_OCTAL:
                                GlobalKeyboard.downKeys[(int)Key.KP_OCTAL] = false;
                                break;
                            case SDL_Keycode.SDLK_KP_PERCENT:
                                GlobalKeyboard.downKeys[(int)Key.KP_PERCENT] = false;
                                break;
                            case SDL_Keycode.SDLK_KP_PERIOD:
                                GlobalKeyboard.downKeys[(int)Key.KP_PERIOD] = false;
                                break;
                            case SDL_Keycode.SDLK_KP_PLUS:
                                GlobalKeyboard.downKeys[(int)Key.KP_PLUS] = false;
                                break;
                            case SDL_Keycode.SDLK_KP_PLUSMINUS:
                                GlobalKeyboard.downKeys[(int)Key.KP_PLUSMINUS] = false;
                                break;
                            case SDL_Keycode.SDLK_KP_POWER:
                                GlobalKeyboard.downKeys[(int)Key.KP_POWER] = false;
                                break;
                            case SDL_Keycode.SDLK_KP_RIGHTBRACE:
                                GlobalKeyboard.downKeys[(int)Key.KP_RIGHTBRACE] = false;
                                break;
                            case SDL_Keycode.SDLK_KP_RIGHTPAREN:
                                GlobalKeyboard.downKeys[(int)Key.KP_RIGHTPAREN] = false;
                                break;
                            case SDL_Keycode.SDLK_KP_SPACE:
                                GlobalKeyboard.downKeys[(int)Key.KP_SPACE] = false;
                                break;
                            case SDL_Keycode.SDLK_KP_TAB:
                                GlobalKeyboard.downKeys[(int)Key.KP_TAB] = false;
                                break;
                            case SDL_Keycode.SDLK_KP_VERTICALBAR:
                                GlobalKeyboard.downKeys[(int)Key.KP_VERTICALBAR] = false;
                                break;
                            case SDL_Keycode.SDLK_KP_XOR:
                                GlobalKeyboard.downKeys[(int)Key.KP_XOR] = false;
                                break;
                            case SDL_Keycode.SDLK_l:
                                GlobalKeyboard.downKeys[(int)Key.L] = false;
                                break;
                            case SDL_Keycode.SDLK_LEFT:
                                GlobalKeyboard.downKeys[(int)Key.LEFT] = false;
                                break;
                            case SDL_Keycode.SDLK_LEFTBRACKET:
                                GlobalKeyboard.downKeys[(int)Key.LEFTBRACKET] = false;
                                break;
                            case SDL_Keycode.SDLK_LGUI:
                                GlobalKeyboard.downKeys[(int)Key.LGUI] = false;
                                break;
                            case SDL_Keycode.SDLK_m:
                                GlobalKeyboard.downKeys[(int)Key.M] = false;
                                break;
                            case SDL_Keycode.SDLK_MAIL:
                                GlobalKeyboard.downKeys[(int)Key.MAIL] = false;
                                break;
                            case SDL_Keycode.SDLK_MEDIASELECT:
                                GlobalKeyboard.downKeys[(int)Key.MEDIASELECT] = false;
                                break;
                            case SDL_Keycode.SDLK_MENU:
                                GlobalKeyboard.downKeys[(int)Key.MENU] = false;
                                break;
                            case SDL_Keycode.SDLK_MINUS:
                                GlobalKeyboard.downKeys[(int)Key.MINUS] = false;
                                break;
                            case SDL_Keycode.SDLK_MODE:
                                GlobalKeyboard.downKeys[(int)Key.MODE] = false;
                                break;
                            case SDL_Keycode.SDLK_MUTE:
                                GlobalKeyboard.downKeys[(int)Key.MUTE] = false;
                                break;
                            case SDL_Keycode.SDLK_n:
                                GlobalKeyboard.downKeys[(int)Key.N] = false;
                                break;
                            case SDL_Keycode.SDLK_NUMLOCKCLEAR:
                                GlobalKeyboard.downKeys[(int)Key.NUMLOCKCLEAR] = false;
                                break;
                            case SDL_Keycode.SDLK_o:
                                GlobalKeyboard.downKeys[(int)Key.O] = false;
                                break;
                            case SDL_Keycode.SDLK_OPER:
                                GlobalKeyboard.downKeys[(int)Key.OPER] = false;
                                break;
                            case SDL_Keycode.SDLK_OUT:
                                GlobalKeyboard.downKeys[(int)Key.OUT] = false;
                                break;
                            case SDL_Keycode.SDLK_p:
                                GlobalKeyboard.downKeys[(int)Key.P] = false;
                                break;
                            case SDL_Keycode.SDLK_PAGEDOWN:
                                GlobalKeyboard.downKeys[(int)Key.PAGEDOWN] = false;
                                break;
                            case SDL_Keycode.SDLK_PAGEUP:
                                GlobalKeyboard.downKeys[(int)Key.PAGEUP] = false;
                                break;
                            case SDL_Keycode.SDLK_PASTE:
                                GlobalKeyboard.downKeys[(int)Key.PASTE] = false;
                                break;
                            case SDL_Keycode.SDLK_PAUSE:
                                GlobalKeyboard.downKeys[(int)Key.PAUSE] = false;
                                break;
                            case SDL_Keycode.SDLK_PERIOD:
                                GlobalKeyboard.downKeys[(int)Key.PERIOD] = false;
                                break;
                            case SDL_Keycode.SDLK_POWER:
                                GlobalKeyboard.downKeys[(int)Key.POWER] = false;
                                break;
                            case SDL_Keycode.SDLK_PRINTSCREEN:
                                GlobalKeyboard.downKeys[(int)Key.PRINTSCREEN] = false;
                                break;
                            case SDL_Keycode.SDLK_PRIOR:
                                GlobalKeyboard.downKeys[(int)Key.PRIOR] = false;
                                break;
                            case SDL_Keycode.SDLK_q:
                                GlobalKeyboard.downKeys[(int)Key.Q] = false;
                                break;
                            case SDL_Keycode.SDLK_r:
                                GlobalKeyboard.downKeys[(int)Key.R] = false;
                                break;
                            case SDL_Keycode.SDLK_RETURN:
                                GlobalKeyboard.downKeys[(int)Key.RETURN] = false;
                                break;
                            case SDL_Keycode.SDLK_RETURN2:
                                GlobalKeyboard.downKeys[(int)Key.RETURN2] = false;
                                break;
                            case SDL_Keycode.SDLK_RGUI:
                                GlobalKeyboard.downKeys[(int)Key.RGUI] = false;
                                break;
                            case SDL_Keycode.SDLK_RIGHT:
                                GlobalKeyboard.downKeys[(int)Key.RIGHT] = false;
                                break;
                            case SDL_Keycode.SDLK_RIGHTBRACKET:
                                GlobalKeyboard.downKeys[(int)Key.RIGHTBRACKET] = false;
                                break;
                            case SDL_Keycode.SDLK_s:
                                GlobalKeyboard.downKeys[(int)Key.S] = false;
                                break;
                            case SDL_Keycode.SDLK_SCROLLLOCK:
                                GlobalKeyboard.downKeys[(int)Key.SCROLLLOCK] = false;
                                break;
                            case SDL_Keycode.SDLK_SELECT:
                                GlobalKeyboard.downKeys[(int)Key.SELECT] = false;
                                break;
                            case SDL_Keycode.SDLK_SEMICOLON:
                                GlobalKeyboard.downKeys[(int)Key.SEMICOLON] = false;
                                break;
                            case SDL_Keycode.SDLK_SEPARATOR:
                                GlobalKeyboard.downKeys[(int)Key.SEPARATOR] = false;
                                break;
                            case SDL_Keycode.SDLK_SLASH:
                                GlobalKeyboard.downKeys[(int)Key.SLASH] = false;
                                break;
                            case SDL_Keycode.SDLK_SLEEP:
                                GlobalKeyboard.downKeys[(int)Key.SLEEP] = false;
                                break;
                            case SDL_Keycode.SDLK_SPACE:
                                GlobalKeyboard.downKeys[(int)Key.SPACE] = false;
                                break;
                            case SDL_Keycode.SDLK_STOP:
                                GlobalKeyboard.downKeys[(int)Key.STOP] = false;
                                break;
                            case SDL_Keycode.SDLK_SYSREQ:
                                GlobalKeyboard.downKeys[(int)Key.SYSREQ] = false;
                                break;
                            case SDL_Keycode.SDLK_t:
                                GlobalKeyboard.downKeys[(int)Key.T] = false;
                                break;
                            case SDL_Keycode.SDLK_TAB:
                                GlobalKeyboard.downKeys[(int)Key.TAB] = false;
                                break;
                            case SDL_Keycode.SDLK_THOUSANDSSEPARATOR:
                                GlobalKeyboard.downKeys[(int)Key.THOUSANDSSEPARATOR] = false;
                                break;
                            case SDL_Keycode.SDLK_u:
                                GlobalKeyboard.downKeys[(int)Key.U] = false;
                                break;
                            case SDL_Keycode.SDLK_UNDO:
                                GlobalKeyboard.downKeys[(int)Key.UNDO] = false;
                                break;
                            case SDL_Keycode.SDLK_UNKNOWN:
                                GlobalKeyboard.downKeys[(int)Key.UNKNOWN] = false;
                                break;
                            case SDL_Keycode.SDLK_UP:
                                GlobalKeyboard.downKeys[(int)Key.UP] = false;
                                break;
                            case SDL_Keycode.SDLK_v:
                                GlobalKeyboard.downKeys[(int)Key.V] = false;
                                break;
                            case SDL_Keycode.SDLK_VOLUMEDOWN:
                                GlobalKeyboard.downKeys[(int)Key.VOLUMEDOWN] = false;
                                break;
                            case SDL_Keycode.SDLK_VOLUMEUP:
                                GlobalKeyboard.downKeys[(int)Key.VOLUMEUP] = false;
                                break;
                            case SDL_Keycode.SDLK_w:
                                GlobalKeyboard.downKeys[(int)Key.W] = false;
                                break;
                            case SDL_Keycode.SDLK_WWW:
                                GlobalKeyboard.downKeys[(int)Key.WWW] = false;
                                break;
                            case SDL_Keycode.SDLK_x:
                                GlobalKeyboard.downKeys[(int)Key.X] = false;
                                break;
                            case SDL_Keycode.SDLK_y:
                                GlobalKeyboard.downKeys[(int)Key.Y] = false;
                                break;
                            case SDL_Keycode.SDLK_z:
                                GlobalKeyboard.downKeys[(int)Key.Z] = false;
                                break;
                            case SDL_Keycode.SDLK_AMPERSAND:
                                GlobalKeyboard.downKeys[(int)Key.AMPERSAND] = false;
                                break;
                            case SDL_Keycode.SDLK_ASTERISK:
                                GlobalKeyboard.downKeys[(int)Key.ASTERISK] = false;
                                break;
                            case SDL_Keycode.SDLK_AT:
                                GlobalKeyboard.downKeys[(int)Key.AT] = false;
                                break;
                            case SDL_Keycode.SDLK_CARET:
                                GlobalKeyboard.downKeys[(int)Key.CARET] = false;
                                break;
                            case SDL_Keycode.SDLK_COLON:
                                GlobalKeyboard.downKeys[(int)Key.COLON] = false;
                                break;
                            case SDL_Keycode.SDLK_DOLLAR:
                                GlobalKeyboard.downKeys[(int)Key.DOLLAR] = false;
                                break;
                            case SDL_Keycode.SDLK_EXCLAIM:
                                GlobalKeyboard.downKeys[(int)Key.EXCLAIM] = false;
                                break;
                            case SDL_Keycode.SDLK_GREATER:
                                GlobalKeyboard.downKeys[(int)Key.GREATER] = false;
                                break;
                            case SDL_Keycode.SDLK_HASH:
                                GlobalKeyboard.downKeys[(int)Key.HASH] = false;
                                break;
                            case SDL_Keycode.SDLK_LEFTPAREN:
                                GlobalKeyboard.downKeys[(int)Key.LEFTPAREN] = false;
                                break;
                            case SDL_Keycode.SDLK_LESS:
                                GlobalKeyboard.downKeys[(int)Key.LESS] = false;
                                break;
                            case SDL_Keycode.SDLK_PERCENT:
                                GlobalKeyboard.downKeys[(int)Key.PERCENT] = false;
                                break;
                            case SDL_Keycode.SDLK_PLUS:
                                GlobalKeyboard.downKeys[(int)Key.PLUS] = false;
                                break;
                            case SDL_Keycode.SDLK_QUESTION:
                                GlobalKeyboard.downKeys[(int)Key.QUESTION] = false;
                                break;
                            case SDL_Keycode.SDLK_QUOTEDBL:
                                GlobalKeyboard.downKeys[(int)Key.QUOTEDBL] = false;
                                break;
                            case SDL_Keycode.SDLK_RIGHTPAREN:
                                GlobalKeyboard.downKeys[(int)Key.RIGHTPAREN] = false;
                                break;
                            case SDL_Keycode.SDLK_UNDERSCORE:
                                GlobalKeyboard.downKeys[(int)Key.UNDERSCORE] = false;
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
