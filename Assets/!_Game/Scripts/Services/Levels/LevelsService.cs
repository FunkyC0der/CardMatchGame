using System.Reflection;
using CardMatchGame.Data;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace CardMatchGame.Services.Levels
{
  public class LevelsService : MonoBehaviour, ILevelsService, IInitializable
  {
    public int DefaultLevelIndex;
    public string LevelSceneName = "GameLevel";
    public LevelsData LevelsData;

    public int LevelIndex { get; private set; }
    public LevelData LevelData { get; private set; }
    public LevelData[] Levels => LevelsData.Levels;

    public void Initialize() => 
      SetLevelData(DefaultLevelIndex);

    public void SetLevelData(int index)
    {
      LevelIndex = index;
      LevelData = LevelsData.Levels[index];
    }

    public void LoadLevel(int index)
    {
      SetLevelData(index);
      SceneManager.LoadSceneAsync(LevelSceneName);
    }
  }
}