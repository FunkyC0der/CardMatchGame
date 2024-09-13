using CardMatchGame.Services.Assets;

namespace CardMatchGame.Services.Levels
{
  public class LevelsService : ILevelsService
  {
    private readonly IAssetsService m_assets;

    public LevelsService(IAssetsService assets) => 
      m_assets = assets;

    public int CurrentLevelIndex { get; private set; }
    public LevelData CurrentLevelData { get; private set; }

    public void SetCurrentLevelData(int index)
    {
      CurrentLevelIndex = index;
      CurrentLevelData = m_assets.LevelsData().Levels[index];
    }
  }
}