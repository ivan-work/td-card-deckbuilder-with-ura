using UnityEngine;

[CreateAssetMenu(menuName = "Card/DealDamageCard")]
public class DealDamageCard : Card {
  [SerializeField] public int damage;

  override public void onTargetClicked(GridSystem gridSystem, Vector2Int gridPos) {
    var mobs = gridSystem.getGridEntities(gridPos);
    foreach (var mob in mobs) {
      HealthComponent mobHealth = mob.GetComponent<HealthComponent>();
      if (mobHealth) {
        mobHealth.OnDamage(damage);
      }
    }
  }
}