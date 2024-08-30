using System;
using System.Collections;
using CardMatchGame.Gameplay.Utils;
using UnityEngine;
using Zenject;

namespace CardMatchGame.Gameplay.Services
{
  public class LevelProgress : MonoBehaviour
  {
    public event Action OnGameOver; 
    
    public Cooldown LevelTimer;
    
    private CardsService m_cardsService;
    private LevelInputService m_levelInput;
    private MatchCardsService m_matchCardsService;

    [Inject]
    private void Construct(CardsService cardsService, LevelInputService levelInput, MatchCardsService matchCardsService)
    {
      m_cardsService = cardsService;
      m_levelInput = levelInput;
      m_matchCardsService = matchCardsService;
    }

    private void Start() => 
      RunLevel();

    private void RunLevel() => 
      StartCoroutine(GameLoop());

    private IEnumerator GameLoop()
    {
      EnterGameLoop();

      yield return GameLoopUpdate();

      ExitGameLoop();
    }

    private void EnterGameLoop()
    {
      m_cardsService.FillGrid();
      LevelTimer.Activate();
    }

    private void ExitGameLoop()
    {
      m_levelInput.enabled = false;
      m_matchCardsService.StopMatchProcess();
      
      OnGameOver?.Invoke();
    }

    private IEnumerator GameLoopUpdate()
    {
      yield return new WaitWhile(() => LevelTimer.IsTicking);
    }
  }
}