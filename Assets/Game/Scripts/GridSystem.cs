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

  public IEnumerable<T> getGridEntitiesSpecial<T>(Vector2Int gridPos) {
    return getGridEntities(gridPos)
      .SelectMany(entity => entity.GetComponents<T>())
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


  public Vector3 gridPos2World(Vector2Int vector, float? y = null) {
    var worldPosition = grid.GetCellCenterWorld(new Vector3Int(vector.x, vector.y));
    // worldPosition.z = z ?? gameObject.transform.position.z;
    return worldPosition;
  }

  /*
   * HEX
   */
  public static IEnumerable<Vector2Int> GetLine(Vector2Int startPos, Vector2Int endPos) {
    var startAxial = OffsetToAxial(startPos);
    var endAxial = OffsetToAxial(endPos);
    var points = getAxialDistance(startAxial, endAxial);
    var results = new List<Vector2Int> {startPos};
    for (var i = 1; i <= points; i++) {
      var lerped = Vector2.Lerp(startAxial, endAxial, (float) (1.0 / points * i));
      var lerpedRound = Vector2Int.RoundToInt(lerped);
      var offsetLerpedRound = AxialToOffset(lerpedRound);
      results.Add(offsetLerpedRound);
    }

    return results;
  }

  public static Vector2Int AxialToOffset(Vector2Int hex) {
    var col = hex.x + (hex.y - (hex.y & 1)) / 2;
    var row = hex.y;
    return new Vector2Int(col, row);
  }

  public static Vector2Int OffsetToAxial(Vector2Int hex) {
    var q = hex.x - (hex.y - (hex.y & 1)) / 2;
    var r = hex.y;
    return new Vector2Int(q, r);
  }

  public static int getOffsetDistance(Vector2Int a, Vector2Int b) {
    return getAxialDistance(OffsetToAxial(a), OffsetToAxial(b));
  }


  public static Vector2Int getAxialSubstract(Vector2Int a, Vector2Int b) {
    return new Vector2Int(a.x - b.x, a.y - b.y);
  }

  public static int getAxialDistance(Vector2Int a, Vector2Int b) {
    var vec = getAxialSubstract(a, b);
    return (Math.Abs(vec.x) + Math.Abs(vec.x + vec.y) + Math.Abs(vec.y)) / 2;
  }
}
