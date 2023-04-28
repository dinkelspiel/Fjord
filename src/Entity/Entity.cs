namespace Fjord.Scenes;

public class Entity
{
    internal SceneKeyboard Keyboard;
    internal SceneMouse Mouse;
    internal Scene Parent;

    internal List<Component> Components = new(); 

    public Entity(Scene parent)
    {
        this.Parent = parent;
        this.Keyboard = parent.Keyboard;
        this.Mouse = parent.Mouse;

        this.Components.Add(new Transform());
    }

    public Entity Add(Component component)
    {
        component.Keyboard = Keyboard;
        component.Mouse = Mouse;
        component.ParentEntity = this;
        component.ParentScene = this.Parent;
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

    public T Get<T>()
    {
        var scene = Components.Find((comp) => comp.GetType() == typeof(T));
        if (scene != null)
            return (T)(dynamic)scene;
        else
            throw new Exception("Component doesn't exist in entity");
    }

    public bool TryGet<T>(out T component)
    {
        var scene = Components.Find((comp) => comp.GetType() == typeof(T));
        if (scene != null) {
            component = (T)(dynamic)scene;
            return true;
        } else {
            component = default(T)!;
            return false;
        } 
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