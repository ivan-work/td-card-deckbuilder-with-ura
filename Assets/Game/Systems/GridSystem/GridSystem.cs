using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GridSystem {
  public class GridSystem : MonoBehaviour {
    [NonSerialized] private readonly Dictionary<Vector2Int, HashSet<GridComponent>> entities = new();
    public HexGridDriver GridDriver { get; } = new HexGridDriver(.5f);

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

    public IEnumerable<GridComponent> GetGridEntities(Vector2Int gridLoc) {
      return entities.ContainsKey(gridLoc) ? entities[gridLoc] : Enumerable.Empty<GridComponent>();
    }

    public IEnumerable<T> GetGridEntities<T>(Vector2Int gridLoc) {
      return GetGridEntities(gridLoc)
        .SelectMany(entity => entity.GetComponents<T>());
    }

    public IEnumerable<GridComponent> GetNeighbors(Vector2Int gridLoc) {
      return HexGridDriver.GetNeighbors(gridLoc)
        .SelectMany(GetGridEntities);
    }


    public Vector3 GridLoc2World(Vector2Int vector) {
      return GridDriver.GridLoc2World(vector);
    }

    /*
   * HEX
   */
    public static IEnumerable<Vector2Int> GetLine(Vector2Int startLoc, Vector2Int endLoc) {
      var points = GetDistance(startLoc, endLoc);
      var results = new List<Vector2Int> { startLoc };
      for (var i = 1; i <= points; i++) {
        var lerped = Vector2.Lerp(startLoc, endLoc, (float) (1.0 / points * i));
        var lerpedRound = Vector2Int.RoundToInt(lerped + new Vector2(.01f, -.01f));
        results.Add(lerpedRound);
      }

      return results;
    }

    public static int GetDistance(Vector2Int a, Vector2Int b) {
      var vec = a - b;
      return (Math.Abs(vec.x) + Math.Abs(vec.x + vec.y) + Math.Abs(vec.y)) / 2;
    }
  }
}
