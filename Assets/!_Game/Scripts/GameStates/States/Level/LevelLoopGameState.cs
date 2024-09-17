using CardMatchGame.Gameplay.Services;

namespace CardMatchGame.GameStates.States.Level
{
  public class LevelLoopGameState : GameState
  {
    private readonly TimerService m_timer;
    private readonly WinConditionService m_winCondition;
    private readonly GameStateChanger m_gameStateChanger;

    public LevelLoopGameState(TimerService timer, WinConditionService winCondition, GameStateChanger gameStateChanger)
    {
      m_timer = timer;
      m_winCondition = winCondition;
      m_gameStateChanger = gameStateChanger;
    }

    public override void Enter() => 
      m_timer.Start();

    public override void Update()
    {
      if(m_timer.IsFinished || m_winCondition.Achieved)
        m_gameStateChanger.Enter<LevelOverGameState>();
    }
  }
}