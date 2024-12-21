using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Components;
using UnityEngine;

// Возможно, должен объединиться с GridComponent 
public class PathComponent : MonoBehaviour, ITargetableComponent {
  public float distanceToBase = float.PositiveInfinity;
  public float moveCost = 1;

  public IEnumerable<PathComponent> getNeighbors() {
    var gridSystem = GetComponent<GridComponent>().gridSystem;
    var gridPos = GetComponent<GridComponent>().gridPos;

    return gridSystem.getNeighbors4(gridPos)
      .Select(x => x.GetComponent<PathComponent>())
      .Where(x => x);
  }
}
