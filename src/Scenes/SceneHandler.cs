using Fjord.Ui;

namespace Fjord.Scenes;

public static class SceneHandler
{
    internal static Dictionary<string, Scene> Scenes = new ();
    internal static Dictionary<string, Scene> OriginalScenes = new ();
    internal static List<string> LoadedScenes = new();
    
    public static void Initialize() {
        Debug.RegisterCommand("cl_sceneclose", (args) => {
            if(args.Length > 0) {
                if(SceneHandler.Scenes.ContainsKey((string)args[0])) {
                    if(SceneHandler.IsLoaded((string)args[0])) {
                        SceneHandler.Unload((string)args[0]);
                        Debug.Log(LogLevel.Message, $"Unloaded {(string)args[0]}");
                    } else {
                        Debug.Log(LogLevel.Warning, $"{(string)args[0]} is not loaded");
                    }
                } else {
                    Debug.Log(LogLevel.Warning, $"No scene named {(string)args[0]}");
                }
            } else {
                Debug.Log(LogLevel.Error, $"No argument provided");
            }
        });

        Debug.RegisterCommand("cl_sceneopen", (args) => {
            if(args.Length > 0) {
                if(SceneHandler.Scenes.ContainsKey((string)args[0])) {
                    if(!SceneHandler.IsLoaded((string)args[0])) {
                        SceneHandler.Load((string)args[0]);
                        Debug.Log(LogLevel.Message, $"Loaded {(string)args[0]}");
                    } else {
                        Debug.Log(LogLevel.Warning, $"{(string)args[0]} is already loaded");
                    }
                } else {
                    Debug.Log(LogLevel.Warning, $"No scene named {(string)args[0]}");
                }
            } else {
                Debug.Log(LogLevel.Error, $"No argument provided");
            }
        });
    }

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
        FUI.selectedTextField = null;
    }

    public static void Remake(string id)
    {
        Scenes[id] = (Scene)OriginalScenes[id].Clone();
        Scenes[id].Entities.Clear();
        Scenes[id].Awake();
    }

    public static T? Get<T>()
    {
        var scene = Scenes.Values.ToList().Find((val) => val.GetType() == typeof(T));
        if(scene != null)
            return (T)(dynamic)scene;
        else    
            return default(T);
    }

    public static Scene Get(string id)
    {
        return Scenes[id];
    }

    public static bool IsLoaded(string id)
    {
        return LoadedScenes.Contains(id);
    }
}