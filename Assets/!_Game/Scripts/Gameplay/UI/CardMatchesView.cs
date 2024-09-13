using CardMatchGame.Gameplay.Services;
using CardMatchGame.Gameplay.UI.Utils;
using UnityEngine;
using Zenject;

namespace CardMatchGame.Gameplay.UI
{
  public class CardMatchesView : MonoBehaviour
  {
    public PrintfText Text;

    private WinConditionService m_winConditionService;

    [Inject]
    private void Construct(WinConditionService winConditionService)
    {
      m_winConditionService = winConditionService;
      m_winConditionService.OnMatchesCountChanged += UpdateView;
    }

    private void Start() => 
      UpdateView();

    private void UpdateView() => 
      Text.UpdateView(m_winConditionService.MatchesCount, m_winConditionService.MatchesCountToWin);
  }
}