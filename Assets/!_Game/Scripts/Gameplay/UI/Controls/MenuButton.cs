using CardMatchGame.GameStates;
using CardMatchGame.GameStates.States;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CardMatchGame.Gameplay.UI.Controls
{
  public class MenuButton : MonoBehaviour
  {
    public Button Button;
    
    private GameStateChanger m_gameStateChanger;

    [Inject]
    private void Construct(GameStateChanger gameStateChanger)
    {
      m_gameStateChanger = gameStateChanger;
      
      Button.onClick.AddListener(GoToMenu);
    }

    private void GoToMenu() => 
      m_gameStateChanger.Enter<MainMenuGameState>();
  }
}