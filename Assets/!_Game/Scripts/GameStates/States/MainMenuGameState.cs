using CardMatchGame.Services;
using CardMatchGame.Services.Scenes;
using CardMatchGame.Services.UI;

namespace CardMatchGame.GameStates.States
{
  public class MainMenuGameState : GameState
  {
    private readonly SceneLoader m_sceneLoader;
    private readonly LoadingCurtain m_loadingCurtain;
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
      m_sceneLoader.LoadScene(SceneNames.MainMenu, ShowMainMenu);
    }

    private void ShowMainMenu()
    {
      m_loadingCurtain.Hide();
      m_uiFactory.CreateWindow(WindowType.MainMenu);
    }
  }
}