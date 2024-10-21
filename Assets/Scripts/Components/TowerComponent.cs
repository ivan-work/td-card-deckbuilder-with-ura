using System;
using System.Collections;
using UnityEngine;

public class TowerComponent : MonoBehaviour {
  [SerializeField] private int damage = 3;
  [SerializeField] private int range = 3;
  [SerializeField] private GameObject shootAttachmentGO;
  private Vector3 shootAttachment = Vector3.zero;
  [SerializeField] private BulletPrefab bulletPrefab;

  public Vector2Int? targetPos;
  GridComponent gridComponent;

  private void OnEnable() {
    Debug.Log("TowerComponent.OnEnable()");
    gridComponent = this.GetAssertComponent<GridComponent>();
    
    EventManager.PhaseActionFast.AddListener(OnActionFast);
    EventManager.PhaseActionSlow.AddListener(OnActionSlow);
  }
  
  private void OnDisable() {
    Debug.Log("TowerComponent.OnDisable");
    EventManager.PhaseActionFast.RemoveListener(OnActionFast);
    EventManager.PhaseActionSlow.RemoveListener(OnActionSlow);
  }

  void Start() {
    Debug.Log("TowerComponent.Start()");
    shootAttachment = shootAttachmentGO.transform.localPosition;
  }
  
  private IEnumerator OnActionFast() {
    if (targetPos.HasValue) {
      var bullet = Instantiate(bulletPrefab, gameObject.transform.position + shootAttachment, Quaternion.identity);
      yield return StartCoroutine(bullet.Shoot(gridComponent.gridPos2World(targetPos.Value)));
      DealDamageComponent.dealDamage(gridComponent.gridSystem, targetPos.Value, damage);
    }
  }

  private IEnumerator OnActionSlow() {
    targetPos = SearchNewTarget();
    yield break;
  }

  private Vector2Int? SearchNewTarget() {
    for (int x = -range; x < range; x++) {
      for (int y = -range; y < range; y++) {
        Vector2Int absolutePos = gridComponent.gridPos + new Vector2Int(x, y);
        if (isValidTarget(gridComponent.gridSystem, absolutePos)) {
          return absolutePos;
        }
      }
    }
    return null;
  }

  public bool isValidTarget(GridSystem gridSystem, Vector2Int gridPos) {
    return DealDamageComponent.isValidTarget(gridSystem, gridPos);
  }
}