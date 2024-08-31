namespace CardMatchGame.Services.Progress
{
  public class ProgressService : IProgressService
  {
    private ProgressData m_progressData = new();
    
    public CompletedLevelData FindCompletedLevelData(int levelIndex) => 
      m_progressData.CompletedLevels.Find(data => data.Index == levelIndex);

    public void AddCompletedLevelData(CompletedLevelData data) => 
      m_progressData.CompletedLevels.Add(data);
  }
}