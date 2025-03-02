using System;

namespace Intents {
  [Serializable]
  public class BaseIntentValues {
    public BaseIntentValues Clone() {
      return MemberwiseClone() as BaseIntentValues;
    }
  }
}
