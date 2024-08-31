using CardMatchGame.Services.Levels;
using CardMatchGame.Services.Progress;
using Zenject;

namespace CardMatchGame
{
  public class ProjectInstaller : MonoInstaller
  {
    public LevelsService LevelsService;

    public override void InstallBindings()
    {
      Container.Bind(typeof(ILevelsService), typeof(IInitializable))
        .FromInstance(LevelsService)
        .AsSingle();

      Container.Bind<IProgressService>()
        .To<ProgressService>()
        .AsSingle();
    }
  }
}