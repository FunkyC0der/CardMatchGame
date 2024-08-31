using UnityEditor;
using UnityEngine;

namespace CardMatchGame.Editor
{
  public static class Tools
  {
    [MenuItem(".Tools/Clear Player Prefs Saves")]
    public static void ClearPlayerPrefsSaves()
    {
      PlayerPrefs.DeleteAll();
      PlayerPrefs.Save();
    }
  }
}