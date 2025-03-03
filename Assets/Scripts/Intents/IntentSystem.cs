using System.Collections.Generic;
using System.Linq;
using Intents.Engine;
using Unity.VisualScripting;
using UnityEngine;

namespace Intents {
  public class IntentSystem : MonoBehaviour {
    private LinkedList<AnyIntent> queuedIntents = new();

    public void AddIntents(params AnyIntent[] intents) {
      queuedIntents.AddRange(intents);
      // Debug.Log(queuedEffects.Aggregate(new StringBuilder("Current chain of effects: "), (sb, val) => sb.Append(val).Append(", "), sb => sb.ToString()));
    }

    public void AddImmediateIntents(params AnyIntent[] intents) {
      // хз как добавить массив вначале линкед листа
      queuedIntents = new LinkedList<AnyIntent>(intents.Concat(queuedIntents));
    }

    public void PerformIntents() {
      foreach (var intent in queuedIntents) {
        var context = new IntentContext {
          IntentSystem = this,
        };
        intent.Data.PerformIntent(intent, context);
      }
    }
  }
}
