using CardMatchGame.Services;
using UnityEngine;
using Zenject;

namespace CardMatchGame.Utils
{
  public class LoadingCurtainAutoHider : MonoBehaviour
  {
    private LoadingCurtain m_loadingCurtain;

    [Inject]
    private void Construct(LoadingCurtain loadingCurtain) =>
      m_loadingCurtain = loadingCurtain;

    private void Start() => 
      m_loadingCurtain.Hide();
  }
}