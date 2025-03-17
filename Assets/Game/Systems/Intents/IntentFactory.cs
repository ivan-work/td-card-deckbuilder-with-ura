using System;
using Intents.Engine;
using NUnit.Framework;
using UnityEngine;

namespace Intents {
  [Serializable]
  public class IntentFactory {
    [SerializeReference] private IntentBehaviour Behaviour = null!; // Can't be generic

    [SerializeReference] private IntentValues Values = null!; // Can't be generic
    
    

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
      return new Intent {
        Source = source,
        Behaviour = Behaviour,
        Values = Values,
        Targets = targets,
      };
    }
  }
}
