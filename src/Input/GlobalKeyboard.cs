using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fjord.Scenes;

namespace Fjord.Input;

public enum Key
{ 
 N0,
 N1,
 N2,
 N3,
 N4,
 N5,
 N6,
 N7,
 N8,
 N9,
 A,
 AC_BACK,
 AC_BOOKMARKS,
 AC_FORWARD,
 AC_HOME,
 AC_REFRESH,
 AC_SEARCH,
 AC_STOP,
 AGAIN,
 ALTERASE,
 QUOTE,
 APPLICATION,
 AUDIOMUTE,
 AUDIONEXT,
 AUDIOPLAY,
 AUDIOPREV,
 AUDIOSTOP,
 B,
 BACKSLASH,
 BACKSPACE,
 BRIGHTNESSDOWN,
 BRIGHTNESSUP,
 C,
 CALCULATOR,
 CANCEL,
 CAPSLOCK,
 CLEAR,
 CLEARAGAIN,
 COMMA,
 COMPUTER,
 COPY,
 CRSEL,
 CURRENCYSUBUNIT,
 CURRENCYUNIT,
 CUT,
 D,
 DECIMALSEPARATOR,
 DELETE,
 DISPLAYSWITCH,
 DOWN,
 E,
 EJECT,
 END,
 EQUALS,
 ESCAPE,
 EXECUTE,
 EXSEL,
 F,
 F1,
 F10,
 F11,
 F12,
 F13,
 F14,
 F15,
 F16,
 F17,
 F18,
 F19,
 F2,
 F20,
 F21,
 F22,
 F23,
 F24,
 F3,
 F4,
 F5,
 F6,
 F7,
 F8,
 F9,
 FIND,
 G,
 BACKQUOTE,
 H,
 HELP,
 HOME,
 I,
 INSERT,
 J,
 K,
 KBDILLUMDOWN,
 KBDILLUMTOGGLE,
 KBDILLUMUP,
 KP_0,
 KP_00,
 KP_000,
 KP_1,
 KP_2,
 KP_3,
 KP_4,
 KP_5,
 KP_6,
 KP_7,
 KP_8,
 KP_9,
 KP_A,
 KP_AMPERSAND,
 KP_AT,
 KP_B,
 KP_BACKSPACE,
 KP_BINARY,
 KP_C,
 KP_CLEAR,
 KP_CLEARENTRY,
 KP_COLON,
 KP_COMMA,
 KP_D,
 KP_DBLAMPERSAND,
 KP_DBLVERTICALBAR,
 KP_DECIMAL,
 KP_DIVIDE,
 KP_E,
 KP_ENTER,
 KP_EQUALS,
 KP_EQUALSAS400,
 KP_EXCLAM,
 KP_F,
 KP_GREATER,
 KP_HASH,
 KP_HEXADECIMAL,
 KP_LEFTBRACE,
 KP_LEFTPAREN,
 KP_LESS,
 KP_MEMADD,
 KP_MEMCLEAR,
 KP_MEMDIVIDE,
 KP_MEMMULTIPLY,
 KP_MEMRECALL,
 KP_MEMSTORE,
 KP_MEMSUBTRACT,
 KP_MINUS,
 KP_MULTIPLY,
 KP_OCTAL,
 KP_PERCENT,
 KP_PERIOD,
 KP_PLUS,
 KP_PLUSMINUS,
 KP_POWER,
 KP_RIGHTBRACE,
 KP_RIGHTPAREN,
 KP_SPACE,
 KP_TAB,
 KP_VERTICALBAR,
 KP_XOR,
 L,
 LEFT,
 LEFTBRACKET,
 LGUI,
 M,
 MAIL,
 MEDIASELECT,
 MENU,
 MINUS,
 MODE,
 MUTE,
 N,
 NUMLOCKCLEAR,
 O,
 OPER,
 OUT,
 P,
 PAGEDOWN,
 PAGEUP,
 PASTE,
 PAUSE,
 PERIOD,
 POWER,
 PRINTSCREEN,
 PRIOR,
 Q,
 R,
 RETURN,
 RETURN2,
 RGUI,
 RIGHT,
 RIGHTBRACKET,
 S,
 SCROLLLOCK,
 SELECT,
 SEMICOLON,
 SEPARATOR,
 SLASH,
 SLEEP,
 SPACE,
 STOP,
 SYSREQ,
 T,
 TAB,
 THOUSANDSSEPARATOR,
 U,
 UNDO,
 UNKNOWN,
 UP,
 V,
 VOLUMEDOWN,
 VOLUMEUP,
 W,
 WWW,
 X,
 Y,
 Z,
 AMPERSAND,
 ASTERISK,
 AT,
 CARET,
 COLON,
 DOLLAR,
 EXCLAIM,
 GREATER,
 HASH,
 LEFTPAREN,
 LESS,
 PERCENT,
 PLUS,
 QUESTION,
 QUOTEDBL,
 RIGHTPAREN,
 UNDERSCORE,
}

public enum Mod
{
    LCtrl,
    RCtrl,
    LShift,
    RShift,
    RAlt,
    LAlt
}

public static class GlobalKeyboard
{
    internal static bool[] pressedKeys = new bool[229];
    internal static bool[] downKeys = new bool[229];
    internal static List<Mod> pressedModifiers = new();

    internal static void AddKey(Key key)
    {
        GlobalKeyboard.downKeys[(int)key] = true;
        GlobalKeyboard.pressedKeys[(int)key] = true;
    }

    public static bool Down(Key key)
    {
        return downKeys[(int)key];
    }

    public static bool Down(Key key, params Mod[] mods)
    {
        bool containsModifiers = true;
        foreach (var i in mods)
        {
            if (!GlobalKeyboard.pressedModifiers.Contains(i))
            {
                containsModifiers = false;
                break;
            }
        }

        return GlobalKeyboard.downKeys[(int)key] && containsModifiers;
    }

    //public static KeyboardDownBuilder DownExt(Key key)
    //{
    //    return new KeyboardDownBuilder(key);
    //}

    public static bool Pressed(Key key)
    {
        return pressedKeys[(int)key];
    }

    public static bool Pressed(Key key, params Mod[] mods)
    {
        bool containsModifiers = true;
        foreach(var i in mods)
        {
            if(!GlobalKeyboard.pressedModifiers.Contains(i))
            {
                containsModifiers = false;
                break;
            }
        }

        return GlobalKeyboard.pressedKeys[(int)key] && containsModifiers;
    }

    //public static KeyboardPressedBuilder PressedExt(Key key)
    //{
    //    return new KeyboardPressedBuilder(key);
    //}
}

//public class KeyboardDownBuilder
//{
//    private Key key;
//    private string? scene = null;

//    public KeyboardDownBuilder(Key key)
//    {
//        this.key = key;
//    }

//    public bool With(params Mod[] mods)
//    {
//        bool containsModifiers = true;
//        foreach(var i in mods)
//        {
//            if(!GlobalKeyboard.pressedModifiers.Contains(i))
//            {
//                containsModifiers = false;
//                break;
//            }
//        }

//        if(scene == null)
//            return GlobalKeyboard.pressedKeys.Contains(this.key) && containsModifiers;
//        else 
//            return Keyboard.pressedKeys.Contains(this.key) && containsModifiers && SceneHandler.Scenes[scene].MouseInsideScene;
//    }

//    public KeyboardDownBuilder InScene(string scene) 
//    {
//        this.scene = scene;
//        return this;
//    }

//    public bool Get() 
//    {
//        if(scene == null) {
//            return GlobalKeyboard.pressedKeys.Contains(key);
//        } else if(SceneHandler.Scenes[scene].MouseInsideScene) {
//            return GlobalKeyboard.pressedKeys.Contains(key);
//        } 
//        return false;
//    }
//}

//public class KeyboardPressedBuilder
//{
//    private Key key;

//    public KeyboardPressedBuilder(Key key)
//    {
//        this.key = key;
//    }

//    public bool With(params Mod[] mods)
//    {
//        bool containsModifiers = true;
//        foreach (var i in mods)
//        {
//            if (!GlobalKeyboard.pressedModifiers.Contains(i))
//            {
//                containsModifiers = false;
//                break;
//            }
//        }
//        return GlobalKeyboard.pressedKeys.Contains(this.key) && containsModifiers;
//    }
//}