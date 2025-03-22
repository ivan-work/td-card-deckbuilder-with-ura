using System;
using Components;
using Intents.Engine;
using Intents.IReactions;
using UnityEngine;

namespace Intents.IntentBehaviours {
  [CreateAssetMenu(fileName = "IntentBehaviours/DamageIntentBehaviour")]
  public class DamageIntentBehaviour : IntentBehaviour<DamageIntentValues> {
    // #TODO #INTENT FIX - make event
    protected override void Perform(Intent<DamageIntentValues> intent, IntentProgressContext context) {
      var target = intent.Targets.TargetGameObject;
      var targetPos = intent.Targets.TargetPos;

      if (target != null) {
        foreach (var reactToDamage in target.GetComponents<IReactToDamage>()) {
          reactToDamage.OnDamage(intent, context);
        }
      }

      if (targetPos.HasValue) {
        foreach (var reactToDamage in context.GlobalContext.GridSystem.getGridEntitiesSpecial<IReactToDamage>(targetPos.Value)) {
          reactToDamage.OnDamage(intent, context);
        }
      }
      
      // if (target != null && target.TryGetComponent<HealthComponent>(out var healthComponent)) {
      //   healthComponent.OnDamage(intent.Values.Damage);
      // }
      //
      // if (target != null && target.TryGetComponent<StatusComponent>(out var statusComponent)) {
      //   statusComponent.OnDamage();
      // }

      // #TODO this code is bad, rework
      if (targetPos is not null) {
        context.Animation = new DamageAnimation(
          context.GlobalContext.GridSystem.gridPos2World(targetPos.Value),
          intent.Values.DamageType
        );
      } else if (target is not null) {
        context.Animation = new DamageAnimation(
          target.transform.position,
          intent.Values.DamageType
        );
      }
    }
  }
  
  [Serializable]
  public class DamageIntentValues : IntentValues {
    public int Damage;
    public DamageType DamageType;
  }
}
