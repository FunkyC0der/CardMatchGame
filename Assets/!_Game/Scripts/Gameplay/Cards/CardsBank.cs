using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace CardMatchGame.Gameplay.Cards
{
  [CreateAssetMenu(fileName = "CardsBank", menuName = "Game/CardsBank")]
  public class CardsBank : ScriptableObject
  {
    [ContextMenuItem("Collect All Cards", "CollectAllCards")]
    public CardDesc[] Cards;

    public List<CardDesc> SelectRandomCards(int count)
    {
      List<CardDesc> cards = new();
      if (count > Cards.Length)
      {
        Debug.LogError("Cards bank is to small");
        return cards;
      }
      
      cards = Cards.ToList();
      while(cards.Count > count)
        cards.RemoveAt(Random.Range(0, cards.Count));

      return cards;
    }

#if UNITY_EDITOR
    private void CollectAllCards()
    {
      string[] guids = AssetDatabase.FindAssets($"t:{nameof(CardDesc)}");
      Cards = new CardDesc[guids.Length];
      
      for (int i = 0; i < guids.Length; ++i)
      {
        string path = AssetDatabase.GUIDToAssetPath(guids[i]);
        Cards[i] = AssetDatabase.LoadAssetAtPath<CardDesc>(path);
      }
      
      EditorUtility.SetDirty(this);
      AssetDatabase.SaveAssetIfDirty(this);
    }
#endif
  }
}