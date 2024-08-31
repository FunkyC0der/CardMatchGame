using CardMatchGame.Gameplay.Services;
using CardMatchGame.Gameplay.Services.Input;
using Zenject;

namespace CardMatchGame.Gameplay
{
  public class LevelInstaller : MonoInstaller
  {
    public LevelInput levelInput;
    public GridService GridService;
    public CardsService CardsService;
    public MatchCardsService MatchCardsService;
    public LevelProgress LevelProgress;
    
    public override void InstallBindings()
    {
      Container.Bind<ILevelInput>()
        .FromInstance(levelInput)
        .AsSingle();
      
      Container.BindInterfacesAndSelfTo<GridService>()
        .FromInstance(GridService)
        .AsSingle();
      
      Container.BindInterfacesAndSelfTo<CardsService>()
        .FromInstance(CardsService)
        .AsSingle();
      
      Container.BindInterfacesAndSelfTo<MatchCardsService>()
        .FromInstance(MatchCardsService)
        .AsSingle();
      
      Container.BindInterfacesAndSelfTo<LevelProgress>()
        .FromInstance(LevelProgress)
        .AsSingle();
    }
  }
}