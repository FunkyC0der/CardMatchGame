namespace CardMatchGame.Services.Progress
{
  public interface IProgressService
  {
    ProgressData Progress { get; set; }
    CompletedLevelData FindCompletedLevelData(int levelIndex);
    void AddCompletedLevelData(CompletedLevelData data);
  }
}