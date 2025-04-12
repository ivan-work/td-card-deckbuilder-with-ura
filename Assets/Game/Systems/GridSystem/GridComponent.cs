using UnityEngine;

namespace GridSystem {
  public class GridComponent : MonoBehaviour {
    public GridSystem GridSystem { get; private set; } = null!;

    [SerializeField] private Vector2Int _gridLoc = Vector2Int.zero;
    public Vector2Int GridLoc => _gridLoc;

    private void Awake() {
      GridSystem = this.GetAssertComponentInParent<GridSystem>();
    }

    private void Start() {
      ensureLocalPosition();
    }

    private void OnEnable() {
      GridSystem.Register(this);
      
      if (didStart) {
        ensureLocalPosition();
      }
    }

    private void OnDisable() {
      GridSystem.Unregister(this);
    }

    public void MoveTo(Vector2Int targetPos) {
      GridSystem.Unregister(this);
      _gridLoc = targetPos;
      GridSystem.Register(this);
    }

    private void ensureLocalPosition() {
      gameObject.transform.localPosition = GridSystem.GridLoc2World(_gridLoc);
    }
  }
}
