using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Architecture;
using Components;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

namespace Intents {
  [Serializable]
  public abstract class BaseIntentValues { }

  [Serializable]
  public class DamageIntentValues : BaseIntentValues {
    public int Damage { get; init; }
  }

  [Serializable]
  public class MoveIntentValues : BaseIntentValues {
    public int2 Direction { get; init; }
  }

  [Serializable]
  public class StatusIntentValues : BaseIntentValues {
    public int Stacks { get; init; }
  }

  public class IntentContext<T> {
    public GameObject Source { get; init; }
    public GameObject Target { get; init; }
    public T Values { get; init; }
    public GridSystem GridSystem { get; init; }
    public ActorManager ActorManager { get; init; }
  }


  public abstract class BaseIntentData<T> {
    public abstract void PerformIntent(IntentContext<T> context);
  }

  public class DamageIntentData : BaseIntentData<DamageIntentValues> {
    public override void PerformIntent(IntentContext<DamageIntentValues> context) {
      if (context.Target.TryGetComponent<HealthComponent>(out var healthComponent)) {
        healthComponent.OnDamage(context.Values.Damage);
        healthComponent.GetComponents<StatusComponent>().ToList().ForEach(component => component.OnDamage(context));
      }
    }
  }

  public class BaseIntent<T> {
    public GameObject Source { get; init; }
    public GameObject Target { get; init; }
    public BaseIntentData<T> Data { get; init; }
    public T Values { get; init; }
  }

  public class IntentSystem {
    private readonly List<BaseIntent<BaseIntentValues>> queue = new();

    public void AddIntent<T>(BaseIntent<T> intent) where T : BaseIntentValues {
      queue.Add(intent as BaseIntent<BaseIntentValues>);
    }

    public void PerformIntents() {
      foreach (var intent in queue) {
        var context = new IntentContext<BaseIntentValues> {
          Source = intent.Source,
          Target = intent.Target,
          Values = intent.Values
        };
        intent.Data.PerformIntent(context);
      }
    }
  }

  [Serializable]
  public class IntentSpawner {
    public int Number;

    public void Spawn() { }
  }

  public class IntentTester : MonoBehaviour {
    [SerializeField] private IntentSpawner Spawner;

    public void Test() {
      var system = new IntentSystem();
      var data = new DamageIntentData();
      var intent = new BaseIntent<DamageIntentValues>() {
        Data = data
      };
      system.AddIntent(intent);
      system.PerformIntents();
    }
  }
}
