using System.Collections;
using CardMatchGame.Gameplay.Services;
using CardMatchGame.Gameplay.Services.Input;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CardMatchGame.Gameplay.UI.Controls
{
  public class ShowCardsButton : MonoBehaviour
  {
    public Button Button;

    private ILevelInput m_levelInput;
    private CardsService m_cardsService;

    [Inject]
    private void Construct(ILevelInput levelInput, CardsService cardsService)
    {
      m_levelInput = levelInput;
      m_cardsService = cardsService;
      
      Button.onClick.AddListener(() => StartCoroutine(ShowCards()));
    }

    private IEnumerator ShowCards()
    {
      m_levelInput.Enabled = false;
      
      yield return m_cardsService.ShowCardsHint()
        .ToYieldInstruction();
      
      m_levelInput.Enabled = true;
    }
  }
}