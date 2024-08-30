using CardMatchGame.Gameplay.Services;
using CardMatchGame.Gameplay.UI.Utils;
using UnityEngine;
using Zenject;

namespace CardMatchGame.Gameplay.UI
{
  public class LevelNameView : MonoBehaviour
  {
    public PrintfText Text;

    private LevelProgress m_levelProgress;

    [Inject]
    private void Construct(LevelProgress levelProgress) => 
      m_levelProgress = levelProgress;

    private void Start() => 
      Text.UpdateView(m_levelProgress.LevelIndex + 1);
  }
}