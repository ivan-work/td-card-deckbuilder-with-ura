using System;
using System.Collections.Generic;
using Effects.EffectAnimations;
using Intents.Engine;
using UnityEngine;

namespace IntentsForTestSingleFile {
  [Serializable]
  public class BaseIntentValues {
    public BaseIntentValues Clone() {
      return MemberwiseClone() as BaseIntentValues;
    }

    public T Clone<T>() where T : BaseIntentValues {
      return MemberwiseClone() as T;
    }
  }

  [Serializable]
  public class DamageIntentValues : BaseIntentValues {
    public int Damage;
  }

  [Serializable]
  public class StatusIntentValues : BaseIntentValues {
    public int Stacks;
  }

  [Serializable]
  public class IntentTargets { }

  public class BaseIntent {
    public IntentBehaviour Behaviour { get; init; }
    public BaseIntentValues Values { get; init; }
    public IntentTargets Targets { get; init; }
  }

  public class Intent<T> where T : BaseIntentValues {
    public IntentBehaviour<T> Behaviour { get; init; }
    public T Values { get; init; }
    public IntentTargets Targets { get; init; }
  }

  public class IntentContext<T> where T : BaseIntentValues {
    public Intent<T> Intent { get; init; }
    public IntentManagementSystem IntentManagementSystem { get; init; }
    public BaseAnimation Animation { get; set; }
  }


  public abstract class IntentBehaviour : ScriptableObject {
    public abstract void Perform(BaseIntent intent, IntentManagementSystem system);
  }

  public abstract class IntentBehaviour<T> : IntentBehaviour where T : BaseIntentValues {
    public abstract void Perform(IntentContext<T> context);

    public override void Perform(BaseIntent intent, IntentManagementSystem system) {
      if (intent.Values is T values) {
        var context = new IntentContext<T> {
          Intent = new Intent<T> {
            Behaviour = this,
            Values = values,
            Targets = intent.Targets
          },
          IntentManagementSystem = system
        };
        Perform(context);
      } else {
        Debug.LogError($"Type mismatch: Expected {typeof(T)}, got {intent.Values.GetType()}");
      }
    }
  }

  public class DamageIntentBehaviour : IntentBehaviour<DamageIntentValues> {
    public override void Perform(IntentContext<DamageIntentValues> context) {
      throw new NotImplementedException();
    }
  }

  public class IntentManagementSystem : MonoBehaviour {
    private LinkedList<BaseIntent> Queue = new();

    public void AddIntent(BaseIntent intent) {
      Queue.AddLast(intent);
    }

    public void PerformIntents() {
      foreach (var intent in Queue) {
        intent.Behaviour.Perform(intent, this);
      }
    }
  }

  public class IntentFactory : MonoBehaviour {
    [SerializeField] private IntentBehaviour Behaviour; // Can't be generic

    [SerializeField] private BaseIntentValues Values; // Can't be generic

#if UNITY_INCLUDE_TESTS
    public IntentBehaviour BehaviourTest {
      get => Behaviour;
      set => Behaviour = value;
    }

    public BaseIntentValues ValuesTest {
      get => Values;
      set => Values = value;
    }
#endif

    public BaseIntent CreateIntent(IntentTargets targets) {
      return new BaseIntent {
        Behaviour = Behaviour,
        Values = Values,
        Targets = targets,
      };
    }
  }
}
