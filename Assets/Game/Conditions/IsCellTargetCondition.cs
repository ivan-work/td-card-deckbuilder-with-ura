using System;

namespace Abilities {
  [Serializable]
  
  public class IsCellTargetCondition : BaseTargetCondition {
    public override bool isValidTarget(ITarget target) {
      return target.IsCell;
    }
  }
}
