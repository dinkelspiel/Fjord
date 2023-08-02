using System.Reflection;
using Fjord.Scenes;

namespace Fjord.Input;

public record InputEvent {
    public record Keyboard(Key key, params Mod[] mods) : InputEvent();
    public record Mouse(MB mb, params Mod[] mods) : InputEvent();
}

public class InputAction
{   
    public List<InputEvent> events = new();

    protected InputAction(params InputEvent[] events)
    {
        List<InputEvent> ev = new();
        events.ToList().ForEach(e => {
            ev.Add(e);
        });
        this.events = ev;
    }

    public InputAction()
    {

    }
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
            Register(action!);
        }
    }

    public static bool Pressed<T>() where T : InputAction
    {
        var validActions = _actions.Where(e => e.GetType() == typeof(T)).ToArray();

        if(validActions.Length == 0)
            return false;

        var action = validActions[0];

        return action.events.Any(e => e switch {
            InputEvent.Keyboard ev => GlobalKeyboard.Pressed(ev.key, ev.mods),
            InputEvent.Mouse ev => GlobalMouse.Pressed(ev.mb, ev.mods),
            _ => false
        });
    }

    public static bool Released<T>() where T : InputAction
    {
        var validActions = _actions.Where(e => e.GetType() == typeof(T)).ToArray();

        if(validActions.Length == 0)
            return false;

        var action = validActions[0];

        return action.events.Any(e => e switch {
            InputEvent.Keyboard ev => GlobalKeyboard.Released(ev.key, ev.mods),
            InputEvent.Mouse ev => GlobalMouse.Released(ev.mb, ev.mods),
            _ => false
        });
    }

    public static bool Down<T>() where T : InputAction
    {
        var validActions = _actions.Where(e => e.GetType() == typeof(T)).ToArray();

        if(validActions.Length == 0)
        {
            Debug.Log(LogLevel.Warning, $"InputAction \"{typeof(T).Name}\" is not registered");
            return false;
        }

        var action = validActions[0];

        return action.events.Any(e => e switch {
            InputEvent.Keyboard ev => GlobalKeyboard.Down(ev.key, ev.mods),
            InputEvent.Mouse ev => GlobalMouse.Down(ev.mb, ev.mods),
            _ => false
        });
    }

    public static void Register(InputAction action)
    {
        _actions.Add(action);
    }
}