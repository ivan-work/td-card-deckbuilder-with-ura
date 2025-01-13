using Effects;
using UnityEngine;

namespace Status.StatusData {
  [CreateAssetMenu(menuName = "Status/BleedStatusData")]
  public class BleedStatusData : BaseStatusData {
    public override void OnMove(StatusContext context) {
      context.actorManager.addImmediateEffects(
        new DamageEffect(context.component.gridComponent.gridPos, DamageType.Internal, context.statusStruct.stacks)
      );
      
      context.statusStruct.stacks--;
      
      if (context.statusStruct.stacks <= 0) {
        context.component.removeStatus(context.statusStruct);
      }
    }
  }
}
