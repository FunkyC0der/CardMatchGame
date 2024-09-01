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

    private LevelProgress m_levelProgress;
    private IProgressService m_progressService;
    private ILevelsService m_levelsService;

    [Inject]
    private void Construct(LevelProgress levelProgress, IProgressService progressService, ILevelsService levelsService)
    {
      m_levelProgress = levelProgress;
      m_levelProgress.OnGameStart += Hide;
      m_levelProgress.OnGameOver += Show;

      m_progressService = progressService;
      m_levelsService = levelsService;
    }

    private void Show()
    {
      ResetElements();
      
      gameObject.SetActive(true);

      Tween.UIAnchoredPosition(AnimWindow, ShowHideAnimSettings);
      
      if (m_levelProgress.IsLevelCompleted)
        SetupLevelCompletedView();
      else
        LevelFailedText.gameObject.SetActive(true);
    }

    private void Hide()
    {
      if (!gameObject.activeSelf)
        return;
      
      Sequence.Create()
        .Chain(Tween.UIAnchoredPosition(AnimWindow, ShowHideAnimSettings.WithDirection(false)))
        .ChainCallback(() => gameObject.SetActive(false));
    }

    private void ResetElements()
    {
      LevelCompletedText.gameObject.SetActive(false);
      LevelFailedText.gameObject.SetActive(false);
      NewTimeRecordText.gameObject.SetActive(false);
      TimeText.gameObject.SetActive(false);
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
      m_progressService.FindCompletedLevelData(m_levelsService.LevelIndex);

    private CompletedLevelData AddNewCompletedLevelData()
    {
      var completedLevelData = new CompletedLevelData()
      {
        Index = m_levelsService.LevelIndex,
        TimeRecord = m_levelProgress.LevelTimer.TimeElapsed
      };

      m_progressService.AddCompletedLevelData(completedLevelData);
      return completedLevelData;
    }

    private bool IsNewTimeRecord(CompletedLevelData completedLevelData) => 
      m_levelProgress.LevelTimer.TimeElapsed < completedLevelData.TimeRecord;

    private void UpdateNewTimeRecordData(CompletedLevelData completedLevelData) => 
      completedLevelData.TimeRecord = m_levelProgress.LevelTimer.TimeElapsed;

    private void ShowNewTimeRecordText(CompletedLevelData completedLevelData)
    {
      NewTimeRecordText.UpdateView(completedLevelData.TimeRecord.ToMinutesAndSeconds(), 
        m_levelProgress.LevelTimer.TimeElapsed.ToMinutesAndSeconds());

      NewTimeRecordText.gameObject.SetActive(true);
    }

    private void ShowTimeText()
    {
      TimeText.UpdateView(m_levelProgress.LevelTimer.TimeElapsed.ToMinutesAndSeconds());
      TimeText.gameObject.SetActive(true);
    }
  }
}