using System.Threading.Tasks;
using CardMatchGame.Services.Assets;
using CardMatchGame.Services.Progress;
using CardMatchGame.Services.SaveLoad;
using CardMatchGame.Services.Settings;
using UnityEngine;
using Zenject;

namespace CardMatchGame.Services
{
  public class BootService : IInitializable
  {
    public static EScene FirstScene = EScene.MainMenu;

    private readonly ISaveLoadService m_saveLoadService;
    private readonly ISettingsService m_settingsService;
    private readonly IProgressService m_progressService;
    private readonly SceneLoader m_sceneLoader;
    private readonly LoadingCurtain m_loadingCurtain;
    private readonly IAssetsService m_assets;

    public BootService(ISaveLoadService saveLoadService,
      ISettingsService settingsService,
      IProgressService progressService,
      SceneLoader sceneLoader,
      LoadingCurtain loadingCurtain, IAssetsService assets)
    {
      m_saveLoadService = saveLoadService;
      m_settingsService = settingsService;
      m_progressService = progressService;
      m_sceneLoader = sceneLoader;
      m_loadingCurtain = loadingCurtain;
      m_assets = assets;
    }

    public async void Initialize()
    {
      m_loadingCurtain.Show();

      if (m_saveLoadService.NeedAsyncRun())
        await Task.Run(LoadGameData, Application.exitCancellationToken);
      else
        LoadGameData();
      
      m_assets.Load();
      
      m_sceneLoader.LoadScene(FirstScene);
    }

    private void LoadGameData()
    {
      m_settingsService.Settings = m_saveLoadService.LoadSettingsData() ?? new SettingsData();
      m_progressService.Progress = m_saveLoadService.LoadProgressData() ?? new ProgressData();
    }
  }
}