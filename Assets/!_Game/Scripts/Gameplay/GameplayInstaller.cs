using CardMatchGame.Gameplay.Services;
using Zenject;

namespace CardMatchGame.Gameplay
{
  public class GameplayInstaller : MonoInstaller
  {
    public LevelInputService LevelInputService;
    public GridService GridService;
    public CardsService CardsService;
    public MatchCardsService MatchCardsService;
    public LevelProgress LevelProgress;
    
    public override void InstallBindings()
    {
      Container.BindInstance(LevelInputService).AsSingle();
      Container.BindInstance(GridService).AsSingle();
      Container.BindInstance(CardsService).AsSingle();
      Container.BindInstance(MatchCardsService).AsSingle();
      Container.BindInstance(LevelProgress).AsSingle();
    }
  }
}