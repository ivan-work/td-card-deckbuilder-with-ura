using System;
using Intents.Engine;
using Status.StatusData;
using UnityEngine;

namespace Intents.IntentData {
  [Serializable]
  public class ApplyStatusIntentDataValues : BaseIntentDataValues {
    [SerializeField] private BaseStatusData StatusData;
    [SerializeField] public int Stacks;
  }

  [CreateAssetMenu]
  public class ApplyStatusIntentData : BaseIntentData<ApplyStatusIntentDataValues> {
    public override void PerformIntent(IntentContext<ApplyStatusIntentDataValues, IntentTargetValues> context) {
      throw new NotImplementedException();
    }
  }
}
