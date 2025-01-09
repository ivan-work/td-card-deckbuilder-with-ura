using System;
using System.Collections.Generic;
using System.Linq;
using Components;
using Effects;
using Status;
using UnityEngine;
using UnityEngine.Serialization;

namespace Cards {
  [CreateAssetMenu(menuName = "Card/BurnCard")]
  public class BurnCard : Card {
    [SerializeField] public int damage;

    public override IEnumerable<BaseEffect> doCardAction(GridSystem gridSystem, Vector2Int[] gridPoses) {
      return gridPoses.Select(gridPos => new DamageEffect(gridPos, DamageType.Fire, damage));
      
    }
  }
}
