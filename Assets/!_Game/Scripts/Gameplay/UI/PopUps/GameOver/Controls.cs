using CardMatchGame.Services;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CardMatchGame.Gameplay.UI.PopUps.GameOver
{
  public class Controls : MonoBehaviour
  {
    public Button RestartButton;
    public Button MenuButton;

    private LoadingCurtain m_loadingCurtain;
    private SceneLoader m_sceneLoader;

    [Inject]
    private void Construct(LoadingCurtain loadingCurtain, SceneLoader sceneLoader)
    {
      m_loadingCurtain = loadingCurtain;
      m_sceneLoader = sceneLoader;
      
      RestartButton.onClick.AddListener(RestartLevel);
      MenuButton.onClick.AddListener(GoToMenu);
    }

    private void RestartLevel()
    {
      m_loadingCurtain.Show();
      m_sceneLoader.LoadScene(EScene.Level);
    }

    private void GoToMenu()
    {
      m_loadingCurtain.Show();
      m_sceneLoader.LoadScene(EScene.MainMenu);
    }
  }
}