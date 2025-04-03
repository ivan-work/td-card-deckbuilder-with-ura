using Components;
using Effects;
using GridSystem;
using Intents;
using Intents.Engine;
using UnityEngine;

public class TowerComponent : MonoBehaviour, IHasIntent {
  [SerializeField] private int range = 3;
  [SerializeField] private GameObject shootAttachmentGO;
  public Vector3 shootAttachment = Vector3.zero;
  
  [SerializeField] private int damage = 3;
  [SerializeField] public BulletPrefab bulletPrefab;
  [SerializeField] private AbstractTargetCondition condition;

  public GridComponent gridComponent;

  private void Awake() {
    gridComponent = this.GetAssertComponent<GridComponent>();
  }

  public void WriteIntents(IIntentHolder intentHolder) {
    //#TODO Заменить добавление в IntentSystem на intentComponent
    // var targetPos = searchNewTarget();
    // if (targetPos.HasValue) {
    //   actorManager.addImmediateEffects(  
    //     new ShootEffect(this, targetPos.Value),
    //     new DamageEffect(targetPos.Value, DamageType.Pierce, damage)
    //   );
    // }
  }

  private void Start() {
    Debug.Log("TowerComponent.Start()");
    shootAttachment = shootAttachmentGO.transform.localPosition;
  }

  private Vector2Int? searchNewTarget() {
    for (int x = -range; x < range; x++) {
      for (int y = -range; y < range; y++) {
        var absolutePos = gridComponent.GridLoc + new Vector2Int(x, y);
        if (condition.isValidTarget(gridComponent.GridSystem, absolutePos)) {
          return absolutePos;
        }
      }
    }

    return null;
  }
}
