using System.Collections.Generic;
using Intents;
using UnityEngine;

namespace Abilities {
  [CreateAssetMenu(menuName = "Ability/BaseAbility")]
  public class Ability : ScriptableObject {
    [SerializeField] public string _name;
    [SerializeField] public Texture2D _icon;
    [SerializeField] public TargetMode TargetMode = null!;
    [SerializeField] public List<ITargetCondition> Conditions = new();
    [SerializeField] public IntentFactory _intentFactory = null!;
  }
}
