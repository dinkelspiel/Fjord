using Fjord.Input;
using System.Numerics;

namespace Fjord.Scenes;
public class SceneMouse
{
    public string SceneID { get; private set; }

    public Vector2 Position = new();
    public Vector2 LocalPosition = new();

    public SceneMouse(string sceneID)
    {
        this.SceneID = sceneID;
    }

    public bool Down(MB mouseButton)
    {
        if (SceneHandler.Get(SceneID).MouseInsideScene)
            return GlobalMouse.Down(mouseButton);
        else
            return false;
    }

    public bool Down()
    { 
        if (SceneHandler.Get(SceneID).MouseInsideScene)
            return GlobalMouse.Down();
        else
            return false;
    }

    public bool Pressed(MB mouseButton)
    {
        if (SceneHandler.Get(SceneID).MouseInsideScene)
            return GlobalMouse.Pressed(mouseButton);
        else
            return false;
    }

    public bool Pressed()
    {
        if (SceneHandler.Get(SceneID).MouseInsideScene)
            return GlobalMouse.Pressed();
        else
            return false;
    }

    public bool Released(MB mouseButton)
    {
        if (SceneHandler.Get(SceneID).MouseInsideScene)
            return GlobalMouse.Released(mouseButton);
        else
            return false;
    }

    public bool Released()
    {
        if (SceneHandler.Get(SceneID).MouseInsideScene)
            return GlobalMouse.Released();
        else
            return false;
    }
}
