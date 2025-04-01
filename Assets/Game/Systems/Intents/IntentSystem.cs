using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Intents.Engine;
using Unity.VisualScripting;
using UnityEngine;

namespace Intents {
  public class IntentSystem : MonoBehaviour, IIntentHolder {
    private LinkedList<Intent> queuedIntents = new();
    private readonly LinkedList<IntentProgressContext> activeIntents = new();
    private GridSystem gridSystem = null!;
    private IntentGlobalContext globalContext = null!;

    private void Awake() {
      EventManager.Instance.PhaseCreateIntents.AddListener(onCreateIntents);
      EventManager.Instance.PhasePerformIntents.AddListener(onPerformIntents);

      gridSystem = this.AssertFind<GridSystem>();

      globalContext = new IntentGlobalContext { GridSystem = gridSystem, IntentHolder = this };
    }


    public void AddIntents(params Intent[] intents) {
      AddIntents(intents, false);
    }

    public void AddIntents(IEnumerable<Intent> intents, bool toFront) {
      if (toFront) {
        // хз как добавить массив вначале линкед листа
        queuedIntents = new LinkedList<Intent>(intents.Concat(queuedIntents));
      } else {
        queuedIntents.AddRange(intents);
      }
      // Debug.Log(queuedEffects.Aggregate(new StringBuilder("Current chain of effects: "), (sb, val) => sb.Append(val).Append(", "), sb => sb.ToString()));
    }

    private void onCreateIntents() {
      EventManager.Instance.ImsStartPlayerTurn.Invoke(this);
    }

    private void onPerformIntents() {
      EventManager.Instance.ImsEndTurn.Invoke(this);
      EventManager.Instance.ImsWriteIntents.Invoke(this);

      Debug.Log(queuedIntents.Aggregate(new StringBuilder("On Perform Intents: "), (sb, val) => sb.Append(val).Append(", "), sb => sb.ToString()));

      StartCoroutine(performIntents());
    }

    private IEnumerator performIntents() {
      while (queuedIntents.Any() || activeIntents.Any()) {
        performNextIntent();
        foreach (var activeIntent in activeIntents.ToList()) {
          var result = activeIntent.Animation?.animate() ?? false;
          if (!result) {
            activeIntents.Remove(activeIntent);
          }
        }

        yield return null;
      }

      EventManager.Instance.PhaseCreateIntents.Invoke();
    }

    private void performNextIntent() {
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

#if UNITY_INCLUDE_TESTS
    public void Test_performNextIntent() {
      performNextIntent();
    }
#endif
  }
}
