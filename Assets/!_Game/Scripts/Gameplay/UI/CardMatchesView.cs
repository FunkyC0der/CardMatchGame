using CardMatchGame.Gameplay.Services;
using CardMatchGame.Gameplay.UI.Utils;
using UnityEngine;
using Zenject;

namespace CardMatchGame.Gameplay.UI
{
  public class CardMatchesView : MonoBehaviour
  {
    public PrintfText Text;

    private MatchCardsService m_matchCardsService;

    [Inject]
    private void Construct(MatchCardsService matchCardsService)
    {
      m_matchCardsService = matchCardsService;
      m_matchCardsService.OnMatchesCountChanged += UpdateView;
    }

    private void Start() => 
      UpdateView();

    private void UpdateView() => 
      Text.UpdateView(m_matchCardsService.MatchesCount, m_matchCardsService.MatchesCountToWin);
  }
}