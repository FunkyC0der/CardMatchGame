using CardMatchGame.Services.GameStates;
using CardMatchGame.Services.GameStates.States;
using CardMatchGame.Services.Levels;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CardMatchGame.Gameplay.UI.Controls
{
  public class RestartButton : MonoBehaviour
  {
    public Button Button;
    
    private GameStateChanger m_gameStateChanger;
    private ICurrentLevelDataProvider m_currentLevelData;

    [Inject]
    private void Construct(GameStateChanger gameStateChanger, ICurrentLevelDataProvider currentLevelData)
    {
      m_gameStateChanger = gameStateChanger;
      m_currentLevelData = currentLevelData;
      
      Button.onClick.AddListener(RestartLevel);
    }

    private void RestartLevel() => 
      m_gameStateChanger.Enter<LoadLevelGameState, int>(m_currentLevelData.Index);
  }
}