using System;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof (Grid))]
public class GridSystem : MonoBehaviour {
  Dictionary<Vector2Int, List<GridComponent>> entities = new();
  [NonSerialized] public Grid grid;

  private void Awake() {
    grid = GetComponent<Grid>();
  }

  public void moveTo(GridComponent GridComponent, Vector2Int newGridPos) {
    unregister(GridComponent, GridComponent.gridPos);
    GridComponent.gridPos = newGridPos;
    register(GridComponent, GridComponent.gridPos);
  }

  public void register(GridComponent GridComponent, Vector2Int gridPos) {
    if (!entities.ContainsKey(gridPos)) {
      entities.Add(gridPos, new List<GridComponent>());
    }
    entities[gridPos].Add(GridComponent);
  }

  public void unregister(GridComponent GridComponent, Vector2Int gridPos) {
    if (entities.ContainsKey(gridPos)) {
      entities[gridPos].Remove(GridComponent);
    }
  }

  public List<GridComponent> getGridEntities(Vector2Int gridPos) {
    if (entities.ContainsKey(gridPos)) {
      return new List<GridComponent>(entities[gridPos]);
    }
    return new List<GridComponent>();
  }
}