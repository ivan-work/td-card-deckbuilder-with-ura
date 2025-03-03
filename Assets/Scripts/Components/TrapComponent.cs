using Architecture;
using Components;
using Effects;
using Intents;
using Status;
using Status.StatusData;
using UnityEngine;

public class TrapComponent : MonoBehaviour {
  [SerializeField] private BaseStatusData statusData;
  [SerializeField] private int stacks;

  public void OnEntityEnter(ActorManager am, GridComponent entity) {
    if (entity.TryGetComponent<StatusComponent>(out var statusComponent)) {
      am.addEffects(new ApplyStatusEffect(statusComponent, new StatusStruct(statusData, stacks)));
    }
  }
}
