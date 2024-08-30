using CardMatchGame.Gameplay.UI.Utils;
using CardMatchGame.Services;
using UnityEngine;
using Zenject;

namespace CardMatchGame.Gameplay.UI
{
  public class LevelNameView : MonoBehaviour
  {
    public PrintfText Text;

    private LevelsDataService m_levelsDataService;

    [Inject]
    private void Construct(LevelsDataService levelsDataService) => 
      m_levelsDataService = levelsDataService;

    private void Start() => 
      Text.UpdateView(m_levelsDataService.LevelIndex + 1);
  }
}