using System.Collections;
using CardMatchGame.Gameplay.Services;
using CardMatchGame.Gameplay.UI.Utils;
using CardMatchGame.Services.Levels;
using PrimeTween;
using UnityEngine;
using Zenject;

namespace CardMatchGame.Gameplay.UI
{
  public class StartLevelHintView : MonoBehaviour
  {
    public PrintfText HintText;
    
    public RectTransform AnimWindow;
    public CanvasGroup AnimCanvasGroup;

    public float ShowDuration = 2;
    public TweenSettings<Vector2> ShowPositionAnimSettings;
    public TweenSettings<float> ShowAlphaAnimSettings;

    private ILevelsService m_levelsService;
    
    [Inject]
    private void Construct(ILevelsService levelsService, LevelProgress levelProgress)
    {
      m_levelsService = levelsService;
      levelProgress.StartLevelHintView = this;
    }

    public IEnumerator ShowHint()
    {
      gameObject.SetActive(true);
      HintText.UpdateView(m_levelsService.CurrentLevelData.CardsCountToMatch);
      
      return Sequence.Create()
        .Chain(Tween.UIAnchoredPosition(AnimWindow, ShowPositionAnimSettings))
        .Group(Tween.Alpha(AnimCanvasGroup, ShowAlphaAnimSettings))
        .ChainDelay(ShowDuration)
        .Chain(Tween.Alpha(AnimCanvasGroup, ShowAlphaAnimSettings.WithDirection(false)))
        .ChainCallback(() => gameObject.SetActive(false))
        .ToYieldInstruction();
    }
  }
}