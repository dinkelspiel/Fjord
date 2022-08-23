#nullable enable

using System.Collections.Generic;
using System;

using static Fjord.Modules.Debug.Debug;

namespace Fjord.Modules.Window;

public static class SceneHandler {
    private static Dictionary<string, Scene> _scenes = new Dictionary<string, Scene>();
    private static string? _currentScene;
    private static int _scenesLoaded;

    public static void Register(string SceneId, Scene SceneObject) {
        _scenes.Add(SceneId, SceneObject);
    }

    public static void Start(string SceneId) {
        if(_currentScene != null) 
            _scenes[_currentScene].OnSleep();

        Debug.Debug.Send($"Loaded Scene {SceneId}");

        _currentScene = SceneId;

        if(_scenesLoaded > 0)
            _scenes[_currentScene].OnAwake();

        _scenesLoaded ++;
    }

    public static void Update() {
        if(_currentScene == null)
            return;

        _scenes[_currentScene].UpdateCall();
    }

    public static void Render() {
        if(_currentScene == null)
            return;

        _scenes[_currentScene].RenderCall();
    }

    public static void Stop() {
        if(_currentScene != null)   
            _scenes[_currentScene].OnSleep();
    }
}