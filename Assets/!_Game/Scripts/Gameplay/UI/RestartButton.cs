using CardMatchGame.Gameplay.Services;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CardMatchGame.Gameplay.UI
{
  public class RestartButton : MonoBehaviour
  {
    public Button Button;

    private LevelInputService m_levelInput;

    [Inject]
    private void Construct(LevelInputService levelInput, LevelProgress levelProgress)
    {
      m_levelInput = levelInput;
      m_levelInput.OnEnabledChanged += value => Button.interactable = value; 
      
      Button.onClick.AddListener(levelProgress.Restart);
    }
  }
}