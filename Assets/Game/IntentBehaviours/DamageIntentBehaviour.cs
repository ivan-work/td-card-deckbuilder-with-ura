using System;
using Intents;
using Intents.Engine;
using Intents.IReactions;
using UnityEngine;

namespace IntentBehaviours {
  [CreateAssetMenu(fileName = "IntentBehaviours/DamageIntentBehaviour")]
  public class DamageIntentBehaviour : IntentBehaviour<DamageIntentValues> {
    // #TODO #INTENT FIX - make event
    protected override void Perform(Intent<DamageIntentValues> intent, IntentProgressContext context) {
      // var target = intent.Targets.TargetGameObject;
      // var targetPos = intent.Targets.TargetPos;
      //
      // if (target != null) {
      //   foreach (var reactToDamage in target.GetComponents<IReactToDamage>()) {
      //     reactToDamage.OnDamage(intent, context);
      //   }
      // }
      //
      // if (targetPos.HasValue) {
      //   foreach (var reactToDamage in context.GlobalContext.GridSystem.GetGridEntities<IReactToDamage>(targetPos.Value)) {
      //     reactToDamage.OnDamage(intent, context);
      //   }
      // }
      //
      // // #TODO this code is bad, rework
      // if (targetPos is not null) {
      //   context.Animation = new DamageAnimation(
      //     context.GlobalContext.GridSystem.gridPos2World(targetPos.Value),
      //     intent.Values.DamageType
      //   );
      // } else if (target is not null) {
      //   context.Animation = new DamageAnimation(
      //     target.transform.position,
      //     intent.Values.DamageType
      //   );
      // }
    }
  }
  
  [Serializable]
  public class DamageIntentValues : IntentValues {
    public int Damage;
    public DamageType DamageType;
  }
}
