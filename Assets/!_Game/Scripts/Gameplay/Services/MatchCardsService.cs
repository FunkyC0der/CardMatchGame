using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CardMatchGame.Gameplay.Cards;
using CardMatchGame.Gameplay.Services.Input;
using CardMatchGame.Services.Levels;
using UnityEngine;
using Zenject;

namespace CardMatchGame.Gameplay.Services
{
  public class MatchCardsService : MonoBehaviour, IInitializable
  {
    public event Action OnMatchesCountChanged;

    private int m_cardsToMatchCount = 2;

    public int MatchesCountToWin { get; private set; }
    public int MatchesCount { get; private set; }

    private ILevelInput m_levelInput;
    private ILevelsService m_levelsService;
    
    private readonly List<Card> m_cardsToMatch = new();
    
    [Inject]
    private void Construct(ILevelInput levelInput, ILevelsService levelsService)
    {
      m_levelInput = levelInput;
      levelInput.OnCardSelected += SelectCard;

      m_levelsService = levelsService;
    }

    public void Initialize()
    {
      m_cardsToMatchCount = m_levelsService.LevelData.CardsCountToMatch;
      MatchesCountToWin = m_levelsService.LevelData.MatchesCountToWin;
    }

    public void StartGame()
    {
      MatchesCount = 0;
      OnMatchesCountChanged?.Invoke();
    }

    public void StopMatchProcess()
    {
      if (m_cardsToMatch.Count == 0)
        return;
      
      StopAllCoroutines();

      foreach (Card card in m_cardsToMatch) 
        card.Animator.PlayFlipAnim();
      
      m_cardsToMatch.Clear();
    }

    private void SelectCard(Card card) => 
      StartCoroutine(MatchProcess(card));

    private IEnumerator MatchProcess(Card newCard)
    {
      m_cardsToMatch.Add(newCard);
      
      newCard.Selectable = false;
      newCard.IsSelected = true;
      
      bool allCardsMatch = m_cardsToMatch.All(card => card.Desc == m_cardsToMatch.First().Desc);
      
      m_levelInput.SetEnabled(false);
      
      yield return newCard.Animator.PlayFlipAnim().ToYieldInstruction();
      
      if (!allCardsMatch)
        yield return MatchFailProcess();
      else if (m_cardsToMatch.Count == m_cardsToMatchCount)
        yield return MatchSuccessProcess();
      
      m_levelInput.SetEnabled(true);
    }

    private IEnumerator MatchFailProcess()
    {
      yield return m_cardsToMatch.GroupTweens(card => card.Animator.PlayMatchFailedAnim())
        .ToYieldInstruction();

      yield return m_cardsToMatch.GroupTweens(card => card.Animator.PlayFlipAnim())
        .ToYieldInstruction();

      foreach (Card card in m_cardsToMatch)
      {
        card.Selectable = true;
        card.IsSelected = false;
      }
        
      m_cardsToMatch.Clear();
    }

    private IEnumerator MatchSuccessProcess()
    {
      foreach (Card card in m_cardsToMatch)
      {
        card.IsMatched = true;
        card.IsSelected = false;
      }
      
      yield return m_cardsToMatch.GroupTweens(card => card.Animator.PlayMatchSuccessAnim())
        .ToYieldInstruction();
        
      m_cardsToMatch.Clear();

      ++MatchesCount;
      OnMatchesCountChanged?.Invoke();
    }
  }
}