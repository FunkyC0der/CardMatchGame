namespace CardMatchGame.Services.Levels
{
  public interface ICurrentLevelDataProvider
  {
    int Index { get; }
    LevelData Data { get; }
    void SetCurrentData(int index);
  }
}