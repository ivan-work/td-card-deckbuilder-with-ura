using IntentBehaviours;
using Intents.Engine;
using Intents.IntentBehaviours;
using UnityEngine;

namespace Intents.IReactions {
  public interface IReactToDamage {
    GameObject gameObject { get; }


    void OnDamage(Intent<DamageIntentValues> intent, IntentProgressContext context);
  }
}
