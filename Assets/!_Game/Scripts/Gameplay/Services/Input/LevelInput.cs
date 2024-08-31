using System;
using CardMatchGame.Gameplay.Cards;
using UnityEngine;

namespace CardMatchGame.Gameplay.Services.Input
{
  public class LevelInput : MonoBehaviour, ILevelInput
  {
    public event Action<bool> OnEnabledChanged;
    public event Action<Card> OnCardSelected;

    public bool IsEnabled => enabled;

    public void SetEnabled(bool value)
    {
      enabled = value;
      OnEnabledChanged?.Invoke(value);
    }

    private void Update()
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