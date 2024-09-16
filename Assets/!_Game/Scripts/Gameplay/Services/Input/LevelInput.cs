using System;
using CardMatchGame.Gameplay.Cards;
using UnityEngine;
using Zenject;

namespace CardMatchGame.Gameplay.Services.Input
{
  public class LevelInput : ILevelInput, ITickable
  {
    public event Action<Card> OnCardSelected;

    public void Tick()
    {
      if (!UnityEngine.Input.GetMouseButtonDown(0))
        return;

      Ray ray = Camera.main.ScreenPointToRay(UnityEngine.Input.mousePosition);
      RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
      if (!hit.transform)
        return;

      var card = hit.transform.GetComponent<Card>();
      if(card)
        OnCardSelected?.Invoke(card);
    }
  }
}