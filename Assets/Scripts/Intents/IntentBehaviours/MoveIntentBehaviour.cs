using System;
using Intents.Engine;
using Unity.Mathematics;
using UnityEngine;

namespace Intents.IntentBehaviours {
  [CreateAssetMenu]
  public class MoveIntentBehaviour : IntentBehaviour<MoveIntentValues> {
    public override void Perform(Intent<MoveIntentValues> intent, IntentProgressContext context) {
      throw new NotImplementedException();
    }
  }
  
  [Serializable]
  public class MoveIntentValues : IntentValues {
    public int2 Direction;
  }
}
