using System;
using Intents.Engine;
using UnityEngine;

namespace Intents.IntentBehaviours {
  [CreateAssetMenu]
  public class PushIntentBehaviour : IntentBehaviour<PushIntentValues> {
    public override void Perform(Intent<PushIntentValues> intent, IntentProgressContext context) {
      throw new NotImplementedException();
    }
  }
  
  [Serializable]
  public class PushIntentValues : IntentValues {
    public int Force;

    public PushIntentValues(int force) {
      Force = force;
    }
  }
}
