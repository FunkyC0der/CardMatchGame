using PrimeTween;
using UnityEngine;

namespace CardMatchGame.Gameplay.UI.PopUps
{
  public class ShowPopUpAnim : MonoBehaviour
  {
    public RectTransform AnimWindow;
    public TweenSettings<Vector2> AnimSettings;

    private void Start() => 
      Tween.UIAnchoredPosition(AnimWindow, AnimSettings);
  }
}