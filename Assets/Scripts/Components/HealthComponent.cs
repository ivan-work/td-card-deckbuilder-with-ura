using UnityEngine;

public class HealthComponent : MonoBehaviour {
  [SerializeField] public int initialHp = 5;
  [SerializeField] public int currentHp = 5;

  void Start() {
    currentHp = initialHp;
  }

  public void OnDamage(int damage) {
    currentHp -= damage;
    if (currentHp <= 0) {
      Destroy(gameObject);
    }
  }
}