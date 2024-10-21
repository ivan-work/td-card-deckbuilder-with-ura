using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "Card/BuildTowerCard")]
public class BuildTowerCard : Card {
  [SerializeField] GameObject towerPrefab;

  public override void doCardAction(GridSystem gridSystem, Vector2Int gridPos) {
    var entities = gridSystem.getGridEntities(gridPos);

    bool isNewTower = true;

    foreach (var entity in entities) {
      BuildTowerComponent buildTowerComponent = entity.GetComponent<BuildTowerComponent>();
      Debug.Log($"buildTowerComponent: {buildTowerComponent}");
      if (buildTowerComponent) {
        isNewTower = false;
        buildTowerComponent.makeProgress();
      }
    }

    if (isNewTower) {
      var tower = Instantiate(towerPrefab, gridSystem.grid.transform);
      tower.GetComponent<GridComponent>().moveTo(gridPos);
      tower.GetComponent<TowerComponent>().enabled = false;
      tower.GetComponent<BuildTowerComponent>().enabled = true;
    }
  }

  public override bool isValidTarget(GridSystem gridSystem, Vector2Int gridPos) {
    var entities = gridSystem.getGridEntities(gridPos);

    var hasEmptyCell = entities.Any(entity => entity.GetComponent<CellPrefab>()?.cellType == CellType.Empty);
    var buildComponentEnabled = entities.Any(entity => entity.GetComponent<BuildTowerComponent>()?.enabled == true);
    var buildComponentNotExist = entities.All(entity => !entity.GetComponent<BuildTowerComponent>());

    return hasEmptyCell && (buildComponentEnabled || buildComponentNotExist);
  }
}