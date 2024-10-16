using TMPro;
using UnityEngine;

public class MobPrefab : MonoBehaviour {
  HealthComponent healthComponent;
  GridComponent gridComponent;

  void Awake() {
    healthComponent = GetComponent<HealthComponent>();
    gridComponent = GetComponent<GridComponent>();
    EventManager.PhaseMobAction.AddListener(OnMobAction);
  }

  void OnMobAction() {
    gridComponent.moveTo(gridComponent.gridPos + Vector2Int.right);
    // gameObject.transform.position += Vector3.right;
  }

  void Update() {
    if (healthComponent) {
      GetComponentInChildren<TextMeshPro>().text = $"HP: {healthComponent.currentHp}";
    }
  }
}