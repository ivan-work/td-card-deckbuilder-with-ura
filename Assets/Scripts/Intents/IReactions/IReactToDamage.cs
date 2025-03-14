using Intents.Engine;

namespace Intents.IntentBehaviours {
  public interface IReactToDamage {
    void OnDamage(Intent<DamageIntentValues> intent, IntentProgressContext context);
  }
}
