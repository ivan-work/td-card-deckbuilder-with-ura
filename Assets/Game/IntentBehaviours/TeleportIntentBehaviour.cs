using System;
using System.Linq;
using Components;
using Effects.EffectAnimations;
using GridSystem;
using Intents;
using Intents.Engine;
using Intents.IReactions;
using UnityEngine;

namespace IntentBehaviours {
  [CreateAssetMenu(fileName = "IntentBehaviours/TeleportIntentBehaviour")]
  public class TeleportIntentBehaviour: IntentBehaviour<IntentValues> {
    protected override void Perform(Intent<IntentValues> intent, IntentProgressContext context) {
      Vector2Int? target = intent.Targets.Locations.FirstOrDefault();
      
      if (intent.Source.TryGetComponent<GridComponent>(out var gridComponent) && target is not null) {
        var sourcePos = gridComponent.GridLoc;
        var targetPos = target.Value;
        var gridSystem = context.GlobalContext.GridSystem;
      
        var hasPath = gridSystem.GetGridEntities<PathComponent>(targetPos).Any();
        var hasMob = gridSystem.GetGridEntities<MoveComponent>(targetPos).Any();
      
        if (hasPath && !hasMob) {
          context.Animation = new MoveAnimation(intent.Source, gridSystem.GridLoc2World(sourcePos),
            gridSystem.GridLoc2World(targetPos));
          gridComponent.MoveTo(targetPos);
          sendEvents(context.GlobalContext, intent.Source, targetPos);
        } else {
          context.Animation = new MoveAttemptAnimation(intent.Source, gridSystem.GridLoc2World(sourcePos),
            gridSystem.GridLoc2World(targetPos));
        }
      }
    }
    //#TODO Убрать дублирование метода
    private static void sendEvents(IntentGlobalContext context, GameObject source, Vector2Int targetPos) {
      context.GridSystem.GetGridEntities<IReactToEntityEnter>(targetPos)
        .ToList()
        .ForEach(component => component.OnEntityEnter(context, source));

      source.GetComponents<IReactToMove>().ToList().ForEach(component => component.OnMove(context));
    }
  }
}
