namespace CardMatchGame.Services.Levels
{
  public interface ILevelsService
  {
    int LevelIndex { get; }
    LevelData LevelData { get; }
    LevelData[] Levels { get; }
    void SetLevelData(int index);

    void LoadLevel(int index);
  }
}