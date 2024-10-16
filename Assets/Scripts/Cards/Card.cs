using System;
using TMPro;
using UnityEngine;

public class Card : ScriptableObject {
  [SerializeField] public string cardName;

  public virtual void doCardAction(GridSystem gridSystem, Vector2Int vector2Int) {
    throw new NotImplementedException();
  }

  public virtual bool isValidTarget(GridSystem gridSystem, Vector2Int vector2Int) {
    throw new NotImplementedException();
  }
  // [SerializeField] public GameObject cardPrefab;

  // public void OnInstantiate() {
  //   cardPrefab.GetComponentInChildren<TextMeshPro>().text = $"{cardName}";
  // }
}