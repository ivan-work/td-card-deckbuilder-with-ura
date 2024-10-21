using TMPro;
using UnityEngine;

public class TowerPrefab : MonoBehaviour {
  private GridComponent gridComponent;
  private TowerComponent towerComponent;
  private LineRenderer lineRenderer;
  Vector3 attachmentPoint = Vector3.zero;

  void Awake() {
    attachmentPoint = transform.Find("ShootAttachment").transform.position;
    gridComponent = GetComponent<GridComponent>();
    towerComponent = GetComponentInChildren<TowerComponent>();
    lineRenderer = GetComponentInChildren<LineRenderer>();
    lineRenderer.SetPosition(0, attachmentPoint);
    lineRenderer.enabled = false;
  }

  void Update() {
    if (towerComponent.targetPos.HasValue) {
      Vector3 targetPosition = gridComponent.gridPos2World(towerComponent.targetPos.Value);
      lineRenderer.enabled = true;
      lineRenderer.SetPosition(1, targetPosition - transform.position);
      // transform.LookAt(transform.InverseTransformPoint(targetPosition), Vector3.back);
      // transform.up = targetPosition - transform.position;
      // lineRenderer.SetPosition(1, Vector3.down);
    } else {
      transform.up = Vector3.up;
      // transform.rotation = Quaternion.identity;
      // transform.rotation = Quaternion.identity;
      lineRenderer.enabled = false;
    }
  }
}