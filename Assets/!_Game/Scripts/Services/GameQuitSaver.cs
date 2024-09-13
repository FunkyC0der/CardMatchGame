using System;
using CardMatchGame.Services.Progress;
using CardMatchGame.Services.SaveLoad;
using CardMatchGame.Services.Settings;

namespace CardMatchGame.Services
{
  public class GameQuitSaver : IDisposable
  {
    private readonly ISaveLoadService m_saveLoadService;
    private readonly ProgressService m_progressService;
    private readonly SettingsService m_settingsService;

    public GameQuitSaver(ISaveLoadService saveLoadService, ProgressService progressService, SettingsService settingsService)
    {
      m_saveLoadService = saveLoadService;
      m_progressService = progressService;
      m_settingsService = settingsService;
    }

    public void Dispose()
    {
      m_saveLoadService.Save(m_progressService.Progress);
      m_saveLoadService.Save(m_settingsService.Settings);
    }
  }
}