using System.Collections.Generic;
using System.Linq;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;

public class ActorManager : MonoBehaviour {
  public LinkedList<BaseEffect> queuedEffects = new();
  private readonly LinkedList<BaseEffect> activeEffects = new();
  private bool isAppyingEffects;

  private void Awake() {
    Debug.Log("ActorManager.Awake()");
    EventManager.PhaseGetIntents.AddListener(OnGetIntents);
    EventManager.PhaseApplyEffects.AddListener(OnApplyEffects);
  }

  private void OnGetIntents() {
    Debug.Log("ActorManager.OnGetIntents()");
    EventManager.AmStartRequestIntent.Invoke(this);
  }

  public void addEffects(params BaseEffect[] effects) {
    queuedEffects.AddRange(effects);
    // Debug.Log(queuedEffects.Aggregate(new StringBuilder("Current chain of effects: "), (sb, val) => sb.Append(val).Append(", "), sb => sb.ToString()));
  }

  public void addImmediateEffects(params BaseEffect[] effects) {
    // хз как добавить массив вначале линкед листа
    queuedEffects = new LinkedList<BaseEffect>(effects.Concat(queuedEffects));
  }

  private void OnApplyEffects() {
    isAppyingEffects = true;
    Debug.Log(queuedEffects.Aggregate(new StringBuilder("On Apply Effects: "), (sb, val) => sb.Append(val).Append(", "),
      sb => sb.ToString()));
  }

  private void Update() {
    if (isAppyingEffects) {
      if (queuedEffects.Any()) {
        var currentEffect = queuedEffects.First();
        if (!(currentEffect.waitForOthers && activeEffects.Any())) {
          queuedEffects.RemoveFirst();
          activeEffects.AddLast(currentEffect);
          currentEffect.start(this);
        }
      }

      foreach (var activeEffect in activeEffects.ToList()) {
        if (!activeEffect.isActive) {
          activeEffects.Remove(activeEffect);
        } else {
          activeEffect.update();
        }
      }

      if (!queuedEffects.Any() && !activeEffects.Any()) {
        isAppyingEffects = false;
        EventManager.PhaseGetIntents.Invoke();
      }
    }
  }
}