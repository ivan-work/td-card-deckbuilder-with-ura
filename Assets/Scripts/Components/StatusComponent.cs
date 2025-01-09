using Architecture;
using Effects;
using Status;
using UnityEngine;

namespace Components {
  public class StatusComponent : MonoBehaviour {
    public GridComponent gridComponent;
    public BaseStatus status;
    private void Awake() {
      gridComponent = this.GetAssertComponent<GridComponent>();
      EventManager.AmEndTurn.AddListener(OnEndTurnListner);
    }

    public void OnDamage(ActorManager actorManager, DamageEffect damageEffect) {
      status.OnDamage(new StatusContext {actorManager = actorManager, component = this}, damageEffect);
    }
    private void OnEndTurnListner(ActorManager actorManager) {
      status.OnEndTurn(new StatusContext{actorManager = actorManager, component = this});
    }

    public void OnMove(ActorManager actorManager) {
      status.OnMove(new StatusContext{actorManager = actorManager, component = this});
    }

    
  }
}
