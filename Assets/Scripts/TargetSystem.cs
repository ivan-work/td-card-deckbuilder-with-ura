using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSystem : MonoBehaviour {
  [SerializeField] private GameObject mouseIndicator;
  [SerializeField] private InputManager inputManager;
  [SerializeField] private GridSystem gridSystem;

  [SerializeField] private GameObject towerPrefab;
  bool enabled = false;

  void Start() {
    StopTargeting();
  }

  public void OnCardClicked() {
    StartTargeting();
  }

  private void Update() {
    if (enabled) {
      mouseIndicator.transform.position = gridSystem.grid.GetCellCenterWorld((Vector3Int)GetMouseCell());
    }

    if (enabled && Input.GetMouseButtonDown(0)) {
      StopTargeting();
      // var tower = Instantiate(towerPrefab, gridSystem.grid.GetCellCenterWorld(GetMouseCell()), Quaternion.identity, gridSystem.grid.transform);
      DealDamage(GetMouseCell(), 3);
    }
  }

  void DealDamage(Vector2Int gridPos, int damage) {
    var mobs = gridSystem.getGridEntities(gridPos);
    foreach (var mob in mobs) {
      HealthComponent mobHealth = mob.GetComponent<HealthComponent>();
      if (mobHealth) {
        mobHealth.OnDamage(damage);
      }
    }
  }

  Vector2Int GetMouseCell() {
    // Debug.Log($"{Input.mousePosition}, {Camera.main.ScreenToWorldPoint(Input.mousePosition, 0)}, {Camera.main.ScreenToWorldPoint(Input.mousePosition)}");
    Vector3Int cellPosition = gridSystem.grid.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    return ((Vector2Int)cellPosition);
  }

  void StartTargeting() {
    enabled = true;
    mouseIndicator.SetActive(true);
  }

  void StopTargeting() {
    enabled = false;
    mouseIndicator.SetActive(false);
  }
}
