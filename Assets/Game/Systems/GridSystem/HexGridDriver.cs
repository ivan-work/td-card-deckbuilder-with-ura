using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GridSystem {
  public class HexGridDriver {
    public float Size { get; init; }

    private static readonly Vector2Int[] NeighborOffsets = {
      new Vector2Int(0, -1), new Vector2Int(+1, -1), new Vector2Int(+1, 0), new Vector2Int(0, +1), new Vector2Int(-1, +1), new Vector2Int(-1, 0),
    };

    public HexGridDriver(float size) {
      Size = size;
    }

    public static IEnumerable<Vector2Int> GetNeighbors(Vector2Int gridLoc) {
      return NeighborOffsets.Select(offset => gridLoc + offset);
    }

    public Vector3 GridLoc2World(Vector2Int hex) {
      var x = Size * (1.5f * hex.x);
      var z = Size * (Mathf.Sqrt(3) / 2f * hex.x + Mathf.Sqrt(3) * hex.y);
      return new Vector3(x, 0, z);
    }
  }
}
