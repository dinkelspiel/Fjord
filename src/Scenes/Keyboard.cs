using Fjord.Input;

namespace Fjord.Scenes;
public class SceneKeyboard
{
    public string SceneID { get; private set; }

    public SceneKeyboard(string SceneID)
    {
        this.SceneID = SceneID;
    }

    public bool Down(Key key)
    {
        if (SceneHandler.Get(SceneID).MouseInsideScene)
            return GlobalKeyboard.Down(key);
        else
            return false;
    }

    public bool Down(Key key, params Mod[] mods)
    {
        if (SceneHandler.Get(SceneID).MouseInsideScene)
            return GlobalKeyboard.Down(key, mods);
        else
            return false;
    }

    public bool Pressed(Key key)
    {
        if (SceneHandler.Get(SceneID).MouseInsideScene)
            return GlobalKeyboard.Pressed(key);
        else
            return false;
    }

    public bool Pressed(Key key, params Mod[] mods)
    {
        if (SceneHandler.Get(SceneID).MouseInsideScene)
            return GlobalKeyboard.Pressed(key, mods);
        else
            return false;
    }

    public bool Released(Key key)
    {
        if (SceneHandler.Get(SceneID).MouseInsideScene)
            return GlobalKeyboard.Released(key);
        else
            return false;
    }

    public bool Released(Key key, params Mod[] mods)
    {
        if (SceneHandler.Get(SceneID).MouseInsideScene)
            return GlobalKeyboard.Released(key, mods);
        else
            return false;
    }
}
