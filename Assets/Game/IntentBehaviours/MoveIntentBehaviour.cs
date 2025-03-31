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
    [SerializeField] private GameObject? _display;
    protected override void Perform(Intent<MoveIntentValues> intent, IntentProgressContext context) {

      Vector2Int? direction = GetDirection(intent);
      if (intent.Source.TryGetComponent<GridComponent>(out var gridComponent) && direction is not null) {
        var sourcePos = gridComponent.gridLoc;
        var targetPos = direction.Value + sourcePos;
        var gridSystem = context.GlobalContext.GridSystem;
      
        var hasPath = gridSystem.GetGridEntities<PathComponent>(targetPos).Any();
        var hasMob = gridSystem.GetGridEntities<MoveComponent>(targetPos).Any();
      
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

    public override GameObject? CreateDisplay(Intent<MoveIntentValues> intent, IntentGlobalContext context) {
      if (_display is not null) {
        Vector2Int? direction = GetDirection(intent);
        if (intent.Source.TryGetComponent<GridComponent>(out var gridComponent) && direction is not null) {
          var sourcePos = gridComponent.gridLoc;
          var targetPos = direction.Value + sourcePos;
          
          //var rotation = Quaternion.LookRotation(gridSystem.gridPos2World(sourcePos), Vector3.up);
        }

        GameObject display = Instantiate(_display, intent.Source.transform);
        return display;
      }
      return null;
    }

    private static Vector2Int? GetDirection(Intent<MoveIntentValues> intent) {
      return intent.Targets.Locations.FirstOrDefault();
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
