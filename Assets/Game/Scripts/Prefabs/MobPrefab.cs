using Abilities;
using Components;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Prefabs {
  [SelectionBase]
  public class MobPrefab : MonoBehaviour {
    private HealthComponent healthComponent = null!;

    private void Awake() {
      healthComponent = this.GetAssertComponent<HealthComponent>();
    }

    private void Update() {
      if (healthComponent) {
        GetComponentInChildren<TextMeshPro>().text = $"HP: {healthComponent.currentHp}";
      }
    }
  }
}
