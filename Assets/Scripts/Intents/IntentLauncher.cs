using System;
using System.Collections.Generic;
using Effects.EffectAnimations;
using UnityEngine;

namespace Intents2 {
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

    public static Intent<T> FromBaseIntent(BaseIntent baseIntent) {
      var intent = new Intent<T>() {
        Behaviour = IntentBehaviour<T>.FromIntentBehaviour(baseIntent.Behaviour),
        Values = baseIntent.Values.Clone<T>(),
      };
      return intent;
    }
  }

  public class IntentContext<T> where T : BaseIntentValues {
    public Intent<T> Intent { get; init; }
    public IntentManagementSystem IntentManagementSystem { get; init; }
    public BaseAnimation Animation { get; set; }
  }

  public interface IIntentBehaviour<in T> where T : BaseIntentValues {
    public void Perform(IntentContext<T> context);
  }

  public abstract class IntentBehaviour : ScriptableObject {
    public abstract void BasePerform(IntentContext<BaseIntentValues> context);
  }

  public abstract class IntentBehaviour<in T> : IntentBehaviour where T : BaseIntentValues {
    public override void BasePerform(IntentContext<BaseIntentValues> context) {
      Perform(context);
    }

    public abstract void Perform(IntentContext<T> context);

    public static IntentBehaviour<T> FromIntentBehaviour(IntentBehaviour foo) {
      IntentBehaviour<T> newfoo = CreateInstance<IntentBehaviour<T>>();
      return newfoo;
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
        intent.Behaviour.BasePerform(new IntentContext<BaseIntentValues> {
          IntentManagementSystem = this,
          Intent = Intent<BaseIntentValues>.FromBaseIntent(intent)
        });
      }
    }
  }

  public class IntentFactory : MonoBehaviour {
    [SerializeField] private IntentBehaviour Behaviour;

    [SerializeField] private BaseIntentValues Values;

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
