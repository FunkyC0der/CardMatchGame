using System.IO;
using CardMatchGame.Services.Progress;
using CardMatchGame.Services.Serialization;
using UnityEngine;

namespace CardMatchGame.Services.SaveLoad
{
  public class FileSaveLoadService : ISaveLoadService
  {
    private const string m_kSavesDirectoryName = "GameSaves";
    private static string m_sSavesDirectoryPath;
    
    private const string m_kProgressDataFileName = "ProgressData";

    private readonly ISerializer m_serializer;
    
    public FileSaveLoadService(ISerializer serializer) => 
      m_serializer = serializer;

    public void Save(ProgressData data) =>
      SaveToFile(data, m_kProgressDataFileName);

    public ProgressData LoadProgressData() =>
      ReadFromFile<ProgressData>(m_kProgressDataFileName);

    private void SaveToFile<T>(T data, string fileName)
    {
      string filePath = Path.Combine(SavesDirectoryPath(), fileName);
      File.WriteAllText(filePath, m_serializer.Serialize(data));
    }

    private T ReadFromFile<T>(string fileName) where T : class
    {
      string filePath = Path.Combine(SavesDirectoryPath(), fileName);
      return File.Exists(filePath)
        ? m_serializer.Deserialize<T>(File.ReadAllText(filePath))
        : null;
    }
    
    private static string SavesDirectoryPath()
    {
      if (m_sSavesDirectoryPath == null)
        InitSavesDirectoryPath();
      
      return m_sSavesDirectoryPath;
    }

    private static void InitSavesDirectoryPath()
    {
      m_sSavesDirectoryPath = CreateSavesDirectoryPath();

      if (!Directory.Exists(m_sSavesDirectoryPath))
        Directory.CreateDirectory(m_sSavesDirectoryPath);
    }

    public static string CreateSavesDirectoryPath()
    {
      string dataPath = Application.isEditor ? Directory.GetCurrentDirectory() : Application.persistentDataPath;
      return Path.Combine(dataPath, m_kSavesDirectoryName);
    }
  }
}