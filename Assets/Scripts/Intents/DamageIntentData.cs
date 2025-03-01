using Components;
using UnityEngine;

namespace Intents {
  [CreateAssetMenu]
  public class DamageIntentData : BaseIntentData<DamageIntentValues> {
    // #TODO #INTENT FIX - make event
    public override void PerformIntent(IntentContext<DamageIntentValues> context) {
      GameObject source = context.Source;
      GameObject target = context.Target;
      DamageIntentValues values = context.Values;
      
      if (context.Target.TryGetComponent<HealthComponent>(out var healthComponent)) {
        healthComponent.OnDamage(context.Values.Damage);
      }

      if (context.Target.TryGetComponent<StatusComponent>(out var statusComponent)) {
        statusComponent.OnDamage(context);
      }
    }
  }
}
