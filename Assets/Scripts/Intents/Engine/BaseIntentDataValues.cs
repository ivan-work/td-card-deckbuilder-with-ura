using System;

namespace Intents.Engine {
  [Serializable]
  public class BaseIntentDataValues {
    public BaseIntentDataValues Clone() {
      return MemberwiseClone() as BaseIntentDataValues;
    }
  }
}
