using System.Collections;
using CardMatchGame.Gameplay.Services.Input;
using CardMatchGame.Services;
using CardMatchGame.Services.Coroutines;
using CardMatchGame.Services.GameStates;
using CardMatchGame.Services.UI;
using CardMatchGame.Utils;
using UnityEngine;

namespace CardMatchGame.Gameplay.Services.GameStates
{
  public class LevelStartGameState : GameState
  {
    private Coroutine m_enterCoroutine;

    private readonly ICoroutineRunner m_coroutineRunner;
    private readonly UIFactory m_uiFactory;
    private readonly ILevelInput m_levelInput;
    private readonly CardsService m_cardsService;
    private readonly GameStateChanger m_gameStateChanger;
    private readonly LoadingCurtain m_loadingCurtain;

    public LevelStartGameState(LoadingCurtain loadingCurtain,
      ICoroutineRunner coroutineRunner,
      UIFactory uiFactory,
      ILevelInput levelInput,
      CardsService cardsService,
      GameStateChanger gameStateChanger)
    {
      m_loadingCurtain = loadingCurtain;
      m_coroutineRunner = coroutineRunner;
      m_uiFactory = uiFactory;
      m_levelInput = levelInput;
      m_cardsService = cardsService;
      m_gameStateChanger = gameStateChanger;
    }

    public override void Enter() => 
      m_enterCoroutine = m_coroutineRunner.StartCoroutine(EnterCoroutine());

    public override void Exit()
    {
      if (m_enterCoroutine != null)
        m_coroutineRunner.StopCoroutine(m_enterCoroutine);
    }

    private IEnumerator EnterCoroutine()
    {
      m_loadingCurtain.Hide();
      
      m_uiFactory.CreateLevelHUD();
      
      m_levelInput.Enabled = false;

      yield return m_cardsService.FlipAllCardsToBack()
        .ToYieldInstruction();
      
      m_cardsService.Shuffle();

      CoroutineWait waitCloseHintPopUp = new();
      m_uiFactory.CreateWindow(WindowType.HintPopUp, waitCloseHintPopUp);
      yield return waitCloseHintPopUp.Wait();

      yield return m_cardsService.ShowCardsHint()
        .ToYieldInstruction();
      
      m_levelInput.Enabled = true;
      m_enterCoroutine = null;
      
      m_gameStateChanger.Enter<LevelLoopGameState>();
    }
  }
}