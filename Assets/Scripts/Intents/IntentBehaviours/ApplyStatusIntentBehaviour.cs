using System;
using Intents.Engine;
using Status.StatusData;
using UnityEngine;

namespace Intents.IntentBehaviours {
  [CreateAssetMenu]
  public class ApplyStatusIntentBehaviour : IntentBehaviour<ApplyStatusIntentValues> {
    public override void Perform(Intent<ApplyStatusIntentValues> intent, IntentProgressContext context) {
      throw new NotImplementedException();
    }
  }
  
  [Serializable]
  public class ApplyStatusIntentValues : IntentValues {
    [SerializeField] private BaseStatusData StatusData;
    [SerializeField] public int Stacks;
  }
}
