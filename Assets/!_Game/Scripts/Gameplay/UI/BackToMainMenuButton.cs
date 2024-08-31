using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CardMatchGame.Gameplay.UI
{
  public class BackToMainMenuButton : MonoBehaviour
  {
    public string MainMenuSceneName = "MainMenu";
    public Button Button;
    
    private void Start() => 
      Button.onClick.AddListener(() => SceneManager.LoadSceneAsync(MainMenuSceneName));
  }
}