using System.Numerics;
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
            FUI.OverrideMousePosition(MouseOverride.Value);
        }
    }
    public UiBuilder(Vector4 rect, Vector2? MouseOverride = null)
    {
        this.Position = new(rect.X, rect.Y);
        this.Size = new(rect.Z, rect.W);

        if (MouseOverride != null)
        {
            FUI.OverrideMousePosition(MouseOverride.Value);
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

    public UiBuilder Title(string text)
    {
        UiComponents.Add(new UiTitle(text));
        return this;
    }

    public UiBuilder Text(string text)
    {
        UiComponents.Add(new UiText(text));
        return this;
    }

    public UiBuilder Container(List<object> components)
    {
        UiComponents.Add(components);
        return this;
    }

    public UiBuilder ForEach<T>(List<T> objects, Func<T, UiComponent> callback)
    {
        foreach(T obj in objects)
        {
            UiComponents.Add(callback(obj));
        }

        return this;
    }

    public UiBuilder ForEach<T>(List<T> objects, Func<T, List<object>> callback)
    {
        foreach (T obj in objects)
        {
            UiComponents.Add(callback(obj));
        }

        return this;
    }
    
    public UiBuilder ForEach<T>(List<T> objects, Func<T, int, UiComponent> callback)
    {
        int idx = -1;
        foreach(T obj in objects)
        {
            idx++;
            UiComponents.Add(callback(obj, idx));
        }

        return this;
    }

    public UiBuilder ForEach<T>(List<T> objects, Func<T, int, List<object>> callback)
    {
        int idx = -1;
        foreach (T obj in objects)
        {
            idx++;
            UiComponents.Add(callback(obj, idx));
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

    public List<object> Build()
    {
        return UiComponents;
    }

    public void Render()
    {
        FUI.SetRenderOffset(Position + new Vector2(10, 5));

        //float yOffset = 0;
        //float y = Ui.Render(Build(), ref yOffset);

        SDL_Rect rect = new()
        {
            x = (int)(Position.X),
            y = (int)(Position.Y),
            w = Size.HasValue ? (int)Size.Value.X : 200,
            h = Size.HasValue ? (int)Size.Value.Y : 400
        };
        SDL_SetRenderDrawColor(Game.SDLRenderer, UiColors.Background.r, UiColors.Background.g, UiColors.Background.b, UiColors.Background.a);
        SDL_RenderFillRect(Game.SDLRenderer, ref rect);

        FUI.Render(Build());
        FUI.ResetMousePosition();
        FUI.ResetRenderOffset();
    }
}