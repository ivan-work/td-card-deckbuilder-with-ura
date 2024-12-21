using System;
using System.Collections.Generic;
using UnityEngine;

public class Card : ScriptableObject {
  [SerializeField] public string cardName;
  [SerializeField] public TargetModesHelper.TargetMode targetMode;
  [SerializeField] public AbstractTargetCondition[] targetCondition;

  public virtual IEnumerable<BaseEffect> doCardAction(GridSystem gridSystem, Vector2Int[] affectedCells) {
    throw new NotImplementedException();
  }
}
