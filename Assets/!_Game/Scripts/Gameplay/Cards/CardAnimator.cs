using PrimeTween;
using UnityEngine;
using UnityEngine.Serialization;

namespace CardMatchGame.Gameplay.Cards
{
  public class CardAnimator : MonoBehaviour
  {
    public Card Card;
    
    public TweenSettings<Vector3> FlipToFrontSideSettings;
    public ShakeSettings ShakeSettings;
    public ShakeSettings PunchSettings;

    public Sequence PlayFlipAnim()
    {
      return Sequence.Create()
        .Chain(Tween.Rotation(transform, FlipToFrontSideSettings.WithDirection(!Card.IsFrontSide)))
        .ChainCallback(Card.Flip);
    }

    public Tween PlayMatchSuccessAnim() =>
      Tween.PunchScale(transform, PunchSettings);

    public Tween PlayMatchFailedAnim() => 
      Tween.ShakeLocalRotation(transform, ShakeSettings);
  }
}