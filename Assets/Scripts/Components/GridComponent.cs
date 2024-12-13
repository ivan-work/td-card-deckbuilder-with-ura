using Unity.VisualScripting;
using UnityEngine;

public class GridComponent : MonoBehaviour {
  [SerializeField] public GridSystem gridSystem;

  [SerializeField] public Vector2Int gridPos = Vector2Int.zero;

  private void Awake() {
    Debug.Log("GridComponent.Awake()");
    gridSystem = this.GetAssertComponentInParent<GridSystem>();
  }

  private void Start() {
    moveTo(gridPos);
    gameObject.transform.position = gridPos2World(gridPos);
  }

  private void OnDestroy() {
    Debug.Log("GridComponent.OnDestroy()");
    gridSystem.unregister(this, gridPos);
  }

  public void moveTo(Vector2Int targetPos) {
    gridSystem.moveTo(this, targetPos);
  }

  public Vector3 gridPos2World() {
    return gridPos2World(gridPos);
  }
  public Vector3 gridPos2World(Vector2Int vector) {
    Vector3 worldPosition = gridSystem.grid.GetCellCenterWorld(new Vector3Int(vector.x, vector.y));
    worldPosition.z = gameObject.transform.position.z;
    return worldPosition;
  }
}