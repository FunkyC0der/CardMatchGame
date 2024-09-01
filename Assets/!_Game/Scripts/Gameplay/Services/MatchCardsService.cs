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

      m_cardsToMatch.GroupTweens(card => card.Animator.PlayFlipToBackAnim());
      m_cardsToMatch.Clear();
    }

    private void SelectCard(Card card) => 
      StartCoroutine(MatchProcess(card));

    private IEnumerator MatchProcess(Card newCard)
    {
      newCard.Selectable = false;
      m_cardsToMatch.Add(newCard);
      
      bool allCardsMatch = m_cardsToMatch.All(card => card.Desc == m_cardsToMatch.First().Desc);
      
      yield return newCard.Animator.PlayFlipToFrontAnim().ToYieldInstruction();
      
      if (!allCardsMatch)
        yield return MatchFailProcess();
      else if (m_cardsToMatch.Count == m_cardsToMatchCount)
        yield return MatchSuccessProcess();
    }

    private IEnumerator MatchFailProcess()
    {
      yield return PlayLongAnim(
        m_cardsToMatch.GroupTweens(card => card.Animator.PlayMatchFailedAnim())
        .Chain(m_cardsToMatch.GroupTweens(card => card.Animator.PlayFlipToBackAnim()))
        .ToYieldInstruction());

      foreach (Card card in m_cardsToMatch) 
        card.Selectable = true;
        
      m_cardsToMatch.Clear();
    }

    private IEnumerator MatchSuccessProcess()
    {
      yield return PlayLongAnim(
        m_cardsToMatch.GroupTweens(card => card.Animator.PlayMatchSuccessAnim())
        .ToYieldInstruction());

      m_cardsToMatch.Clear();

      ++MatchesCount;
      OnMatchesCountChanged?.Invoke();
    }

    private IEnumerator PlayLongAnim(IEnumerator anim)
    {
      m_levelInput.SetEnabled(false);
      yield return anim;
      m_levelInput.SetEnabled(true);
    }
  }
}