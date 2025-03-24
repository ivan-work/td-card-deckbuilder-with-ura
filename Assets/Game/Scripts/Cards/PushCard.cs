using System;
using System.Linq;
using Intents.Engine;
using Intents.IntentBehaviours;
using UnityEngine;

namespace Cards {
  public class PushCard : Card {
    [SerializeField] private PushIntentBehaviour _pushIntentBehaviour = null!;
    [SerializeField] private int _force = 1;

    private void Awake() {
      // #TODO Nullable
      NullableHelper.ThrowWhenNull(_pushIntentBehaviour);
      this.ThrowWhenNull(_pushIntentBehaviour);
    }

    // public override void DoCardAction(IntentGlobalContext context, Vector2Int[] gridPoses) {
    //   if (gridPoses.Length < 2) {
    //     Debug.LogError($"PushCard.DoCardAction: gridPoses is not valid: {gridPoses}");
    //   }
    //
    //   var sourcePos = gridPoses[0];
    //   var targetPos = gridPoses[1];
    //
    //   var differenceVector = targetPos - sourcePos;
    //
    //   var direction = new Vector2Int(Math.Sign(differenceVector.x), Math.Sign(differenceVector.y));
    //   if (direction.x == 0 && direction.y == 0 || direction.x != 0 && direction.y != 0) {
    //     Debug.LogError($"PushCard.DoCardAction: direction is not valid: {direction}");
    //   }
    //
    //   // yield return ApplyForceComponent.applyForce(gridSystem, gridPoses[0], direction, Force);
    //
    //   var intents = context.GridSystem.GetGridEntities<MoveComponent>(sourcePos)
    //     .Select(
    //       component => new Intent { Behaviour = _pushIntentBehaviour, Values = new PushIntentValues(_force), Targets = IntentTargets.Create(component.gameObject, direction) }
    //     )
    //     .ToArray();
    //   context.IntentSystem.AddImmediateIntents(intents);
    // }
  }
}
