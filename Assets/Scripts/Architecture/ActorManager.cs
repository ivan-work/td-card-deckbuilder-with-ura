using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;

public class ActorManager : MonoBehaviour {
  private readonly LinkedList<BaseEffect> queuedEffects = new();
  private readonly LinkedList<BaseEffect> activeEffects = new();

  private void Awake() {
    EventManager.AmStartTurn.AddListener(OnStartTurn);
    EventManager.AmApplyEffects.AddListener(OnApplyEffects);
  }

  private void OnStartTurn() {
    EventManager.AmStartRequestIntent.Invoke(this);
  }

  public void addEffects(IEnumerable<BaseEffect> effects) {
    queuedEffects.AddRange(effects);
    // Debug.Log(queuedEffects.Aggregate(new StringBuilder("Current chain of effects: "), (sb, val) => sb.Append(val).Append(", "), sb => sb.ToString()));
  }

  private void OnApplyEffects() {
    Debug.Log(queuedEffects.Aggregate(new StringBuilder("On Apply Effects: "), (sb, val) => sb.Append(val).Append(", "), sb => sb.ToString()));
    while (queuedEffects.First != null) {
      var currentEffect = queuedEffects.First();

      queuedEffects.RemoveFirst();

      activeEffects.AddLast(currentEffect);

      currentEffect.start();
    }
  }

  private void Update() {
    foreach (var activeEffect in activeEffects.ToList()) {
      if (!activeEffect.active) {
        activeEffects.Remove(activeEffect);
      } else {
        activeEffect.update();
      }
    }
  }
}