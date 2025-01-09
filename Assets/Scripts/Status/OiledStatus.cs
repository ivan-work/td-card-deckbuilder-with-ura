using Effects;
using UnityEngine;
namespace Status {
  public class OiledStatus: BaseStatus {

    public override void OnDamage(StatusContext context, DamageEffect damageEffect) {
      if (damageEffect.damageType == DamageType.Fire) {
        context.actorManager.addImmediateEffects(new ApplyStatusEffect(context.component.gameObject, new BurningStatus(damageEffect.damage)));
      }
    }
    
  }
}
