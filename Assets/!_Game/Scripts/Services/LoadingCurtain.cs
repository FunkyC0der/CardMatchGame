using PrimeTween;
using UnityEngine;

namespace CardMatchGame.Services
{
  public class LoadingCurtain : MonoBehaviour
  {
    public CanvasGroup CanvasGroup;
    public TweenSettings<float> HideAlphaAnimSettings;

    public void Show()
    {
      gameObject.SetActive(true);
      CanvasGroup.alpha = 1;
    }

    public void Hide()
    {
      Tween.Alpha(CanvasGroup, HideAlphaAnimSettings)
        .OnComplete(() => gameObject.SetActive(false));
    }
  }
}