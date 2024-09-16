using System;
using CardMatchGame.Services.Levels;

namespace CardMatchGame.Gameplay.Services
{
  public class WinConditionService
  {
    public event Action OnMatchesCountChanged;
    
    public int MatchesCountToWin { get; }
    public int MatchesCount { get; private set; }

    public bool Achieved => MatchesCount >= MatchesCountToWin;

    private WinConditionService(ICurrentLevelDataProvider currentLevelData)
    {
      MatchesCountToWin = currentLevelData.Data.MatchesCountToWin;
      MatchesCount = 0;
    }

    public void Match()
    {
      ++MatchesCount;
      OnMatchesCountChanged?.Invoke();
    }
  }
}