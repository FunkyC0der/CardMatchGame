using CardMatchGame.Gameplay.Utils;
using CardMatchGame.Services.GameStates;
using CardMatchGame.Services.Levels;
using CardMatchGame.Services.Progress;
using CardMatchGame.Services.SaveLoad;
using CardMatchGame.Services.UI;

namespace CardMatchGame.Gameplay.Services.GameStates
{
  public class LevelOverGameState : GameState
  {
    private readonly TimerService m_timer;
    private readonly UIFactory m_uiFactory;
    private readonly IProgressService m_progressService;
    private readonly ISaveLoadService m_saveLoadService;
    private readonly ICurrentLevelDataProvider m_currentLevelData;

    public LevelOverGameState(TimerService timer,
      UIFactory uiFactory,
      IProgressService progressService,
      ISaveLoadService saveLoadService, 
      ICurrentLevelDataProvider currentLevelData)
    {
      m_timer = timer;
      m_uiFactory = uiFactory;
      m_progressService = progressService;
      m_saveLoadService = saveLoadService;
      m_currentLevelData = currentLevelData;
    }

    public override void Enter()
    {
      m_timer.Stop();
      
      if (m_timer.IsFinished)
      {
        m_uiFactory.CreateWindow(WindowType.GameOverLosePopUp);
        return;
      }

      CompletedLevelData completedLevelData = FindCompletedLevelData() ?? AddNewCompletedLevelData();

      if (IsNewTimeRecord(completedLevelData))
      {
        float oldTimeRecord = completedLevelData.TimeRecord;
        completedLevelData.TimeRecord = m_timer.TimeElapsed;

        m_uiFactory.CreateWindow(WindowType.GameOverWinTimeRecordPopUp,
          new NewTimeRecordPayload(oldTimeRecord, m_timer.TimeElapsed));
      }
      else
      {
        m_uiFactory.CreateWindow(WindowType.GameOverWinPopUp, m_timer.TimeElapsed);
      }

      m_saveLoadService.Save(m_progressService.Progress);
    }
    
    private CompletedLevelData FindCompletedLevelData() =>
      m_progressService.FindCompletedLevelData(m_currentLevelData.Index);

    private CompletedLevelData AddNewCompletedLevelData()
    {
      var completedLevelData = new CompletedLevelData()
      {
        Index = m_currentLevelData.Index,
        TimeRecord = m_timer.TimeElapsed
      };

      m_progressService.AddCompletedLevelData(completedLevelData);
      return completedLevelData;
    }

    private bool IsNewTimeRecord(CompletedLevelData completedLevelData) =>
      m_timer.TimeElapsed < completedLevelData.TimeRecord;
  }
}