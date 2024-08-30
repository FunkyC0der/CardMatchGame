using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CardMatchGame.Gameplay.Cards;
using PrimeTween;
using UnityEngine;
using Zenject;

namespace CardMatchGame.Gameplay.Services
{
  public class MatchCardsService : MonoBehaviour
  {
    [Min(2)]
    public int CardsToMatchCount = 2;
    
    private LevelInputService m_levelInput;

    private readonly List<Card> m_cardsToMatch = new();

    [Inject]
    private void Construct(LevelInputService levelInput)
    {
      m_levelInput = levelInput;
      m_levelInput.OnCardSelected += SelectCard;
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
      m_levelInput.enabled = false;
      yield return newCard.Animator.PlayFlipAnim().ToYieldInstruction();

      if (m_cardsToMatch.Count == 1)
      {
        m_levelInput.enabled = true;
        yield break;
      }

      bool allCardsMatch = m_cardsToMatch.All(card => card.Desc == m_cardsToMatch.First().Desc);
      
      if (!allCardsMatch)
        yield return MatchFailProcess();
      else if (m_cardsToMatch.Count == CardsToMatchCount)
        yield return MatchSuccessProcess();

      m_levelInput.enabled = true;
    }

    private IEnumerator MatchFailProcess()
    {
      yield return m_cardsToMatch.GroupTweens(card => card.Animator.PlayMatchFailedAnim())
        .ToYieldInstruction();

      yield return m_cardsToMatch.GroupTweens(card => card.Animator.PlayFlipAnim())
        .ToYieldInstruction();

      foreach (Card card in m_cardsToMatch)
        card.Selectable = true;
        
      m_cardsToMatch.Clear();
    }

    private IEnumerator MatchSuccessProcess()
    {
      yield return m_cardsToMatch.GroupTweens(card => card.Animator.PlayMatchSuccessAnim())
        .ToYieldInstruction();
        
      m_cardsToMatch.Clear();
    }
  }
}