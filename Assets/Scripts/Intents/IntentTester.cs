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
    public string ValueName;

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
    [SerializeField] public T defaultValues;

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
    private readonly List<Intent<BaseIntentValues>> queue = new();

    public void AddIntent<T>(Intent<T> intent) where T : BaseIntentValues {
      queue.Add(intent as Intent<BaseIntentValues>);
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

    public Intent<T> Spawn<T>() where T : BaseIntentValues {
      return new Intent<T>() {
        Data = GetIntentData<T>(),
        Values = GetIntentValues<T>()
      };
    }

    public T GetIntentValues<T>() where T : BaseIntentValues {
      return ValuesReference as T;
    }

    public BaseIntentData<T> GetIntentData<T>() where T : BaseIntentValues {
      return IntentData as BaseIntentData<T>;
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

    #region Inspector Fields

    [SerializeReference, MySubclassSelectorAttribute]
    private IBaseInterface _baseInterface = null;

    [SerializeReference, MySubclassSelectorAttribute]
    private BaseClass _classBase = null;

    [SerializeReference, MySubclassSelectorAttribute]
    private List<BaseClass> _classes = new();

    #endregion

    #region Properties

    public IBaseInterface BaseInterface => _baseInterface;

    public BaseClass ClassBase => _classBase;

    public List<BaseClass> Classes => _classes;

    #endregion

    public void Awake() {
      var system = new IntentSystem();
      // var data = ScriptableObject.CreateInstance<DamageIntentData>();
      var intent = new Intent<DamageIntentValues> {
        Data = Spawner.GetIntentData<DamageIntentValues>(),
        Values = Spawner.GetIntentValues<DamageIntentValues>()
      };
      system.AddIntent(intent);
      system.PerformIntents();

      var spawner = new IntentSpawner();
      spawner.IntentData = new DamageIntentData();
      spawner.IntentData.BaseDefaultValues = new DamageIntentValues();
    }
  }


  #region Types

  public interface IBaseInterface { }

  [System.Serializable]
  public abstract class BaseClass : IBaseInterface { }

  [System.Serializable]
  public class ClassA : BaseClass {
    public string ClassAString;
  }

  [System.Serializable]
  public class ClassB : BaseClass {
    public float ClassBFLoat;
  }

  [System.Serializable]
  public class ClassC : ClassA {
    public int ClassCInt;
  }

  #endregion
}
