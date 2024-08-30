using System.Collections.Generic;
using CardMatchGame.Gameplay.Cards;
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

    [ContextMenu("FillGrid")]
    public void FillGrid()
    {
      ClearGrid();

      int cardsCount = m_grid.CellsCount;
      m_cardDescs = CardsBank.SelectRandomCards(cardsCount / 2);

      foreach (CardDesc desc in m_cardDescs)
      {
        m_cards.Add(CreateCard(desc));
        m_cards.Add(CreateCard(desc));
      }

      m_cards.Shuffle();
      
      for (int i = 0; i < m_cards.Count; ++i) 
        m_cards[i].transform.position = m_grid.CellCenterPosition(i);
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