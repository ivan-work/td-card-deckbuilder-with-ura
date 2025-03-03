using System;
using Intents.Engine;
using Unity.Mathematics;
using UnityEngine;

namespace Intents.IntentData {
  [Serializable]
  public class PushIntentDataValues : BaseIntentDataValues {
    public int Force;

    public PushIntentDataValues(int force) {
      Force = force;
    }
  }

  [CreateAssetMenu]
  public class PushIntentData : BaseIntentData<PushIntentDataValues> {
    public override void PerformIntent(IntentContext<PushIntentDataValues, IntentTargetValues> context) {
      throw new NotImplementedException();
    }
  }
}
