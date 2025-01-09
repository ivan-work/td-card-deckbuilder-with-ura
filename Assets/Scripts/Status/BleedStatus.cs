using Effects;
using UnityEngine;

namespace Status {
  public class BleedStatus: BaseStatus {
    public BleedStatus(int count) {
      this.count = count;
    }

    public override void OnMove(StatusContext context) {
      context.actorManager.addImmediateEffects(new DamageEffect(context.component.gridComponent.gridPos, DamageType.Internal, count));
      count--;
      if (count <= 0) {
        Object.DestroyImmediate(context.component);
      }
    }
  }
}
