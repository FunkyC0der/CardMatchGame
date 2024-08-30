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

    [HideInInspector]
    public Cooldown LevelTimer;

    private GridService m_grid;
    private CardsService m_cardsService;
    private LevelInputService m_levelInput;
    private MatchCardsService m_matchCardsService;

    private LevelData m_levelData;

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

    private void Start() => 
      RunLevel();

    public Sequence ShowCardsHint()
    {
      return m_cardsService.FlipCardsToFront()
        .ChainDelay(m_levelData.ShowCardsDuration)
        .Chain(m_cardsService.FlipCards());
    }

    private void RunLevel() => 
      StartCoroutine(GameLoop());

    private IEnumerator GameLoop()
    {
      yield return EnterGameLoop();

      yield return WaitGameOverCondition();

      ExitGameLoop();
    }

    private IEnumerator EnterGameLoop()
    {
      m_levelData = LevelsData.Levels[LevelIndex];

      m_grid.SetSize(m_levelData.GridSize);
      m_matchCardsService.CardsToMatchCount = m_levelData.CardsCountToMatch;
      m_cardsService.Init(m_levelData.CardsCountToMatch);
      LevelTimer.Duration = m_levelData.TimerDuration;

      yield return StartGame();
    }
    
    private void ExitGameLoop()
    {
      m_levelInput.enabled = false;
      m_matchCardsService.StopMatchProcess();
      LevelTimer.Pause();
      
      OnGameOver?.Invoke();
    }

    private IEnumerator WaitGameOverCondition()
    {
      yield return new WaitUntil(() => LevelTimer.IsDone);
    }

    private IEnumerator StartGame()
    {
      m_levelInput.enabled = false;

      LevelTimer.Activate();
      LevelTimer.Pause();

      yield return m_cardsService.FlipCardsToBack()
        .ToYieldInstruction();
      
      m_cardsService.Shuffle();

      yield return ShowCardsHint()
        .ToYieldInstruction();
      
      LevelTimer.Resume();
      m_levelInput.enabled = true;
    }
  }
}