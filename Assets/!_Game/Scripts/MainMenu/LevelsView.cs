using CardMatchGame.Services.Assets;
using CardMatchGame.Services.UI;
using UnityEngine;
using Zenject;

namespace CardMatchGame.MainMenu
{
  public class LevelsView : MonoBehaviour
  {
    public Transform ContentParent;

    private IAssetsService m_assets;
    private UIFactory m_uiFactory;

    [Inject]
    private void Construct(IAssetsService assets, UIFactory uiFactory)
    {
      m_assets = assets;
      m_uiFactory = uiFactory;
    }

    private LevelItemView LevelItemPrefab => m_assets.UIAssetsData().LevelItemPrefab;

    private void Start()
    {
      for(var i = 0; i < m_assets.LevelsData().Levels.Length; ++i)
      {
        LevelItemView levelItem = m_uiFactory.Create(LevelItemPrefab, ContentParent);
        levelItem.LevelIndex = i;
      }
    }
  }
}