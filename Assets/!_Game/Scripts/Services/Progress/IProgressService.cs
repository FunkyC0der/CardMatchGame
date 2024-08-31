namespace CardMatchGame.Services.Progress
{
  public interface IProgressService
  {
    CompletedLevelData FindCompletedLevelData(int levelIndex);
    void AddCompletedLevelData(CompletedLevelData data);
    void Save();
    void Load();
  }
}