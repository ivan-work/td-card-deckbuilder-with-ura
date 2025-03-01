using System;
using UnityEngine;

namespace Intents {
  [CreateAssetMenu]
  public class StatusIntentData : BaseIntentData<StatusIntentValues> {
    public override void PerformIntent(IntentContext<StatusIntentValues> context) {
      throw new NotImplementedException();
    }
  }
}
