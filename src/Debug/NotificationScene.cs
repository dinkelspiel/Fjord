using Fjord.Scenes;

class NotificationScene : Scene
{
    private class Notification
    {
        public float Life = 0;
        public string Message = "";
    }

    public List<Notification> Notifications = new();

    public NotificationScene(int width, int height, string id) : base(width, height, id)
    {
    }

    public override void Awake()
    {
        SetCaptureMouse(false);
        SetClearColor(0, 0, 0, 255);
    }
}