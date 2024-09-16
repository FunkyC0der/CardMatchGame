using CardMatchGame.Services.Scenes;
using CardMatchGame.Services.UI;

namespace CardMatchGame.Services.GameStates.States
{
  public class MainMenuGameState : GameState
  {
    private readonly SceneLoader m_sceneLoader;
    private LoadingCurtain m_loadingCurtain;
    private readonly UIFactory m_uiFactory;

    public MainMenuGameState(SceneLoader sceneLoader, UIFactory uiFactory, LoadingCurtain loadingCurtain)
    {
      m_sceneLoader = sceneLoader;
      m_uiFactory = uiFactory;
      m_loadingCurtain = loadingCurtain;
    }

    public override void Enter()
    {
      m_loadingCurtain.Show();
      m_sceneLoader.LoadScene(SceneNames.MainMenu, ShowMaineMenu);
    }

    private void ShowMaineMenu()
    {
      m_loadingCurtain.Hide();
      m_uiFactory.CreateWindow(WindowType.MainMenu);
    }
  }
}