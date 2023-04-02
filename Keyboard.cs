using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fjord.Input;

public enum Key
{
    D
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

public static class Keyboard
{
    internal static List<Key> pressedKeys = new();
    internal static List<Key> downKeys = new();
    internal static List<Mod> pressedModifiers = new();

    internal static void AddKey(Key key)
    {
        if (!Keyboard.downKeys.Contains(Key.D))
        {
            Keyboard.downKeys.Add(key);
            Keyboard.pressedKeys.Add(key);
        }
    }

    public static bool Down(Key key)
    {
        return downKeys.Contains(key);
    }

    public static KeyboardDownBuilder DownExt(Key key)
    {
        return new KeyboardDownBuilder(key);
    }

    public static bool Pressed(Key key)
    {
        return pressedKeys.Contains(key);
    }

    public static KeyboardPressedBuilder PressedExt(Key key)
    {
        return new KeyboardPressedBuilder(key);
    }
}

public class KeyboardDownBuilder
{
    private Key key;

    public KeyboardDownBuilder(Key key)
    {
        this.key = key;
    }

    public bool With(params Mod[] mods)
    {
        bool containsModifiers = true;
        foreach(var i in mods)
        {
            if(!Keyboard.pressedModifiers.Contains(i))
            {
                containsModifiers = false;
                break;
            }
        }
        return Keyboard.downKeys.Contains(this.key) && containsModifiers;
    }
}

public class KeyboardPressedBuilder
{
    private Key key;

    public KeyboardPressedBuilder(Key key)
    {
        this.key = key;
    }

    public bool With(params Mod[] mods)
    {
        bool containsModifiers = true;
        foreach (var i in mods)
        {
            if (!Keyboard.pressedModifiers.Contains(i))
            {
                containsModifiers = false;
                break;
            }
        }
        return Keyboard.pressedKeys.Contains(this.key) && containsModifiers;
    }
}