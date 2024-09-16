using CardMatchGame.Services.Assets;

namespace CardMatchGame.Services.Levels
{
  public class CurrentLevelDataProvider : ICurrentLevelDataProvider
  {
    private readonly IAssetsService m_assets;

    public CurrentLevelDataProvider(IAssetsService assets) => 
      m_assets = assets;

    public int Index { get; private set; }
    public LevelData Data { get; private set; }
    
    public void SetCurrentData(int index)
    {
      Index = index;
      Data = m_assets.LevelsData().Levels[index];
    }
  }
}