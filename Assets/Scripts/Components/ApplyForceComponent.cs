using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public static class ApplyForceComponent {
  public static IEnumerator applyForce(GridSystem gridSystem, Vector2Int gridPos, Vector2Int direction, int force) {
    var entities = gridSystem.getGridEntities(gridPos)
      .Select(entity => entity.GetComponent<MoveComponent>())
      .Where(entity => entity);
    var sourcePos = gridPos;
    for (var remainingForce = force; remainingForce > 0; --remainingForce) {
      var targetPos = sourcePos + direction;
      var hasPath = gridSystem.getGridEntitiesSpecial<PathComponent>(targetPos).Any();
      if (hasPath) {
        var targetMobs = gridSystem.getGridEntitiesSpecial<MoveComponent>(targetPos);
        var hasMobs = targetMobs.Any();
        if (hasMobs) {
          DealDamageComponent.dealDamage(gridSystem, sourcePos, remainingForce);
          DealDamageComponent.dealDamage(gridSystem, targetPos, remainingForce);
        } else {
          yield return launchMobs(entities, targetPos);
          sourcePos = targetPos;
        }
      } else {
        DealDamageComponent.dealDamage(gridSystem, targetPos, remainingForce);
      }
    }
  }

  private static IEnumerator launchMobs(IEnumerable<MoveComponent> entities, Vector2Int targetPos) {
    foreach (var entity in entities) {
      yield return entity.StartCoroutine(entity.changePositionToTargetPos(targetPos));
    }
  }
}