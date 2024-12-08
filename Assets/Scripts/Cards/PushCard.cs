using System;
using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Card/PushCard")]
public class PushCard : Card {
  [SerializeField] public int force = 1;

  public override IEnumerator doCardAction(GridSystem gridSystem, Vector2Int[] gridPoses) {
    if (gridPoses.Length < 2) {
      Debug.LogError($"PushCard.doCardAction: gridPoses is not valid: {gridPoses}");

      yield break;
    }

    var differenceVector = gridPoses[1] - gridPoses[0];

    var direction = new Vector2Int(Math.Sign(differenceVector.x), Math.Sign(differenceVector.y));
    if (direction.x == 0 && direction.y == 0 || direction.x != 0 && direction.y != 0) {
      Debug.LogError($"PushCard.doCardAction: direction is not valid: {direction}");

      yield break;
    }

    yield return ApplyForceComponent.applyForce(gridSystem, gridPoses[0], direction, force);
  }
}