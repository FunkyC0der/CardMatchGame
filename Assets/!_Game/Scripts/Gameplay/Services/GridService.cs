using System.Reflection;
using CardMatchGame.Services.Levels;
using UnityEngine;
using Zenject;

namespace CardMatchGame.Gameplay.Services
{
  public class GridService : MonoBehaviour, IInitializable
  {
    public Vector2Int Size;
    public Vector2 CellSize;

    private ILevelsService m_levelsService;
    
    private Vector3 m_origin;

    [Inject]
    private void Construct(ILevelsService levelsService) =>
      m_levelsService = levelsService;

    public int CellsCount => Size.x * Size.y;

    public void Initialize()
    {
      Debug.Log(MethodBase.GetCurrentMethod());

      Size = m_levelsService.LevelData.GridSize;
      m_origin = ComputeOrigin();
    }

    public Vector3 CellCenterPosition(int index) => 
      CellCenterPosition(new Vector2Int(index % Size.x, index / Size.x));

    public Vector3 CellCenterPosition(Vector2Int index)
    {
      Vector3 offset = CellSize * 0.5f;
      return CellPosition(index) + offset;
    }

    public Vector3 CellPosition(Vector2Int index)
    {
      index = Vector2Int.Min(index, Size - Vector2Int.one);
      
      Vector3 offset = CellSize * index;
      return m_origin + offset;
    }

    private Vector3 ComputeOrigin()
    {
      Vector3 offset = (CellSize * Size) * -0.5f;
      return transform.position + offset;
    }

    private void OnValidate()
    {
      if(CellsCount % 2 > 0)
        Debug.LogError("Grid cells count must even");
    }

    private void OnDrawGizmosSelected()
    {
      m_origin = ComputeOrigin();
      
      for (Vector2Int index = Vector2Int.zero; index.x < Size.x; ++index.x)
      {
        for (index.y = 0; index.y < Size.y; ++index.y)
        {
          Gizmos.DrawWireCube(CellCenterPosition(index), CellSize);
        }
      }
    }
  }
}