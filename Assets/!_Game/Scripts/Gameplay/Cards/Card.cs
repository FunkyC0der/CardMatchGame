using UnityEngine;

namespace CardMatchGame.Gameplay.Cards
{
  public class Card : MonoBehaviour
  {
    public CardDesc Desc;

    public SpriteRenderer FrontRenderer;
    public Collider2D InteractionCollider;
    public CardAnimator Animator;

    public bool IsMatched;
    public bool IsFrontSide;
    public bool IsSelected;
    
    public bool Selectable
    {
      get => InteractionCollider.enabled;
      set => InteractionCollider.enabled = value;
    }

    private void Awake() => 
      SetDesc(Desc);

    public void SetDesc(CardDesc desc)
    {
      Desc = desc;
      FrontRenderer.sprite = desc.FrontSprite;
    }

    public void Flip() => 
      IsFrontSide = !IsFrontSide;
  }
}