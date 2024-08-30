using System;
using UnityEngine;

namespace CardMatchGame.Gameplay.Utils
{
  [Serializable]
  public class Cooldown
  {
    public float TimeInSec;

    private float m_endTime;
    
    public float TimeLeft => m_endTime - Time.time;
    public bool IsTicking => TimeLeft > 0;
    public bool IsDone => !IsTicking;

    public void Activate() =>
      m_endTime = Time.time + TimeInSec;
  }
}