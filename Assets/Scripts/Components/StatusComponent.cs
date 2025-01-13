using System.Collections.Generic;
using System.Linq;
using Architecture;
using Effects;
using Status;
using Status.StatusData;
using UnityEngine;

namespace Components {
  public class StatusComponent : MonoBehaviour {
    public GridComponent gridComponent;
    private readonly List<StatusStruct> statusList = new();

    private void Awake() {
      gridComponent = this.GetAssertComponent<GridComponent>();
      EventManager.AmEndTurn.AddListener(OnEndTurnListner);
    }

    public void addStatus(StatusStruct statusStruct, ActorManager actorManager) {
      statusList.Add(statusStruct);
      statusStruct.data.OnApply(new StatusContext
        {actorManager = actorManager, component = this, statusStruct = statusStruct});
    }

    public void removeStatus(StatusStruct statusStruct) {
      statusList.Remove(statusStruct);
    }

    public void OnDamage(ActorManager actorManager, DamageEffect damageEffect) {
      statusList.ToList().ForEach(statusStruct =>
        statusStruct.data.OnDamage(
          new StatusContext {actorManager = actorManager, component = this, statusStruct = statusStruct}, damageEffect)
      );
    }

    private void OnEndTurnListner(ActorManager actorManager) {
      statusList.ToList().ForEach(statusStruct =>
        statusStruct.data.OnEndTurn(new StatusContext
          {actorManager = actorManager, component = this, statusStruct = statusStruct})
      );
    }

    public void OnMove(ActorManager actorManager) {
      statusList.ToList().ForEach(statusStruct =>
        statusStruct.data.OnMove(new StatusContext
          {actorManager = actorManager, component = this, statusStruct = statusStruct})
      );
    }

    public bool hasStatus(BaseStatusData statusData) {
      return statusList.Exists(statusStruct => statusStruct.data == statusData);
    }
  }
}
