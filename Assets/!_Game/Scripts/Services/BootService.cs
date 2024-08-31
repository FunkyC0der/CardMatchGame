using System;
using CardMatchGame.Services.Progress;
using CardMatchGame.Services.Settings;
using Zenject;

namespace CardMatchGame.Services
{
  public class BootService : IInitializable, IDisposable
  {
    private readonly ISettingsService m_settingsService;
    private readonly IProgressService m_progressService;

    public BootService(ISettingsService settingsService, IProgressService progressService)
    {
      m_settingsService = settingsService;
      m_progressService = progressService;
    }

    public void Initialize()
    {
      m_settingsService.Load();
      m_progressService.Load();
    }

    public void Dispose()
    {
      m_settingsService.Save();
      m_progressService.Save();
    }
  }
}