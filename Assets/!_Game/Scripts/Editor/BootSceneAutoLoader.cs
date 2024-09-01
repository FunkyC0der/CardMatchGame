using CardMatchGame.Boot;
using CardMatchGame.Services;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CardMatchGame.Editor
{
  [InitializeOnLoad]
  public static class BootSceneAutoLoader
  {
    static BootSceneAutoLoader()
    {
      EditorSceneManager.sceneOpened += OnSceneOpened;
      EditorBuildSettings.sceneListChanged += SceneListChanged;
      
      UpdateFirstScene(EditorSceneManager.GetActiveScene());
    }

    private static void OnSceneOpened(Scene scene, OpenSceneMode mode) =>
      UpdateFirstScene(scene);
    
    private static void UpdateFirstScene(Scene scene) =>
      BootService.FirstScene = scene.name is "Boot" or "MainMenu" ? EScene.MainMenu : EScene.Level;

    static void SceneListChanged() 
    {
      SceneAsset bootSceneAsset = AssetDatabase.LoadAssetAtPath<SceneAsset>(EditorBuildSettings.scenes[0].path);

      if (bootSceneAsset.name != "Boot")
      {
        Debug.LogError("First scene in the build settings must be Boot");
        EditorSceneManager.playModeStartScene = default;
      }
      
      EditorSceneManager.playModeStartScene = bootSceneAsset;
    }
  }
}