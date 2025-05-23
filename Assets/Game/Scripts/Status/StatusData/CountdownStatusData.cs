using IntentBehaviours;
using Intents;
using Intents.Engine;
using UnityEngine;

namespace Status.StatusData {
  [CreateAssetMenu(menuName = "Status/CountdownStatusData")]
  public class CountdownStatusData : BaseStatusData {
    [SerializeField] private DamageIntentBehaviour? _intentBehaviour;
    [SerializeField] private DamageType _damageType;

    public override void OnEndTurn(StatusContext context) {
      if (_intentBehaviour) {
        var intent =
          new Intent(
            source: context.Component.gameObject,
            targets: IntentTargets.Create(context.Component.gameObject),
            behaviour: _intentBehaviour,
            values: new DamageIntentValues() { DamageType = _damageType, Damage = context.StatusStruct.stacks });
        context.IntentSystem.AddIntents(intent);
      }
      context.Component.updateStatus(context.StatusStruct, -1);
    }
  }
}
