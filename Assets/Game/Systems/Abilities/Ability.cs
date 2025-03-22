using System.Collections.Generic;
using System.Linq;
using Intents;
using UnityEngine;

namespace Abilities {
  public abstract class Ability : ScriptableObject {
    [SerializeField] public string Name = null!;
    [SerializeField] public Texture2D Icon = null!;
    [SerializeField] public List<ITargetCondition> Conditions = new();
    [SerializeField] public IntentFactory IntentFactory = null!;

    public abstract ITargetingContext CreateTargetingContext();
    
    public bool CheckTarget(ITarget target) {
      return Conditions
        .All(condition => condition.isValidTarget(target));
    }
  }
}
