using System;
using CardMatchGame.Services.Levels;
using CardMatchGame.Services.UI;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CardMatchGame.Services.Assets
{
  public class AssetsService : IAssetsService
  {
    private LevelsData m_levelsData;
    private UIAssetsData m_uiAssetsData;
    
    public void Load()
    {
      m_levelsData = LoadAsset<LevelsData>(AssetsPath.LevelsDataPath);

      m_uiAssetsData = LoadAsset<UIAssetsData>(AssetsPath.UIAssetsPath);
      m_uiAssetsData.Init();
    }

    public LevelsData LevelsData() => 
      m_levelsData;

    public UIAssetsData UIAssetsData() => 
      m_uiAssetsData;

    private static TAsset LoadAsset<TAsset>(string path) where TAsset : Object
    {
      var asset = Resources.Load<TAsset>(path);
      
      if(asset == null)
        throw new Exception($"Failed to load asset {typeof(TAsset).Name} by path {path}");

      return asset;
    }
  }
}