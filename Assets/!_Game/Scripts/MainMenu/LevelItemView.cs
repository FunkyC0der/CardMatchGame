using CardMatchGame.GameStates;
using CardMatchGame.GameStates.States;
using CardMatchGame.Services.Progress;
using CardMatchGame.UI.Utils;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CardMatchGame.MainMenu
{
  public class LevelItemView : MonoBehaviour
  {
    public int LevelIndex;
    
    public Button Button;
    public PrintfText NameText;
    public Color CompletedColor;

    private IProgressService m_progressService;
    private GameStateChanger m_gameStateChanger;

    [Inject]
    private void Construct(IProgressService progressService, GameStateChanger gameStateChanger)
    {
      m_progressService = progressService;
      m_gameStateChanger = gameStateChanger;
    }

    private void Start()
    {
      Button.onClick.AddListener(LoadLevel);

      UpdateView();
    }

    private void LoadLevel() => 
      m_gameStateChanger.Enter<LoadLevelGameState, int>(LevelIndex);

    private void UpdateView()
    {
      NameText.UpdateView(LevelIndex + 1);
      
      CompletedLevelData completedLevel = m_progressService.FindCompletedLevelData(LevelIndex);
      if (completedLevel != null)
        Button.image.color = CompletedColor;
    }
  }
}