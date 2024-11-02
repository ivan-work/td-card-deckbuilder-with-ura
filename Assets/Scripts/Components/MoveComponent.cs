using System;
using System.Collections;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using Random = UnityEngine.Random;

public class MoveComponent : MonoBehaviour {
  private enum State {
    Calm,
    Charged,
    Tired
  }

  private GridComponent gridComponent;
  private State state = State.Calm;

  void Awake() {
    gridComponent = this.GetAssertComponent<GridComponent>();
    EventManager.PhaseMove.AddListener(OnPhaseMove);
    EventManager.EndTurn.AddListener(() => state = State.Calm);
  }
  
  private void OnDestroy() {
    EventManager.PhaseMove.RemoveListener(OnPhaseMove);
  }

  private IEnumerator OnPhaseMove() {
    startMovingChain();
    
    yield break;
  }

  private void startMovingChain() {
    if (state == State.Tired) return;
    
    state = State.Charged;

    var targetPos = findPath();

    if (!targetPos.HasValue) {
      state = State.Tired;
      return;
    }

    var canMove = checkIfNextChainMoved(targetPos.Value); // Двигает других мобов
    
    if (canMove) {
      changePositionToTargetPos(targetPos.Value);
    }
    state = State.Tired;
  }

  private bool checkIfNextChainMoved(Vector2Int targetPos) {
    var targetMoveComponent = findTargetMoveComponent(targetPos);

    if (!targetMoveComponent) {
      // Никого нет, можно ходить
      return true;
    }

    var targetState = targetMoveComponent.state;

    if (targetState == State.Calm) {
      // Другой чел чилит, заряжаем его
      targetMoveComponent.startMovingChain();

      if (targetPos != targetMoveComponent.gridComponent.gridPos) {
        // Другой чел походил, ходим сами его
        return true;
      }
    } else if (targetState == State.Charged) {
      // Другой чел заряжен = мы наткнулись на цикл, если мы ходим, то и он свалит
      return true;
    }

    return false;
  }

  private void changePositionToTargetPos(Vector2Int targetPos) {
    var sourcePos = gridComponent.gridPos;
    
    StartCoroutine(
      CorouTweens.LerpWithSpeed(
        gridComponent.gridPos2World(sourcePos),
        gridComponent.gridPos2World(targetPos),
        2,
        (value) => transform.position = value
      )
    );
    
    gridComponent.moveTo(targetPos);
  }

  [CanBeNull]
  private MoveComponent findTargetMoveComponent(Vector2Int targetPos) {
    return gridComponent.gridSystem.getGridEntities(targetPos)
      .Select(entity => entity.GetComponent<MoveComponent>())
      .FirstOrDefault(entity => entity);
  }
  
  private Vector2Int? findPath() {
    var mobCell = gridComponent.gridSystem.getGridEntities(gridComponent.gridPos)
      .Select(entity => entity.GetComponent<PathComponent>())
      .FirstOrDefault(entity => entity.GetComponent<PathComponent>());

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