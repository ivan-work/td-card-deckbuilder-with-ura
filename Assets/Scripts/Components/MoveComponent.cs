using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
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
    EventManager.PhaseMove.AddListener(XXX);
  }
  
  private void OnDestroy() {
    EventManager.PhaseMove.RemoveListener(XXX);
  }

  private void XXX() {
    if (state == State.Tired) return;
    
    state = State.Charged;

    var targetPos = findPath();

    if (!targetPos.HasValue) {
      state = State.Tired;
      return;
    }

    var targetMoveComponent = findTargetMoveComponent(targetPos.Value);

    if (!targetMoveComponent) {
      doMove(targetPos.Value);
      return;
    }

    var targetState = targetMoveComponent.state;

    switch (targetState) {
      case State.Calm:
        targetMoveComponent.state = State.Charged;
        targetMoveComponent.XXX();
        break;
      case State.Charged:
        break;
      case State.Tired:
        state = State.Tired;
        return;
      default:
        throw new ArgumentOutOfRangeException();
    }
  }

  void doMove(Vector2Int targetPos) {
    var sourcePos = gridComponent.gridPos;
    
    StartCoroutine(
      CorouTweens.LerpWithSpeed(
        gridComponent.gridPos2World(sourcePos),
        gridComponent.gridPos2World(targetPos),
        2,
        (value) => transform.position = value
      )
    );
    
    state = State.Tired;
    gridComponent.moveTo(targetPos);
  }

  private MoveComponent findTargetMoveComponent(Vector2Int targetPos) {
    return gridComponent.gridSystem.getGridEntities(targetPos)
      .Select(entity => entity.GetComponent<MoveComponent>())
      .First(entity => entity);
  }
  
  private Vector2Int? findPath() {
    var mobCell = gridComponent.gridSystem.getGridEntities(gridComponent.gridPos)
      .Select(entity => entity.GetComponent<PathComponent>())
      .First(entity => entity);

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