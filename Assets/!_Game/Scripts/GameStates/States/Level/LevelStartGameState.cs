using System.Collections;
using CardMatchGame.Gameplay.Services;
using CardMatchGame.Gameplay.UI.Controls;
using CardMatchGame.Services;
using CardMatchGame.Services.Coroutines;
using CardMatchGame.Services.UI;
using CardMatchGame.Utils;
using UnityEngine;

namespace CardMatchGame.GameStates.States.Level
{
  public class LevelStartGameState : GameState
  {
    private Coroutine m_enterCoroutine;

    private readonly ICoroutineRunner m_coroutineRunner;
    private readonly UIFactory m_uiFactory;
    private readonly CardsService m_cardsService;
    private readonly GameStateChanger m_gameStateChanger;
    private readonly LoadingCurtain m_loadingCurtain;

    public LevelStartGameState(LoadingCurtain loadingCurtain,
      ICoroutineRunner coroutineRunner,
      UIFactory uiFactory,
      CardsService cardsService,
      GameStateChanger gameStateChanger)
    {
      m_loadingCurtain = loadingCurtain;
      m_coroutineRunner = coroutineRunner;
      m_uiFactory = uiFactory;
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
      GameObject hud = m_uiFactory.CreateLevelHUD();
      
      var showCardsButton = hud.GetComponentInChildren<ShowCardsButton>();
      showCardsButton.Button.interactable = false;
      
      m_cardsService.SetAllCardsSelectable(false);
      m_cardsService.Shuffle();
      
      m_loadingCurtain.Hide();

      yield return ShowHintPopUp();

      yield return m_cardsService.ShowCardsHint()
        .ToYieldInstruction();

      showCardsButton.Button.interactable = true;
      
      m_enterCoroutine = null;
      
      m_gameStateChanger.Enter<LevelLoopGameState>();
    }

    private IEnumerator ShowHintPopUp()
    {
      CoroutineWait waitCloseHintPopUp = new();
      m_uiFactory.CreateWindow(WindowType.HintPopUp, waitCloseHintPopUp);
      yield return waitCloseHintPopUp.Wait();
    }
  }
}