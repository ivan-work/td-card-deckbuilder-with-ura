using UnityEngine;

namespace Intents.Engine {
  public abstract class BaseIntentData : ScriptableObject {
    public abstract BaseIntentDataValues BaseDefaultDataValues { get; set; }
  }

  public abstract class BaseIntentData<TDataValues, TTargetValues> : BaseIntentData where TDataValues : BaseIntentDataValues {
    [SerializeField] private TDataValues DefaultDataValues;

    public override BaseIntentDataValues BaseDefaultDataValues {
      get => DefaultDataValues;
      set => DefaultDataValues = (TDataValues) value;
    }

    public TDataValues DefaultValues {
      get => DefaultDataValues;
      set => DefaultDataValues = value;
    }

    public abstract void PerformIntent(Intent<TDataValues, TTargetValues> intent, IntentContext context);
  }

  public abstract class BaseIntentData<TDataValues> : BaseIntentData<TDataValues, IntentTargetValues> where TDataValues : BaseIntentDataValues { }
}
