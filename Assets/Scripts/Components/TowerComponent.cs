using UnityEngine;

public class TowerComponent : MonoBehaviour {
  [SerializeField] int damage = 3;
  [SerializeField] int range = 3;
  readonly DealDamageComponent dealDamageComponent = new();

  public Vector2Int? targetPos = null;
  GridComponent gridComponent;

  void Start() {
    if (GetComponent<GridComponent>()) {
      gridComponent = GetComponent<GridComponent>();
    } else {
      throw new NoComponentException($"No component ${typeof(GridComponent)}");
    }
    EventManager.PhaseTowerAction.AddListener(OnTowerAction);
    EventManager.EndTurn.AddListener(OnEndTurn);
  }

  void OnTowerAction() {
    if (targetPos.HasValue) {
      dealDamageComponent.dealDamage(gridComponent.gridSystem, targetPos.Value, damage);
    }
  }

  void OnEndTurn() {
    targetPos = SearchNewTarget();
  }

  private Vector2Int? SearchNewTarget() {
    for (int x = -range; x < range; x++) {
      for (int y = -range; y < range; y++) {
        Vector2Int absolutePos = gridComponent.gridPos + new Vector2Int(x, y);
        if (isValidTarget(gridComponent.gridSystem, absolutePos)) {
          return absolutePos;
        }
      }
    }
    return null;
  }

  public bool isValidTarget(GridSystem gridSystem, Vector2Int gridPos) {
    return dealDamageComponent.isValidTarget(gridSystem, gridPos);
  }
}