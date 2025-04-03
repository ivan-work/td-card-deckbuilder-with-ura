using UnityEngine;

[CreateAssetMenu(menuName = "Card/BuildTowerCard")]
public class BuildTowerCard : Card {
  [SerializeField] GameObject towerPrefab;

  // public override IEnumerable<BaseEffect> doCardAction(GridSystem GridSystem, Vector2Int[] gridPoses) {
  //   foreach (var GridLoc in gridPoses) {
  //     var entities = GridSystem.GetGridEntities(GridLoc);
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
  //       var tower = Instantiate(towerPrefab, GridSystem.grid.transform);
  //       tower.GetComponent<GridComponent>().MoveTo(GridLoc);
  //       tower.GetComponent<TowerComponent>().enabled = false;
  //       tower.GetComponent<BuildTowerComponent>().enabled = true;
  //     }
  //   }
  //
  //   return new List<BaseEffect>();
  // }
}
