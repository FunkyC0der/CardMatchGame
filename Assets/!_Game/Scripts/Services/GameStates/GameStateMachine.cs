using Zenject;

namespace CardMatchGame.Services.GameStates
{
  public class GameStateMachine : ITickable
  {
    private GameState m_state;

    public void Enter(GameState state)
    {
      m_state?.Exit();
      m_state = state;
      m_state.Enter();
    }

    public void Tick() => 
      m_state?.Update();
  }
}