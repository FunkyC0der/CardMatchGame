using System;
using UnityEngine;

namespace CardMatchGame.Services.Levels
{
  [CreateAssetMenu(fileName = "LevelsData", menuName = "Game/LevelsData")]
  public class LevelsData : ScriptableObject
  {
    public LevelData[] Levels;

    private void OnValidate()
    {
      for(int i = 0; i < Levels.Length; ++i)
      {
        LevelData level = Levels[i];
        
        if(level.CardsCountToMatch == 0)
          continue;
        
        if(level.CardsCount % level.CardsCountToMatch > 0)
          Debug.LogError($"Level {i} cards count {level.CardsCount} must be divisible on CardsCountToMatch {level.CardsCountToMatch}");
      }
    }
  }

  [Serializable]
  public class LevelData
  {
    public Vector2Int GridSize = new(2, 2);
    
    [Min(2)]
    public int CardsCountToMatch = 2;

    [Min(0)]
    public float ShowCardsDuration = 3;

    [Min(1)]
    public float TimerDuration = 10;
    
    public int CardsCount => GridSize.x * GridSize.y;
    public int MatchesCountToWin => CardsCount / CardsCountToMatch;
  }
}