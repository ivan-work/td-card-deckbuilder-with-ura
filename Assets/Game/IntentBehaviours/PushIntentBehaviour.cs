using System;
using Intents.Engine;
using UnityEngine;

namespace Intents.IntentBehaviours {
  [CreateAssetMenu(fileName = "IntentBehaviours/PushIntentBehaviour")]
  public class PushIntentBehaviour : IntentBehaviour<PushIntentValues> {
    protected override void Perform(Intent<PushIntentValues> intent, IntentProgressContext context) {
      throw new NotImplementedException();
    }
  }
  
  [Serializable]
  public class PushIntentValues : IntentValues {
    public int _force = 0;

    public PushIntentValues() { }
    
    public PushIntentValues(int force) {
      _force = force;
    }

  }
}
