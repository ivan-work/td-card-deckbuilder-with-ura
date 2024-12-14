using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class TargetSystem : MonoBehaviour {
  [SerializeField] private GridSystem gridSystem;

  AbstractTargetMode currentTargetMode;
  [SerializeField] ActorManager actorManager;

  private void Awake() {
    Debug.Log("TargetSystem.Awake()");
    EventManager.CardClicked.AddListener(OnCardClicked);
    EventManager.AmStartRequestIntent.AddListener(OnAmStartRequestIntent);
  }

  private void OnAmStartRequestIntent(ActorManager _actorManager) {
    Debug.Log($"TargetSystem.OnAmStartRequestIntent({_actorManager})");
    actorManager = _actorManager;
    Debug.Log($"TargetSystem.OnAmStartRequestIntent({actorManager != null})");
  }

  public void OnCardClicked(Card card) {
    StartTargeting(card);
  }

  void StartTargeting(Card card) {
    Debug.Log($"TargetSystem.StartTargeting({actorManager}, {actorManager != null})");
    if (actorManager) {
      currentTargetMode = TargetModesHelper.createTargetMode(card);
    }
  }

  void StopTargeting() {
    actorManager = null;
    currentTargetMode = null;
    CellIndicatorObjectPool.SharedInstance.reset();
  }

  private void Update() {
    if (currentTargetMode != null) {
      var targetCondition = currentTargetMode.card.targetCondition[0];

      var selectionResult = currentTargetMode.drawIndicator(
        gridSystem,
        mouseCell: GetMouseCell(),
        targetCondition
      );

      if (GameManager.Instance.watchPlayersActions) {
        if (Input.GetMouseButtonDown(1)) {
          StopTargeting();
        }

        if (Input.GetMouseButtonDown(0)) {
          // Debug.Log($"CLICK HAPPENED, valid: {selectionResult.isValid}");
          if (selectionResult.isValid) {
            var shouldEndTargeting = currentTargetMode.onClick(
              gridSystem,
              selectionResult
            );
            // Debug.Log($"shouldEndTargeting {shouldEndTargeting}");

            if (shouldEndTargeting) {
              actorManager.queuedEffects = new LinkedList<BaseEffect>(
                currentTargetMode.card.doCardAction(
                    gridSystem,
                    selectionResult.affectedCells
                  )
                  .Concat(actorManager.queuedEffects)
              );


              StopTargeting();

              EventManager.PhasePlayerIntent.Invoke();
            }
          } else {
            StopTargeting();
          }
        }
      }
    }
  }

  Vector2Int GetMouseCell() {
    // Debug.Log($"{Input.mousePosition}, {Camera.main.ScreenToWorldPoint(Input.mousePosition, 0)}, {Camera.main.ScreenToWorldPoint(Input.mousePosition)}");
    Vector3Int cellPosition = gridSystem.grid.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    return ((Vector2Int) cellPosition);
  }
}