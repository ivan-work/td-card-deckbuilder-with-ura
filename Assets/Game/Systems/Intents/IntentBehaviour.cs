using System;
using Intents.Engine;
using UnityEngine;

namespace Intents {
  public abstract class IntentBehaviour : ScriptableObject {
    public abstract IntentValues BaseDefaultValues { get; set; }
    public abstract void Perform(Intent intent, IntentProgressContext context);

    public abstract GameObject? CreateDisplay(Intent intent, IntentGlobalContext context);
  }

  public abstract class IntentBehaviour<T> : IntentBehaviour where T : IntentValues, new() {
    [SerializeField] private T _defaultValues = new();

    public override IntentValues BaseDefaultValues {
      get => _defaultValues;
      set => _defaultValues = (T) value!;
    }

    protected abstract void Perform(Intent<T> intent, IntentProgressContext context);

    public override void Perform(Intent intent, IntentProgressContext context) {
      Perform(Intent<T>.FromBaseIntent(intent), context);
    }

    protected virtual GameObject? CreateDisplay(Intent<T> intent, IntentGlobalContext context) {
      return null;
    }

    public override GameObject? CreateDisplay(Intent intent, IntentGlobalContext context) {
      return CreateDisplay(Intent<T>.FromBaseIntent(intent), context);
    }
  }
}
