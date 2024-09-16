using System;
using System.Collections.Generic;
using CardMatchGame.MainMenu;
using UnityEngine;

namespace CardMatchGame.Services.UI
{
  [CreateAssetMenu(fileName = "UIAssets", menuName = "Game/UIAssets", order = 0)]
  public class UIAssetsData : ScriptableObject
  {
    public GameObject UIRootPrefab;
    public LevelItemView LevelItemPrefab;

    [Space]
    [SerializeField]
    private WindowData[] m_windows;

    private readonly Dictionary<WindowType, GameObject> m_windowsDictionary = new();

    public void Init()
    {
      foreach (WindowData window in m_windows) 
        m_windowsDictionary[window.Type] = window.Prefab;
    }

    public GameObject GetWindowPrefab(WindowType type) =>
      m_windowsDictionary[type];
  }

  [Serializable]
  public class WindowData
  {
    public WindowType Type;
    public GameObject Prefab;
  }
}