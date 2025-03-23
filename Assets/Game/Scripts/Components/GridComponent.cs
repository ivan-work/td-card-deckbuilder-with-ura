using UnityEngine;
using UnityEngine.Serialization;

namespace Components {
  public class GridComponent : MonoBehaviour {
    public enum zLayerEnum {
      Effect = -5,
      Mob = 3,
      Entity = 4,
      Ground = 5,
    }

    [SerializeField] public GridSystem gridSystem;

    [FormerlySerializedAs("gridPos")] [SerializeField] public Vector2Int gridLoc = Vector2Int.zero;
    [SerializeField] public zLayerEnum zLayer = 0;

    private void Awake() {
      gridSystem = this.GetAssertComponentInParent<GridSystem>();
    }

    private void Start() {
      moveTo(gridLoc);
      gameObject.transform.position = gridPos2World();
    }

    private void OnDestroy() {
      gridSystem.unregister(this, gridLoc);
    }

    public void moveTo(Vector2Int targetPos) {
      gridSystem.moveTo(this, targetPos);
    }

    private Vector3 gridPos2World() {
      return gridSystem.gridPos2World(gridLoc, (float) zLayer);
    }
  }
}
