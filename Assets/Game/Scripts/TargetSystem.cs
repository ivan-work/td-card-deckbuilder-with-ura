using System.Diagnostics.CodeAnalysis;
using Architecture.Targeting.TargetMode;
using Intents;
using Intents.Engine;
using UnityEngine;

public class TargetSystem : MonoBehaviour {
  private GridSystem gridSystem = null!;
  private AbstractTargetMode? currentTargetMode;
  private IntentSystem? intentSystem;

  private void Awake() {
    gridSystem = this.AssertFind<GridSystem>();
    EventManager.Instance.CardClicked.AddListener(StartTargeting);
    EventManager.Instance.ImsStartRequestIntent.AddListener(OnImsStartRequestIntent);
  }

  private void OnImsStartRequestIntent(IntentSystem iSystem) {
    this.intentSystem = iSystem;
  }

  private void StartTargeting(Card card) {
    // Debug.Log($"TargetSystem.StartTargeting({intentManagementSystem}, {intentManagementSystem != null})");
    if (intentSystem) {
      currentTargetMode = TargetModesHelper.createTargetMode(card);
    }
  }

  private void StopTargeting() {
    currentTargetMode = null;
    CellIndicatorObjectPool.SharedInstance.reset();
  }

  private void Update() {
    if (currentTargetMode is not null && intentSystem is not null) {
      var targetCondition = currentTargetMode.card.targetCondition[0];

      var selectionResult = currentTargetMode.drawIndicator(
        gridSystem,
        mouseCell: getMouseCell(),
        targetCondition
      );

      if (GameManager.Instance.watchPlayersActions) {
        if (Input.GetMouseButtonDown(1)) {
          StopTargeting();
        }

        if (Input.GetMouseButtonDown(0)) {
          // Debug.Log($"CLICK HAPPENED, valid: {selectionResult.isValid}");
          if (selectionResult.IsValid) {
            var shouldEndTargeting = currentTargetMode.onClick(
              gridSystem,
              selectionResult
            );
            // Debug.Log($"shouldEndTargeting {shouldEndTargeting}");

            if (shouldEndTargeting) {
              currentTargetMode.card.DoCardAction(
                new IntentGlobalContext() {
                  GridSystem = gridSystem,
                  IntentSystem = intentSystem
                },
                selectionResult.AffectedCells
              );

              StopTargeting();

              EventManager.Instance.PhasePlayerIntent.Invoke();
            }
          } else {
            StopTargeting();
          }
        }
      }
    }
  }

  private Vector2Int getMouseCell() {
    // Debug.Log($"{Input.mousePosition}, {Camera.main.ScreenToWorldPoint(Input.mousePosition, 0)}, {Camera.main.ScreenToWorldPoint(Input.mousePosition)}");
    Vector3Int cellPosition = gridSystem.grid.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    return ((Vector2Int) cellPosition);
  }
}
