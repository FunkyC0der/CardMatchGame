using System.Collections;
using CardMatchGame.Gameplay.Cards;
using PrimeTween;
using UnityEngine;
using Zenject;

namespace CardMatchGame.Gameplay.Services
{
  public class MatchCardsService : MonoBehaviour
  {
    private LevelInputService m_levelInput;

    private Card m_selectedCard;

    [Inject]
    private void Construct(LevelInputService levelInput)
    {
      m_levelInput = levelInput;
      m_levelInput.OnCardSelected += SelectCard;
    }

    private void SelectCard(Card card) => 
      StartCoroutine(!m_selectedCard ? StartMatchProcess(card) : EndMatchProcess(card));

    private IEnumerator StartMatchProcess(Card card)
    {
      m_selectedCard = card;
      m_selectedCard.Selectable = false;

      m_levelInput.enabled = false;
      yield return m_selectedCard.Animator.PlayFlipAnim().ToYieldInstruction();
      m_levelInput.enabled = true;
    }

    private IEnumerator EndMatchProcess(Card card)
    {
      card.Selectable = false;
      m_levelInput.enabled = false;

      yield return card.Animator.PlayFlipAnim().ToYieldInstruction();

      if (card.Desc == m_selectedCard.Desc)
      {
        yield return Sequence.Create()
          .Chain(card.Animator.PlayMatchSuccessAnim())
          .Group(m_selectedCard.Animator.PlayMatchSuccessAnim())
          .ToYieldInstruction();
      }
      else 
      {
        yield return Sequence.Create()
          .Chain(card.Animator.PlayMatchFailedAnim())
          .Group(m_selectedCard.Animator.PlayMatchFailedAnim())
          .Chain(card.Animator.PlayFlipAnim())
          .Group(m_selectedCard.Animator.PlayFlipAnim())
          .ToYieldInstruction();

        card.Selectable = true;
        m_selectedCard.Selectable = true;
      }

      m_selectedCard = null;
      m_levelInput.enabled = true;
    }
  }
}