﻿using Effects;
using UnityEngine;

namespace Status.StatusData {
  [CreateAssetMenu(menuName = "Status/BleedStatusData")]
  public class BleedStatusData : BaseStatusData {
    public override void OnMove(StatusContext context) {
      //#TODO INTENT FIX
      // context.actorManager.addImmediateEffects(
      //   new DamageEffect(context.component.gridComponent.gridPos, DamageType.Internal, context.statusStruct.stacks)
      // );
      //
      // context.component.updateStatus(context.statusStruct, -1);
    }
  }
}
