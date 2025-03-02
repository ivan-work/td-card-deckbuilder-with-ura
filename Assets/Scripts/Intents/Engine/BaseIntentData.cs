using UnityEngine;

namespace Intents {
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

  public abstract class BaseIntentData : ScriptableObject {
    public abstract BaseIntentValues BaseDefaultValues { get; set; }
  }
}
