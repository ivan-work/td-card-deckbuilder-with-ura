using System;
using System.Linq;
using Components;
using Effects;
using GridSystem;
using Intents;
using Intents.Engine;
using Intents.IntentBehaviours;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class MoveComponent : MonoBehaviour, IHasIntent {
  [FormerlySerializedAs("_moveIntentBehaviour")] [SerializeField] StepIntentBehaviour _stepIntentBehaviour; // #TODO FIXNULL

  private enum State {
    Calm,
    Charged,
    Tired
  }

  private State state = State.Calm;

  private GridComponent gridComponent;

  private void Awake() {
    gridComponent = this.GetAssertComponent<GridComponent>();
    EventManager.Instance.PhasePerformIntents.AddListener(() => state = State.Calm);
  }

  public void WriteIntents(IIntentHolder intentHolder) {
    startMovingChain(intent => {
      // Debug.Log($"Adding {intent}");
      intentHolder.AddIntents(intent);
    });
  }


  private void startMovingChain(Action<Intent> addIntent) {
    // Debug.Log($"MoveComponent(pos: {gridComponent.GridLoc}, state: {state}).startMovingChain()");
    if (state == State.Tired) return;

    state = State.Charged;

    var targetPos = findPath();

    if (!targetPos.HasValue) {
      state = State.Tired;
      return;
    }

    TryChargeNextTarget(addIntent, targetPos.Value); // Двигает других мобов

    state = State.Tired;


    addIntent(new Intent(
      source: gameObject,
      behaviour: _stepIntentBehaviour,
      values: new IntentValues(),
      targets: IntentTargets.Create(targetPos.Value - gridComponent.GridLoc)
    ));
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
    return gridComponent.GridSystem.GetGridEntities(targetPos)
      .Select(entity => entity.GetComponent<MoveComponent>())
      .FirstOrDefault(entity => entity);

    // если у этих мув компонентов есть мув эффект, то в целом там никого и нет так-то
  }

  private Vector2Int? findPath() {
    var mobCell = gridComponent.GridSystem.GetGridEntities(gridComponent.GridLoc)
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
        return minimumNeighbor.GetComponent<GridComponent>().GridLoc;
      }
    }

    return null;
  }
}
