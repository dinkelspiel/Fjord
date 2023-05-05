using Fjord.Scenes;
using Fjord.Ui;
using Fjord.Graphics;

internal class Notification
{
    public float Life = 0;
    public string Message = "";
}

class NotificationScene : Scene
{
    internal List<Notification> Notifications = new();

    public NotificationScene(int width, int height, string id) : base(width, height, id)
    {
    }

    public override void Awake()
    {
        SetCaptureMouse(false);
        SetClearColor(0, 0, 0, 0);
    }

    public override void Render()
    {
        float yOffset = 10;
        foreach(Notification notif in Notifications)
        {
            new Rectangle(new(10f, yOffset, WindowSize.X - 10, 100))
                .Color(UiColors.Background)
                .Fill(true)
                .Render();
            new Text(Font.DefaultFont, notif.Message)
                .Color(UiColors.TextColor)
                .Size(16)
                .Position(new(15f, yOffset + 5f))
                .Render();
            yOffset += 110;
        }
    }
}