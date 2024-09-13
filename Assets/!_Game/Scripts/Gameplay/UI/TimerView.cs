using System;
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

    private TimerService m_timerService;

    private Tween m_endTimeAnim;

    [Inject]
    private void Construct(TimerService timerService) => 
      m_timerService = timerService;

    private void Start() => 
      UpdateTextView();

    private void Update()
    {
      if (m_timerService.IsActive)
      {
        UpdateTextView();

        if(m_timerService.TimeLeft < EndTimeAnimThreshold && !m_endTimeAnim.isAlive)
          m_endTimeAnim = Tween.Color(Text, EndTimeAnimSettings);
      }
      else if (m_endTimeAnim.isAlive)
        m_endTimeAnim.Complete();
    }

    private void UpdateTextView() => 
      Text.text = m_timerService.TimeLeft.ToMinutesAndSeconds();
  }
}