using System.Collections.Generic;
using System.Linq;
using Components;
using GridSystem;
using UnityEngine;

// Возможно, должен объединиться с GridComponent 
public class PathComponent : MonoBehaviour {
  public float distanceToBase = float.PositiveInfinity;
  public float moveCost = 1;

  public IEnumerable<PathComponent> getNeighbors() {
    var gridSystem = GetComponent<GridComponent>().GridSystem;
    var gridPos = GetComponent<GridComponent>().GridLoc;

    return gridSystem.getNeighbors4(gridPos)
      .Select(x => x.GetComponent<PathComponent>())
      .Where(x => x);
  }
}
