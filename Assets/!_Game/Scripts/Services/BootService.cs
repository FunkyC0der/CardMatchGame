using CardMatchGame.Services.GameStates;
using CardMatchGame.Services.GameStates.States;
using Zenject;

namespace CardMatchGame.Services
{
  public class BootService : IInitializable
  {
    private readonly GameStateChanger m_gameStateChanger;

    public BootService(GameStateChanger gameStateChanger) => 
      m_gameStateChanger = gameStateChanger;

    public void Initialize() => 
      m_gameStateChanger.Enter<BootGameState>();
  }
}