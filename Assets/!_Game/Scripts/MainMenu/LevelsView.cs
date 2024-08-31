using CardMatchGame.Services.Levels;
using UnityEngine;
using Zenject;

namespace CardMatchGame.MainMenu
{
  public class LevelsView : MonoBehaviour
  {
    public LevelItemView LevelItemPrefab;
    public Transform ContentParent;

    private ILevelsService m_levelsService;

    [Inject]
    private void Construct(ILevelsService levelsService) => 
      m_levelsService = levelsService;

    private void Start()
    {
      for(var i = 0; i < m_levelsService.Levels.Length; ++i)
      {
        LevelItemView levelItem = Instantiate(LevelItemPrefab, ContentParent);
        levelItem.LevelIndex = i;
      }
    }
  }
}