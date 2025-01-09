using System;
using System.Collections.Generic;
using System.Linq;
using Components;
using Effects;
using Status;
using UnityEngine;

namespace Cards {
  [CreateAssetMenu(menuName = "Card/OilCard")]
  public class OilCard : Card {
    public override IEnumerable<BaseEffect> doCardAction(GridSystem gridSystem, Vector2Int[] gridPoses) {
      return gridPoses.SelectMany(gridPos => {
        return gridSystem.getGridEntitiesSpecial<HealthComponent>(gridPos);
        
      }).Select(healthComponent => new ApplyStatusEffect(healthComponent.gameObject, new OiledStatus()));
    }
  }
}
