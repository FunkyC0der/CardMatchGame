using CardMatchGame.Services.SaveLoad;

namespace CardMatchGame.Services.Progress
{
  public class ProgressService : IProgressService
  {
    private readonly ISaveLoadService m_saveLoadService;

    private ProgressData m_progressData = new();

    public ProgressService(ISaveLoadService saveLoadService) => 
      m_saveLoadService = saveLoadService;

    public CompletedLevelData FindCompletedLevelData(int levelIndex) =>
      m_progressData.CompletedLevels.Find(data => data.Index == levelIndex);

    public void AddCompletedLevelData(CompletedLevelData data) =>
      m_progressData.CompletedLevels.Add(data);

    public void Save() => 
      m_saveLoadService.Save(m_progressData);

    public void Load()
    {
      ProgressData savedData = m_saveLoadService.LoadProgressData();
      if (savedData != null)
        m_progressData = savedData;
    }
  }
}