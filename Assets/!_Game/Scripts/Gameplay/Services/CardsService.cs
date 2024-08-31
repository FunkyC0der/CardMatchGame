using System.Collections;
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
    private ILevelsService m_levelsService;

    private readonly List<Card> m_cards = new();
    private List<CardDesc> m_cardDescs = new();
    
    [Inject]
    private void Construct(GridService grid, ILevelsService levelsService)
    {
      m_grid = grid;
      m_levelsService = levelsService;
    }

    public void Initialize()
    {
      int cardsCountToMatch = m_levelsService.LevelData.CardsCountToMatch;
      int cardsCount = m_grid.CellsCount;
      
      m_cardDescs = CardsBank.SelectRandomCards(cardsCount / cardsCountToMatch);

      CreateCards(cardsCountToMatch);
      UpdateCardsPosition();
    }

    public void Shuffle()
    {
      m_cards.Shuffle();
      ResetCardsState();
      UpdateCardsPosition();
    }

    public IEnumerator ShowCardsHint()
    {
      yield return FlipAllCardsToFront()
        .ChainDelay(m_levelsService.LevelData.ShowCardsDuration)
        .ToYieldInstruction();

      yield return m_cards
        .Where(card => !card.IsMatched && !card.IsSelected)
        .GroupTweens(card => card.Animator.PlayFlipAnim())
        .ToYieldInstruction();
    }

    public Sequence FlipAllCardsToBack()
    {
      return m_cards
        .Where(card => card.IsFrontSide)
        .GroupTweens(card => card.Animator.PlayFlipAnim());
    }

    private Sequence FlipAllCardsToFront()
    {
      return m_cards
        .Where(card => !card.IsFrontSide)
        .GroupTweens(card => card.Animator.PlayFlipAnim());
    }

    private void ResetCardsState()
    {
      foreach (Card card in m_cards)
      {
        card.Selectable = true;
        card.IsSelected = false;
        card.IsMatched = false;
      }
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
  }
}