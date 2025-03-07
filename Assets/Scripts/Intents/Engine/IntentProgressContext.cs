using Effects.EffectAnimations;

namespace Intents.Engine {
  public class IntentProgressContext {
    public IntentGlobalContext GlobalContext { get; init; }
    public BaseAnimation Animation { get; set; }
  }
}
