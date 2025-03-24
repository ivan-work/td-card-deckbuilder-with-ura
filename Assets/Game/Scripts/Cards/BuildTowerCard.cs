using UnityEngine;

[CreateAssetMenu(menuName = "Card/BuildTowerCard")]
public class BuildTowerCard : Card {
  [SerializeField] GameObject towerPrefab;

  // public override IEnumerable<BaseEffect> doCardAction(GridSystem gridSystem, Vector2Int[] gridPoses) {
  //   foreach (var gridLoc in gridPoses) {
  //     var entities = gridSystem.GetGridEntities(gridLoc);
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
  //       tower.GetComponent<GridComponent>().moveTo(gridLoc);
  //       tower.GetComponent<TowerComponent>().enabled = false;
  //       tower.GetComponent<BuildTowerComponent>().enabled = true;
  //     }
  //   }
  //
  //   return new List<BaseEffect>();
  // }
}
