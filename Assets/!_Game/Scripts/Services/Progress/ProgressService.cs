namespace CardMatchGame.Services.Progress
{
  public class ProgressService : IProgressService
  {
    public ProgressData Progress { get; set; }

    public CompletedLevelData FindCompletedLevelData(int levelIndex) =>
      Progress.CompletedLevels.Find(data => data.Index == levelIndex);

    public void AddCompletedLevelData(CompletedLevelData data) =>
      Progress.CompletedLevels.Add(data);
  }
}