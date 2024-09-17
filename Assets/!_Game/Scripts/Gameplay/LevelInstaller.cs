using CardMatchGame.Gameplay.Services;
using CardMatchGame.Gameplay.Services.Input;
using CardMatchGame.GameStates.States.Level;
using Zenject;

namespace CardMatchGame.Gameplay
{
  public class LevelInstaller : MonoInstaller
  {
    public GridService GridService;
    public CardsService CardsService;
    public MatchCardsService MatchCardsService;

    public override void InstallBindings()
    {
      BindLevelInput();
      BindGridService();
      BindTimerService();
      BindWinConditionService();
      BindCardServices();

      BindGameStates();
      BindLevelBootService();
    }

    private void BindLevelInput() =>
      Container.BindInterfacesTo<LevelInput>()
        .AsSingle();

    private void BindGridService() =>
      Container.BindInterfacesAndSelfTo<GridService>()
        .FromInstance(GridService)
        .AsSingle();

    private void BindTimerService() =>
      Container.BindInterfacesAndSelfTo<TimerService>()
        .AsSingle();

    private void BindWinConditionService() =>
      Container.Bind<WinConditionService>()
        .AsSingle();

    private void BindCardServices()
    {
      Container.BindInterfacesAndSelfTo<CardsService>()
        .FromInstance(CardsService)
        .AsSingle();

      Container.Bind<MatchCardsService>()
        .FromInstance(MatchCardsService)
        .AsSingle();
    }

    private void BindGameStates()
    {
      Container.Bind<LevelStartGameState>().AsTransient();
      Container.Bind<LevelLoopGameState>().AsTransient();
      Container.Bind<LevelOverGameState>().AsTransient();
    }

    private void BindLevelBootService() => 
      Container.BindInterfacesTo<LevelBootService>()
        .AsSingle();
  }
}