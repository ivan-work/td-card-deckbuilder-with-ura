using System;
using UnityEngine;

namespace Abilities {
  [Serializable]
  public abstract class BaseTargetCondition: ScriptableObject, ITargetCondition {
    public abstract bool isValidTarget(ITarget target);
  }


}
