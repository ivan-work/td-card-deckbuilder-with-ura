using Architecture;
using IntentBehaviours;
using Intents;
using Intents.Engine;
using UnityEngine;

namespace Abilities {
  public class AbilityManager : Singleton<AbilityManager> {
    private AbilityContext? context;
    private GridSystem gridSystem = null!;
    private IntentSystem? intentSystem;

    protected override void OnAwake() {
      gridSystem = this.AssertFind<GridSystem>();
      EventManager.Instance.StartAbility.AddListener(onStartAbility);
      
    }

    private void OnImsStartRequestIntent(IntentSystem iSystem) {
      intentSystem = iSystem;
    }

    private void onStartAbility(GameObject source, Ability ability) {
      if (intentSystem is not null) {
        context = new AbilityContext(source, ability, new IntentGlobalContext() { IntentSystem = intentSystem, GridSystem = gridSystem });
        EventManager.Instance.AbilityTargetingStart.Invoke();
      }
    }
    
    private void stopAbility() {
      if (context is not null) {
        EventManager.Instance.AbilityTargetingStop.Invoke();
        context.TargetingContext.Stop(context);
        context = null;
      }
    }

    public void OnHoverStart(ITarget potentialTarget) {
      context?.TargetingContext.OnHoverStart(context, potentialTarget);
    }

    public void OnHoverStop(ITarget potentialTarget) {
      context?.TargetingContext.OnHoverStop(context, potentialTarget);
    }

    public void ConfirmTarget(ITarget potentialTarget) {
      if (context is not null && context.TargetingContext.ConfirmTarget(context, potentialTarget)) {
        stopAbility();
      }
    }

    public void Update() {
      if (Input.GetMouseButtonDown(1) && context is not null) {
        stopAbility();
      }
    }

  }
}
