using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using Intents.Engine;
using Unity.VisualScripting;
using UnityEngine;

namespace Intents {
  public class IntentManagementSystem : MonoBehaviour {
    private LinkedList<AnyIntent> queuedIntents = new();
    private readonly LinkedList<IntentContext<BaseIntentDataValues, IntentTargetValues>> activeIntents = new();
    private bool isPerformingIntents;
    [NotNull] private GridSystem gridSystem;

    private void Awake() {
      Debug.Log("ActorManager.Awake()");
      EventManager.PhaseGetIntents.AddListener(OnGetIntents);
      EventManager.PhaseApplyEffects.AddListener(OnPerformIntents);

      gridSystem = FindFirstObjectByType<GridSystem>();
      if (!gridSystem) throw new NoComponentException($"No component ${typeof(GridSystem)}");
    }
    
    public void AddIntents(params AnyIntent[] intents) {
      queuedIntents.AddRange(intents);
      // Debug.Log(queuedEffects.Aggregate(new StringBuilder("Current chain of effects: "), (sb, val) => sb.Append(val).Append(", "), sb => sb.ToString()));
    }

    public void AddImmediateIntents(params AnyIntent[] intents) {
      // хз как добавить массив вначале линкед листа
      queuedIntents = new LinkedList<AnyIntent>(intents.Concat(queuedIntents));
    }
    
    private void OnGetIntents() {
      // Debug.Log("ActorManager.OnGetIntents()");
      EventManager.ImsStartRequestIntent.Invoke(this);
    }

    private void OnPerformIntents() {
      EventManager.ImsEndTurn.Invoke(this);
      isPerformingIntents = true;
      Debug.Log(queuedIntents.Aggregate(new StringBuilder("On Perform Intents: "),
        (sb, val) => sb.Append(val).Append(", "),
        sb => sb.ToString()));
      // foreach (var intent in queuedIntents) {
      //   var context = new IntentGlobalContext {
      //     IntentManagementSystem = this,
      //   };
      //   intent.Data.PerformIntent(intent, context);
      // }
    }
    
    private void Update() {
      if (isPerformingIntents) {
        if (queuedIntents.Any()) {
          var currentIntent = queuedIntents.First();
          if (!activeIntents.Any()) {
            queuedIntents.RemoveFirst();
            var context = new IntentContext<BaseIntentDataValues, IntentTargetValues>(
              source: currentIntent.Source,
              data: currentIntent.Data,
              dataValues: currentIntent.DataValues,
              targetValues: currentIntent.TargetValues,
              gridSystem: gridSystem,
              intentManagementSystem: this
            );
            currentIntent.Data.PerformIntent(context);
            if (context.Animation != null) {
              activeIntents.AddLast(context);
            }
          }
        }

        foreach (var activeIntent in activeIntents.ToList()) {
          bool result = activeIntent.Animation?.animate() ?? false;
          if (!result) {
            activeIntents.Remove(activeIntent);
          }
        }

        if (!queuedIntents.Any() && !activeIntents.Any()) {
          isPerformingIntents = false;
          EventManager.PhaseGetIntents.Invoke();
        }
      }
    }
  }
}
