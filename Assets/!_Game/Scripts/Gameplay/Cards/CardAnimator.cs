using PrimeTween;
using UnityEngine;

namespace CardMatchGame.Gameplay.Cards
{
  public class CardAnimator : MonoBehaviour
  {
    public Card Card;

    public TweenSettings<Vector3> FlipToFrontSideSettings;
    public ShakeSettings ShakeSettings;
    public ShakeSettings PunchSettings;

    public Sequence PlayFlipToFrontAnim() =>
      PlayFlipAnim(true);

    public Sequence PlayFlipToBackAnim() =>
      PlayFlipAnim(false);

    public Sequence PlayFlipAnim(bool toFront) =>
      Sequence.Create()
        .Chain(Tween.Rotation(transform, FlipToFrontSideSettings.WithDirection(toFront)))
        .ChainCallback(() => Card.IsFrontSide = toFront);

    public Tween PlayMatchSuccessAnim() =>
      Tween.PunchScale(transform, PunchSettings);

    public Tween PlayMatchFailedAnim() => 
      Tween.ShakeLocalRotation(transform, ShakeSettings);
  }
}