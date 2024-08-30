using CardMatchGame.Gameplay.Services;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CardMatchGame.Gameplay.UI
{
  public class RestartButton : MonoBehaviour
  {
    public Button Button;

    [Inject]
    private void Construct(LevelProgress levelProgress) => 
      Button.onClick.AddListener(levelProgress.Restart);
  }
}