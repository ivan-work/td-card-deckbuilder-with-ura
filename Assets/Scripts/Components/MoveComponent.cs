using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using Random = UnityEngine.Random;

public class MoveComponent : MonoBehaviour, IHasIntent {
  private enum State {
    Calm,
    Charged,
    Tired
  }

  public GridComponent gridComponent;
  private State state = State.Calm;

  void Awake() {
    gridComponent = this.GetAssertComponent<GridComponent>();
    EventManager.EndTurn.AddListener(() => state = State.Calm);
  }

  public IEnumerable<BaseEffect> getIntents() {
    List<BaseEffect> intents = new();
    startMovingChain(effect => {
      Debug.Log($"Adding {effect}");
      intents.Add(effect);
    });
    return intents;
  }

  private bool startMovingChain(Action<BaseEffect> addEffect) {
    Debug.Log($"MoveComponent(pos: {gridComponent.gridPos}, state: {state}).startMovingChain()");
    if (state == State.Tired) return false;

    state = State.Charged;

    var targetPos = findPath();

    if (!targetPos.HasValue) {
      state = State.Tired;
      return false;
    }

    var canMove = checkIfNextChainMoved(addEffect, targetPos.Value); // Двигает других мобов
    
    state = State.Tired;

    if (canMove) {
      addEffect(new MoveEffect(this, targetPos.Value));
    }

    return canMove;
  }

  private bool checkIfNextChainMoved(Action<BaseEffect> addEffect, Vector2Int targetPos) {
    var targetMoveComponent = findTargetMoveComponent(targetPos);

    if (!targetMoveComponent) {
      Debug.Log($".checkIfNextChainMoved: Никого нет, можно ходить => true");
      // Никого нет, можно ходить
      return true;
    }

    var targetState = targetMoveComponent.state;

    if (targetState == State.Calm) {
      Debug.Log($".checkIfNextChainMoved: Другой чел чилит, заряжаем его => ???");
      // Другой чел чилит, заряжаем его
      return targetMoveComponent.startMovingChain(addEffect);
    } else if (targetState == State.Charged) {
      
      // Другой чел заряжен = мы наткнулись на цикл, если мы ходим, то и он свалит
      Debug.Log($".checkIfNextChainMoved: Другой чел заряжен => true");
      return true;
    }

    Debug.Log($".checkIfNextChainMoved: false");
    return false;
  }

  [CanBeNull]
  private MoveComponent findTargetMoveComponent(Vector2Int targetPos) {
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