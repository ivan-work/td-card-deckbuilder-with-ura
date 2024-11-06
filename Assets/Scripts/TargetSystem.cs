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
  }

  private void Update() {
    
    if (currentTargetMode != null) {
      var targetCondition = currentTargetMode.card.targetCondition[0];

      var selectionResult = currentTargetMode.drawIndicator(
        gridSystem,
        mouseCell: GetMouseCell(),
        targetCondition
      );
      
      // Двигать курсор
      
      
      if (!GameManager.Instance.isBusy) {
        // var mouseCell = GetMouseCell();

      
        // var selectionResult = currentTargetMode.card.shapeMode.drawIndicator(
        //   gridSystem: gridSystem,
        //   mouseIndicator: mouseIndicator, 
        //   centerGridPos: mouseCell,
        //   condition: targetCondition
        // );

        if (Input.GetMouseButton(0)) {
          if (selectionResult.isValid) {
            var result = currentTargetMode.onClick(
              gridSystem,
              selectionResult
            );

            if (result) {
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
