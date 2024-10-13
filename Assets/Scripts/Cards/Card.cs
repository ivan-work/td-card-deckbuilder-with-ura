using System;
using TMPro;
using UnityEngine;

public class Card : ScriptableObject {
  [SerializeField] public string cardName;

  virtual public void onTargetClicked(GridSystem gridSystem, Vector2Int vector2Int) {
    throw new NotImplementedException();
  }
  // [SerializeField] public GameObject cardPrefab;

  // public void OnInstantiate() {
  //   cardPrefab.GetComponentInChildren<TextMeshPro>().text = $"{cardName}";
  // }
}