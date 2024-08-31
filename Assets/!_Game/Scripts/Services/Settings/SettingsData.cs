using System;

namespace CardMatchGame.Services.Settings
{
  // It's sample settings
  // In real life we will add settings which are specific for concrete game
  [Serializable]
  public class SettingsData
  {
    public ELanguage Language = ELanguage.Ukrainian;
    public float SoundVolume;
    public bool BlindingMode;
  }

  public enum ELanguage
  {
    English,
    Ukrainian,
    COUNT
  }
}