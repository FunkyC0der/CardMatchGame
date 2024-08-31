using CardMatchGame.Gameplay.Services;
using CardMatchGame.Gameplay.Services.Input;
using UnityEngine.Serialization;
using Zenject;

namespace CardMatchGame.Gameplay
{
  public class GameplayInstaller : MonoInstaller
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
      
      Container.BindInstance(GridService).AsSingle();
      Container.BindInstance(CardsService).AsSingle();
      Container.BindInstance(MatchCardsService).AsSingle();
      Container.BindInstance(LevelProgress).AsSingle();
    }
  }
}