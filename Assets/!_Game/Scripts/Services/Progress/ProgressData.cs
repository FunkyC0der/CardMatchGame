using System;
using System.Collections.Generic;
using UnityEngine.Serialization;

namespace CardMatchGame.Services.Progress
{
  [Serializable]
  public class ProgressData
  {
    public List<CompletedLevelData> CompletedLevels = new();
  }

  [Serializable]
  public class CompletedLevelData
  {
    public int Index;
    [FormerlySerializedAs("BestTime")]
    public float TimeRecord;
  }
}