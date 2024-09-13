using System.Collections;
using CardMatchGame.Gameplay.Services.Input;
using CardMatchGame.Gameplay.UI;
using UnityEngine;
using Zenject;

namespace CardMatchGame.Gameplay.Services
{
  public class LevelProgress : MonoBehaviour
  {
    public StartLevelHintView StartLevelHintView;
    public GameOverPopUp GameOverPopUp;

    private CardsService m_cardsService;
    private ILevelInput m_levelInput;
    private MatchCardsService m_matchCardsService;
    private TimerService m_timerService;
    private WinConditionService m_winConditionService;

    [Inject]
    private void Construct(CardsService cardsService,
      ILevelInput levelInput,
      MatchCardsService matchCardsService,
      TimerService timerService,
      WinConditionService winConditionService)
    {
      m_cardsService = cardsService;
      m_levelInput = levelInput;
      m_matchCardsService = matchCardsService;
      m_timerService = timerService;
      m_winConditionService = winConditionService;
    }

    private IEnumerator Start()
    {
      yield return EnterGameLoop();

      yield return WaitGameOverCondition();
      
      ExitGameLoop();
    }

    private IEnumerator EnterGameLoop()
    {
      m_levelInput.SetEnabled(false);

      yield return m_cardsService.FlipAllCardsToBack()
        .ToYieldInstruction();
      
      m_cardsService.Shuffle();

      yield return StartLevelHintView.ShowHint();

      yield return m_cardsService.ShowCardsHint()
        .ToYieldInstruction();
      
      m_timerService.Activate();
      m_levelInput.SetEnabled(true);
    }

    private IEnumerator WaitGameOverCondition()
    {
      while (!m_timerService.IsFinished && !m_winConditionService.WinCondition)
        yield return null;
    }

    private void ExitGameLoop()
    {
      m_levelInput.SetEnabled(false);
      m_matchCardsService.StopMatchProcess();
      m_timerService.Stop();
      
      GameOverPopUp.Show();
    }
  }
}