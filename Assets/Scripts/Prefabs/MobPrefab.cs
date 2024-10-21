using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class MobPrefab : MonoBehaviour {
  HealthComponent healthComponent;
  GridComponent gridComponent;

  void Awake() {
    healthComponent = GetComponent<HealthComponent>();
    gridComponent = GetComponent<GridComponent>();
    EventManager.PhaseMove.AddListener(OnMove);
  }

  private void OnDestroy() {
    Debug.Log("MobPrefab.OnDestroy");
    EventManager.PhaseMove.RemoveListener(OnMove);
  }

  private IEnumerator OnMove() {
    yield return CorouTweens.LerpWithSpeed(
      gridComponent.gridPos2World(gridComponent.gridPos),
      gridComponent.gridPos2World(gridComponent.gridPos + Vector2Int.right),
      2,
      (value) => transform.position = value
    );
    gridComponent.moveTo(gridComponent.gridPos + Vector2Int.right);
  }

  void Update() {
    if (healthComponent) {
      GetComponentInChildren<TextMeshPro>().text = $"HP: {healthComponent.currentHp}";
    }
  }
}