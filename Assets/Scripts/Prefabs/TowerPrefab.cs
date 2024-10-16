using UnityEngine;

public class TowerPrefab : MonoBehaviour {
  private GridComponent gridComponent;
  private TowerComponent towerComponent;
  private LineRenderer lineRenderer;

  void Awake() {
    gridComponent = GetComponent<GridComponent>();
    towerComponent = GetComponentInChildren<TowerComponent>();
    lineRenderer = GetComponentInChildren<LineRenderer>();
    lineRenderer.SetPosition(0, Vector3.zero);
    lineRenderer.enabled = false;
  }
  
  void Update() {
    if (towerComponent.targetPos.HasValue) {
      lineRenderer.enabled = true;
      lineRenderer.SetPosition(1, gridComponent.gridPos2World(towerComponent.targetPos.Value) - transform.position);
      // lineRenderer.SetPosition(1, Vector3.down);
    } else {
      lineRenderer.enabled = false;
    }
  }
}