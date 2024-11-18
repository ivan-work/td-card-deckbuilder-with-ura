using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class TargetSystem : MonoBehaviour {
  [SerializeField] private GridSystem gridSystem;
  
  AbstractTargetMode currentTargetMode;

  private void Awake() {
    EventManager.CardClicked.AddListener(OnCardClicked);
  }

  void Start() {
    StopTargeting();
  }

  public void OnCardClicked(Card card) {
    StartTargeting(card);
  }

  void StartTargeting(Card card) {
    currentTargetMode = TargetModesHelper.createTargetMode(card);
  }

  void StopTargeting() {
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
      
      if (!GameManager.Instance.isBusy) {
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
              StopTargeting();
              GameManager.Instance.EndTurn();
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
    return ((Vector2Int)cellPosition);
  }
}
