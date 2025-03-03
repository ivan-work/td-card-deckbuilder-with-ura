using System;
using Components;
using Effects.EffectAnimations;
using Intents.Engine;
using UnityEngine;

namespace Intents.IntentData {
  [Serializable]
  public class DamageIntentDataValues : BaseIntentDataValues {
    public int Damage;
    public DamageType DamageType;
  }
  
  [CreateAssetMenu]
  public class DamageIntentData : BaseIntentData<DamageIntentDataValues> {
    private BaseAnimation animation;
    // #TODO #INTENT FIX - make event

    public override void PerformIntent(Intent<DamageIntentDataValues, IntentTargetValues> intent, IntentContext context) {
      GameObject source = intent.Source;
      GameObject target = intent.TargetValues.TargetGo;
      DamageIntentDataValues dataValues = intent.DataValues;
      
      if (target.TryGetComponent<HealthComponent>(out var healthComponent)) {
        healthComponent.OnDamage(intent.DataValues.Damage);
      }

      if (target.TryGetComponent<StatusComponent>(out var statusComponent)) {
        statusComponent.OnDamage();
      }
    }
  }
}
