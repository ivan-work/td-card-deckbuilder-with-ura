using System;
using System.Linq;
using Components;
using IntentBehaviours;
using Intents.Engine;
using Status;
using Status.StatusData;
using UnityEngine;

namespace Intents.IntentBehaviours {
  [CreateAssetMenu(fileName = "IntentBehaviours/ApplyStatusIntentBehaviour")]
  public class ApplyStatusIntentBehaviour : IntentBehaviour<ApplyStatusIntentValues> {
    protected override void Perform(Intent<ApplyStatusIntentValues> intent, IntentProgressContext context) {
      var targets =
        IntentBehaviourHelpers.GetTargets<StatusComponent>(intent.Targets, context.GlobalContext.GridSystem);

      foreach (var target in targets) {
        var statusStruct = new StatusStruct(stacks: intent.Values.Stacks, data: intent.Values.StatusData);
        target.AddStatus(statusStruct);
      }
      

    }
  }

  [Serializable]
  public class ApplyStatusIntentValues : IntentValues {
    [SerializeField] public BaseStatusData StatusData;
    [SerializeField] public int Stacks;
  }
}
