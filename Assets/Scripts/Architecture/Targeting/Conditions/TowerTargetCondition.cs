  using System.Linq;
  using UnityEngine;

  [CreateAssetMenu(menuName = "Architecture/Targeting/Condition/TowerTargetCondition")]
  public class TowerTargetCondition: AbstractTargetCondition {
    public override bool isValidTarget(GridSystem gridSystem, Vector2Int gridPos) {
      var entities = gridSystem.getGridEntities(gridPos).ToList();
    
      var hasEmptyCell = entities.Any(entity => entity.GetComponent<CellPrefab>()?.cellType == CellType.Empty);
      var buildComponentEnabled = entities.Any(entity => entity.GetComponent<BuildTowerComponent>()?.enabled == true);
      var buildComponentNotExist = entities.All(entity => !entity.GetComponent<BuildTowerComponent>());
    
      return hasEmptyCell && (buildComponentEnabled || buildComponentNotExist);
    }
  }
