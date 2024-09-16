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

    public GameObject Create(GameObject prefab, Transform parent = null)
    {
      if (parent == null)
        parent = UIRoot();

      GameObject gameObject = Object.Instantiate(prefab, parent);
      m_diContainer.InjectGameObject(gameObject);
      return gameObject;
    }

    public T Create<T>(T prefab, Transform parent = null) where T : MonoBehaviour
    {
      if (parent == null)
        parent = UIRoot();
      
      T element = Object.Instantiate(prefab, parent);
      m_diContainer.InjectGameObject(element.gameObject);
      return element;
    }

    public GameObject CreateWindow(WindowType type) => 
      Create(m_uiAssets.GetWindowPrefab(type));

    public GameObject CreateWindow<TPayload>(WindowType type, TPayload payload)
    {
      GameObject window = CreateWindow(type);
      window.GetComponent<IPayloaded<TPayload>>().Payload(payload);
      return window;
    }

    public GameObject CreateLevelHUD() =>
      Create(m_uiAssets.LevelHUDPrefab);

    private Transform UIRoot()
    {
      if (m_uiRoot == null)
        m_uiRoot = Object.Instantiate(m_uiAssets.UIRootPrefab).transform;

      return m_uiRoot;
    }
  }
}