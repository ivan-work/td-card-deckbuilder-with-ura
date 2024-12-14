using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Card/BuildTowerCard")]
public class BuildTowerCard : Card {
  [SerializeField] GameObject towerPrefab;

  public override IEnumerable<BaseEffect> doCardAction(GridSystem gridSystem, Vector2Int[] gridPoses) {
    foreach (var gridPos in gridPoses) {
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

    return new List<BaseEffect>();
  }
}