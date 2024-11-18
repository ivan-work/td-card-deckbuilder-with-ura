using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class Card : ScriptableObject {
  [SerializeField] public string cardName;
  [SerializeField] public TargetModesHelper.TargetMode targetMode;
  [SerializeField] public AbstractTargetCondition[] targetCondition;

  public virtual IEnumerator doCardAction(GridSystem gridSystem, Vector2Int[] affectedCells) {
    throw new NotImplementedException();
  }
}