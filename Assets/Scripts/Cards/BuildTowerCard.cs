using UnityEngine;

[CreateAssetMenu(menuName = "Card/BuildTowerCard")]
public class BuildTowerCard : Card {
  [SerializeField] GameObject towerPrefab;

  override public void onTargetClicked(GridSystem gridSystem, Vector2Int gridPos) {
    var tower = Instantiate(towerPrefab, gridSystem.grid.GetCellCenterWorld((Vector3Int)gridPos), 
      Quaternion.identity, 
      gridSystem.grid.transform
      );
  }
}