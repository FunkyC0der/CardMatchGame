using CardMatchGame.Services.Assets;
using UnityEngine;
using Zenject;

namespace CardMatchGame.MainMenu
{
  public class LevelsView : MonoBehaviour
  {
    public LevelItemView LevelItemPrefab;
    public Transform ContentParent;

    private IAssetsService m_assets;

    [Inject]
    private void Construct(IAssetsService assets) => 
      m_assets = assets;

    private void Start()
    {
      for(var i = 0; i < m_assets.LevelsData().Levels.Length; ++i)
      {
        LevelItemView levelItem = Instantiate(LevelItemPrefab, ContentParent);
        levelItem.LevelIndex = i;
      }
    }
  }
}