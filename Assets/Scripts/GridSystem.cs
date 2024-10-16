using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (Grid))]
public class GridSystem : MonoBehaviour {
  readonly Dictionary<Vector2Int, List<GridComponent>> entities = new();
  [NonSerialized] public Grid grid;

  private void Awake() {
    grid = GetComponent<Grid>();
  }

  public void moveTo(GridComponent gridComponent, Vector2Int newGridPos) {
    unregister(gridComponent, gridComponent.gridPos);
    gridComponent.gridPos = newGridPos;
    register(gridComponent, gridComponent.gridPos);
  }

  public void register(GridComponent gridComponent, Vector2Int gridPos) {
    if (!entities.ContainsKey(gridPos)) {
      entities.Add(gridPos, new List<GridComponent>());
    }
    entities[gridPos].Add(gridComponent);
  }

  public void unregister(GridComponent gridComponent, Vector2Int gridPos) {
    if (entities.ContainsKey(gridPos)) {
      entities[gridPos].Remove(gridComponent);
    }
    // Debug.Log($"Unregister@{gridPos}: {entities[gridPos]}");
  }

  public List<GridComponent> getGridEntities(Vector2Int gridPos) {
    if (entities.ContainsKey(gridPos)) {
      return new List<GridComponent>(entities[gridPos]);
    }
    return new List<GridComponent>();
  }
}