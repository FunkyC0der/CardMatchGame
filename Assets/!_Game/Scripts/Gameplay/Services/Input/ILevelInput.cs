using System;
using CardMatchGame.Gameplay.Cards;

namespace CardMatchGame.Gameplay.Services.Input
{
  public interface ILevelInput
  {
    event Action<Card> OnCardSelected;
  }
}