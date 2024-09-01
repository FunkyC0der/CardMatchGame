using Zenject;

namespace CardMatchGame.Boot
{
  public class BootInstaller : MonoInstaller
  {
    public override void InstallBindings()
    {
      Container.BindInterfacesTo<BootService>()
        .AsSingle();
    }
  }
}