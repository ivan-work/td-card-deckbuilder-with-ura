using System;
using System.Linq;
using Components;
using Effects.EffectAnimations;
using Intents.Engine;
using Intents.IReactions;
using UnityEngine;

namespace Intents.IntentBehaviours {
  [CreateAssetMenu(fileName = "IntentBehaviours/MoveIntentBehaviour")]
  public class MoveIntentBehaviour : IntentBehaviour<MoveIntentValues> {
    protected override void Perform(Intent<MoveIntentValues> intent, IntentProgressContext context) {
      // var direction = intent.Targets.Positions.FirstOrDefault(null);
      // if (intent.Source.TryGetComponent<GridComponent>(out var gridComponent) && direction is not null) {
      //   var sourcePos = gridComponent.gridPos;
      //   var targetPos = direction.Value + sourcePos;
      //   var gridSystem = context.GlobalContext.GridSystem;
      //
      //   bool hasPath = gridSystem.GetGridEntities<PathComponent>(targetPos).Any();
      //   bool hasMob = gridSystem.GetGridEntities<MoveComponent>(targetPos).Any();
      //
      //   if (hasPath && !hasMob) {
      //     context.Animation = new MoveAnimation(intent.Source, gridSystem.gridPos2World(sourcePos),
      //       gridSystem.gridPos2World(targetPos));
      //     gridComponent.moveTo(targetPos);
      //     sendEvents(context.GlobalContext, intent.Source, targetPos);
      //   } else {
      //     context.Animation = new MoveAttemptAnimation(intent.Source, gridSystem.gridPos2World(sourcePos),
      //       gridSystem.gridPos2World(targetPos));
      //   }
      // }
    }

    private static void sendEvents(IntentGlobalContext context, GameObject source, Vector2Int targetPos) {
      context.GridSystem.GetGridEntities<IReactToEntityEnter>(targetPos)
        .ToList()
        .ForEach(component => component.OnEntityEnter(context, source));

      source.GetComponents<IReactToMove>().ToList().ForEach(component => component.OnMove(context));
    }
  }

  [Serializable]
  public class MoveIntentValues : IntentValues { }
}
