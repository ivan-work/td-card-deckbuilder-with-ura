using System;
using UnityEngine;

namespace Intents {
  [Serializable]
  public class IntentCreator {
    [SerializeReference] public BaseIntentData IntentData;
    [SerializeReference] public BaseIntentValues IntentValues;

    public AnyIntent Spawn() {
      return new AnyIntent {
        Data = IntentData as BaseIntentData<BaseIntentValues>,
        Values = IntentValues
      };
    }
  }
}
