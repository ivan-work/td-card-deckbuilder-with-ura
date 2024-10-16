using System;
using TMPro;
using UnityEngine;

public class MobPrefab : MonoBehaviour {
  HealthComponent healthComponent;
  GridComponent gridComponent;

  void Awake() {
    healthComponent = GetComponent<HealthComponent>();
    gridComponent = GetComponent<GridComponent>();
  }

  private void OnEnable() {
    EventManager.PhaseMobAction.AddListener(OnMobAction);
  }

  private void OnDisable() {
    EventManager.PhaseMobAction.RemoveListener(OnMobAction);
  }

  void OnMobAction() {
    gridComponent.moveTo(gridComponent.gridPos + Vector2Int.right);
  }

  void Update() {
    if (healthComponent) {
      GetComponentInChildren<TextMeshPro>().text = $"HP: {healthComponent.currentHp}";
    }
  }
}