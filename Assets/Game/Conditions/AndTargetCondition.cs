using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Abilities {
  [Serializable]
  public class AndTargetCondition : BaseTargetCondition {
    [SerializeField] private List<BaseTargetCondition> Conditions;

    public override bool isValidTarget(ITarget target) {
      return Conditions.All(condition => condition.isValidTarget(target));
    }
  }
}
