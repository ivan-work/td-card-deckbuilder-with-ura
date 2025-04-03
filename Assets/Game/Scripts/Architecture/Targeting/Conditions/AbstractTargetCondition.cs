using UnityEngine;

public abstract class AbstractTargetCondition : ScriptableObject {
  public abstract bool isValidTarget(GridSystem.GridSystem gridSystem, Vector2Int gridPos);
}