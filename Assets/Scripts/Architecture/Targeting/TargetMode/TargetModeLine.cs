using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// [CreateAssetMenu(menuName = "Architecture/Targeting/TargetMode/TargetModeDouble")]
public class TargetModeLine : AbstractTargetMode {
  public TargetModeLine(Card card) : base(card) { }

  private Vector2Int? startCell;

  private GameObject startCellIndicator = CellIndicatorObjectPool.SharedInstance.GetPooledObject();
  private List<GameObject> lineIndicators = new();

  public override bool onClick(GridSystem gridSystem, SelectionResult selectionResult) {
    if (!startCell.HasValue) {
      startCell = selectionResult.affectedCells[0];

      return false;
    }

    var secondCell = selectionResult.affectedCells[0];

    // Определить все клетки между firstCell и secondCell и нанести там дмж.
    var lineCells = new[] {startCell.Value, secondCell};

    // card.doCardAction(
    //   gridSystem,
    //   lineCells
    // );

    return false;
  }

  private GameObject getNextIndicator() {
    var indicator = CellIndicatorObjectPool.SharedInstance.GetPooledObject();
    
    lineIndicators.Add(indicator);
    
    return indicator;
  }

  private void resetIndicators() {
    lineIndicators.ForEach(indicator => indicator.SetActive(false));
    lineIndicators.Clear();
  }

  public override SelectionResult drawIndicator(GridSystem gridSystem, Vector2Int mouseCell,
    AbstractTargetCondition condition) {
    var isValid = condition.isValidTarget(gridSystem, mouseCell);

    if (!startCell.HasValue) {
      startCellIndicator.GetComponent<SpriteRenderer>().color = isValid ? Color.green : Color.red;
      startCellIndicator.transform.position = gridSystem.grid.GetCellCenterWorld(new Vector3Int(mouseCell.x, mouseCell.y, -10));

      return new SelectionResult() {
        isValid = isValid,
        affectedCells = new[] {mouseCell}
      };
    }

    startCellIndicator.GetComponent<SpriteRenderer>().color = Color.blue;
    var distanceToStart = mouseCell - startCell.Value;
    
    resetIndicators();
    
    if (Math.Abs(distanceToStart.x) > Math.Abs(distanceToStart.y)) {
      var movingRight = distanceToStart.x > 0;

      if (movingRight) {
        for (var i = startCell.Value.x; i <= mouseCell.x; i++) {
          GameObject indicator = getNextIndicator();
          indicator.GetComponent<SpriteRenderer>().color = Color.blue;
          indicator.transform.position = gridSystem.grid.GetCellCenterWorld(new Vector3Int(i, startCell.Value.y, -10));
        }
      } else {
        for (var i = startCell.Value.x; i >= mouseCell.x; i--) {
          GameObject indicator = getNextIndicator();
          indicator.GetComponent<SpriteRenderer>().color = Color.blue;
          indicator.transform.position = gridSystem.grid.GetCellCenterWorld(new Vector3Int(i, startCell.Value.y, -10));
        }
      }
    } else {
      var movingUp = distanceToStart.y > 0;

      if (movingUp) {
        for (var i = startCell.Value.y; i <= mouseCell.y; i++) {
          GameObject indicator = getNextIndicator();
          indicator.GetComponent<SpriteRenderer>().color = Color.blue;
          indicator.transform.position = gridSystem.grid.GetCellCenterWorld(new Vector3Int(startCell.Value.x, i, -10));
        }
      } else {
        for (var i = startCell.Value.y; i >= mouseCell.y; i--) {
          GameObject indicator = getNextIndicator();
          indicator.GetComponent<SpriteRenderer>().color = Color.blue;
          indicator.transform.position = gridSystem.grid.GetCellCenterWorld(new Vector3Int(startCell.Value.x, i, -10));
        }
      }
    }
    
    return new SelectionResult() {
      isValid = isValid,
      affectedCells = new[] {mouseCell}
    };
    
    // var vectorComponentLong = Math.Abs(distanceToStart.x) > Math.Abs(distanceToStart.y) ? 0 : 1;
    // var vectorComponentShort = Math.Abs(distanceToStart.x) > Math.Abs(distanceToStart.y) ? 1 : 0;
    //
    // for (var i = startCell.Value[vectorComponentLong]; I)
  }
}