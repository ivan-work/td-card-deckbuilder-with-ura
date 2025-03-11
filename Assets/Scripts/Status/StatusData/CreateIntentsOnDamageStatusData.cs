using System.Collections.Generic;
using System.Linq;
using Intents;
using Intents.Engine;
using Intents.IntentBehaviours;
using UnityEngine;
using UnityEngine.Serialization;

namespace Status.StatusData {
  [CreateAssetMenu(menuName = "Status/CreateIntentsOnDamageStatusData")]
  public class CreateIntentsOnDamageStatusData : BaseStatusData {
    [SerializeField] private List<IntentFactory> IntentFactories;
    [SerializeField] private DamageType DamageType;

    public override void OnEndTurn(StatusContext context) {
      context.Component.updateStatus(context.StatusStruct, -1);
    }

    public override void OnDamage(StatusContext context, Intent<DamageIntentValues> intent) {
      if (intent.Values.DamageType == DamageType) {
        context.IntentSystem.AddImmediateIntents(
          IntentFactories
            .Select(intentFactory => intentFactory
              .CreateIntent(intent.Source, new IntentTargets(context.Component.gameObject, null)))
            .ToArray());
      }
    }
  }
}
