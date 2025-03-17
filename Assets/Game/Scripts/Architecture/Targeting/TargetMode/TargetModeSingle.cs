using UnityEngine;

namespace Architecture.Targeting.TargetMode {
  public class TargetModeSingle : AbstractTargetMode {
    public TargetModeSingle(Card card) : base(card) { }

    private readonly GameObject cellIndicator = CellIndicatorObjectPool.SharedInstance.getPooledObject();

    public override SelectionResult drawIndicator(
      GridSystem gridSystem,
      Vector2Int mouseCell,
      AbstractTargetCondition condition
    ) {
      bool isValid = condition.isValidTarget(gridSystem, mouseCell);

      cellIndicator.GetComponent<SpriteRenderer>().color = isValid ? Color.green : Color.red;
      cellIndicator.transform.position = gridSystem.gridPos2World(mouseCell); //grid.GetCellCenterWorld(new Vector3Int(mouseCell.x, mouseCell.y, 0)));

      return new SelectionResult() {
        IsValid = isValid,
        AffectedCells = new[] {mouseCell}
      };
    }

    public override bool onClick(GridSystem gridSystem, SelectionResult selectionResult) {
      return true;
    }
  }
}
