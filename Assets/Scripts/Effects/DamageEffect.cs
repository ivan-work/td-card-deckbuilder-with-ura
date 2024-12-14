using System.Linq;
using UnityEngine;

public class DamageEffect : BaseEffect {
  private readonly GridSystem gridSystem;
  private readonly Vector2Int gridPos;
  private readonly int damage;
  
  public DamageEffect(GridSystem gridSystem, Vector2Int gridPos, int damage) {
    this.gridSystem = gridSystem;
    this.gridPos = gridPos;
    this.damage = damage;
  }

  public override void start() {
    gridSystem.getGridEntitiesSpecial<HealthComponent>(gridPos)
      .ToList()
      .ForEach((entityHealth) => {
        entityHealth.OnDamage(damage);
      });
  }

  public override string ToString() {
    return $"DamageEffect({gridPos})";
  }
}