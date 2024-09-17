using System;
using System.Threading.Tasks;
using CardMatchGame.Services;
using CardMatchGame.Services.Assets;
using CardMatchGame.Services.Progress;
using CardMatchGame.Services.SaveLoad;
using CardMatchGame.Services.Scenes;
using CardMatchGame.Services.Settings;
using UnityEngine;

namespace CardMatchGame.GameStates.States
{
  public class BootGameState : GameState
  {
    public static string FirstSceneName = SceneNames.MainMenu;

    private readonly ISaveLoadService m_saveLoadService;
    private readonly ISettingsService m_settingsService;
    private readonly IProgressService m_progressService;
    private readonly LoadingCurtain m_loadingCurtain;
    private readonly IAssetsService m_assets;
    private readonly GameStateChanger m_gameStateChanger;

    public BootGameState(ISaveLoadService saveLoadService,
      ISettingsService settingsService,
      IProgressService progressService,
      LoadingCurtain loadingCurtain, 
      IAssetsService assets, 
      GameStateChanger gameStateChanger)
    {
      m_saveLoadService = saveLoadService;
      m_settingsService = settingsService;
      m_progressService = progressService;
      m_loadingCurtain = loadingCurtain;
      m_assets = assets;
      m_gameStateChanger = gameStateChanger;
    }

    public override async void Enter()
    {
      m_loadingCurtain.Show();

      if (m_saveLoadService.NeedAsyncRun())
        await Task.Run(LoadGameData, Application.exitCancellationToken);
      else
        LoadGameData();
      
      m_assets.Load();
      
      EnterNextState();
    }

    private void EnterNextState()
    {
      if (FirstSceneName == SceneNames.MainMenu)
        m_gameStateChanger.Enter<MainMenuGameState>();
      else if (FirstSceneName == SceneNames.Level)
        m_gameStateChanger.Enter<LoadLevelGameState, int>(0);
      else
        throw new ArgumentException($"First scene name is invalid: ${FirstSceneName}");
    }

    private void LoadGameData()
    {
      m_settingsService.Settings = m_saveLoadService.LoadSettingsData() ?? new SettingsData();
      m_progressService.Progress = m_saveLoadService.LoadProgressData() ?? new ProgressData();
    }
  }
}