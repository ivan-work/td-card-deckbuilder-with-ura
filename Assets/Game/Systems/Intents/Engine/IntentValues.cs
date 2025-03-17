using System;

namespace Intents.Engine {
  [Serializable]
  public class IntentValues {
    public IntentValues() { }

    public IntentValues Clone() {
      return MemberwiseClone() as IntentValues ?? throw new InvalidOperationException();
    }

    public T Clone<T>() where T : IntentValues {
      return MemberwiseClone() as T ?? throw new InvalidOperationException();
    }
  }
}
