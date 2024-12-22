using System.Collections.Generic;
using System.Linq;
using Effects;
using UnityEngine;

namespace Cards {
  [CreateAssetMenu(menuName = "Card/TestCard")]
  public class TestCard : Card {
    [SerializeField] public int damage;

    public override IEnumerable<BaseEffect> doCardAction(GridSystem gridSystem, Vector2Int[] gridPoses) {
      return gridPoses.Select(gridPos => new DamageEffect(gridPos, DamageType.Fire, damage));
    }
  }
}
