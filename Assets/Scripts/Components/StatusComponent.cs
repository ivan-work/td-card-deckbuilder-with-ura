using System.Collections.Generic;
using System.Linq;
using Architecture;
using Effects;
using Status;
using Status.StatusData;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace Components {
  public class StatusComponent : MonoBehaviour {
    [SerializeField] private GameObject statusBar;
    [SerializeField] private StatusIconPrefab statusIconPrefab;

    public GridComponent gridComponent;
    private readonly List<StatusStruct> statusList = new();
    private readonly List<StatusIconPrefab> statusIconList = new();

    private void Awake() {
      gridComponent = this.GetAssertComponent<GridComponent>();
      EventManager.AmEndTurn.AddListener(OnEndTurnListner);
    }

    public void addStatus(StatusStruct statusStruct, ActorManager actorManager) {
      statusList.Add(statusStruct);
      statusStruct.data.OnApply(new StatusContext {actorManager = actorManager, component = this, statusStruct = statusStruct});

      var statusIcon = Instantiate(statusIconPrefab, statusBar.transform);
      statusStruct.OnChange.AddListener(statusIcon.onChangeListener);
      statusStruct.OnRemove.AddListener(statusIcon.onRemoveListener);
      statusIconList.Add(statusIcon);
      
      statusStruct.OnChange.Invoke(statusStruct);
    }

    private void removeStatus(StatusStruct statusStruct) {
      statusList.Remove(statusStruct);
      statusStruct.OnRemove.Invoke();
      statusIconList.RemoveAll(icon => icon.isGoingToBeDestroyed);
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

    public void decreaseStatus(StatusStruct statusStruct) {
      statusStruct.stacks--;
      statusStruct.OnChange.Invoke(statusStruct);
      
      if (statusStruct.stacks <= 0) {
        removeStatus(statusStruct);
      }
    }
  }
}
