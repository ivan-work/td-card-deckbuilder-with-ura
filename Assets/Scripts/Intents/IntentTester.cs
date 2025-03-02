using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Architecture;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

namespace Intents {
  [Serializable]
  public class BaseIntentValues {
    // public string ValueName;

    public BaseIntentValues Clone() {
      return MemberwiseClone() as BaseIntentValues;
    }
  }

  [Serializable]
  public class DamageIntentValues : BaseIntentValues {
    public int Damage;
  }

  [Serializable]
  public class MoveIntentValues : BaseIntentValues {
    public int2 Direction;
  }

  [Serializable]
  public class StatusIntentValues : BaseIntentValues {
    public int Stacks;
  }

  public class IntentContext<T> where T : BaseIntentValues {
    public GameObject Source { get; init; }
    public GameObject Target { get; init; }
    public T Values { get; init; }
    public IntentSystem IntentSystem { get; init; }
  }


  public abstract class BaseIntentData : ScriptableObject {
    public abstract BaseIntentValues BaseDefaultValues { get; set; }
  }


  public abstract class BaseIntentData<T> : BaseIntentData where T : BaseIntentValues {
    [SerializeField] private T defaultValues;

    public override BaseIntentValues BaseDefaultValues {
      get => defaultValues;
      set => defaultValues = (T) value;
    }

    public T DefaultValues {
      get => defaultValues;
      set => defaultValues = value;
    }

    public abstract void PerformIntent(IntentContext<T> context);
  }

  public class Intent<T> where T : BaseIntentValues {
    public GameObject Source { get; init; }
    public GameObject Target { get; init; }
    public BaseIntentData<T> Data { get; init; }
    public T Values { get; init; }
  }

  public class AnyIntent: Intent<BaseIntentValues> { }

  public class IntentSystem {
    private readonly List<AnyIntent> queue = new();

    public void AddIntent(AnyIntent intent) {
      queue.Add(intent);
    }

    public void PerformIntents() {
      foreach (var intent in queue) {
        var context = new IntentContext<BaseIntentValues> {
          Source = intent.Source,
          Target = intent.Target,
          Values = intent.Values,
          IntentSystem = this,
        };
        intent.Data.PerformIntent(context);
      }
    }
  }

  [Serializable]
  public class IntentSpawner {
    [SerializeReference] public BaseIntentData IntentData;
    [SerializeReference] public BaseIntentValues ValuesReference;
    [SerializeField] public BaseIntentValues ValuesField;

    public AnyIntent Spawn() {
      return new AnyIntent {
        Data = IntentData as BaseIntentData<BaseIntentValues>,
        Values = ValuesReference
      };
    }
  }

  [Serializable]
  public class IntentSpawnerPlain {
    [SerializeField] public BaseIntentData IntentData;
    [SerializeReference] public BaseIntentValues ValuesReference;
    [SerializeField] public BaseIntentValues ValuesField;
  }

  public class IntentTester : MonoBehaviour {
    [SerializeField] private IntentSpawner Spawner;
    [SerializeField] private IntentSpawnerPlain Spawner2;
    public GameObject Source;
    public GameObject Target;

    public void Awake() {
      var system = new IntentSystem();
      var intent = Spawner.Spawn();
      system.AddIntent(intent);
      system.PerformIntents();

      // var spawner = new IntentSpawner();
      // spawner.IntentData = ScriptableObject.CreateInstance<DamageIntentData>();
      // spawner.IntentData.BaseDefaultValues = new DamageIntentValues();
    }
  }
}
