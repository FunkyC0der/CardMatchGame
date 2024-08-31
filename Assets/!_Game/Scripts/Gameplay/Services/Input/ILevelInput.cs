using System;
using CardMatchGame.Gameplay.Cards;

namespace CardMatchGame.Gameplay.Services.Input
{
  public interface ILevelInput
  {
    event Action<bool> OnEnabledChanged;
    event Action<Card> OnCardSelected;
    
    bool IsEnabled { get; }

    void SetEnabled(bool value);
  }
}