using System;
using System.Collections;
using CardMatchGame.Services.Coroutines;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CardMatchGame.Services.Scenes
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

    public void LoadScene(string sceneName, Action onLoaded = null) => 
      m_coroutineRunner.StartCoroutine(LoadSceneRoutine(sceneName, onLoaded));

    private static IEnumerator LoadSceneRoutine(string sceneName, Action onLoaded)
    {
      AsyncOperation loadSceneOp = SceneManager.LoadSceneAsync(sceneName);
      
      while (!loadSceneOp.isDone)
        yield return null;

      onLoaded?.Invoke();
    }
  }
}