using UnityEngine;

public abstract class AbstractTargetMode {
  public Card card { get; set; }
  
  public AbstractTargetMode(Card card) {
    this.card = card;
  }
  
  public abstract bool onClick(GridSystem gridSystem, SelectionResult selectionResult);

  public abstract SelectionResult drawIndicator(
    GridSystem gridSystem, 
    Vector2Int mouseCell,
    AbstractTargetCondition condition
  );
}