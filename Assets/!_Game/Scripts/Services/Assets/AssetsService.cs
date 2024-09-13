using System;
using CardMatchGame.Services.Levels;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CardMatchGame.Services.Assets
{
  public class AssetsService : IAssetsService
  {
    private LevelsData m_levelsData;
    
    public void Load() => 
      m_levelsData = LoadAsset<LevelsData>(AssetsPath.LevelsDataPath);

    public LevelsData LevelsData() => 
      m_levelsData;

    private static TAsset LoadAsset<TAsset>(string path) where TAsset : Object
    {
      var asset = Resources.Load<TAsset>(path);
      
      if(asset == null)
        throw new Exception($"Failed to load asset {typeof(TAsset).Name} by path {path}");

      return asset;
    }
  }
}