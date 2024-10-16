using UnityEngine;

[CreateAssetMenu(menuName = "Card/DealDamageCard")]
public class DealDamageCard : Card {
  [SerializeField] public int damage;

  public override void doCardAction(GridSystem gridSystem, Vector2Int gridPos) {
    DealDamageComponent.dealDamage(gridSystem, gridPos, damage);
  }

  public override bool isValidTarget(GridSystem gridSystem, Vector2Int gridPos) {
    return DealDamageComponent.isValidTarget(gridSystem, gridPos);
  }
}