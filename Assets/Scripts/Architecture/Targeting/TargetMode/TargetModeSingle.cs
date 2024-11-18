using UnityEngine;

// [CreateAssetMenu(menuName = "Architecture/Targeting/TargetMode/TargetModeSingle")]
public class TargetModeSingle : AbstractTargetMode {
  public TargetModeSingle(Card card) : base(card) { }

  private GameObject cellIndicator = CellIndicatorObjectPool.SharedInstance.getPooledObject();


  public override SelectionResult drawIndicator(GridSystem gridSystem, Vector2Int mouseCell,
    AbstractTargetCondition condition) {
    var isValid = condition.isValidTarget(gridSystem, mouseCell);

    cellIndicator.GetComponent<SpriteRenderer>().color = isValid ? Color.green : Color.red;
    cellIndicator.transform.position =
      gridSystem.grid.GetCellCenterWorld(new Vector3Int(mouseCell.x, mouseCell.y, -10));

    return new SelectionResult() {
      isValid = isValid,
      affectedCells = new[] {mouseCell}
    };
  }

  public override bool onClick(GridSystem gridSystem, SelectionResult selectionResult) {
    card.doCardAction(
      gridSystem,
      selectionResult.affectedCells
    );

    cellIndicator.SetActive(false);

    return true;
  }
}

