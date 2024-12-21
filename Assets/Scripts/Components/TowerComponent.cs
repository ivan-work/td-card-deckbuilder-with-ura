using System.Collections;
using System.Collections.Generic;
using Effects;
using UnityEngine;

public class TowerComponent : MonoBehaviour, IHasIntent {
  [SerializeField] private int damage = 3;
  [SerializeField] private int range = 3;
  [SerializeField] private GameObject shootAttachmentGO;
  public Vector3 shootAttachment = Vector3.zero;
  [SerializeField] public BulletPrefab bulletPrefab;
  [SerializeField] private AbstractTargetCondition condition;

  public GridComponent gridComponent;

  private void Awake() {
    gridComponent = this.GetAssertComponent<GridComponent>();
  }

  public void getIntents(ActorManager actorManager) {
    var targetPos = searchNewTarget();
    if (targetPos.HasValue) {
      actorManager.addImmediateEffects(
        new ShootEffect(this, targetPos.Value),
        new DamageEffect(this.gridComponent.gridSystem, targetPos.Value, damage)
      );
    }
  }

  private void Start() {
    Debug.Log("TowerComponent.Start()");
    shootAttachment = shootAttachmentGO.transform.localPosition;
  }

  private Vector2Int? searchNewTarget() {
    for (int x = -range; x < range; x++) {
      for (int y = -range; y < range; y++) {
        var absolutePos = gridComponent.gridPos + new Vector2Int(x, y);
        if (condition.isValidTarget(gridComponent.gridSystem, absolutePos)) {
          return absolutePos;
        }
      }
    }

    return null;
  }
}
