using UnityEngine;

[CreateAssetMenu(menuName = "Card/DealDamageCard")]
public class DealDamageCard : Card {
  [SerializeField] public int damage;
  readonly DealDamageComponent dealDamageComponent = new();

  override public void doCardAction(GridSystem gridSystem, Vector2Int gridPos) {
    dealDamageComponent.dealDamage(gridSystem, gridPos, 3);
  }

  override public bool isValidTarget(GridSystem gridSystem, Vector2Int gridPos) {
    return dealDamageComponent.isValidTarget(gridSystem, gridPos);
  }
}