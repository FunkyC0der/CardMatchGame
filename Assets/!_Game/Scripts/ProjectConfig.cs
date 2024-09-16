using CardMatchGame.Services.SaveLoad;
using CardMatchGame.Services.Serialization;
using UnityEngine;

namespace CardMatchGame
{
  [CreateAssetMenu(fileName = "ProjectConfig", menuName = "Game/ProjectConfig")]
  public class ProjectConfig : ScriptableObject
  {
    public SerializerType SerializerType;
    public SaveLoadType SaveLoadType;
  }
}