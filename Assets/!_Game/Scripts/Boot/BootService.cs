using System;
using CardMatchGame.Services;
using CardMatchGame.Services.Progress;
using CardMatchGame.Services.Settings;
using Zenject;

namespace CardMatchGame.Boot
{
  public class BootService : IInitializable, IDisposable
  {
    public static EScene FirstScene = EScene.MainMenu;
    
    private readonly ISettingsService m_settingsService;
    private readonly IProgressService m_progressService;
    private readonly SceneLoader m_sceneLoader; 

    public BootService(ISettingsService settingsService, IProgressService progressService, SceneLoader sceneLoader)
    {
      m_settingsService = settingsService;
      m_progressService = progressService;
      m_sceneLoader = sceneLoader;
    }

    public void Initialize()
    {
      m_settingsService.Load();
      m_progressService.Load();
      
      m_sceneLoader.LoadScene(FirstScene);
    }

    public void Dispose()
    {
      m_settingsService.Save();
      m_progressService.Save();
    }


  }
}