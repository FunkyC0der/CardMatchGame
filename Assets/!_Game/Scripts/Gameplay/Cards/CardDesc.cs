using UnityEngine;

namespace CardMatchGame.Gameplay.Cards
{
  [CreateAssetMenu(fileName = "_CardDesc", menuName = "Game/CardDesc")]
  public class CardDesc : ScriptableObject
  {
    public Sprite FrontSprite;
  }
}