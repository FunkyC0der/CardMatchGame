using CardMatchGame.Services.Progress;

namespace CardMatchGame.Services.Serialization
{
  public interface ISerializer
  {
    string Serialize<T>(T data);
    T Deserialize<T>(string rawData);
  }
}