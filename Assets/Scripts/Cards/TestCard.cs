using System;
using System.Collections.Generic;
using System.Linq;
using Components;
using Effects;
using Status;
using UnityEngine;

namespace Cards {
  [CreateAssetMenu(menuName = "Card/TestCard")]
  public class TestCard : Card {
    [SerializeField] public int count;

    public override IEnumerable<BaseEffect> doCardAction(GridSystem gridSystem, Vector2Int[] gridPoses) {
      return Enumerable.Empty<BaseEffect>();
      // return gridPoses
      //   .SelectMany(gridSystem.getGridEntitiesSpecial<StatusComponent>)
      //   .Select(component => new ApplyStatusEffect(component, new StatusStruct {
      //     stacks = count,
      //     data = ... // TODO GET DATA
      //   }));
    }
  }
}
