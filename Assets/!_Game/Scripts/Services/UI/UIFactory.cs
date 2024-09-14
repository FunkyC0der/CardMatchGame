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
    GameOverPopUp
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
      window.GetComponentInChildren<IPayloaded<TPayload>>().Payload(payload);
      return window;
    }

    private Transform UIRoot()
    {
      if (m_uiRoot == null)
        m_uiRoot = Object.Instantiate(m_uiAssets.UIRootPrefab).transform;

      return m_uiRoot;
    }
  }
}