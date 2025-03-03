using System;
using System.Collections;
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
    // #TODO #INTENT FIX - make event
    public override void PerformIntent(IntentContext<DamageIntentDataValues, IntentTargetValues> context) {
      var target = context.TargetValues.TargetGo;
      var targetPos = context.TargetValues.TargetPos;
      
      if (target != null && target.TryGetComponent<HealthComponent>(out var healthComponent)) {
        healthComponent.OnDamage(context.DataValues.Damage);
      }

      if (target != null && target.TryGetComponent<StatusComponent>(out var statusComponent)) {
        statusComponent.OnDamage();
      }

      if (targetPos.HasValue) {
        context.Animation = new DamageAnimation(
          context.GridSystem.gridPos2World(targetPos.Value, (float) GridComponent.zLayerEnum.Effect),
          context.DataValues.DamageType
        );
      }
    }
  }
}
