using CardMatchGame.Gameplay.UI.Utils;
using CardMatchGame.Services.Levels;
using UnityEngine;
using Zenject;

namespace CardMatchGame.Gameplay.UI
{
  public class LevelNameView : MonoBehaviour
  {
    public PrintfText Text;

    private ILevelsService m_levelsService;

    [Inject]
    private void Construct(ILevelsService levelsService) => 
      m_levelsService = levelsService;

    private void Start() => 
      Text.UpdateView(m_levelsService.LevelIndex + 1);
  }
}