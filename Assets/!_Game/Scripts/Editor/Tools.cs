using System.IO;
using CardMatchGame.Services.SaveLoad;
using UnityEditor;
using UnityEngine;

namespace CardMatchGame.Editor
{
  public static class Tools
  {
    [MenuItem(".Tools/Clear Saves")]
    public static void ClearSaves()
    {
      PlayerPrefs.DeleteAll();
      PlayerPrefs.Save();

      string savesFilePath = FileSaveLoadService.CreateSavesDirectoryPath();
      if(Directory.Exists(savesFilePath))
        Directory.Delete(FileSaveLoadService.CreateSavesDirectoryPath(), true);
    }
  }
}