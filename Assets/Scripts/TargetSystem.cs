using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Architecture;
using Intents;
using Intents.Engine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class TargetSystem : MonoBehaviour {
  [SerializeField] private GridSystem gridSystem;

  AbstractTargetMode currentTargetMode;

  private IntentManagementSystem intentManagementSystem;

  private void Awake() {
    Debug.Log("TargetSystem.Awake()");
    EventManager.CardClicked.AddListener(StartTargeting);
    EventManager.ImsStartRequestIntent.AddListener(OnImsStartRequestIntent);
  }

  private void OnImsStartRequestIntent(IntentManagementSystem _intentManagementSystem) {
    intentManagementSystem = _intentManagementSystem;
  }

  private void StartTargeting(Card card) {
    // Debug.Log($"TargetSystem.StartTargeting({intentManagementSystem}, {intentManagementSystem != null})");
    if (intentManagementSystem) {
      currentTargetMode = TargetModesHelper.createTargetMode(card);
    }
  }

  private void StopTargeting() {
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
              currentTargetMode.card.DoCardAction(
                new IntentGlobalContext() {
                  GridSystem = gridSystem,
                  IntentManagementSystem = intentManagementSystem
                },
                selectionResult.affectedCells
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
