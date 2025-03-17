using System;
using Intents.Engine;
using Status.StatusData;
using UnityEngine;

namespace Intents.IntentBehaviours {
  [CreateAssetMenu(fileName = "IntentBehaviours/ApplyStatusIntentBehaviour")]
  public class ApplyStatusIntentBehaviour : IntentBehaviour<ApplyStatusIntentValues> {
    protected override void Perform(Intent<ApplyStatusIntentValues> intent, IntentProgressContext context) {
      throw new NotImplementedException();
    }
  }
  
  [Serializable]
  public class ApplyStatusIntentValues : IntentValues {
    [SerializeField] private BaseStatusData StatusData;
    [SerializeField] public int Stacks;
  }
}
