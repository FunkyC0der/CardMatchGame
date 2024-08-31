using CardMatchGame.Data;

namespace CardMatchGame.Services.Levels
{
  public interface ILevelsService
  {
    int LevelIndex { get; }
    LevelData LevelData { get; }
    void SetLevelData(int index);
  }
}