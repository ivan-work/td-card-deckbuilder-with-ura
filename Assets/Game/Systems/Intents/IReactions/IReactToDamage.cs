using IntentBehaviours;
using Intents.Engine;
using Intents.IntentBehaviours;

namespace Intents.IReactions {
  public interface IReactToDamage {
    void OnDamage(Intent<DamageIntentValues> intent, IntentProgressContext context);
  }
}
