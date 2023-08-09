using System.Reflection;
using Fjord.Scenes;

namespace Fjord.Input;

public record InputEvent {
    public record Keyboard(Key key, params Mod[] mods) : InputEvent();
    public record Mouse(MB mb, params Mod[] mods) : InputEvent();
}

public interface InputAction
{
    public InputEvent[] Events { get; init; }
}

public static class GlobalInput
{
    private static List<InputAction> _actions = new();

    public static void Initialize()
    {
        var assembly = Assembly.GetExecutingAssembly();

        var derivedTypes = assembly.GetTypes().Where(t =>
            t != typeof(InputAction) &&
            typeof(InputAction).IsAssignableFrom(t));

        foreach (var type in derivedTypes)
        {
            var action = Activator.CreateInstance(type) as InputAction;
            _register(action!);
        }
    }

    public static bool Pressed<T>() where T : InputAction
    {
        for (var i = 0; i < _actions.Count; i++)
        {
            if(_actions[i].GetType() != typeof(T))
                continue;
            
            return _actions[i].Events.Any(e => e switch {
                InputEvent.Keyboard ev => GlobalKeyboard.Pressed(ev.key, ev.mods),
                InputEvent.Mouse ev => GlobalMouse.Pressed(ev.mb, ev.mods),
                _ => false
            });
        }

        return false;
    }

    public static bool Released<T>() where T : InputAction
    {
        for (var i = 0; i < _actions.Count; i++)
        {
            if(_actions[i].GetType() != typeof(T))
                continue;
            
            return _actions[i].Events.Any(e => e switch {
                InputEvent.Keyboard ev => GlobalKeyboard.Released(ev.key, ev.mods),
                InputEvent.Mouse ev => GlobalMouse.Released(ev.mb, ev.mods),
                _ => false
            });
        }

        return false;
    }

    public static bool Down<T>() where T : InputAction
    {
        for (var i = 0; i < _actions.Count; i++)
        {
            if(_actions[i].GetType() != typeof(T))
                continue;
            
            return _actions[i].Events.Any(e => e switch {
                InputEvent.Keyboard ev => GlobalKeyboard.Down(ev.key, ev.mods),
                InputEvent.Mouse ev => GlobalMouse.Down(ev.mb, ev.mods),
                _ => false
            });
        }

        return false;
    }

    private static void _register(InputAction action)
    {
        _actions.Add(action);
    }
}