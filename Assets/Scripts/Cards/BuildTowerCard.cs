using UnityEngine;

[CreateAssetMenu(menuName = "Card/BuildTowerCard")]
public class BuildTowerCard : Card {
  [SerializeField] GameObject towerPrefab;

  public override void doCardAction(GridSystem gridSystem, Vector2Int gridPos) {
    var tower = Instantiate(towerPrefab, gridSystem.grid.transform );
    tower.GetComponent<GridComponent>().moveTo(gridPos);
  }
  
  public override bool isValidTarget(GridSystem gridSystem, Vector2Int gridPos) {
    var entities = gridSystem.getGridEntities(gridPos);

    foreach (var entity in entities) {
      if (entity.GetComponent<CellPrefab>()?.cellType == CellType.Empty) {
        return true;
      }
    }
    return false;
  }
}