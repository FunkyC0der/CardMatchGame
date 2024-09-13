namespace CardMatchGame.Services.Levels
{
  public interface ILevelsService
  {
    int CurrentLevelIndex { get; }
    LevelData CurrentLevelData { get; }
    void LoadLevel(int index);
  }
}