using System;
using System.Collections.Generic;

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
    public float BestTime;
  }
}