using System.Collections.Generic;
using System.Linq;
using Effects;
using UnityEngine;

[CreateAssetMenu(menuName = "Card/DealDamageCard")]
public class DealDamageCard : Card {
  [SerializeField] public int damage;

  public override IEnumerable<BaseEffect> doCardAction(GridSystem gridSystem, Vector2Int[] gridPoses) {
    return gridPoses.Select(gridPos => new DamageEffect(gridPos, DamageType.Slash, damage));
  }
}
