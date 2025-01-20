using Effects;
using UnityEngine;

namespace Status.StatusData {
  [CreateAssetMenu(menuName = "Status/BleedStatusData")]
  public class BleedStatusData : BaseStatusData {
    public override void OnMove(StatusContext context) {
      context.actorManager.addImmediateEffects(
        new DamageEffect(context.component.gridComponent.gridPos, DamageType.Internal, context.statusStruct.stacks)
      );
      
      context.component.decreaseStatus(context.statusStruct);
    }
  }
}
