using UnityEngine;

namespace Components {
  public class GridComponent : MonoBehaviour {
    public enum zLayerEnum {
      Ground = 5,
      Entity = 4,
      Mob = 3,
      
      
      
    }
    [SerializeField] public GridSystem gridSystem;

    [SerializeField] public Vector2Int gridPos = Vector2Int.zero;
    [SerializeField] public zLayerEnum zLayer = 0;

    private void Awake() {
      gridSystem = this.GetAssertComponentInParent<GridSystem>();
    }

    private void Start() {
      moveTo(gridPos);
      gameObject.transform.position = gridPos2World();
    }

    private void OnDestroy() {
      gridSystem.unregister(this, gridPos);
    }

    public void moveTo(Vector2Int targetPos) {
      gridSystem.moveTo(this, targetPos);
    }

    public Vector3 gridPos2World() {
      return gridSystem.gridPos2World(gridPos, (float)zLayer);
    }
  }
}
