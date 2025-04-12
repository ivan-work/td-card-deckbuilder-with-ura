using System.Linq;
using Effects.EffectAnimations;
using GridSystem;
using Intents;
using Intents.Engine;
using Intents.IReactions;
using UnityEngine;

namespace IntentBehaviours {
  public abstract class MoveIntentBehaviour : IntentBehaviour<IntentValues> {
    [SerializeField] private LineRenderer? _display;

    protected override void Perform(Intent<IntentValues> intent, IntentProgressContext context) {
      var targetLocNullable = GetTargetLoc(intent);
      if (intent.Source.TryGetComponent<GridComponent>(out var gridComponent) && targetLocNullable is not null) {
        var targetLoc = targetLocNullable.Value;

        var sourcePosition = intent.Source.transform.position;
        var targetPosition = gridComponent.GridSystem.GridLoc2World(targetLoc);

        var gridSystem = context.GlobalContext.GridSystem;
        var hasPath = gridSystem.GetGridEntities<PathComponent>(targetLoc).Any();
        var hasMob = gridSystem.GetGridEntities<MoveComponent>(targetLoc).Any();

        if (hasPath && !hasMob) {
          context.Animation = new MoveAnimation(intent.Source, sourcePosition, targetPosition);
          gridComponent.MoveTo(targetLoc);
          sendEvents(context.GlobalContext, intent.Source, targetLoc);
        } else {
          context.Animation = new MoveAttemptAnimation(intent.Source, sourcePosition, targetPosition);
        }
      }
    }


    protected override GameObject? CreateDisplay(Intent<IntentValues> intent) {
      if (_display) {
        var targetLoc = GetTargetLoc(intent);
        if (intent.Source.TryGetComponent<GridComponent>(out var gridComponent) && targetLoc.HasValue) {
          var sourcePosition = intent.Source.transform.position;
          var targetPosition = gridComponent.GridSystem.GridLoc2World(targetLoc.Value);

          var display = Instantiate(_display);
          display.positionCount = 2;
          display.SetPositions(new[] { sourcePosition, targetPosition });

          return display.gameObject;
        }
      }

      return null;
    }

    protected abstract Vector2Int? GetTargetLoc(Intent<IntentValues> intent);


    private static void sendEvents(IntentGlobalContext context, GameObject source, Vector2Int targetPos) {
      context.GridSystem.GetGridEntities<IReactToEntityEnter>(targetPos).ToList().ForEach(component => component.OnEntityEnter(context, source));

      source.GetComponents<IReactToMove>().ToList().ForEach(component => component.OnMove(context));
    }
  }
}
