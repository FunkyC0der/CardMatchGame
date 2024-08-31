using CardMatchGame.Services;
using CardMatchGame.Services.Levels;
using CardMatchGame.Services.Progress;
using CardMatchGame.Services.SaveLoad;
using CardMatchGame.Services.Serialization;
using Zenject;

namespace CardMatchGame
{
  public class ProjectInstaller : MonoInstaller
  {
    public ProjectConfig ProjectConfig;
    public LevelsService LevelsService;

    public override void InstallBindings()
    {
      BindSerializer();
      BindSaveLoadService();
      BindProgressService();
      BindLevelsService();
      BindBootService();
    }

    private void BindSerializer()
    {
      switch (ProjectConfig.SerializerType)
      {
        case ESerializerType.Json:
          Container.Bind<ISerializer>()
            .To<JsonSerializer>()
            .AsSingle();
          break;
        
        case ESerializerType.Base64:
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
        case ESaveLoadType.PlayerPrefs:
          Container.Bind<ISaveLoadService>()
            .To<PlayerPrefsSaveLoadService>()
            .AsSingle();
          break;
        
        case ESaveLoadType.File:
          Container.Bind<ISaveLoadService>()
            .To<FileSaveLoadService>()
            .AsSingle();
          break;
      }
    }

    private void BindProgressService() =>
      Container.Bind<IProgressService>()
        .To<ProgressService>()
        .AsSingle();

    private void BindLevelsService() =>
      Container.Bind(typeof(ILevelsService), typeof(IInitializable))
        .FromInstance(LevelsService)
        .AsSingle();

    private void BindBootService() => 
      Container.BindInterfacesTo<BootService>().AsSingle();
  }
}