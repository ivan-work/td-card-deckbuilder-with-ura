namespace Abilities {
  public class IsCellTargetCondition : ITargetCondition {
    public bool isValidTarget(ITarget target) {
      return target.IsCell;
    }
  }
}
