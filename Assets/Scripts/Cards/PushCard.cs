using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(menuName = "Card/PushCard")]
public class PushCard : Card {
  [SerializeField] public int force = 1;

  public override IEnumerable<BaseEffect> doCardAction(GridSystem gridSystem, Vector2Int[] gridPoses) {
    if (gridPoses.Length < 2) {
      Debug.LogError($"PushCard.doCardAction: gridPoses is not valid: {gridPoses}");
    }

    var sourcePos = gridPoses[0];
    var targetPos = gridPoses[1];

    var differenceVector = targetPos - sourcePos;

    var direction = new Vector2Int(Math.Sign(differenceVector.x), Math.Sign(differenceVector.y));
    if (direction.x == 0 && direction.y == 0 || direction.x != 0 && direction.y != 0) {
      Debug.LogError($"PushCard.doCardAction: direction is not valid: {direction}");

    }

    // yield return ApplyForceComponent.applyForce(gridSystem, gridPoses[0], direction, force);
    
    return gridSystem.getGridEntitiesSpecial<MoveComponent>(sourcePos)
      .Select(component => new PushEffect(component, direction, force));
  }
}