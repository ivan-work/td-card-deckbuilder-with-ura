using System;
using Intents.Engine;
using NUnit.Framework;
using UnityEngine;

namespace Intents {
  [Serializable]
  public class IntentFactory {
    [SerializeReference] public IntentBehaviour Behaviour = null!; // Can't be generic

    [SerializeReference] public IntentValues Values = null!; // Can't be generic

#if UNITY_INCLUDE_TESTS
    public IntentBehaviour BehaviourTest {
      get => Behaviour;
      set => Behaviour = value;
    }

    public IntentValues ValuesTest {
      get => Values;
      set => Values = value;
    }
#endif

    public Intent CreateIntent(GameObject source, IntentTargets targets) {
      return new Intent(
        source: source,
        behaviour: Behaviour,
        values: Values,
        targets: targets
      );
    }
  }
}
