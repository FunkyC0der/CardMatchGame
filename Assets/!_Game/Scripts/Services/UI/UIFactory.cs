using CardMatchGame.MainMenu;
using CardMatchGame.Services.Assets;
using CardMatchGame.Utils;
using UnityEngine;
using Zenject;

namespace CardMatchGame.Services.UI
{
  public enum WindowType
  {
    MainMenu,
    HintPopUp,
    GameOverLosePopUp,
    GameOverWinPopUp,
    GameOverWinTimeRecordPopUp
  }
  
  public class UIFactory
  {
    private readonly DiContainer m_diContainer;
    private readonly UIAssetsData m_uiAssets;
    
    private Transform m_uiRoot;

    public UIFactory(DiContainer diContainer, IAssetsService assets)
    {
      m_diContainer = diContainer;
      m_uiAssets = assets.UIAssetsData();
    }

    public GameObject CreateWindow(WindowType type)
    {
      GameObject window = Object.Instantiate(m_uiAssets.GetWindowPrefab(type), UIRoot());
      m_diContainer.InjectGameObject(window);
      return window;
    }

    public GameObject CreateWindow<TPayload>(WindowType type, TPayload payload)
    {
      GameObject window = CreateWindow(type);
      window.GetComponent<IPayloaded<TPayload>>().Payload(payload);
      return window;
    }

    public T Create<T>(T prefab, Transform parent = null) where T : MonoBehaviour
    {
      if (parent == null)
        parent = UIRoot();
      
      T element = Object.Instantiate(prefab, parent);
      m_diContainer.InjectGameObject(element.gameObject);
      return element;
    }

    private Transform UIRoot()
    {
      if (m_uiRoot == null)
        m_uiRoot = Object.Instantiate(m_uiAssets.UIRootPrefab).transform;

      return m_uiRoot;
    }
  }
}