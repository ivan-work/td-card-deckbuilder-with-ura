using System;
using UnityEngine;

namespace Intents {
  [Serializable]
  public class StatusIntentValues : BaseIntentValues {
    public int Stacks;
  }
  
  [CreateAssetMenu]
  public class StatusIntentData : BaseIntentData<StatusIntentValues> {
    public override void PerformIntent(IntentContext<StatusIntentValues> context) {
      throw new NotImplementedException();
    }
  }
}
