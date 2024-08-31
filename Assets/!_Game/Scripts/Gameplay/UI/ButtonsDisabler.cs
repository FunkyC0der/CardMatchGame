using CardMatchGame.Gameplay.Services.Input;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CardMatchGame.Gameplay.UI
{
  public class ButtonsDisabler : MonoBehaviour
  {
    public Button[] Buttons;
    
    [Inject]
    private void Construct(ILevelInput levelInput) => 
      levelInput.OnEnabledChanged += UpdateView;

    private void UpdateView(bool isEnabled)
    {
      foreach (Button button in Buttons) 
        button.interactable = isEnabled;
    }
  }
}