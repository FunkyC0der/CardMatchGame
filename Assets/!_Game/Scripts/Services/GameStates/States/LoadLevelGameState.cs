using CardMatchGame.Services.Levels;
using CardMatchGame.Services.Scenes;
using CardMatchGame.Utils;

namespace CardMatchGame.Services.GameStates.States
{
  public class LoadLevelGameState : GameState, IPayloaded<int>
  {
    private int m_levelIndex;
    
    private readonly ICurrentLevelDataProvider m_currentLevelData;
    private readonly SceneLoader m_sceneLoader;
    private readonly LoadingCurtain m_loadingCurtain;

    public LoadLevelGameState(ICurrentLevelDataProvider currentLevelData,
      SceneLoader sceneLoader,
      LoadingCurtain loadingCurtain)
    {
      m_currentLevelData = currentLevelData;
      m_sceneLoader = sceneLoader;
      m_loadingCurtain = loadingCurtain;
    }

    public void Payload(int levelIndex) => 
      m_levelIndex = levelIndex;

    public override void Enter()
    {
      m_loadingCurtain.Show();
      
      m_currentLevelData.SetCurrentData(m_levelIndex);
      m_sceneLoader.LoadScene(SceneNames.Level);
    }
  }
}