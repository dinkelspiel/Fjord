using Fjord.Input;
using System.Numerics;
using static SDL2.SDL;

namespace ShooterThingy;

class MainScene : Scene
{
    public MainScene(int width, int height, string id) : base(width, height, id)
    {
        
    }
    
    public override void Awake()
    {
        SetClearColor(122, 62, 101, 255);
    }

    public override void Update()
    {
        
    }
    
    public override void Render()
    {
        //SDL_FRect rect = new SDL_FRect()
        //{
        //    x = LocalMousePosition.X,
        //    y = LocalMousePosition.Y,
        //    w = 20,
        //    h = 20
        //};

        //// SDL_SetRenderDrawColor(Game.SDLRenderer, 255, 255, 255, 255);
        //// SDL_RenderFillRectF(Game.SDLRenderer, ref rect);
        //// SDL_SetRenderDrawColor(Game.SDLRenderer, 0, 0, 0, 255);

        //Font.Draw(LocalMousePosition + new Vector2(-50, -50), Font.DefaultFont, LocalMousePosition.X.ToString(), 24, new SDL_Color() {
        //    r = 255, g = 255, b = 255, a = 255
        //});

        //Font.Draw(LocalMousePosition + new Vector2(-50, -20), Font.DefaultFont, LocalMousePosition.Y.ToString(), 24, new SDL_Color() {
        //    r = 255, g = 255, b = 255, a = 255
        //});

        //new UiBuilder(new Vector2(200, 200), LocalMousePosition)
        //    .Title("Test Ui")
        //    .Button("Render")
        //    .Button("Update")
        //    .Container(
        //        new UiBuilder()
        //            .Title("Entities")
        //            .Button("Spawn")
        //            .Button("Kill")
        //            .Build()
        //    )
        //    .Button("Dilla")
        //    .Render();

        //Font.Draw(new Vector2(10, 10), Font.GetDefaultFont(), "Hello", 32, new() { r = 0, g = 0, b = 0, a = 255 });
    }
}

class Program
{
    static void Main(string[] args)
    {
        Game.Initialize("Shooter", 1920, 1080);

        SceneHandler.Register(new MainScene(1920, 1080, "Main").SetRelativeWindowSize(new SDL_FRect() {
            x = 0f,
            y = 0f,
            w = 1f,
            h = 1f,
        })
            .SetAllowWindowResize(true)
            .SetAlwaysRebuildTexture(false)
            .SetAlwaysAtBack(true));
        SceneHandler.Load("Main");
        
        Game.Run();
    }
}
