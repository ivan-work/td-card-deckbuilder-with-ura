using System;
using System.Collections.Generic;
using UnityEngine;
using Debug = System.Diagnostics.Debug;

// [CreateAssetMenu(menuName = "Architecture/Targeting/TargetMode/TargetModeDouble")]
namespace Architecture.Targeting.TargetMode {
  public class TargetModeLine : AbstractTargetMode {
    public TargetModeLine(Card card) : base(card) { }

    private Vector2Int? startCell;

    private GameObject startCellIndicator = CellIndicatorObjectPool.SharedInstance.getPooledObject();
    private List<GameObject> lineIndicators = new();
    private List<Vector2Int> affectedCells = new();

    public override bool onClick(GridSystem gridSystem, SelectionResult selectionResult) {
      if (!startCell.HasValue) {
        startCell = selectionResult.AffectedCells[0];

        return false;
      }

      return true;
    }

    private GameObject getNextIndicator() {
      var indicator = CellIndicatorObjectPool.SharedInstance.getPooledObject();

      lineIndicators.Add(indicator);

      return indicator;
    }

    private void resetIndicators() {
      lineIndicators.ForEach(indicator => indicator.SetActive(false));
      lineIndicators.Clear();
      affectedCells.Clear();
    }

    public override SelectionResult drawIndicator(GridSystem gridSystem, Vector2Int mouseCell,
      AbstractTargetCondition condition) {
      if (!startCell.HasValue) {
        var isValid = condition.isValidTarget(gridSystem, mouseCell);
        startCellIndicator.GetComponent<SpriteRenderer>().color = isValid ? Color.green : Color.red;
        startCellIndicator.transform.position =
          gridSystem.grid.GetCellCenterWorld(new Vector3Int(mouseCell.x, mouseCell.y, -10));

        return new SelectionResult() {
          IsValid = isValid,
          AffectedCells = new[] {mouseCell}
        };
      }

      startCellIndicator.GetComponent<SpriteRenderer>().color = Color.blue;
      var distanceToMouse = mouseCell - startCell.Value;

      resetIndicators();

      var vectorComponentLong = Math.Abs(distanceToMouse.x) > Math.Abs(distanceToMouse.y) ? 0 : 1;
      var vectorComponentShort = vectorComponentLong == 0 ? 1 : 0;
      var direction = distanceToMouse[vectorComponentLong] >= 0 ? 1 : -1;
      // var distanceAbsLong = Math.Abs(distanceToMouse[vectorComponentLong]);
      const int distanceAbsLong = 5;

      Debug.Assert(startCell != null, nameof(startCell) + " != null");
      for (var i = 0; i <= distanceAbsLong; ++i) {
        Debug.Assert(startCell != null, nameof(startCell) + " != null");
        var cellPos = new Vector2Int {
          [vectorComponentLong] = startCell.Value[vectorComponentLong] + direction * i,
          [vectorComponentShort] = startCell.Value[vectorComponentShort]
        };
        var cellIsValid = condition.isValidTarget(gridSystem, cellPos);
        if (!cellIsValid) break;
        affectedCells.Add(cellPos);

        var indicator = getNextIndicator();
        indicator.transform.position = gridSystem.grid.GetCellCenterWorld(new Vector3Int(cellPos.x, cellPos.y, -10));
      }

      var isValidLine = affectedCells.Count > 1;
      lineIndicators.ForEach(indicator =>
        indicator.GetComponent<SpriteRenderer>().color = isValidLine ? Color.blue : Color.red);

      return new SelectionResult() {
        IsValid = isValidLine,
        AffectedCells = affectedCells.ToArray()
      };
    }
  }
}
