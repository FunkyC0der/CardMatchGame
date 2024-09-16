using CardMatchGame.Gameplay.Services.GameStates;
using CardMatchGame.Services.GameStates;
using Zenject;

namespace CardMatchGame.Gameplay.Services
{
  public class LevelBootService : IInitializable
  {
    private readonly GameStateChanger m_gameStateChanger;

    public LevelBootService(GameStateChanger gameStateChanger)
    {
      m_gameStateChanger = gameStateChanger;
    }

    public void Initialize() => 
      m_gameStateChanger.Enter<LevelStartGameState>();
  }
}