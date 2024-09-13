using System;
using System.Collections;
using CardMatchGame.Services.Coroutines;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CardMatchGame.Services
{
  public enum EScene
  {
    MainMenu,
    Level
  }
  
  public class SceneLoader
  {
    private readonly ICoroutineRunner m_coroutineRunner;

    public SceneLoader(ICoroutineRunner coroutineRunner) => 
      m_coroutineRunner = coroutineRunner;

    public void LoadScene(EScene scene, Action onLoaded = null) => 
      m_coroutineRunner.StartCoroutine(LoadSceneRoutine(scene, onLoaded));

    private static IEnumerator LoadSceneRoutine(EScene scene, Action onLoaded)
    {
      AsyncOperation loadSceneOp = SceneManager.LoadSceneAsync(SceneName(scene));
      
      while (!loadSceneOp.isDone)
        yield return null;

      onLoaded?.Invoke();
    }

    private static string SceneName(EScene scene)
    {
      return scene switch
      {
        EScene.MainMenu => "MainMenu",
        EScene.Level => "GameLevel",
        _ => throw new ArgumentException($"Failed to load scene. Wrong value {scene}")
      };
    }
  }
}