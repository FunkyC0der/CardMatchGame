using UnityEngine;

namespace CardMatchGame.Services.Serialization
{
  public class JsonSerializer : ISerializer
  {
    public string Serialize<T>(T data) => 
      JsonUtility.ToJson(data);

    public T Deserialize<T>(string rawData) => 
      JsonUtility.FromJson<T>(rawData);
  }
}