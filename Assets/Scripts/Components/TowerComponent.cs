using System.Collections;
using UnityEngine;

public class TowerComponent : MonoBehaviour {
  [SerializeField] private int damage = 3;
  [SerializeField] private int range = 3;
  [SerializeField] private Vector3 shootAttachment = Vector3.zero;
  [SerializeField] private BulletPrefab bulletPrefab;

  public Vector2Int? targetPos;
  GridComponent gridComponent;

  void Start() {
    gridComponent = this.GetAssertComponent<GridComponent>();
    
    EventManager.PhaseActionFast.AddListener(OnActionFast);
    EventManager.PhaseActionSlow.AddListener(OnActionSlow);
  }
  
  private void OnDestroy() {
    Debug.Log("TowerComponent.OnDestroy");
    EventManager.PhaseActionFast.RemoveListener(OnActionFast);
    EventManager.PhaseActionSlow.RemoveListener(OnActionSlow);
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