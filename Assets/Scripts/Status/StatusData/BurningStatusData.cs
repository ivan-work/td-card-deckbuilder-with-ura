using Effects;
using UnityEngine;

namespace Status.StatusData {
  
  [CreateAssetMenu(menuName = "Status/BurningStatusData")]
  public class BurningStatusData: BaseStatusData {
    public override void OnEndTurn (StatusContext context) {
      context.actorManager.addImmediateEffects(
        new DamageEffect(context.component.gridComponent.gridPos, DamageType.Fire, context.statusStruct.stacks)
      );

      context.component.updateStatus(context.statusStruct, -1);
    }
  }
}
