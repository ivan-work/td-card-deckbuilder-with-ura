using UnityEngine;

namespace Components {
  public class GridComponent : MonoBehaviour {
    [SerializeField] public GridSystem gridSystem;

    [SerializeField] public Vector2Int gridPos = Vector2Int.zero;

    private void Awake() {
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
      return gridSystem.gridPos2World(vector);
    }
  }
}
