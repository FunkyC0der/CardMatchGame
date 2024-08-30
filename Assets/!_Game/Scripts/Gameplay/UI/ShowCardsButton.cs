using CardMatchGame.Gameplay.Services;
using PrimeTween;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CardMatchGame.Gameplay.UI
{
  public class ShowCardsButton : MonoBehaviour
  {
    public Button Button;

    private LevelInputService m_levelInput;
    private CardsService m_cardsService;

    [Inject]
    private void Construct(LevelInputService levelInput, CardsService cardsService)
    {
      m_levelInput = levelInput;
      m_cardsService = cardsService;
      
      Button.onClick.AddListener(ShowCards);
    }

    private void ShowCards()
    {
      Sequence.Create()
        .ChainCallback(() => m_levelInput.enabled = false)
        .Chain(m_cardsService.ShowCardsHint())
        .ChainCallback(() => m_levelInput.enabled = true);
    }
  }
}