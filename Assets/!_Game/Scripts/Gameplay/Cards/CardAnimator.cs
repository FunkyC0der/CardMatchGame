using PrimeTween;
using UnityEngine;

namespace CardMatchGame.Gameplay.Cards
{
  public class CardAnimator : MonoBehaviour
  {
    public TweenSettings<Vector3> FlipToFrontSideSettings;
    public ShakeSettings ShakeSettings;
    public ShakeSettings PunchSettings;

    public Tween PlayFlipToFrontAnim() =>
      PlayFlipAnim(true);

    public Tween PlayFlipToBackAnim() =>
      PlayFlipAnim(false);

    public Tween PlayFlipAnim(bool toFront) =>
      Tween.Rotation(transform, FlipToFrontSideSettings.WithDirection(toFront));

    public Tween PlayMatchSuccessAnim() =>
      Tween.PunchScale(transform, PunchSettings);

    public Tween PlayMatchFailedAnim() => 
      Tween.ShakeLocalRotation(transform, ShakeSettings);
  }
}