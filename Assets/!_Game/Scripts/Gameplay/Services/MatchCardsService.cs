using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CardMatchGame.Gameplay.Cards;
using CardMatchGame.Gameplay.Services.Input;
using CardMatchGame.Services.Levels;
using PrimeTween;
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

    private void SelectCard(Card card) => 
      StartCoroutine(MatchProcess(card));

    private IEnumerator MatchProcess(Card newCard)
    {
      newCard.IsFrontSide = true;
      newCard.Selectable = false;
      
      m_cardsToMatch.Add(newCard);
      
      bool allCardsMatch = m_cardsToMatch.All(card => card.Desc == m_cardsToMatch.First().Desc);

      Sequence sequence = Sequence.Create()
        .Chain(newCard.Animator.PlayFlipToFrontAnim());

      if (!allCardsMatch)
      {
        sequence.Chain(MatchFailSequence(m_cardsToMatch.ToList()));
        m_cardsToMatch.Clear();
      }
      else if (m_cardsToMatch.Count == m_cardsToMatchCount)
      {
        sequence.Chain(MatchSuccessSequence(m_cardsToMatch.ToList()));
        m_cardsToMatch.Clear();
      }

      yield return sequence.ToYieldInstruction();
    }

    private Sequence MatchFailSequence(List<Card> cards)
    {
      return cards.GroupTweens(card => card.Animator.PlayMatchFailedAnim())
        .Chain(cards.GroupTweens(card => card.Animator.PlayFlipToBackAnim()))
        .ChainCallback(() =>
        {
          foreach (Card card in cards)
          {
            card.IsFrontSide = false;
            card.Selectable = true;
          }
        });
    }

    private Sequence MatchSuccessSequence(List<Card> cards)
    {
      m_winConditionService.Match();
      return cards.GroupTweens(card => card.Animator.PlayMatchSuccessAnim());
    }
  }
}