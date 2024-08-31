using CardMatchGame.Services.Progress;
using CardMatchGame.Services.Serialization;
using CardMatchGame.Services.Settings;
using UnityEngine;

namespace CardMatchGame.Services.SaveLoad
{
  public class PlayerPrefsSaveLoadService : ISaveLoadService
  {
    private const string m_kProgressDataKey = "ProgressData";
    private const string m_kSettingsDataKey = "SettingsData";

    private readonly ISerializer m_serializer;

    public PlayerPrefsSaveLoadService(ISerializer serializer) => 
      m_serializer = serializer;

    public void Save(ProgressData data) =>
      SaveToPrefs(data, m_kProgressDataKey);

    public ProgressData LoadProgressData() =>
      LoadFromPrefs<ProgressData>(m_kProgressDataKey);

    public void Save(SettingsData data) => 
      SaveToPrefs(data, m_kSettingsDataKey);

    public SettingsData LoadSettingsData() => 
      LoadFromPrefs<SettingsData>(m_kSettingsDataKey);

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