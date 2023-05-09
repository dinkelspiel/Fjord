using System.Numerics;
using Fjord.Graphics;
using Fjord.Scenes;

namespace Fjord.Ui;

public abstract class UiComponent { }

public class UiButton : UiComponent
{
    public string text;
    public Action callback;

    public UiButton(string text, Action callback)
    {
        this.text = text;
        this.callback = callback;
    }
}

public class UiButtonGroup : UiComponent
{
    public List<UiButton> buttons;

    public UiButtonGroup(params UiButton[] buttons)
    {
        this.buttons = buttons.ToList();
    }

    public UiButtonGroup(List<UiButton> buttons)
    {
        this.buttons = buttons;
    }
}

public class UiCheckbox : UiComponent
{
    public string text;
    public bool value;
    public Action callback;

    public UiCheckbox(string text, bool value, Action callback)
    {
        this.text = text;
        this.value = value;
        this.callback = callback;
    }
}

public class UiTitle : UiComponent
{
    public string text;

    public UiTitle(string text)
    {
        this.text = text;
    }
}

public class UiText : UiComponent
{
    public string text;
    public Vector4? overrideColor;

    public UiText(string text, Vector4? overrideColor=null)
    {
        this.text = text;
        this.overrideColor = overrideColor;
    }
}

public class UiSpacer : UiComponent
{

}

public class UiDebugLog : UiComponent 
{
    public string time;
    public string sender;
    public string message;
    public LogLevel level; 
    public bool hideInfo = false;
    public int repeat;

    public UiDebugLog(LogLevel level, string time, string sender, string message, bool hideInfo, int repeat) {
        this.level = level;
        this.time = time;
        this.sender = sender;
        this.message = message;
        this.hideInfo = hideInfo;
        this.repeat = repeat;
    }
}

public class UiTextField : UiComponent 
{
    public string id;
    public string value;
    public Action<string> onChange;
    public Action<string> onSubmit;
    public string? placeholder;

    public UiTextField(string id, string value, Action<string> onChange, Action<string> onSubmit, string? placeholder=null) {
        this.id = id;
        this.value = value;
        this.onChange = onChange;
        this.placeholder = placeholder;
        this.onSubmit = onSubmit;
    }
}

public class UiSlider : UiComponent 
{
    public float min;
    public float max;
    public float value;
    public Action<float> onChange;

    public UiSlider(float min, float max, float value, Action<float> onChange) 
    {
        this.min = min;
        this.max = max;
        this.value = value;
        this.onChange = onChange;
    }
}

public class UiImage : UiComponent
{
    public Texture texture;

    public UiImage(Texture texture)
    {
        this.texture = texture;
    }
}

public class HAlign<T> : List<T>
{
    
}