using System;
using UnityEngine;

namespace Components {
  [Serializable]
  public class HealthComponent : MonoBehaviour, ITargetableComponent {
    [SerializeField] public int initialHp = 5;
    [SerializeField] public int currentHp = 5;

    private void Start() {
      currentHp = initialHp;
    }

    public void OnDamage(int damage) {
      currentHp -= damage;
      if (currentHp <= 0) {
        Destroy(gameObject);
      }
    }
  }
}
