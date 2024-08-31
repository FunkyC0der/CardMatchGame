using System;
using System.Collections;
using CardMatchGame.Data;
using CardMatchGame.Gameplay.Utils;
using CardMatchGame.Services;
using UnityEngine;
using Zenject;

namespace CardMatchGame.Gameplay.Services
{
  public class LevelProgress : MonoBehaviour
  {
    public event Action OnGameStart;
    public event Action OnGameOver;

    [NonSerialized]
    public readonly Cooldown LevelTimer = new();

    private GridService m_grid;
    private CardsService m_cardsService;
    private LevelInputService m_levelInput;
    private MatchCardsService m_matchCardsService;
    private LevelsDataService m_levelsDataService;

    public bool IsLevelCompleted => m_matchCardsService.MatchesCount == m_matchCardsService.MatchesCountToWin;
    private LevelData LevelData => m_levelsDataService.LevelData;


    [Inject]
    private void Construct(GridService grid,
      CardsService cardsService,
      LevelInputService levelInput,
      MatchCardsService matchCardsService,
      LevelsDataService levelsDataService)
    {
      m_grid = grid;
      m_cardsService = cardsService;
      m_levelInput = levelInput;
      m_matchCardsService = matchCardsService;
      m_levelsDataService = levelsDataService;
    }

    private void Awake()
    {
      m_grid.SetSize(LevelData.GridSize);
      m_matchCardsService.Init(LevelData.CardsCountToMatch, LevelData.MatchesCountToWin);
      m_cardsService.Init(LevelData.CardsCountToMatch);
      LevelTimer.Init(LevelData.TimerDuration);
    }

    private IEnumerator Start()
    {
      yield return GameLoop();
    }

    public void Restart()
    {
      StopAllCoroutines();
      StartCoroutine(GameLoop());
    }

    private IEnumerator GameLoop()
    {
      yield return StartGame();

      yield return WaitGameOverCondition();

      ExitGameLoop();
    }

    private void ExitGameLoop()
    {
      m_levelInput.enabled = false;
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

    private IEnumerator StartGame()
    {
      OnGameStart?.Invoke();
      
      m_levelInput.enabled = false;
      m_matchCardsService.StartGame();
      LevelTimer.Activate();

      yield return m_cardsService.FlipAllCardsToBack()
        .ToYieldInstruction();
      
      m_cardsService.Shuffle();

      yield return m_cardsService.ShowCardsHint();
      
      m_levelInput.enabled = true;
    }
  }
}