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

public class UiSpacer : UiComponent
{

}

public class UiDebugLog : UiComponent 
{
    public string time;
    public string sender;
    public string message;

    public UiDebugLog(string time, string sender, string message) {
        this.time = time;
        this.sender = sender;
        this.message = message;
    }
}