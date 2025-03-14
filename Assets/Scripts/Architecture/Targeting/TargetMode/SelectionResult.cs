using UnityEngine;

namespace Architecture.Targeting.TargetMode {
  public struct SelectionResult {
    public bool IsValid;
    public Vector2Int[] AffectedCells;
  }
}
