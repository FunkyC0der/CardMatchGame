using System.Reflection;
using CardMatchGame.Data;
using UnityEngine;
using Zenject;

namespace CardMatchGame.Services.Levels
{
  public class LevelsService : MonoBehaviour, ILevelsService, IInitializable
  {
    public int DefaultLevelIndex;
    public LevelsData LevelsData;

    public int LevelIndex { get; private set; }
    public LevelData LevelData { get; private set; }

    public void Initialize()
    {
      Debug.Log(MethodBase.GetCurrentMethod());
      SetLevelData(DefaultLevelIndex);
    }

    public void SetLevelData(int index)
    {
      LevelIndex = index;
      LevelData = LevelsData.Levels[index];
    }
  }
}