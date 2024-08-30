using CardMatchGame.Gameplay.Services;
using PrimeTween;
using TMPro;
using UnityEngine;
using Zenject;

namespace CardMatchGame.Gameplay.UI
{
  public class TimerView : MonoBehaviour
  {
    public TextMeshProUGUI Text;

    public float EndTimeAnimThreshold = 5;
    public TweenSettings<Color> EndTimeAnimSettings;

    private LevelProgress m_levelProgress;

    private Tween m_endTimeAnim;

    [Inject]
    private void Construct(LevelProgress levelProgress)
    {
      m_levelProgress = levelProgress;
      m_levelProgress.OnGameOver += StopTimer;
    }

    private void Update()
    {
      if (m_levelProgress.LevelTimer.IsDone)
        return;
      
      Text.text = m_levelProgress.LevelTimer.TimeLeft.ToMinutesAndSeconds();
      
      if(m_levelProgress.LevelTimer.TimeLeft < EndTimeAnimThreshold && !m_endTimeAnim.isAlive)
        m_endTimeAnim = Tween.Color(Text, EndTimeAnimSettings);
    }

    private void StopTimer()
    {
      Text.text = 0f.ToMinutesAndSeconds();
      m_endTimeAnim.Stop();
    }
  }
}