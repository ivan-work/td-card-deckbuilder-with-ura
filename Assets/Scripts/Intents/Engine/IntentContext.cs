using Effects.EffectAnimations;
using JetBrains.Annotations;
using UnityEngine;

namespace Intents.Engine {
  public class IntentContext<TDataValues, TTargetValues> where TDataValues : BaseIntentDataValues {
    public readonly GameObject Source;
    public readonly BaseIntentData<TDataValues, TTargetValues> Data;
    public readonly TDataValues DataValues;
    public readonly TTargetValues TargetValues;
    public readonly GridSystem GridSystem;
    public readonly IntentManagementSystem IntentManagementSystem;

    public IntentContext(
      GameObject source,
      BaseIntentData<TDataValues, TTargetValues> data,
      TDataValues dataValues,
      TTargetValues targetValues,
      GridSystem gridSystem,
      IntentManagementSystem intentManagementSystem
    ) {
      Source = source;
      Data = data;
      DataValues = dataValues;
      TargetValues = targetValues;
      GridSystem = gridSystem;
      IntentManagementSystem = intentManagementSystem;
    }

    [CanBeNull] public BaseAnimation Animation { get; set; }
  }
}
