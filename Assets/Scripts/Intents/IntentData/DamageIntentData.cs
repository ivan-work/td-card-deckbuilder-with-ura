using System;
using Components;
using Effects.EffectAnimations;
using UnityEngine;

namespace Intents {
  [Serializable]
  public class DamageIntentValues : BaseIntentValues {
    public int Damage;
  }
  
  [CreateAssetMenu]
  public class DamageIntentData : BaseIntentData<DamageIntentValues> {
    private BaseAnimation animation;
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
