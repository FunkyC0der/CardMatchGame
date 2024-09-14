using System;
using System.Collections;
using CardMatchGame.Gameplay.UI.Utils;
using CardMatchGame.Services.Levels;
using CardMatchGame.Utils;
using PrimeTween;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;
using Sequence = PrimeTween.Sequence;

namespace CardMatchGame.Gameplay.UI
{
  public class StartLevelHintView : MonoBehaviour, IPayloaded<CoroutineWait>
  {
    public PrintfText HintText;
    
    public RectTransform AnimWindow;
    public CanvasGroup AnimCanvasGroup;

    public float ShowDuration = 2;
    public TweenSettings<Vector2> ShowPositionAnimSettings;
    public TweenSettings<float> ShowAlphaAnimSettings;

    private CoroutineWait m_wait;

    private ILevelsService m_levelsService;
    
    [Inject]
    private void Construct(ILevelsService levelsService) => 
      m_levelsService = levelsService;

    public void Payload(CoroutineWait wait) => 
      m_wait = wait;

    private void Start()
    {
      HintText.UpdateView(m_levelsService.CurrentLevelData.CardsCountToMatch);

      Sequence.Create()
        .Chain(Tween.UIAnchoredPosition(AnimWindow, ShowPositionAnimSettings))
        .Group(Tween.Alpha(AnimCanvasGroup, ShowAlphaAnimSettings))
        .ChainDelay(ShowDuration)
        .Chain(Tween.Alpha(AnimCanvasGroup, ShowAlphaAnimSettings.WithDirection(false)))
        .ChainCallback(() => gameObject.SetActive(false))
        .OnComplete(() => m_wait.WaitDone());
    }
  }
}