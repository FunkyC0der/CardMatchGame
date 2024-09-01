using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CardMatchGame.Services
{
  public enum EScene
  {
    MainMenu,
    Level
  }
  
  public class SceneLoader : MonoBehaviour
  {
    public void LoadScene(EScene scene, Action onLoaded = null) => 
      StartCoroutine(LoadSceneRoutine(scene, onLoaded));

    private static IEnumerator LoadSceneRoutine(EScene scene, Action onLoaded)
    {
      if (SceneManager.loadedSceneCount > 1)
      {
        AsyncOperation unloadSceneOp = SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        while (!unloadSceneOp.isDone)
          yield return null;
      }

      string sceneName = SceneName(scene);
      AsyncOperation loadSceneOp = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
      
      while (!loadSceneOp.isDone)
        yield return null;

      SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
      
      onLoaded?.Invoke();
    }

    private static string SceneName(EScene scene)
    {
      switch (scene)
      {
        case EScene.MainMenu:
          return "MainMenu";

        case EScene.Level:
          return "GameLevel";
      }

      return "MainMenu";
    }
  }
}