using System;
using Intents.Engine;
using UnityEngine;

namespace Intents {
  public abstract class IntentBehaviour : ScriptableObject {
    public abstract IntentValues BaseDefaultValues { get; set; }
    public abstract void Perform(Intent intent, IntentProgressContext context);
  }

  public abstract class IntentBehaviour<T> : IntentBehaviour where T : IntentValues {
    [SerializeField] private T DefaultValues;

    public override IntentValues BaseDefaultValues {
      get => DefaultValues;
      set => DefaultValues = (T) value;
    }

    public abstract void Perform(Intent<T> intent, IntentProgressContext context);

    public override void Perform(Intent intent, IntentProgressContext context) {
      if (intent.Values is T values) {
        var typedIntent = new Intent<T> {
          Behaviour = this,
          Values = values,
          Source = intent.Source,
          Targets = intent.Targets
        };
        Perform(typedIntent, context);
      } else {
        Debug.LogError($"Type mismatch: Expected {typeof(T)}, got {intent.Values.GetType()}");
        throw new ArgumentException();
      }
    }
  }
}
