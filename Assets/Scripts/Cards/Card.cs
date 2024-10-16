using System;
using TMPro;
using UnityEngine;

public class Card : ScriptableObject {
  [SerializeField] public string cardName;

  virtual public void doCardAction(GridSystem gridSystem, Vector2Int vector2Int) {
    throw new NotImplementedException();
  }

  virtual public bool isValidTarget(GridSystem gridSystem, Vector2Int vector2Int) {
    throw new NotImplementedException();
  }
  // [SerializeField] public GameObject cardPrefab;

  // public void OnInstantiate() {
  //   cardPrefab.GetComponentInChildren<TextMeshPro>().text = $"{cardName}";
  // }
}