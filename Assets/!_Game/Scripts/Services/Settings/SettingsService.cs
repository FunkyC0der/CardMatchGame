using CardMatchGame.Services.SaveLoad;

namespace CardMatchGame.Services.Settings
{
  public class SettingsService : ISettingsService
  {
    private SettingsData m_settings = new();
    
    private readonly ISaveLoadService m_saveLoadService;

    public SettingsData Settings => m_settings;

    public SettingsService(ISaveLoadService saveLoadService)
    {
      m_saveLoadService = saveLoadService;
    }

    public void Save() => 
      m_saveLoadService.Save(m_settings);

    public void Load()
    {
      SettingsData savedData = m_saveLoadService.LoadSettingsData();
      if (savedData != null)
        m_settings = savedData;
    }
  }
}