using System;
using System.Collections.Generic;
using System.Linq;
using Components;
using UnityEngine;


[RequireComponent(typeof(Grid))]
public class GridSystem : MonoBehaviour {
  readonly Dictionary<Vector2Int, List<GridComponent>> entities = new();
  [NonSerialized] public Grid grid;

  private Vector2Int[] offsetsForCross = {new(0, 1), new(1, 0), new(0, -1), new(-1, 0)};

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

  public IEnumerable<GridComponent> getGridEntities(Vector2Int gridPos) {
    if (entities.ContainsKey(gridPos)) {
      return new List<GridComponent>(entities[gridPos]);
    }

    return new List<GridComponent>();
  }

  public IEnumerable<T> getGridEntitiesSpecial<T>(Vector2Int gridPos) where T : MonoBehaviour {
    return getGridEntities(gridPos)
      .Select(entity => entity.GetComponent<T>())
      .Where(entity => entity != null);
  }

  public IEnumerable<GridComponent> getNeighbors4(Vector2Int gridPos) {
    IEnumerable<GridComponent> results = new List<GridComponent>();

    foreach (var offset in offsetsForCross) {
      var gridComponents = getGridEntities(gridPos + offset);

      results = results.Concat(gridComponents);
    }

    return results;
  }
  

  public Vector3 gridPos2World(Vector2Int vector, float? z = null) {
    var worldPosition = grid.GetCellCenterWorld(new Vector3Int(vector.x, vector.y));
    worldPosition.z = z ?? gameObject.transform.position.z;
    return worldPosition;
  }
}
