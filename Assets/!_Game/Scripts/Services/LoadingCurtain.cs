using PrimeTween;
using UnityEngine;

namespace CardMatchGame.Services
{
  public class LoadingCurtain : MonoBehaviour
  {
    public CanvasGroup CanvasGroup;
    public TweenSettings<float> HideAlphaAnimSettings;

    private Tween m_hideAnim;

    public void Show()
    {
      if(m_hideAnim.isAlive)
        m_hideAnim.Stop();
      
      gameObject.SetActive(true);
      CanvasGroup.alpha = 1;
    }

    public void Hide()
    {
       m_hideAnim = Tween.Alpha(CanvasGroup, HideAlphaAnimSettings)
        .OnComplete(() => gameObject.SetActive(false));
    }
  }
}