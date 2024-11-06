using System;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Architecture/Targeting/Condition/PathTargetCondition")]
public class PathTargetCondition : AbstractTargetCondition {
  [SerializeField] private Type componentType = typeof(PathComponent);

  public override bool isValidTarget(GridSystem gridSystem, Vector2Int gridPos) {
    return gridSystem.getGridEntities(gridPos).FirstOrDefault(entity => entity.GetComponent(componentType));
  }
}