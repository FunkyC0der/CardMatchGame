using CardMatchGame.Services.Assets;

namespace CardMatchGame.Services.Levels
{
  public class LevelsService : ILevelsService
  {
    private readonly IAssetsService m_assets;
    private readonly LoadingCurtain m_loadingCurtain;
    private readonly SceneLoader m_sceneLoader;

    public LevelsService(IAssetsService assets, LoadingCurtain loadingCurtain, SceneLoader sceneLoader)
    {
      m_assets = assets;
      m_loadingCurtain = loadingCurtain;
      m_sceneLoader = sceneLoader;
    }

    public int CurrentLevelIndex { get; private set; }
    public LevelData CurrentLevelData { get; private set; }
    
    public void LoadLevel(int index)
    {
      CurrentLevelIndex = index;
      CurrentLevelData = m_assets.LevelsData().Levels[index];
      
      m_loadingCurtain.Show();
      m_sceneLoader.LoadScene(EScene.Level);
    }
  }
}