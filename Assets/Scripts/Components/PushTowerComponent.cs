using System.Linq;
using Architecture;
using Effects;
using Status;
using Status.StatusData;
using UnityEngine;

namespace Components {
  public class PushTowerComponent : MonoBehaviour, IHasIntent {
    [SerializeField] private GameObject shootAttachmentGO;
    private Vector3 shootAttachment = Vector3.zero;


    [SerializeField] private int cooldown = 3;
    [SerializeField] private BaseStatusData cooldownStatusData;

    [SerializeField] private int range = 1;
    [SerializeField] private Vector2Int direction;
    [SerializeField] private int force = 1;
    [SerializeField] private AbstractTargetCondition condition;

    private GridComponent gridComponent;
    private StatusComponent statusComponent;

    private void Awake() {
      gridComponent = this.GetAssertComponent<GridComponent>();
      statusComponent = this.GetAssertComponent<StatusComponent>();
      
      shootAttachment = shootAttachmentGO.transform.localPosition;
    }


    public void getIntents(ActorManager actorManager) {
        if (!statusComponent.hasStatus(cooldownStatusData)) {
          var targetPos = searchNewTarget();
          if (targetPos.HasValue) {
            var effects = gridComponent.gridSystem.getGridEntitiesSpecial<MoveComponent>(targetPos.Value)
              .Select<MoveComponent, BaseEffect>(component => new PushEffect(component, direction, force))
              .Append(new ApplyStatusEffect(statusComponent, new StatusStruct(cooldownStatusData, cooldown))).ToArray();
            actorManager.addImmediateEffects(effects);
          }
        }
    }

    private Vector2Int? searchNewTarget() {
      var currentPos = gridComponent.gridPos;
      for (int i = 0; i < range; i++) {
        currentPos += direction;
        if (condition.isValidTarget(gridComponent.gridSystem, currentPos)) {
          return currentPos;
        }
      }

      return null;
    }
  }
}
