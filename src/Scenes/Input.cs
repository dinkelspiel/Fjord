using Fjord.Input;

namespace Fjord.Scenes;
public class SceneInput
{
    public string SceneID { get; private set; }

    public SceneInput(string SceneID)
    {
        this.SceneID = SceneID;
    }

    public bool Down<T>() where T : InputAction
    {
        if (SceneHandler.Get(SceneID).MouseInsideScene)
            return GlobalInput.Down<T>();
        else
            return false;
    }

    public bool Pressed<T>() where T : InputAction
    {
        if (SceneHandler.Get(SceneID).MouseInsideScene)
            return GlobalInput.Pressed<T>();
        else
            return false;
    }
    public bool Released<T>() where T : InputAction
    {
        if (SceneHandler.Get(SceneID).MouseInsideScene)
            return GlobalInput.Released<T>();
        else
            return false;
    }
}
