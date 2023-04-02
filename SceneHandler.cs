namespace ShooterThingy;

public static class SceneHandler
{
    internal static Dictionary<string, Scene> Scenes = new ();
    internal static List<string> LoadedScenes = new();
    
    public static void Register(string id, Scene scene)
    {
        Scenes.Add(id, scene);
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
}