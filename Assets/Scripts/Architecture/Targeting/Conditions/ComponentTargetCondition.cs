using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Architecture/Targeting/Condition/ComponentTargetCondition")]
public class ComponentTargetCondition : AbstractTargetCondition {
  [SerializeField] private IGameComponent script;
  
  public override bool isValidTarget(GridSystem gridSystem, Vector2Int gridPos) {
    return gridSystem.getGridEntities(gridPos).FirstOrDefault(entity => entity.GetComponent(script.GetType()));
  }
}