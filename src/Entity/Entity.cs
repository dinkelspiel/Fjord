namespace Fjord.Scenes;

public class Entity
{
    internal List<Component> Components = new(); 

    public Entity Add(Component component)
    {
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

    public Component? Get<T>()
    {
        return Components.Find((comp) => comp.GetType() == typeof(T));
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