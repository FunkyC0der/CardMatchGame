using CardMatchGame.Gameplay.UI.Utils;
using CardMatchGame.Services.Levels;
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
    
    private ILevelsService m_levelsService;

    [Inject]
    private void Construct(ILevelsService levelsService) => 
      m_levelsService = levelsService;

    private void Start()
    {
      Button.onClick.AddListener(() => m_levelsService.LoadLevel(LevelIndex));
      NameText.UpdateView(LevelIndex + 1);
    }
  }
}