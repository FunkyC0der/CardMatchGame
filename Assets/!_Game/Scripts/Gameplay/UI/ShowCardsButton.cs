using System.Collections;
using CardMatchGame.Gameplay.Services;
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
      
      Button.onClick.AddListener(() => StartCoroutine(ShowCards()));
    }

    private IEnumerator ShowCards()
    {
      m_levelInput.enabled = false;
      yield return m_cardsService.ShowCardsHint();
      m_levelInput.enabled = true;
    }
  }
}