using System;
using System.Linq;
using Components;
using Effects.EffectAnimations;
using GridSystem;
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
        var sourcePos = gridComponent.GridLoc;
        var targetPos = direction.Value + sourcePos;
        var gridSystem = context.GlobalContext.GridSystem;

        var hasPath = gridSystem.GetGridEntities<PathComponent>(targetPos).Any();
        var hasMob = gridSystem.GetGridEntities<MoveComponent>(targetPos).Any();

        if (hasPath && !hasMob) {
          context.Animation = new MoveAnimation(intent.Source,
            gridSystem.GridLoc2World(sourcePos),
            gridSystem.GridLoc2World(targetPos));
          gridComponent.MoveTo(targetPos);
          sendEvents(context.GlobalContext, intent.Source, targetPos);
        } else {
          context.Animation = new MoveAttemptAnimation(intent.Source,
            gridSystem.GridLoc2World(sourcePos),
            gridSystem.GridLoc2World(targetPos));
        }
      }
    }

    protected override GameObject? CreateDisplay(Intent<MoveIntentValues> intent) {
      if (_display is not null) {
        var direction = GetDirection(intent);
        if (intent.Source.TryGetComponent<GridComponent>(out var gridComponent) && direction.HasValue) {
          var sourceLoc = gridComponent.GridLoc;
          var targetLoc = direction.Value + sourceLoc;
          var sourcePosition = intent.Source.transform.position;
          var targetPosition = gridComponent.GridSystem.GridLoc2World(targetLoc);

          var rotation = Quaternion.LookRotation(targetPosition - sourcePosition, Vector3.up);

          return Instantiate(_display, sourcePosition, rotation);
        }
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
