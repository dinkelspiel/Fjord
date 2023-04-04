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

    public UiText(string text)
    {
        this.text = text;
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

    public UiDebugLog(LogLevel level, string time, string sender, string message, bool hideInfo) {
        this.level = level;
        this.time = time;
        this.sender = sender;
        this.message = message;
        this.hideInfo = hideInfo;
    }
}

public class UiTextField : UiComponent 
{
    public string id;
    public string value;
    public Action<string> onChange;
    public string? placeholder;

    public UiTextField(string id, string value, Action<string> onChange, string? placeholder=null) {
        this.id = id;
        this.value = value;
        this.onChange = onChange;
        this.placeholder = placeholder;
    }
}