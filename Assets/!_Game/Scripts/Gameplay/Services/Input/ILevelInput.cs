using System;
using CardMatchGame.Gameplay.Cards;

namespace CardMatchGame.Gameplay.Services.Input
{
  public interface ILevelInput
  {
    event Action<bool> OnEnabledChanged;
    event Action<Card> OnCardSelected;
    
    bool Enabled { get; set; }
  }
}