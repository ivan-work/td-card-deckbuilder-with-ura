using System.Collections.Generic;
using Intents;
using UnityEngine;

namespace Abilities {
  [CreateAssetMenu(menuName = "Ability/BaseAbility")]
  public class Ability : ScriptableObject {
    [SerializeField] public string Name = null!;
    [SerializeField] public Texture2D Icon = null!;
    [SerializeField] public TargetingSettings _targetingSettings = null!;
    [SerializeField] public List<ITargetCondition> Conditions = new();
    [SerializeField] public IntentFactory IntentFactory = null!;
  }
}
