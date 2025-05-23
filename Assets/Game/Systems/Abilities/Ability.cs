using System.Collections.Generic;
using System.Linq;
using Intents;
using UnityEngine;
using UnityEngine.Serialization;

namespace Abilities {
  public abstract class Ability : ScriptableObject {
    [SerializeField] public string Name = null!;
    [SerializeField] public Texture2D Icon = null!;
    [SerializeField] public BaseTargetCondition? Condition;
    [SerializeField] public IntentFactory IntentFactory = null!;

    public abstract ITargetingContext CreateTargetingContext();

    public bool CheckTarget(ITarget target) {
      return Condition != null && Condition.isValidTarget(target);
    }
  }
}
