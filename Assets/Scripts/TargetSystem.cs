using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSystem : MonoBehaviour {
  [SerializeField] private GameObject mouseIndicator;
  [SerializeField] private InputManager inputManager;
  [SerializeField] private GridSystem gridSystem;

  [SerializeField] private GameObject towerPrefab;
  Card selectedCard;

  void Start() {
    StopTargeting();
    EventManager.OnCardClicked.AddListener(OnCardClicked);
  }

  public void OnCardClicked(Card card) {
    StartTargeting(card);
  }

  private void Update() {
    if (selectedCard != null) {
      mouseIndicator.transform.position = gridSystem.grid.GetCellCenterWorld((Vector3Int)GetMouseCell());
      var isValid = selectedCard.isValidTarget(gridSystem, GetMouseCell());
      if (isValid) {
        mouseIndicator.GetComponent<SpriteRenderer>().color = Color.green;
      } else {
        mouseIndicator.GetComponent<SpriteRenderer>().color = Color.red;
      }


      if (isValid && Input.GetMouseButtonDown(0)) {
        selectedCard.onTargetClicked(
          gridSystem,
          GetMouseCell()
        );
        
        StopTargeting();
      }
    }
  }

  Vector2Int GetMouseCell() {
    // Debug.Log($"{Input.mousePosition}, {Camera.main.ScreenToWorldPoint(Input.mousePosition, 0)}, {Camera.main.ScreenToWorldPoint(Input.mousePosition)}");
    Vector3Int cellPosition = gridSystem.grid.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    return ((Vector2Int)cellPosition);
  }

  void StartTargeting(Card card) {
    selectedCard = card;
    mouseIndicator.SetActive(true);
  }

  void StopTargeting() {
    selectedCard = null;
    mouseIndicator.SetActive(false);
  }
}
