using System;
using UnityEngine;

namespace CardMatchGame.Gameplay.Utils
{
  [Serializable]
  public class Cooldown
  {
    public float Duration;

    private float m_timeElapsed;
    
    public float TimeLeft => Duration - m_timeElapsed;
    public float TimeElapsed => m_timeElapsed;
    public bool IsReady => !IsTicking;
    public bool IsTicking => TimeLeft > 0;

    public void Activate(float duration)
    {
      Duration = duration;
      m_timeElapsed = 0;
    }

    public void Stop() =>
      m_timeElapsed = Duration;

    public void Update() => 
      m_timeElapsed += Time.deltaTime;
  }
}