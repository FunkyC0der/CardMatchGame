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
  public class MatchCardsService : MonoBehaviour
  {
    private int m_cardsToMatchCount = 2;

    private ILevelInput m_levelInput;
    private WinConditionService m_winConditionService;
    
    private readonly List<Card> m_cardsToMatch = new();
    
    [Inject]
    private void Construct(ILevelInput levelInput, ICurrentLevelDataProvider currentLevelData, WinConditionService winConditionService)
    {
      m_levelInput = levelInput;
      levelInput.OnCardSelected += SelectCard;

      m_cardsToMatchCount = currentLevelData.Data.CardsCountToMatch;

      m_winConditionService = winConditionService;
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
      
      if(!allCardsMatch)
        yield return MatchFailProcess(newCard);
      else if (m_cardsToMatch.Count == m_cardsToMatchCount)
        yield return MatchSuccessProcess(newCard);
      else
        yield return newCard.Animator.PlayFlipToFrontAnim()
          .ToYieldInstruction();
    }

    private IEnumerator MatchFailProcess(Card newCard)
    {
      yield return PlayLongAnim(
        newCard.Animator.PlayFlipToFrontAnim()
          .Chain(m_cardsToMatch.GroupTweens(card => card.Animator.PlayMatchFailedAnim()))
          .Chain(m_cardsToMatch.GroupTweens(card => card.Animator.PlayFlipToBackAnim()))
          .ToYieldInstruction());

      foreach (Card card in m_cardsToMatch) 
        card.Selectable = true;
        
      m_cardsToMatch.Clear();
    }

    private IEnumerator MatchSuccessProcess(Card newCard)
    {
      yield return PlayLongAnim(
        newCard.Animator.PlayFlipToFrontAnim()
          .Chain(m_cardsToMatch.GroupTweens(card => card.Animator.PlayMatchSuccessAnim()))
          .ToYieldInstruction());

      m_cardsToMatch.Clear();

      m_winConditionService.Match();
    }

    private IEnumerator PlayLongAnim(IEnumerator anim)
    {
      m_levelInput.SetEnabled(false);
      yield return anim;
      m_levelInput.SetEnabled(true);
    }
  }
}