using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ActorManager : MonoBehaviour {
  private readonly LinkedList<BaseEffect> effects = new();
  private readonly LinkedList<BaseEffect> activeEffects = new();
  
  private void Awake() {
    EventManager.AmStartTurn.AddListener(OnStartTurn);
    EventManager.AmApplyEffects.AddListener(OnApplyEffects);
  }

  private void OnStartTurn() {
    EventManager.AmStartRequestIntent.Invoke(this);
  }

  public void addEffect(BaseEffect effect) {
    effects.AddLast(effect);
  }

  private void OnApplyEffects() {
    while (effects.First != null) {
      var currentEffect = effects.First();
      
      effects.RemoveFirst();
      
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