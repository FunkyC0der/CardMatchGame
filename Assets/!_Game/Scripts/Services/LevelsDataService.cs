using CardMatchGame.Data;
using UnityEngine;

namespace CardMatchGame.Services
{
  public class LevelsDataService : MonoBehaviour
  {
    public LevelsData LevelsData;

    public int LevelIndex;
    public LevelData LevelData;

    private void Awake() => 
      SetLevel(LevelIndex);

    public void SetLevel(int index)
    {
      LevelIndex = index;
      LevelData = LevelsData.Levels[index];
    }
  }
}