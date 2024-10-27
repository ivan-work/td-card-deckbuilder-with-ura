using System;
using System.Collections;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

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
    var sourcePos = gridComponent.gridPos;
    var targetPos = findPath();

    if (targetPos.HasValue) {
      yield return CorouTweens.LerpWithSpeed(
        gridComponent.gridPos2World(sourcePos),
        gridComponent.gridPos2World(targetPos.Value),
        2,
        (value) => transform.position = value
      );
      
      gridComponent.moveTo(targetPos.Value);
    }
  }

  private Vector2Int? findPath() {
    var mobCell = gridComponent.gridSystem.getGridEntities(gridComponent.gridPos)
      .Select(entity => entity.GetComponent<PathComponent>())
      .First(entity => entity);
    if (mobCell != null) {
      var minimumNeighbor = mobCell.getNeighbors()
        .Aggregate((prev, next) => {
          if (Math.Abs(prev.distanceToBase - next.distanceToBase) < 0.1) {
            return Random.value < 0.5f ? prev : next;
          }

          return prev.distanceToBase < next.distanceToBase ? prev : next;
        });
      
      if (minimumNeighbor != null) {
        return minimumNeighbor.GetComponent<GridComponent>().gridPos;
      }
    }

    return null;
  }

  void Update() {
    if (healthComponent) {
      GetComponentInChildren<TextMeshPro>().text = $"HP: {healthComponent.currentHp}";
    }
  }
}