using System;
using System.Collections.Generic;
using System.Linq;
using Effects;
using Intents.Engine;
using Intents.IntentBehaviours;
using UnityEngine;

namespace Cards {
  [CreateAssetMenu(menuName = "Card/PushCard")]
  public class PushCard : Card {
    [SerializeField] private PushIntentBehaviour PushIntentBehaviour;
    [SerializeField] private int Force = 1;

    public override void DoCardAction(IntentGlobalContext context, Vector2Int[] gridPoses) {
      if (gridPoses.Length < 2) {
        Debug.LogError($"PushCard.DoCardAction: gridPoses is not valid: {gridPoses}");
      }
      
      var sourcePos = gridPoses[0];
      var targetPos = gridPoses[1];
      
      var differenceVector = targetPos - sourcePos;

      var direction = new Vector2Int(Math.Sign(differenceVector.x), Math.Sign(differenceVector.y));
      if (direction.x == 0 && direction.y == 0 || direction.x != 0 && direction.y != 0) {
        Debug.LogError($"PushCard.DoCardAction: direction is not valid: {direction}");

      }

      // yield return ApplyForceComponent.applyForce(gridSystem, gridPoses[0], direction, Force);
    
      var intents = context.GridSystem.getGridEntitiesSpecial<MoveComponent>(sourcePos)
        .Select(component => new Intent {
          Behaviour = PushIntentBehaviour,
          Values = new PushIntentValues(Force),
          Targets = new IntentTargets(component.gameObject, direction)
        })
        .ToArray();
      context.IntentSystem.AddImmediateIntents(intents);
    }
  }
}
