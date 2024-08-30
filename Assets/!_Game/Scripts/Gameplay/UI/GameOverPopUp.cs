using CardMatchGame.Gameplay.Services;
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

    public RectTransform AnimWindow;
    public TweenSettings<Vector2> ShowHideAnimSettings;

    private LevelProgress m_levelProgress;

    [Inject]
    private void Construct(LevelProgress levelProgress)
    {
      m_levelProgress = levelProgress;
      m_levelProgress.OnGameStart += Hide;
      m_levelProgress.OnGameOver += Show;
    }

    private void Show()
    {
      gameObject.SetActive(true);

      Tween.UIAnchoredPosition(AnimWindow, ShowHideAnimSettings);
      
      LevelCompletedText.gameObject.SetActive(m_levelProgress.IsLevelCompleted);
      LevelFailedText.gameObject.SetActive(!m_levelProgress.IsLevelCompleted);
    }

    private void Hide()
    {
      if (!gameObject.activeSelf)
        return;
      
      Sequence.Create()
        .Chain(Tween.UIAnchoredPosition(AnimWindow, ShowHideAnimSettings.WithDirection(false)))
        .ChainCallback(() => gameObject.SetActive(false));
    }
  }
}