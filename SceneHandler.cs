namespace ShooterThingy;

public static class SceneHandler
{
    internal static Dictionary<string, Scene> Scenes = new ();
    internal static Dictionary<string, Scene> OriginalScenes = new ();
    internal static List<string> LoadedScenes = new();
    
    public static List<string> GetLoadedScenes()
    {
        return new List<string>(LoadedScenes);
    }

    public static void Register(Scene scene)
    {
        Scenes.Add(scene.GetSceneID(), (Scene)scene.Clone());
        OriginalScenes.Add(scene.GetSceneID(), (Scene)scene.Clone());
    }

    public static void Load(string id)
    {
        if (!LoadedScenes.Contains(id))
        {
            LoadedScenes.Add(id);
            Scenes[id].AwakeCall();
        }
    }

    public static void Unload(string id)
    {
        if (LoadedScenes.Contains(id))
        {
            Scenes[id].SleepCall();
            LoadedScenes.Remove(id);
        }
    }

    public static void Remake(string id)
    {
        Scenes[id] = (Scene)OriginalScenes[id].Clone();
        Scenes[id].Awake();
    }
}