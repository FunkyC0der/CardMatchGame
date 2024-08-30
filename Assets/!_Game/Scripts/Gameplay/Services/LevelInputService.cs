using System;
using CardMatchGame.Gameplay.Cards;
using UnityEngine;

namespace CardMatchGame.Gameplay.Services
{
  public class LevelInputService : MonoBehaviour
  {
    public event Action<bool> OnEnabledChanged;
    public Action<Card> OnCardSelected;

    private void OnEnable() => 
      OnEnabledChanged?.Invoke(true);

    private void OnDisable() => 
      OnEnabledChanged?.Invoke(false);

    private void Update()
    {
      if (!Input.GetMouseButtonDown(0))
        return;

      Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
      RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
      if (!hit.transform)
        return;

      var card = hit.transform.GetComponent<Card>();
      if(card)
        OnCardSelected?.Invoke(card);
    }
  }
}