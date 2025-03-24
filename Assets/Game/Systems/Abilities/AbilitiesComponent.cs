using System.Collections.Generic;
using Abilities;
using UnityEngine;

namespace Components {
  public class AbilitiesComponent : MonoBehaviour {
    [SerializeField] private List<Ability> _abilities = null!;
    public List<Ability> Abilities => _abilities;
  }
}
