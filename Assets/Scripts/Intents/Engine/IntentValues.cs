using System;

namespace Intents.Engine {
  [Serializable]
  public class IntentValues {
    public IntentValues Clone() {
      return MemberwiseClone() as IntentValues;
    }

    public T Clone<T>() where T : IntentValues {
      return MemberwiseClone() as T;
    }
  }
}
