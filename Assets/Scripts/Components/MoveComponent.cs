using System;
using System.Linq;
using Components;
using Effects;
using Intents;
using Intents.Engine;
using Intents.IntentBehaviours;
using JetBrains.Annotations;
using UnityEngine;
using Random = UnityEngine.Random;

public class MoveComponent : MonoBehaviour, IHasIntent {
  [SerializeField] MoveIntentBehaviour _moveIntentBehaviour; // #TODO FIXNULL

  private enum State {
    Calm, Charged, Tired
  }

  private State state = State.Calm;

  private GridComponent gridComponent;

  private void Awake() {
    gridComponent = this.GetAssertComponent<GridComponent>();
    EventManager.PhasePlayerIntent.AddListener(() => state = State.Calm);
  }

  public void WriteIntents(IntentSystem intentSystem) {
    startMovingChain(intent => {
      // Debug.Log($"Adding {intent}");
      intentSystem.AddIntents(intent);
    });
  }


  private void startMovingChain(Action<Intent> addIntent) {
    // Debug.Log($"MoveComponent(pos: {gridComponent.gridPos}, state: {state}).startMovingChain()");
    if (state == State.Tired) return;

    state = State.Charged;

    var targetPos = findPath();

    if (!targetPos.HasValue) {
      state = State.Tired;
      return;
    }

    TryChargeNextTarget(addIntent, targetPos.Value); // Двигает других мобов

    state = State.Tired;


    addIntent(new Intent {
      Source = gameObject,
      Behaviour = _moveIntentBehaviour,
      Values = new MoveIntentValues(),
      Targets = new IntentTargets(null, targetPos.Value - gridComponent.gridPos),
    });
  }

  private void TryChargeNextTarget(Action<Intent> addIntent, Vector2Int targetPos) {
    var targetMoveComponent = findTargetMoveComponent(targetPos);
    if (targetMoveComponent?.state == State.Calm) {
      Debug.Log($".checkIfNextChainMoved: Другой чел чилит, заряжаем его => ???");
      // Другой чел чилит, заряжаем его
      targetMoveComponent.startMovingChain(addIntent);
    }
  }

  private MoveComponent? findTargetMoveComponent(Vector2Int targetPos) {
    return gridComponent.gridSystem.getGridEntities(targetPos)
      .Select(entity => entity.GetComponent<MoveComponent>())
      .FirstOrDefault(entity => entity);

    // если у этих мув компонентов есть мув эффект, то в целом там никого и нет так-то
  }

  private Vector2Int? findPath() {
    var mobCell = gridComponent.gridSystem.getGridEntities(gridComponent.gridPos)
      .Select(entity => entity.GetComponent<PathComponent>())
      .FirstOrDefault(entity => entity);

    if (mobCell != null) {
      var minimumNeighbor = mobCell.getNeighbors()
        .Aggregate((prev, next) => {
          if (Math.Abs(prev.distanceToBase - next.distanceToBase) < 0.1) {
            return Random.value < 0.5f ? prev : next;
          }

          return prev.distanceToBase < next.distanceToBase ? prev : next;
        });

      if (minimumNeighbor != null) {
        return minimumNeighbor.GetComponent<GridComponent>().gridPos;
      }
    }

    return null;
  }
}
