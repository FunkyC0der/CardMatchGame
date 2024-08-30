using UnityEngine;

namespace CardMatchGame.Gameplay.Services
{
  public class GridService : MonoBehaviour
  {
    public Vector2Int Size;
    public Vector2 CellSize;

    private Vector3 m_origin;

    public int CellsCount => Size.x * Size.y;

    private void Awake() => 
      m_origin = ComputeOrigin();

    public void SetSize(Vector2Int size)
    {
      Size = size;
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