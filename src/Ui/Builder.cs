using System.Numerics;
using Fjord.Graphics;
using static SDL2.SDL;

namespace Fjord.Ui;


public class UiBuilder
{
    List<object> UiComponents = new();
    Vector2 Position = new();
    Vector2? Size = null;

    public UiBuilder(Vector2 position=new(), Vector2? MouseOverride=null)
    {
        this.Position = position;
        if(MouseOverride != null)
        {
            FUI.OverMousePosition = MouseOverride.Value;
        }
    }
    public UiBuilder(Vector4 rect, Vector2? MouseOverride = null)
    {
        this.Position = new(rect.X, rect.Y);
        this.Size = new(rect.Z, rect.W);

        if (MouseOverride != null)
        {
            FUI.OverMousePosition = MouseOverride.Value;
        }
    }

    public UiBuilder Button(string text, Action callback)
    {
        UiComponents.Add(new UiButton(text, callback));
        return this;
    }

    public UiBuilder Button(string text)
    {
        UiComponents.Add(new UiButton(text, () => Console.WriteLine($"{text} Pressed")));
        return this;
    }

    public UiBuilder ButtonGroup(params UiButton[] buttons)
    {
        UiComponents.Add(new UiButtonGroup(buttons));
        return this;
    }

    public UiBuilder Title(string text)
    {
        UiComponents.Add(new UiTitle(text));
        return this;
    }

    public UiBuilder Text(string text, Vector4? overrideColor=null)
    {
        UiComponents.Add(new UiText(text, overrideColor));
        return this;
    }

    public UiBuilder Container(string label, List<object> components)
    {
        components.Insert(0, new UiTitle(label));
        UiComponents.Add(components);
        return this;
    }

    public UiBuilder HAlign(HAlign<UiComponent> components)
    {
        UiComponents.Add(components);
        return this;
    }

    public UiBuilder ForEach<T>(List<T> objects, Func<T, UiComponent?> callback)
    {
        foreach(T obj in objects)
        {
            var val = callback(obj);
            if(val != null)
                UiComponents.Add(val);
        }

        return this;
    }

    public UiBuilder ForEach<T>(List<T> objects, Func<T, List<object>?> callback)
    {
        foreach (T obj in objects)
        {
            var val = callback(obj);
            if(val != null)
                UiComponents.Add(val);
        }

        return this;
    }

    public UiBuilder ForEach<T>(List<T> objects, Func<T, HAlign<UiComponent>?> callback)
    {
        foreach (T obj in objects)
        {
            var val = callback(obj);
            if(val != null)
                UiComponents.Add(val);
        }

        return this;
    }
    
    public UiBuilder ForEach<T>(List<T> objects, Func<T, int, UiComponent?> callback)
    {
        int idx = -1;
        foreach(T obj in objects)
        {
            idx++;
            var val = callback(obj, idx);
            if(val != null)
                UiComponents.Add(val);
        }

        return this;
    }

    public UiBuilder ForEach<T>(List<T> objects, Func<T, int, List<object>?> callback)
    {
        int idx = -1;
        foreach (T obj in objects)
        {
            idx++;
            var val = callback(obj, idx);
            if(val != null)
                UiComponents.Add(val);
        }

        return this;
    }

    public UiBuilder If(bool expression, List<object> result)
    {
        if(expression)
        {
            UiComponents.AddRange(result);
        }

        return this;
    }
    public UiBuilder If(bool expression, UiComponent result)
    {
        if (expression)
        {
            UiComponents.Add(result);
        }

        return this;
    }

    public UiBuilder Spacer()
    {
        UiComponents.Add(new UiSpacer());
        return this;
    }

    public UiBuilder Checkbox(string text, bool value, Action callback)
    {
        UiComponents.Add(new UiCheckbox(text, value, callback));
        return this;
    }

    public UiBuilder TextField(string id, string value, Action<string> onChange, Action<string> onSubmit, string? placeholder=null) 
    {
        UiComponents.Add(new UiTextField(id, value, onChange, onSubmit, placeholder));
        return this;
    }

    public UiBuilder Slider(float min, float max, float value, Action<float> onChange) {
        UiComponents.Add(new UiSlider(min, max, value, onChange));
        return this;
    }

    public UiBuilder Image(Texture texture)
    {
        UiComponents.Add(new UiImage(texture));
        return this;
    }

    public List<object> Build()
    {
        return UiComponents;
    }

    public HAlign<UiComponent> BuildHAlign()
    {
        return UiComponents.ConvertAll(e => (UiComponent)e).IntoHAlign();
    }

    public void Render(out int height)
    {
        FUI.UiRenderOffset = Position + new Vector2(10, 5);

        //float yOffset = 0;
        //float y = Ui.Render(Build(), ref yOffset);

        //new Rectangle(new(Position.X, Position.Y, Size.HasValue ? Size.Value.X : 200, Size.HasValue ? Size.Value.Y : 400))
        //    .Color(UiStyles.Background)
        //    .Render();

        FUI.Render(Build(), out float renderHeight);
        FUI.OverMousePosition = null;
        FUI.UiRenderOffset = new();
        height = (int)renderHeight;
    }

    public void Render()
    {
        Render(out int height);
    }
}