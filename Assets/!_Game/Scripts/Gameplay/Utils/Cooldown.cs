using System;
using UnityEngine;

namespace CardMatchGame.Gameplay.Utils
{
  [Serializable]
  public class Cooldown
  {
    public event Action OnReady; 
    
    public float Duration;

    private float m_timeElapsed;
    
    public float TimeLeft => Duration - m_timeElapsed;
    public float TimeElapsed => m_timeElapsed;
    public bool IsReady => !IsTicking;
    public bool IsTicking => TimeLeft > 0;

    public void Init(float duration)
    {
      Duration = duration;
      m_timeElapsed = Duration;
    }

    public void Activate() => 
      m_timeElapsed = 0;

    public void Stop() =>
      m_timeElapsed = Duration;

    public void Update()
    {
      if (IsReady)
        return;
      
      m_timeElapsed += Time.deltaTime;

      if (m_timeElapsed > Duration)
      {
        m_timeElapsed = Duration;
        OnReady?.Invoke();
      }
    }
  }
}