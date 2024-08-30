using System.Collections.Generic;
using System.Linq;
using CardMatchGame.Gameplay.Cards;
using PrimeTween;
using UnityEngine;
using Zenject;

namespace CardMatchGame.Gameplay.Services
{
  public class CardsService : MonoBehaviour
  {
    public CardsBank CardsBank;
    public Card CardPrefab;

    private GridService m_grid;

    private readonly List<Card> m_cards = new();
    private List<CardDesc> m_cardDescs = new();

    [Inject]
    private void Construct(GridService grid) => 
      m_grid = grid;

    public void Init(int cardsCountToMatch)
    {
      int cardsCount = m_grid.CellsCount;
      m_cardDescs = CardsBank.SelectRandomCards(cardsCount / cardsCountToMatch);

      CreateCards(cardsCountToMatch);
      UpdateCardsPosition();
    }

    public void Shuffle()
    {
      m_cards.Shuffle();
      UpdateCardsPosition();
    }

    private void UpdateCardsPosition()
    {
      for (int i = 0; i < m_cards.Count; ++i) 
        m_cards[i].transform.position = m_grid.CellCenterPosition(i);
    }

    private void CreateCards(int cardsCountToMatch)
    {
      foreach (CardDesc desc in m_cardDescs)
      {
        for(int i = 0; i < cardsCountToMatch; ++i)
          m_cards.Add(CreateCard(desc));
      }
    }

    private Card CreateCard(CardDesc desc)
    {
      Card card = Instantiate(CardPrefab, transform);
      card.SetDesc(desc);
      return card;
    }

    public Sequence FlipCardsToBack()
    {
      return m_cards
        .Where(card => card.IsFrontSideVisible)
        .GroupTweens(card => card.Animator.PlayFlipAnim());
    }

    public Sequence FlipCards() => 
      m_cards.GroupTweens(card => card.Animator.PlayFlipAnim());

    public Sequence FlipCardsToFront()
    {
      return m_cards
        .Where(card => !card.IsFrontSideVisible)
        .GroupTweens(card => card.Animator.PlayFlipAnim());
    }
  }
}