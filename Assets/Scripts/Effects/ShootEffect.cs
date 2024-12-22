using System.Collections;
using Architecture;
using UnityEngine;

namespace Effects {
  public class ShootEffect : BaseEffect {
    private readonly TowerComponent component;
    private readonly Vector2Int targetPos;

    private Vector2Int sourcePos => component.gridComponent.gridPos;

    public ShootEffect(TowerComponent component, Vector2Int targetPos) {
      this.component = component;
      this.targetPos = targetPos;
    }

    public override void start(ActorManager am, GridSystem gridSystem) {
      isActive = true;
      am.StartCoroutine(playAnimation(am));
    }

    private IEnumerator playAnimation(ActorManager am) {
      var bullet = Object.Instantiate(component.bulletPrefab, component.gameObject.transform.position + component.shootAttachment, Quaternion.identity);

      yield return am.StartCoroutine(bullet.Shoot(component.gridComponent.gridPos2World(targetPos)));

      isActive = false;
    }
  }
}
