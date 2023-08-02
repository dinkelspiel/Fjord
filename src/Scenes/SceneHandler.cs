using Fjord.Ui;

namespace Fjord.Scenes;

public static class SceneHandler
{
    internal static Dictionary<string, Scene> Scenes = new ();
    internal static Dictionary<string, Scene> OriginalScenes = new ();
    
    public static List<string> LoadedScenes = new();
    
    public static void Initialize() {
        Debug.RegisterCommand("scene_unload", (args) => {
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

        Debug.RegisterCommand("scene_load", (args) => {
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

        Debug.RegisterCommand("scene_remake", (args) => {
            if(args.Length > 0) {
                if(SceneHandler.Scenes.ContainsKey((string)args[0])) {
                    SceneHandler.Remake((string)args[0]);
                    Debug.Log(LogLevel.Message, $"Remade {(string)args[0]}");
                } else {
                    Debug.Log(LogLevel.Warning, $"No scene named {(string)args[0]}");
                }
            } else {
                Debug.Log(LogLevel.Error, $"No argument provided");
            }
        });

        Debug.RegisterCommand("scene_allowresize", (args) => {
            if(args.Length > 0) {
                if(SceneHandler.Scenes.ContainsKey((string)args[0])) {
                    SceneHandler.Get((string)args[0]).AllowWindowResize = !SceneHandler.Get((string)args[0]).AllowWindowResize;
                    Debug.Log(LogLevel.Message, $"{(string)args[0]} can now{(SceneHandler.Get((string)args[0]).AllowWindowResize ? "" : "not ")}be resized");
                } else {
                    Debug.Log(LogLevel.Warning, $"No scene named {(string)args[0]}");
                }
            } else {
                Debug.Log(LogLevel.Error, $"No argument provided");
            }
        });

        Debug.RegisterCommand("scene_alwaysback", (args) => {
            if(args.Length > 0) {
                if(SceneHandler.Scenes.ContainsKey((string)args[0])) {
                    SceneHandler.Get((string)args[0]).AlwaysAtBack = !SceneHandler.Get((string)args[0]).AlwaysAtBack;
                    Debug.Log(LogLevel.Message, $"{(string)args[0]} is now {(SceneHandler.Get((string)args[0]).AlwaysAtBack ? "" : "not ")}always at front");
                } else {
                    Debug.Log(LogLevel.Warning, $"No scene named {(string)args[0]}");
                }
            } else {
                Debug.Log(LogLevel.Error, $"No argument provided");
            }
        });

        Debug.RegisterCommand("scene_alwaysfront", (args) => {
            if(args.Length > 0) {
                if(SceneHandler.Scenes.ContainsKey((string)args[0])) {
                    SceneHandler.Get((string)args[0]).AlwaysAtFront = !SceneHandler.Get((string)args[0]).AlwaysAtFront;
                    Debug.Log(LogLevel.Message, $"{(string)args[0]} is now {(SceneHandler.Get((string)args[0]).AlwaysAtFront ? "" : "not ")}always at front");
                } else {
                    Debug.Log(LogLevel.Warning, $"No scene named {(string)args[0]}");
                }
            } else {
                Debug.Log(LogLevel.Error, $"No argument provided");
            }
        });

        Debug.RegisterCommand("scene_rebuildalways", (args) => {
            if(args.Length > 0) {
                if(SceneHandler.Scenes.ContainsKey((string)args[0])) {
                    SceneHandler.Get((string)args[0]).AlwaysRebuildTexture = !SceneHandler.Get((string)args[0]).AlwaysRebuildTexture;
                    Debug.Log(LogLevel.Message, $"{(string)args[0]} is now {(SceneHandler.Get((string)args[0]).AlwaysRebuildTexture ? "" : "not ")}always rebuilt");
                } else {
                    Debug.Log(LogLevel.Warning, $"No scene named {(string)args[0]}");
                }
            } else {
                Debug.Log(LogLevel.Error, $"No argument provided");
            }
        });

        Debug.RegisterCommand("scene_getall", (args) => {
            foreach(string scene in Scenes.Keys)
            {
                Debug.Log(scene); 
            }
        });
    }

    public static void Register(Scene scene)
    {
        Scenes.Add(scene.SceneID, (Scene)scene.Clone());
        OriginalScenes.Add(scene.SceneID, (Scene)scene.Clone());
    }

    internal static void Load(string id)
    {
        if (!LoadedScenes.Contains(id))
        {
            LoadedScenes.Add(id);
            Scenes[id].AwakeCall();
        } else {
            Debug.Log($"Scene \"{id}\" doesnt exist");
        }
    }

    internal static void Unload(string id)
    {
        if (LoadedScenes.Contains(id))
        {
            Scenes[id].SleepCall();
            LoadedScenes.Remove(id);
        }
        FUI.selectedTextField = null;
    }

    internal static void Remake(string id)
    {
        Scenes[id] = (Scene)OriginalScenes[id].Clone();
        Scenes[id].Entities.Clear();
        Scenes[id].Awake();
    }

    public static void Load<T>()
    {
        Load(typeof(T).Name);
    }

    public static void Unload<T>()
    {
        Unload(typeof(T).Name);
    }

    public static void Remake<T>()
    {
        Remake(typeof(T).Name);
    }

    public static T Get<T>()
    {
        var scene = Scenes.Values.ToList().Find((val) => val.GetType() == typeof(T));
        if(scene != null)
            return (T)(dynamic)scene;
        else    
            throw new Exception("Scene doesn't exist");
    }

    public static bool Get<T>(out T scene)
    {
        var s = Scenes.Values.ToList().Find((val) => val.GetType() == typeof(T));
        if(s != null) {
            scene = (T)(dynamic)s;
            return true;
        } else {    
            scene = default(T)!;
            return false;
        }
    }

    internal static Scene Get(string id)
    {
        return Scenes[id];
    }

    internal static bool IsLoaded(string id)
    {
        return LoadedScenes.Contains(id);
    }

    public static bool IsLoaded<T>()
    {
        return IsLoaded(typeof(T).Name);
    }
}