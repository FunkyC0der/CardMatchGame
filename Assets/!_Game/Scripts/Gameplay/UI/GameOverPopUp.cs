using CardMatchGame.Gameplay.Services;
using CardMatchGame.Gameplay.UI.Utils;
using CardMatchGame.Services.Levels;
using CardMatchGame.Services.Progress;
using PrimeTween;
using TMPro;
using UnityEngine;
using Zenject;

namespace CardMatchGame.Gameplay.UI
{
  public class GameOverPopUp : MonoBehaviour
  {
    public TextMeshProUGUI LevelCompletedText;
    public TextMeshProUGUI LevelFailedText;

    public PrintfText TimeText;
    public PrintfText NewTimeRecordText;

    public RectTransform AnimWindow;
    public TweenSettings<Vector2> ShowHideAnimSettings;

    private TimerService m_timerService;
    private WinConditionService m_winConditionService;
    private IProgressService m_progressService;
    private ILevelsService m_levelsService;

    [Inject]
    private void Construct(TimerService timerService,
      WinConditionService winConditionService,
      IProgressService progressService,
      ILevelsService levelsService)
    {
      m_timerService = timerService;
      m_winConditionService = winConditionService;
      m_progressService = progressService;
      m_levelsService = levelsService;
    }

    public void Start()
    {
      Tween.UIAnchoredPosition(AnimWindow, ShowHideAnimSettings);
      
      if (m_winConditionService.WinCondition)
        SetupLevelCompletedView();
      else
        LevelFailedText.gameObject.SetActive(true);
    }

    private void SetupLevelCompletedView()
    {
      LevelCompletedText.gameObject.SetActive(true);
        
      CompletedLevelData completedLevelData = FindCompletedLevelData() ?? AddNewCompletedLevelData();

      if (IsNewTimeRecord(completedLevelData))
      {
        ShowNewTimeRecordText(completedLevelData);
        UpdateNewTimeRecordData(completedLevelData);
      }
      else
      {
        ShowTimeText();
      }
    }

    private CompletedLevelData FindCompletedLevelData() => 
      m_progressService.FindCompletedLevelData(m_levelsService.CurrentLevelIndex);

    private CompletedLevelData AddNewCompletedLevelData()
    {
      var completedLevelData = new CompletedLevelData()
      {
        Index = m_levelsService.CurrentLevelIndex,
        TimeRecord = m_timerService.TimeElapsed
      };

      m_progressService.AddCompletedLevelData(completedLevelData);
      return completedLevelData;
    }

    private bool IsNewTimeRecord(CompletedLevelData completedLevelData) => 
      m_timerService.TimeElapsed < completedLevelData.TimeRecord;

    private void UpdateNewTimeRecordData(CompletedLevelData completedLevelData) => 
      completedLevelData.TimeRecord = m_timerService.TimeElapsed;

    private void ShowNewTimeRecordText(CompletedLevelData completedLevelData)
    {
      NewTimeRecordText.UpdateView(completedLevelData.TimeRecord.ToMinutesAndSeconds(), 
        m_timerService.TimeElapsed.ToMinutesAndSeconds());

      NewTimeRecordText.gameObject.SetActive(true);
    }

    private void ShowTimeText()
    {
      TimeText.UpdateView(m_timerService.TimeElapsed.ToMinutesAndSeconds());
      TimeText.gameObject.SetActive(true);
    }
  }
}