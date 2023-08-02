using Fjord.Graphics;
using Fjord.Scenes;
using Fjord.Ui;

public class Notification
{
    public string Message;
    public float Life;

    public Notification(string message, float life)
    {
        this.Message = message;
        this.Life = life;
    }
}

public class NotificationScene : Scene
{
    public List<Notification> Notifications = new();

    public NotificationScene(int width, int height) : base(width, height)
    {
    }

    public override void Awake()
    {
        ClearColor = new(0, 0, 0, 0);
    }   

    public override void Sleep()
    {

    }

    public override void Update()
    {
        foreach(var notif in Notifications)
        {
            notif.Life -= 5 * DeltaTime;
        }

        List<string> removeList = new();
        new UiBuilder()
            .ForEach(Notifications, e => {
                if(e.Life < 0)
                    removeList.Add(e.Message);
                return new UiBuilder()
                    .Button(e.Message, () => {
                        removeList.Add(e.Message);
                    })
                    .Build();
            })
            .Render();

        removeList.ForEach(e => Notifications.RemoveAll(n => n.Message == e));
    }
}