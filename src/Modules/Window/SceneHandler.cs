#nullable enable

using System.Collections.Generic;
using System;

using static Fjord.Modules.Debug.Debug;
using Fjord.Modules.Graphics;

using static SDL2.SDL;

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

        Debug.Debug.SendInternal($"Loaded Scene {SceneId}");

        _currentScene = SceneId;

        if (_scenesLoaded > 0)
        {
            _scenes[_currentScene].OnAwake();
        }
        SDL_RenderSetLogicalSize(Game.Renderer, (int)_scenes[_currentScene].Resolution.X, (int)_scenes[_currentScene].Resolution.Y);

        _scenesLoaded ++;
    }

    public static void Update() {
        if(_currentScene == null)
            return;

        _scenes[_currentScene].Update();
    }

    public static void Render() {
        if(_currentScene == null)
            return;

        try {
            _scenes[_currentScene].Render();
        } catch(Exception e) {
            Debug.Debug.SendInternal(e.StackTrace);
            Debug.Debug.SendInternal(e.Source);
            Debug.Debug.SendInternal(e.Message);
        }

        Draw.DrawGUI = true;
        try
        {
            _scenes[_currentScene].RenderGUI();
        }
        catch (Exception e)
        {
            Debug.Debug.SendInternal(e.StackTrace);
            Debug.Debug.SendInternal(e.Source);
            Debug.Debug.SendInternal(e.Message);
        }
        Draw.DrawGUI = false;
    }

    public static void Stop() {
        if(_currentScene != null)   
            _scenes[_currentScene].OnSleep();
    }
    public static Scene GetCurrentScene()
    {
        return _scenes[_currentScene];
    }

    public static string GetCurrentSceneID()
    {
        return _currentScene;
    }
}