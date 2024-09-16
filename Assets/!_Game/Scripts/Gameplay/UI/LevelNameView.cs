using CardMatchGame.Services.Levels;
using CardMatchGame.UI.Utils;
using UnityEngine;
using Zenject;

namespace CardMatchGame.Gameplay.UI
{
  public class LevelNameView : MonoBehaviour
  {
    public PrintfText Text;

    private ICurrentLevelDataProvider m_currentLevelData;

    [Inject]
    private void Construct(ICurrentLevelDataProvider currentLevelData) => 
      m_currentLevelData = currentLevelData;

    private void Start() => 
      Text.UpdateView(m_currentLevelData.Index + 1);
  }
}