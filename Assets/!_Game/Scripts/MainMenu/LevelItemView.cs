using CardMatchGame.Gameplay.UI.Utils;
using CardMatchGame.Services.Levels;
using CardMatchGame.Services.Progress;
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

    private ILevelsService m_levelsService;
    private IProgressService m_progressService;

    [Inject]
    private void Construct(ILevelsService levelsService, IProgressService progressService)
    {
      m_levelsService = levelsService;
      m_progressService = progressService;
    }

    private void Start()
    {
      Button.onClick.AddListener(() => m_levelsService.SetLevelData(LevelIndex));
      NameText.UpdateView(LevelIndex + 1);
      
      CompletedLevelData completedLevel = m_progressService.FindCompletedLevelData(LevelIndex);
      if (completedLevel != null)
        Button.image.color = CompletedColor;
    }
  }
}