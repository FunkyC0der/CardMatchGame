using System;
using System.Collections;
using CardMatchGame.Gameplay.Services.Input;
using CardMatchGame.Gameplay.UI;
using CardMatchGame.Gameplay.Utils;
using CardMatchGame.Services.Levels;
using UnityEngine;
using Zenject;

namespace CardMatchGame.Gameplay.Services
{
  public class LevelProgress : MonoBehaviour, IInitializable
  {
    public event Action OnGameStart;
    public event Action OnGameOver;

    [NonSerialized]
    public readonly Cooldown LevelTimer = new();

    [NonSerialized]
    public StartLevelHintView StartLevelHintView;

    private GridService m_grid;
    private CardsService m_cardsService;
    private ILevelInput m_levelInput;
    private MatchCardsService m_matchCardsService;
    private ILevelsService m_levelsService;

    public bool IsLevelCompleted => m_matchCardsService.MatchesCount == m_matchCardsService.MatchesCountToWin;

    [Inject]
    private void Construct(CardsService cardsService,
      ILevelInput levelInput,
      MatchCardsService matchCardsService,
      ILevelsService levelsService)
    {
      m_cardsService = cardsService;
      m_levelInput = levelInput;
      m_matchCardsService = matchCardsService;
      m_levelsService = levelsService;
    }

    public void Initialize()
    {
      LevelTimer.Init(m_levelsService.LevelData.TimerDuration);
      m_levelInput.SetEnabled(false);
    }

    private void Start() => 
      StartGame();

    public void Restart()
    {
      StopAllCoroutines();
      m_matchCardsService.StopMatchProcess();
      
      StartGame();
    }

    private void StartGame()
    {
      StartCoroutine(GameLoop());
    }

    private IEnumerator GameLoop()
    {
      yield return EnterGameLoop();

      yield return WaitGameOverCondition();

      ExitGameLoop();
    }

    private void ExitGameLoop()
    {
      m_levelInput.SetEnabled(false);
      m_matchCardsService.StopMatchProcess();
      
      OnGameOver?.Invoke();
    }

    private IEnumerator WaitGameOverCondition()
    {
      while (LevelTimer.IsTicking && !IsLevelCompleted)
      {
        LevelTimer.Update();
        yield return null;
      }
    }

    private IEnumerator EnterGameLoop()
    {
      OnGameStart?.Invoke();
      
      m_levelInput.SetEnabled(false);
      m_matchCardsService.StartGame();
      LevelTimer.Activate();

      yield return StartLevelHintView.ShowHint();

      yield return m_cardsService.FlipAllCardsToBack()
        .ToYieldInstruction();
      
      m_cardsService.Shuffle();

      yield return m_cardsService.ShowCardsHint();
      
      m_levelInput.SetEnabled(true);
    }
  }
}