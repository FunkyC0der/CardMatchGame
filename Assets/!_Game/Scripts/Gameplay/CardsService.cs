using System.Collections.Generic;
using CardMatchGame.Gameplay.Cards;
using UnityEngine;

namespace CardMatchGame.Gameplay
{
  public class CardsService : MonoBehaviour
  {
    public CardsBank CardsBank;
    public Card CardPrefab;

    public GridService Grid;

    private readonly List<Card> m_cards = new();
    private List<CardDesc> m_cardDescs = new();

    [ContextMenu("FillGrid")]
    public void FillGrid()
    {
      ClearGrid();

      int cardsCount = Grid.CellsCount;
      m_cardDescs = CardsBank.SelectRandomCards(cardsCount / 2);

      foreach (CardDesc desc in m_cardDescs)
      {
        m_cards.Add(CreateCard(desc));
        m_cards.Add(CreateCard(desc));
      }

      m_cards.Shuffle();
      
      for (int i = 0; i < m_cards.Count; ++i) 
        m_cards[i].transform.position = Grid.CellCenterPosition(i);
    }

    private void ClearGrid()
    {
      foreach (Card card in m_cards) 
        Destroy(card.gameObject);
      
      m_cards.Clear();
      m_cardDescs.Clear();
    }

    private Card CreateCard(CardDesc desc)
    {
      Card card = Instantiate(CardPrefab, transform);
      card.SetDesc(desc);
      return card;
    }
  }
}