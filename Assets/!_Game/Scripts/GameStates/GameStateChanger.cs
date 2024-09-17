using CardMatchGame.Utils;

namespace CardMatchGame.GameStates
{
  public class GameStateChanger
  {
    private readonly GameStateMachine m_stateMachine;
    private readonly GameStateFactory m_stateFactory;

    public GameStateChanger(GameStateMachine stateMachine, GameStateFactory stateFactory)
    {
      m_stateMachine = stateMachine;
      m_stateFactory = stateFactory;
    }

    public void Enter<TState>() where TState : GameState => 
      m_stateMachine.Enter(m_stateFactory.Create<TState>());

    public void Enter<TState, TPayload>(TPayload payload) where TState : GameState
    {
      var state = m_stateFactory.Create<TState>();
      
      (state as IPayloaded<TPayload>).Payload(payload);
      
      m_stateMachine.Enter(state);
    }
  }
}