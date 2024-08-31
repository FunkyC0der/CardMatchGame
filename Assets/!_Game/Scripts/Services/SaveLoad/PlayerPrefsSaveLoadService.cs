using CardMatchGame.Services.Progress;
using CardMatchGame.Services.Serialization;
using UnityEngine;

namespace CardMatchGame.Services.SaveLoad
{
  public class PlayerPrefsSaveLoadService : ISaveLoadService
  {
    private const string m_kProgressDataKey = "ProgressData";

    private readonly ISerializer m_serializer;

    public PlayerPrefsSaveLoadService(ISerializer serializer) => 
      m_serializer = serializer;

    public void Save(ProgressData data) =>
      SaveToPrefs(data, m_kProgressDataKey);

    public ProgressData LoadProgressData() =>
      LoadFromPrefs<ProgressData>(m_kProgressDataKey);

    private void SaveToPrefs<T>(T data, string key)
    {
      PlayerPrefs.SetString(key, m_serializer.Serialize(data));
      PlayerPrefs.Save();
    }

    private T LoadFromPrefs<T>(string key) where T : class =>
      PlayerPrefs.HasKey(key)
        ? m_serializer.Deserialize<T>(PlayerPrefs.GetString(key))
        : null;
  }
}