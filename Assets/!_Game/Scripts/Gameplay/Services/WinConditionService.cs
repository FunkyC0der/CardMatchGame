using System;
using CardMatchGame.Services.Levels;

namespace CardMatchGame.Gameplay.Services
{
  public class WinConditionService
  {
    public event Action OnMatchesCountChanged;
    
    public int MatchesCountToWin { get; }
    public int MatchesCount { get; private set; }

    public bool WinCondition => MatchesCount >= MatchesCountToWin;

    private WinConditionService(ILevelsService levelsService)
    {
      MatchesCountToWin = levelsService.CurrentLevelData.MatchesCountToWin;
      MatchesCount = 0;
    }

    public void Match()
    {
      ++MatchesCount;
      OnMatchesCountChanged?.Invoke();
    }
  }
}