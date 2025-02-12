using Effects;
using UnityEngine;

namespace Status.StatusData {
  [CreateAssetMenu(menuName = "Status/ApplyAnotherStatusOnDamageStatusData")]
  public class ApplyAnotherStatusOnDamageStatusData : BaseStatusData {
    [SerializeField] private DamageType damageType;
    [SerializeField] private BaseStatusData anotherStatus;

    public override void OnEndTurn(StatusContext context) {
      context.component.updateStatus(context.statusStruct, -1);
    }

    public override void OnDamage(StatusContext context, DamageEffect damageEffect) {
      if (damageEffect.damageType == damageType && !context.component.hasStatus(anotherStatus)) {
        context.actorManager.addImmediateEffects(
          new ApplyStatusEffect(
            context.component,
            new StatusStruct(
              anotherStatus,
              context.statusStruct.stacks
            )
          )
        );
      }
    }
  }
}
