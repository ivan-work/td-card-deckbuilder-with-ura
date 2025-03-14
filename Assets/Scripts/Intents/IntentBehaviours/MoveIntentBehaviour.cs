using System;
using System.Linq;
using Components;
using Effects.EffectAnimations;
using Intents.Engine;
using Unity.Mathematics;
using UnityEngine;

namespace Intents.IntentBehaviours {
  [CreateAssetMenu]
  public class MoveIntentBehaviour : IntentBehaviour<MoveIntentValues> {
    public override void Perform(Intent<MoveIntentValues> intent, IntentProgressContext context) {
      var direction = intent.Targets.TargetPos;
      if (intent.Source.TryGetComponent<GridComponent>(out var gridComponent) && direction.HasValue) {
        var sourcePos = gridComponent.gridPos;
        var targetPos = direction.Value + sourcePos;
        var gridSystem = context.GlobalContext.GridSystem;

        bool hasPath = gridSystem.getGridEntitiesSpecial<PathComponent>(targetPos).Any();
        bool hasMob = gridSystem.getGridEntitiesSpecial<MoveComponent>(targetPos).Any();

        if (hasPath && !hasMob) {
          context.Animation = new MoveAnimation(intent.Source, gridSystem.gridPos2World(sourcePos),
            gridSystem.gridPos2World(targetPos));
          gridComponent.moveTo(targetPos);
          sendEvents(context.GlobalContext, intent.Source, targetPos);
        } else {
          context.Animation = new MoveAttemptAnimation(intent.Source, gridSystem.gridPos2World(sourcePos),
            gridSystem.gridPos2World(targetPos));
        }
      }
    }

    private static void sendEvents(IntentGlobalContext context, GameObject source, Vector2Int targetPos) {
      context.GridSystem.getGridEntitiesSpecial<IReactToEntityEnter>(targetPos)
        .ToList()
        .ForEach(component => component.OnEntityEnter(context, source));

      source.GetComponents<IReactToMove>().ToList().ForEach(component => component.OnMove(context));
    }
  }

  [Serializable]
  public class MoveIntentValues : IntentValues { }
}
