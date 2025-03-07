using System;
using Intents.Engine;
using Intents.IntentBehaviours;
using UnityEngine;

namespace Components {
  [Serializable]
  public class HealthComponent : MonoBehaviour, ITargetableComponent, IReactToDamage {
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

    public void OnDamage(Intent<DamageIntentValues> intent, IntentProgressContext context) {
      currentHp -= intent.Values.Damage;
      if (currentHp <= 0) {
        Destroy(gameObject);
      }
    }
  }
}
