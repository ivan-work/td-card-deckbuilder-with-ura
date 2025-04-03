using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GridSystem {
  [RequireComponent(typeof(Grid))]
  public class GridSystem : MonoBehaviour {
    [NonSerialized] private Grid grid = null!;
    [NonSerialized] private readonly Dictionary<Vector2Int, HashSet<GridComponent>> entities = new();

    private static readonly Vector2Int[] OffsetsForCross = { new(0, 1), new(1, 0), new(0, -1), new(-1, 0) };

    private void Awake() {
      grid = this.GetAssertComponent<Grid>();
    }

    public void Register(GridComponent gridComponent) {
      if (!entities.ContainsKey(gridComponent.GridLoc)) {
        entities.Add(gridComponent.GridLoc, new HashSet<GridComponent>());
      }

      entities[gridComponent.GridLoc].Add(gridComponent);
    }

    public void Unregister(GridComponent gridComponent) {
      if (entities.ContainsKey(gridComponent.GridLoc)) {
        entities[gridComponent.GridLoc].Remove(gridComponent);
      }
      // Debug.Log($"Unregister@{GridLoc}: {entities[GridLoc]}");
    }

    public IEnumerable<GridComponent> GetGridEntities(Vector2Int gridPos) {
      return entities.ContainsKey(gridPos) ? entities[gridPos] : Enumerable.Empty<GridComponent>();
    }

    public IEnumerable<T> GetGridEntities<T>(Vector2Int gridPos) {
      return GetGridEntities(gridPos)
        .SelectMany(entity => entity.GetComponents<T>());
    }

    public HashSet<GridComponent> getNeighbors4(Vector2Int gridPos) {
      var results = new HashSet<GridComponent>();

      foreach (var offset in OffsetsForCross) {
        var gridComponents = GetGridEntities(gridPos + offset);

        results = results.Concat(gridComponents).ToHashSet();
      }

      return results;
    }


    public Vector3 GridLoc2World(Vector2Int vector, float? y = null) {
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
      var points = GetAxialDistance(startAxial, endAxial);
      var results = new List<Vector2Int> { startPos };
      for (var i = 1; i <= points; i++) {
        var lerped = Vector2.Lerp(startAxial, endAxial, (float) (1.0 / points * i));
        var lerpedRound = Vector2Int.RoundToInt(lerped + new Vector2(.01f, -.01f));
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

    public static int GetOffsetDistance(Vector2Int a, Vector2Int b) {
      return GetAxialDistance(OffsetToAxial(a), OffsetToAxial(b));
    }


    public static Vector2Int GetAxialSubstract(Vector2Int a, Vector2Int b) {
      return new Vector2Int(a.x - b.x, a.y - b.y);
    }

    public static int GetAxialDistance(Vector2Int a, Vector2Int b) {
      var vec = GetAxialSubstract(a, b);
      return (Math.Abs(vec.x) + Math.Abs(vec.x + vec.y) + Math.Abs(vec.y)) / 2;
    }
  }
}
