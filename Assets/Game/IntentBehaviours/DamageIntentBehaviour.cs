using System;
using System.Linq;
using GridSystem;
using Intents;
using Intents.Engine;
using Intents.IReactions;
using Unity.VisualScripting;
using UnityEngine;

namespace IntentBehaviours {
  [CreateAssetMenu(fileName = "IntentBehaviours/DamageIntentBehaviour")]
  public class DamageIntentBehaviour : IntentBehaviour<DamageIntentValues> {
    protected override void Perform(Intent<DamageIntentValues> intent, IntentProgressContext context) {
      var targets =
        IntentBehaviourHelpers.GetTargets<IReactToDamage>(intent.Targets, context.GlobalContext.GridSystem);
      foreach (var target in targets) {
        if (target is not null) {
          target.OnDamage(intent, context);
          context.Animation ??= new DamageAnimation(target.gameObject.transform.position, intent.Values.DamageType);
        }
      }
    }
  }

  [Serializable]
  public class DamageIntentValues : IntentValues {
    public int Damage;
    public DamageType DamageType;
  }
}
