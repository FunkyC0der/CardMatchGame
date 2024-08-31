using System;
using System.Text;

namespace CardMatchGame.Services.Serialization
{
  public class Base64Serializer : ISerializer
  {
    private readonly JsonSerializer m_jsonSerializer = new();
    
    public string Serialize<T>(T data) => 
      Convert.ToBase64String(Encoding.Default.GetBytes(m_jsonSerializer.Serialize(data)));

    public T Deserialize<T>(string rawData) => 
      m_jsonSerializer.Deserialize<T>(Encoding.Default.GetString(Convert.FromBase64String(rawData)));
  }
}