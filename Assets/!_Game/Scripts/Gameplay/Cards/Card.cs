using UnityEngine;

namespace CardMatchGame.Gameplay.Cards
{
  [RequireComponent(typeof(Collider2D))]
  public class Card : MonoBehaviour
  {
    public CardDesc Desc;

    public SpriteRenderer FrontRenderer;
    public SpriteRenderer BackRenderer;

    public Collider2D InteractionCollider;
    public CardAnimator Animator;

    public bool IsFrontSideVisible
    {
      get => FrontRenderer.gameObject.activeSelf;
      set
      {
        FrontRenderer.gameObject.SetActive(value);
        BackRenderer.gameObject.SetActive(!value);
      }
    }
    
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
      IsFrontSideVisible = !IsFrontSideVisible;
  }
}