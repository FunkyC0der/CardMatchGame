using System;
using UnityEngine;

namespace CardMatchGame.Gameplay.Utils
{
  [Serializable]
  public class Cooldown
  {
    public float Duration;

    private float m_endTime;
    private float m_pausedTimeLeft;

    public float TimeLeft => IsPaused ? m_pausedTimeLeft : (m_endTime - Time.time);
    public bool IsTicking => IsPaused || m_endTime > Time.time;
    public bool IsDone => Time.time > m_endTime;
    public bool IsPaused => m_pausedTimeLeft > 0;

    public void Activate() =>
      m_endTime = Time.time + Duration;

    public void Pause() =>
      m_pausedTimeLeft = TimeLeft;

    public void Resume()
    {
      m_endTime = Time.time + m_pausedTimeLeft;
      m_pausedTimeLeft = 0;
    }
  }
}