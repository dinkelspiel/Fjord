using System.Numerics;
using Fjord.Graphics;
using Fjord.Ui;
using static SDL2.SDL;

namespace Fjord.Scenes;

public class PerformanceScene : Scene
{
    float InputFPS = 0f;
    float UpdateFPS = 0f;
    float ProgramFPS = 0f;
    ulong setFps = 0;

    List<float> recentInputFPS = new();
    List<float> recentUpdateFPS = new();
    List<float> recentProgramFPS = new();

    [Export(0, 3)]
    public int Position = 0;

    int LastPosition = 0;

    [Export]
    public bool Size = false;

    bool LastSize = false;

    public PerformanceScene(int width, int height) : base(width, height)
    {
    }

    public override void Awake()
    {
        ClearColor = UiStyles.Background;
    }

    public override void Update()
    {
        if (SDL_GetTicks64() - setFps > 100)
        {
            setFps = SDL_GetTicks64();

            InputFPS = Game.InputFPS;
            UpdateFPS = Game.UpdateFPS;
            ProgramFPS = Game.ProgramFPS;

            recentInputFPS.Add(InputFPS);
            recentProgramFPS.Add(ProgramFPS);

            recentUpdateFPS.Add(UpdateFPS);
        }

        if (LastPosition != Position)
        {
            if (Position == 0)
            {
                SetRelativeWindowSize(0f, 0.89f, 0.10f, 1.001f);
            }
            else if (Position == 1)
            {
                SetRelativeWindowSize(0f, 0f, 0.1f, 0.11f);
            }
            else if (Position == 2)
            {
                SetRelativeWindowSize(0.9f, 0f, 1.001f, 0.11f);
            }
            else if (Position == 3)
            {
                SetRelativeWindowSize(0.9f, 0.89f, 1.001f, 1.001f);
            }
            Size = false;
            LastSize = false;
        }

        if(LastSize != Size)
        {
            PerformanceScene scene = SceneHandler.Get<PerformanceScene>()!;
            if (Size)
            {
                if (scene.Position < 2)
                    scene.SetRelativeWindowSize(scene.RelativeWindowSize.x, scene.RelativeWindowSize.y, scene.RelativeWindowSize.w + 0.14f, scene.RelativeWindowSize.h);
                else
                    scene.SetRelativeWindowSize(scene.RelativeWindowSize.x - 0.14f, scene.RelativeWindowSize.y, scene.RelativeWindowSize.w, scene.RelativeWindowSize.h);
            } else
            {
                if (scene.Position < 2)
                    scene.SetRelativeWindowSize(scene.RelativeWindowSize.x, scene.RelativeWindowSize.y, scene.RelativeWindowSize.w - 0.14f, scene.RelativeWindowSize.h);
                else
                    scene.SetRelativeWindowSize(scene.RelativeWindowSize.x + 0.14f, scene.RelativeWindowSize.y, scene.RelativeWindowSize.w, scene.RelativeWindowSize.h);
            }
        }


        //new Rectangle(new(0, 0, WindowSize.X, WindowSize.Y))
        //    .Color(new(0, 0, 0, 120))
        //    .Fill(true)
        //    .Render();

        new UiBuilder(new Vector2(0, 0), Mouse.Position)
            .Title("FPS")
            .Text(((int)ProgramFPS).ToString() + " FPS")
            .Title("Input")
            .Text(((int)InputFPS).ToString() + " FPS")
            .Render();

        new UiBuilder(new Vector2(WindowSize.X / 2, 0), Mouse.Position)
            .Title("Update")
            .Text(((int)UpdateFPS).ToString() + " FPS")
            .Render();

        if(recentInputFPS.Count > 65)
        {
            recentInputFPS.RemoveAt(0);
            recentProgramFPS.RemoveAt(0);

            recentUpdateFPS.RemoveAt(0);
        }

        if(WindowSize.X > 450)
        {
            for(var i = 0; i < recentInputFPS.Count; i++)
            {
                new Rectangle(new(95 + i * 4, 10, 4, (recentProgramFPS[i] / recentProgramFPS.Max()) * 30))
                    .Color(UiStyles.ContainerIdleColor)
                    .Fill(true)
                    .Render();

                new Rectangle(new(95 + i * 4, 65, 4, (recentInputFPS[i] / recentInputFPS.Max()) * 30))
                    .Color(UiStyles.ContainerIdleColor)
                    .Fill(true)
                    .Render();


                new Rectangle(new(95 + i * 4 + WindowSize.X / 2, 10, 4, (recentUpdateFPS[i] / recentUpdateFPS.Max()) * 30))
                    .Color(UiStyles.ContainerIdleColor)
                    .Fill(true)
                    .Render();
            }
        }

        LastPosition = Position;
        LastSize = Size;

        //Draw.Text(new(10, 10), Font.DefaultFont, Game.inputFPS.ToString(), 32, new(255, 255, 255, 255));
        //Draw.Text(new(10, 10), Font.DefaultFont, Game.updateFPS.ToString(), 32, new(255, 255, 255, 255));
        //Draw.Text(new(10, 10), Font.DefaultFont, Game.renderFPS.ToString(), 32, new(255, 255, 255, 255));
    }
}