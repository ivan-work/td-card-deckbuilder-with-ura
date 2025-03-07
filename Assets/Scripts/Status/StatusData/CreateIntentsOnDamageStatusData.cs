using System.Collections.Generic;
using System.Linq;
using Effects;
using Intents;
using Intents.Engine;
using UnityEngine;

namespace Status.StatusData {
  [CreateAssetMenu(menuName = "Status/CreateIntentsOnDamageStatusData")]
  public class CreateIntentsOnDamageStatusData : BaseStatusData {
    [SerializeField] private List<IntentFactory> IntentFactory;
    [SerializeField] private DamageType DamageType;

    public override void OnEndTurn(StatusContext context) {
      context.component.updateStatus(context.statusStruct, -1);
    }

    public override void OnDamage(StatusContext context, DamageEffect damageEffect) {
      if (damageEffect.damageType == DamageType) {
        // context.intentSystem.addImmediateIntents(
        //   IntentCreators
        //     .Select(intentCreator => intentCreator
        //       .CreateIntent(context.component.gameObject, new IntentTargetValues(context.component.gameObject, null))
        //     )
        //     .ToArray()
        // );
      }
    }
  }
}
