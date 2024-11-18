  using System.Linq;
  using UnityEngine;

  public class TowerTargetCondition: AbstractTargetCondition {
    public override bool isValidTarget(GridSystem gridSystem, Vector2Int gridPos) {
      var entities = gridSystem.getGridEntities(gridPos);
    
      var hasEmptyCell = entities.Any(entity => entity.GetComponent<CellPrefab>()?.cellType == CellType.Empty);
      var buildComponentEnabled = entities.Any(entity => entity.GetComponent<BuildTowerComponent>()?.enabled == true);
      var buildComponentNotExist = entities.All(entity => !entity.GetComponent<BuildTowerComponent>());
    
      return hasEmptyCell && (buildComponentEnabled || buildComponentNotExist);
    }
  }
