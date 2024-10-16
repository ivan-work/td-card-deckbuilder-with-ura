using UnityEngine;

[CreateAssetMenu(menuName = "Card/DealDamageCard")]
public class DealDamageCard : Card {
  [SerializeField] public int damage;

  override public void onTargetClicked(GridSystem gridSystem, Vector2Int gridPos) {
    var entities = gridSystem.getGridEntities(gridPos);
    foreach (var entity in entities) {
      HealthComponent entityHealth = entity.GetComponent<HealthComponent>();
      if (entityHealth) {
        entityHealth.OnDamage(damage);
      }
    }
  }

  override public bool isValidTarget(GridSystem gridSystem, Vector2Int gridPos) {
    var entities = gridSystem.getGridEntities(gridPos);
    foreach (var entity in entities) {
      HealthComponent entityHealth = entity.GetComponent<HealthComponent>();
      if (entityHealth) {
        return true;
      }
    }
    return false;
  }
}