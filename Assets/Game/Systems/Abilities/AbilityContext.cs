using Intents.Engine;
using UnityEngine;

namespace Abilities {
  public class AbilityContext {
    public readonly Ability Ability;
    public readonly ITargetingContext TargetingContext;
    public readonly IntentGlobalContext GlobalContext;
    public readonly GameObject Source;

    public AbilityContext(GameObject source, Ability ability, IntentGlobalContext intentGlobalContext) {
      Ability = ability;
      TargetingContext = ability.CreateTargetingContext();
      GlobalContext = intentGlobalContext;
      Source = source;
    }
  }
}
