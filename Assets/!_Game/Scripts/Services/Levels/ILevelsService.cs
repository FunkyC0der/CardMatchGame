namespace CardMatchGame.Services.Levels
{
  public interface ILevelsService
  {
    int CurrentLevelIndex { get; }
    LevelData CurrentLevelData { get; }
    void SetCurrentLevelData(int index);
  }
}