using System.Collections;
using System.Collections.Generic;
using Components;
using Effects;
using Intents.Engine;
using UnityEngine;

[CreateAssetMenu(menuName = "Card/BuildTowerCard")]
public class BuildTowerCard : Card {
  [SerializeField] GameObject towerPrefab;

  public override void DoCardAction(IntentGlobalContext context, Vector2Int[] gridPoses) {
    throw new System.NotImplementedException();
  }

  // public override IEnumerable<BaseEffect> doCardAction(GridSystem gridSystem, Vector2Int[] gridPoses) {
  //   foreach (var gridPos in gridPoses) {
  //     var entities = gridSystem.getGridEntities(gridPos);
  //
  //     bool isNewTower = true;
  //
  //     foreach (var entity in entities) {
  //       BuildTowerComponent buildTowerComponent = entity.GetComponent<BuildTowerComponent>();
  //       Debug.Log($"buildTowerComponent: {buildTowerComponent}");
  //       if (buildTowerComponent) {
  //         isNewTower = false;
  //         buildTowerComponent.makeProgress();
  //       }
  //     }
  //
  //     if (isNewTower) {
  //       var tower = Instantiate(towerPrefab, gridSystem.grid.transform);
  //       tower.GetComponent<GridComponent>().moveTo(gridPos);
  //       tower.GetComponent<TowerComponent>().enabled = false;
  //       tower.GetComponent<BuildTowerComponent>().enabled = true;
  //     }
  //   }
  //
  //   return new List<BaseEffect>();
  // }
}
