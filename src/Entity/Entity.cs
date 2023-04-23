namespace Fjord.Scenes;

public class Entity
{
    internal SceneKeyboard Keyboard;
    internal SceneMouse Mouse;

    internal List<Component> Components = new(); 

    public Entity(SceneKeyboard keyboard, SceneMouse mouse)
    {
        this.Keyboard = keyboard;
        this.Mouse = mouse;
    }

    public Entity Add(Component component)
    {
        component.Keyboard = Keyboard;
        component.Mouse = Mouse;
        this.Components.Add(component);
        this.Components[this.Components.Count - 1].AwakeCall();
        return this;
    }  

    public void Remove<T>()
    {
        Component? comp = this.Components.Find((comp) => comp.GetType() == typeof(T));
        if(comp != null) {
            comp.SleepCall();
            this.Components.Remove(comp);
        }
    }

    public T? Get<T>()
    {
        var scene = Components.Find((comp) => comp.GetType() == typeof(T));
        if (scene != null)
            return (T)(dynamic)scene;
        else
            return default(T);
    }

    internal void AwakeCall()
    {
        foreach(Component comp in Components)
        {
            comp.AwakeCall();
        }
    }

    internal void SleepCall()
    {
        foreach(Component comp in Components)
        {
            comp.SleepCall();
        }
    }

    internal void UpdateCall()
    {
        foreach(Component comp in Components)
        {
            comp.UpdateCall();
        }
    }

    internal void RenderCall()
    {
        foreach(Component comp in Components)
        {
            comp.RenderCall();
        }
    }
}