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
      var targetLocs = intent.Targets.Locations;

      foreach (var targetLoc in targetLocs) {
        foreach (var reactToDamage in context.GlobalContext.GridSystem.GetGridEntities<IReactToDamage>(targetLoc)) {
          reactToDamage.OnDamage(intent, context);
        }

        context.Animation ??= new DamageAnimation(
          context.GlobalContext.GridSystem.gridPos2World(targetLoc),
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
