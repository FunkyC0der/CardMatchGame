using System;
using System.Collections;
using CardMatchGame.Data;
using CardMatchGame.Gameplay.Utils;
using PrimeTween;
using UnityEngine;
using Zenject;

namespace CardMatchGame.Gameplay.Services
{
  public class LevelProgress : MonoBehaviour
  {
    public event Action OnGameOver;

    public LevelsData LevelsData;
    public int LevelIndex;

    [NonSerialized]
    public readonly Cooldown LevelTimer = new();

    private GridService m_grid;
    private CardsService m_cardsService;
    private LevelInputService m_levelInput;
    private MatchCardsService m_matchCardsService;

    private LevelData m_levelData;

    private bool IsLevelCompleted => m_matchCardsService.MatchesCount == m_matchCardsService.MatchesCountToWin;

    [Inject]
    private void Construct(GridService grid,
      CardsService cardsService,
      LevelInputService levelInput,
      MatchCardsService matchCardsService)
    {
      m_grid = grid;
      m_cardsService = cardsService;
      m_levelInput = levelInput;
      m_matchCardsService = matchCardsService;
    }

    public void Awake()
    {
      m_levelData = LevelsData.Levels[LevelIndex];

      m_grid.SetSize(m_levelData.GridSize);
      m_matchCardsService.Init(m_levelData.CardsCountToMatch, m_levelData.MatchesCountToWin);
      m_cardsService.Init(m_levelData.CardsCountToMatch);
      LevelTimer.Init(m_levelData.TimerDuration);
    }

    private IEnumerator Start()
    {
      yield return GameLoop();
    }

    public Sequence ShowCardsHint()
    {
      return m_cardsService.FlipCardsToFront()
        .ChainDelay(m_levelData.ShowCardsDuration)
        .Chain(m_cardsService.FlipCards());
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
      m_levelInput.enabled = false;
      m_matchCardsService.StartGame();
      LevelTimer.Activate();

      yield return m_cardsService.FlipCardsToBack()
        .ToYieldInstruction();
      
      m_cardsService.Shuffle();

      yield return ShowCardsHint()
        .ToYieldInstruction();
      
      m_levelInput.enabled = true;
    }
  }
}