using System.Collections.Generic;
using System.Linq;
using CardMatchGame.Gameplay.Cards;
using CardMatchGame.Services.Levels;
using PrimeTween;
using UnityEngine;
using Zenject;

namespace CardMatchGame.Gameplay.Services
{
  public class CardsService : MonoBehaviour, IInitializable
  {
    public CardsBank CardsBank;
    public Card CardPrefab;

    private GridService m_grid;
    private ICurrentLevelDataProvider m_currentLevelData;

    private readonly List<Card> m_cards = new();
    private List<CardDesc> m_cardDescs = new();
    
    [Inject]
    private void Construct(GridService grid, ICurrentLevelDataProvider currentLevelData)
    {
      m_grid = grid;
      m_currentLevelData = currentLevelData;
    }

    public void Initialize()
    {
      int cardsCountToMatch = m_currentLevelData.Data.CardsCountToMatch;
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

    public Sequence ShowCardsHint()
    {
      Card[] cardsToShow = m_cards.Where(card => !card.IsFrontSide).ToArray();

      SetCardsSelectable(cardsToShow, false);

      return FlipCards(cardsToShow, toFront: true)
        .ChainDelay(m_currentLevelData.Data.ShowCardsDuration)
        .Chain(FlipCards(cardsToShow, toFront: false))
        .ChainCallback(() => SetCardsSelectable(cardsToShow, true));
    }

    public void SetAllCardsSelectable(bool selectable) => 
      SetCardsSelectable(m_cards, selectable);

    private static void SetCardsSelectable(IReadOnlyCollection<Card> cards, bool selectable)
    {
      foreach (Card card in cards) 
        card.Selectable = selectable;
    }

    private static Sequence FlipCards(IReadOnlyCollection<Card> cards, bool toFront) =>
      cards.GroupTweens(card => card.Animator.PlayFlipAnim(toFront));

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
  }
}