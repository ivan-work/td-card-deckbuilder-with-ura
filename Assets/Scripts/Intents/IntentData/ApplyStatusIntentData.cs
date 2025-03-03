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
    public override void PerformIntent(Intent<ApplyStatusIntentDataValues, IntentTargetValues> intent, IntentContext context) {
      throw new NotImplementedException();
    }
  }
}
