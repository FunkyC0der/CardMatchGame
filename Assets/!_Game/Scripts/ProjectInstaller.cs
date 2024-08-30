using CardMatchGame.Services;
using Zenject;

namespace CardMatchGame
{
  public class ProjectInstaller : MonoInstaller
  {
    public LevelsDataService LevelsDataService;

    public override void InstallBindings()
    {
      Container.BindInstance(LevelsDataService).AsSingle();
    }
  }
}