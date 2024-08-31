using System;
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
    public PrintfText BestTimeText;

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
      BestTimeText.gameObject.SetActive(false);
      TimeText.gameObject.SetActive(false);
    }

    private void SetupLevelCompletedView()
    {
      LevelCompletedText.gameObject.SetActive(true);
        
      CompletedLevelData completedLevelData = m_progressService.FindCompletedLevelData(m_levelsService.LevelIndex);
      if (completedLevelData == null)
      {
        completedLevelData = new CompletedLevelData()
        {
          Index = m_levelsService.LevelIndex,
          BestTime = m_levelProgress.LevelTimer.TimeElapsed
        };

        m_progressService.AddCompletedLevelData(completedLevelData);
      }
      
      if (completedLevelData.BestTime > m_levelProgress.LevelTimer.TimeElapsed)
      {
        BestTimeText.UpdateView(completedLevelData.BestTime.ToMinutesAndSeconds(), 
          m_levelProgress.LevelTimer.TimeElapsed.ToMinutesAndSeconds());
        
        BestTimeText.gameObject.SetActive(true);
      }
      else
      {
        TimeText.UpdateView(m_levelProgress.LevelTimer.TimeElapsed.ToMinutesAndSeconds());
        TimeText.gameObject.SetActive(true);
      }
    }
  }
}