using CardMatchGame.Services;
using CardMatchGame.Services.Assets;
using CardMatchGame.Services.Coroutines;
using CardMatchGame.Services.GameStates;
using CardMatchGame.Services.GameStates.States;
using CardMatchGame.Services.Levels;
using CardMatchGame.Services.Progress;
using CardMatchGame.Services.SaveLoad;
using CardMatchGame.Services.Scenes;
using CardMatchGame.Services.Serialization;
using CardMatchGame.Services.Settings;
using CardMatchGame.Services.UI;
using UnityEngine;
using Zenject;

namespace CardMatchGame
{
  public class ProjectInstaller : MonoInstaller
  {
    public ProjectConfig ProjectConfig;

    [Space]
    public CoroutineRunner CoroutineRunner;
    public LoadingCurtain LoadingCurtain;

    public override void InstallBindings()
    {
      BindGameStateMachine();
      BindLoadingCurtain();
      BindCoroutineRunner();
      BindAssetsService();
      BindUIFactory();
      BindSceneLoader();
      BindSerializer();
      BindSaveLoadService();
      BindSettingsService();
      BindProgressService();
      BindLevelsService();
      BindBootService();
      BindGameQuitSaver();
      BindGameStates();
    }

    private void BindGameStateMachine()
    {
      Container.BindInterfacesAndSelfTo<GameStateMachine>()
        .AsSingle();

      Container.Bind<GameStateFactory>()
        .AsSingle()
        .CopyIntoAllSubContainers();

      Container.Bind<GameStateChanger>()
        .AsSingle()
        .CopyIntoAllSubContainers();
    }

    private void BindLoadingCurtain() => 
      Container.BindInstance(LoadingCurtain)
        .AsSingle();

    private void BindCoroutineRunner() =>
      Container.Bind<ICoroutineRunner>()
        .FromInstance(CoroutineRunner)
        .AsSingle();

    private void BindAssetsService() =>
      Container.Bind<IAssetsService>()
        .To<AssetsService>()
        .AsSingle();

    private void BindUIFactory() =>
      Container.Bind<UIFactory>()
        .AsSingle()
        .CopyIntoAllSubContainers();

    private void BindSceneLoader() => 
      Container.Bind<SceneLoader>()
        .AsSingle();

    private void BindSerializer()
    {
      switch (ProjectConfig.SerializerType)
      {
        case SerializerType.Json:
          Container.Bind<ISerializer>()
            .To<JsonSerializer>()
            .AsSingle();
          break;
        
        case SerializerType.Base64:
          Container.Bind<ISerializer>()
            .To<Base64Serializer>()
            .AsSingle();
          break;
      }
    }

    private void BindSaveLoadService()
    {
      switch (ProjectConfig.SaveLoadType)
      {
        case SaveLoadType.PlayerPrefs:
          Container.Bind<ISaveLoadService>()
            .To<PlayerPrefsSaveLoadService>()
            .AsSingle();
          break;
        
        case SaveLoadType.File:
          Container.Bind<ISaveLoadService>()
            .To<FileSaveLoadService>()
            .AsSingle();
          break;
      }
    }

    private void BindSettingsService() =>
      Container.Bind<ISettingsService>()
        .To<SettingsService>()
        .AsSingle();

    private void BindProgressService() =>
      Container.Bind<IProgressService>()
        .To<ProgressService>()
        .AsSingle();

    private void BindLevelsService() =>
      Container.Bind<ICurrentLevelDataProvider>()
        .To<CurrentLevelDataProvider>()
        .AsSingle();

    private void BindBootService() =>
      Container.BindInterfacesTo<BootService>()
        .AsSingle();

    private void BindGameQuitSaver() =>
      Container.BindInterfacesTo<GameQuitSaver>()
        .AsSingle();

    private void BindGameStates()
    {
      Container.Bind<BootGameState>().AsTransient();
      Container.Bind<MainMenuGameState>().AsTransient();
      Container.Bind<LoadLevelGameState>().AsTransient();
    }
  }
}