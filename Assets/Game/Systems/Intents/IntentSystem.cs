using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Intents.Engine;
using Unity.VisualScripting;
using UnityEngine;

namespace Intents {
  public class IntentSystem : MonoBehaviour {
    private LinkedList<Intent> queuedIntents = new();
    private readonly LinkedList<IntentProgressContext> activeIntents = new();
    private GridSystem gridSystem = null!;
    public IntentGlobalContext GlobalContext { get; private set; } = null!;

    private void Awake() {
      EventManager.Instance.PhaseCreateIntents.AddListener(onCreateIntents);
      EventManager.Instance.PhasePerformIntents.AddListener(onPerformIntents);

      gridSystem = this.AssertFind<GridSystem>();
      
      GlobalContext = new IntentGlobalContext { GridSystem = gridSystem, IntentSystem = this };
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
            var context = new IntentProgressContext { GlobalContext = GlobalContext };
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
