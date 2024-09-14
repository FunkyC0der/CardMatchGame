using System.Collections;
using CardMatchGame.Gameplay.Services.Input;
using CardMatchGame.Services.UI;
using CardMatchGame.Utils;
using UnityEngine;
using Zenject;

namespace CardMatchGame.Gameplay.Services
{
  public class LevelProgress : MonoBehaviour
  {
    private CardsService m_cardsService;
    private ILevelInput m_levelInput;
    private MatchCardsService m_matchCardsService;
    private TimerService m_timerService;
    private WinConditionService m_winConditionService;
    private UIFactory m_uiFactory;

    [Inject]
    private void Construct(CardsService cardsService,
      ILevelInput levelInput,
      MatchCardsService matchCardsService,
      TimerService timerService,
      WinConditionService winConditionService,
      UIFactory uiFactory)
    {
      m_cardsService = cardsService;
      m_levelInput = levelInput;
      m_matchCardsService = matchCardsService;
      m_timerService = timerService;
      m_winConditionService = winConditionService;
      m_uiFactory = uiFactory;
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

      CoroutineWait waitCloseHintPopUp = new();
      m_uiFactory.CreateWindow(WindowType.HintPopUp, waitCloseHintPopUp);
      yield return waitCloseHintPopUp.Wait();

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

      m_uiFactory.CreateWindow(WindowType.GameOverPopUp);
    }
  }
}