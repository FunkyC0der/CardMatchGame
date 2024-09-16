using CardMatchGame.Services.GameStates.States;
using CardMatchGame.Services.Scenes;
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
      EditorBuildSettings.sceneListChanged += SetBootAsStartScene;
      
      SetBootAsStartScene();
      UpdateFirstScene(EditorSceneManager.GetActiveScene());
    }

    private static void OnSceneOpened(Scene scene, OpenSceneMode mode) =>
      UpdateFirstScene(scene);
    
    private static void UpdateFirstScene(Scene scene) =>
      BootGameState.FirstSceneName = scene.name == SceneNames.Boot ? SceneNames.MainMenu : scene.name;

    static void SetBootAsStartScene() 
    {
      SceneAsset bootSceneAsset = AssetDatabase.LoadAssetAtPath<SceneAsset>(EditorBuildSettings.scenes[0].path);

      if (bootSceneAsset.name != SceneNames.Boot)
      {
        Debug.LogError($"First scene in the build settings must be ${SceneNames.Boot}");
        EditorSceneManager.playModeStartScene = default;
      }
      
      EditorSceneManager.playModeStartScene = bootSceneAsset;
    }
  }
}