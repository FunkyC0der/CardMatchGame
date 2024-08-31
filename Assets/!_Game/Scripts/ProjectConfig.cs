using CardMatchGame.Services.SaveLoad;
using CardMatchGame.Services.Serialization;
using UnityEngine;

namespace CardMatchGame
{
  [CreateAssetMenu(fileName = "ProjectConfig", menuName = "Game/ProjectConfig")]
  public class ProjectConfig : ScriptableObject
  {
    public ESerializerType SerializerType;
    public ESaveLoadType SaveLoadType;
  }
}