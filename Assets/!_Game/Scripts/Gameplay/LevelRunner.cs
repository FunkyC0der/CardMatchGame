using CardMatchGame.Gameplay.Services;
using UnityEngine;
using Zenject;

namespace CardMatchGame.Gameplay
{
  public class LevelRunner : MonoBehaviour
  {
    private CardsService m_cardsService;

    [Inject]
    private void Construct(CardsService cardsService) =>
      m_cardsService = cardsService;

    private void Start() => 
      m_cardsService.FillGrid();
  }
}