using UnityEngine;

[CreateAssetMenu(menuName = "Card/BuildTowerCard")]
public class BuildTowerCard : Card {
  [SerializeField] GameObject towerPrefab;

  override public void doCardAction(GridSystem gridSystem, Vector2Int gridPos) {
    var tower = Instantiate(towerPrefab, gridSystem.grid.transform );
    tower.GetComponent<GridComponent>().moveTo(gridPos);
  }
  
  override public bool isValidTarget(GridSystem gridSystem, Vector2Int gridPos) {
    var entities = gridSystem.getGridEntities(gridPos);

    foreach (var entity in entities) {
      if (entity.GetComponent<CellPrefab>()?.cellType == CellType.EMPTY) {
        return true;
      }
    }
    return false;
  }
}