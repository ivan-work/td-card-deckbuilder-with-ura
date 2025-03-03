using System;
using Intents.Engine;
using Unity.Mathematics;
using UnityEngine;

namespace Intents.IntentData {
  [Serializable]
  public class MoveIntentDataValues : BaseIntentDataValues {
    public int2 Direction;
  }

  [CreateAssetMenu]
  public class MoveIntentData : BaseIntentData<MoveIntentDataValues> {
    
    public override void PerformIntent(Intent<MoveIntentDataValues, IntentTargetValues> intent, IntentContext context) {
      throw new NotImplementedException();
    }
  }
}
