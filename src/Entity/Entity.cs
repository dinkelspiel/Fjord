namespace Fjord.Scenes;

public class Entity
{
    internal SceneKeyboard Keyboard;
    internal SceneMouse Mouse;
    internal SceneInput Input;
    internal Scene Parent;
    internal string name;
    internal bool excludeFromInspector = false;

    internal List<Component> Components = new(); 

    internal Entity()
    {
        name = this.GetType().Name;
        Keyboard = new("")!;
        Mouse = new("")!;
        Input = new("")!;
        Parent = default(Scene)!;
    }

    public Entity(Scene parent)
    {
        this.name = this.GetType().Name;
        this.Parent = parent;
        this.Keyboard = parent.Keyboard;
        this.Mouse = parent.Mouse;
        this.Input = parent.Input;

        this.Components.Add(new Transform());
    }

    public Entity Name(string name)
    {
        this.name = name;
        return this;
    }

    public Entity ExcludeFromInspector(bool exclude)
    {
        this.excludeFromInspector = exclude;
        return this;
    }

    public Entity Add(Component component)
    {
        component.Keyboard = Keyboard;
        component.Mouse = Mouse;
        component.Input = Input;
        component.ParentEntity = this;
        component.ParentScene = this.Parent;
        this.Components.Add(component);
        try {
            this.Components[this.Components.Count - 1].AwakeCall();
        } catch(Exception e) {
            Debug.Log(LogLevel.Error, $"Component \"{this.Components[this.Components.Count - 1].GetType().Name}\" awake crashed!");
            Debug.Log(LogLevel.Message, e.ToString());
        }
        return this;
    }  

    public void Remove<T>()
    {
        Component? comp = this.Components.Find((comp) => comp.GetType() == typeof(T));
        if(comp != null) {
            try {
                comp.SleepCall();
            } catch(Exception e) {
                Debug.Log(LogLevel.Error, $"Component \"{comp.GetType().Name}\" sleep crashed!");
                Debug.Log(LogLevel.Message, e.ToString());
            }
            this.Components.Remove(comp);
        }
    }

    public T Get<T>()
    {
        var scene = Components.Find((comp) => comp.GetType() == typeof(T));
        if (scene != null)
            return (T)(dynamic)scene;
        else
            throw new Exception($"Component '{typeof(T)}' doesn't exist in entity");
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
}