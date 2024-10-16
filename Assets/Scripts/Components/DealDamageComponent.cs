using UnityEngine;

public class DealDamageComponent {
  public bool isValidTarget(GridSystem gridSystem, Vector2Int gridPos) {
    var entities = gridSystem.getGridEntities(gridPos);
    foreach (var entity in entities) {
      HealthComponent entityHealth = entity.GetComponent<HealthComponent>();
      if (entityHealth) {
        return true;
      }
    }
    return false;
  }

  public void dealDamage(GridSystem gridSystem, Vector2Int gridPos, int damage) {
    var entities = gridSystem.getGridEntities(gridPos);
    foreach (var entity in entities) {
      HealthComponent entityHealth = entity.GetComponent<HealthComponent>();
      if (entityHealth) {
        entityHealth.OnDamage(damage);
      }
    }
  }
}