using System.Collections.Generic;
using System.Linq;
using System.Text;
using Intents.Engine;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;

namespace Intents {
  public class IntentSystem : MonoBehaviour {
    private LinkedList<Intent> queuedIntents = new();
    private readonly LinkedList<IntentProgressContext> activeIntents = new();
    private bool isPerformingIntents;
    private GridSystem gridSystem = null!;
    private IntentGlobalContext globalContext = null!;

    private void Awake() {
      EventManager.Instance.PhasePerformIntents.AddListener(OnPerformIntents);

      gridSystem = this.AssertFind<GridSystem>();
      Assert.IsNotNull(gridSystem, $"No component {typeof(GridSystem)}");

      globalContext = new IntentGlobalContext { GridSystem = gridSystem, IntentSystem = this };
    }


    public void AddIntents(params Intent[] intents) {
      AddIntents((IEnumerable<Intent>) intents);
    }

    public void AddIntents(IEnumerable<Intent> intents) {
      queuedIntents.AddRange(intents);
      // Debug.Log(queuedEffects.Aggregate(new StringBuilder("Current chain of effects: "), (sb, val) => sb.Append(val).Append(", "), sb => sb.ToString()));
    }

    public void AddImmediateIntents(params Intent[] intents) {
      // хз как добавить массив вначале линкед листа
      queuedIntents = new LinkedList<Intent>(intents.Concat(queuedIntents));
    }


    private void OnPerformIntents() {
      
      EventManager.Instance.ImsEndTurn.Invoke(this);
      EventManager.Instance.ImsWriteIntents.Invoke(this);

      isPerformingIntents = true;
      Debug.Log(queuedIntents.Aggregate(new StringBuilder("On Perform Intents: "), (sb, val) => sb.Append(val).Append(", "), sb => sb.ToString()));
      // foreach (var intent in queuedIntents) {
      //   var context = new GlobalContext {
      //     IntentManagementSystem = this,
      //   };
      //   intent.Data.PerformIntent(intent, context);
      // }
    }

    public void PerformNextIntent() {
      // #TODO make private/internal
      if (queuedIntents.Any()) {
        var currentIntent = queuedIntents.First();
        if (!activeIntents.Any()) {
          queuedIntents.RemoveFirst();
          if (!currentIntent.Source.IsDestroyed()) {
            var context = new IntentProgressContext { GlobalContext = globalContext };
            currentIntent.Behaviour.Perform(currentIntent, context);
            if (context.Animation != null) {
              activeIntents.AddLast(context);
            }
          }
        }
      }
    }

    private void Update() {
      if (isPerformingIntents) {
        PerformNextIntent();

        foreach (var activeIntent in activeIntents.ToList()) {
          bool result = activeIntent.Animation?.animate() ?? false;
          if (!result) {
            activeIntents.Remove(activeIntent);
          }
        }

        if (!queuedIntents.Any() && !activeIntents.Any()) {
          isPerformingIntents = false;
          EventManager.Instance.PhaseCreateIntents.Invoke();
        }
      }
    }
  }
}
