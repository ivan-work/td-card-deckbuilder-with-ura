using System;
using UnityEngine;

namespace Intents.Engine {
  public class Intent {
    public GameObject Source { get; init; }
    public IntentBehaviour Behaviour { get; init; }
    public IntentValues Values { get; init; }
    public IntentTargets Targets { get; init; }
    public Intent(GameObject source, IntentBehaviour behaviour, IntentValues values, IntentTargets targets) {
      Source = source;
      Behaviour = behaviour;
      Values = values;
      Targets = targets;
    }
  }

  public class Intent<T> where T : IntentValues, new() {
    public GameObject Source { get; init; }
    public IntentTargets Targets { get; init; }
    public IntentBehaviour<T> Behaviour { get; init; }
    public T Values { get; init; }
    public Intent(GameObject source, IntentBehaviour<T> behaviour, T values, IntentTargets targets) {
      Source = source;
      Targets = targets;
      Behaviour = behaviour;
      Values = values;
    }

    public static Intent<T> FromBaseIntent(Intent intent) {
      if (intent.Values is T values && intent.Behaviour is IntentBehaviour<T> behaviour) {
        return new Intent<T> (intent.Source, behaviour, values, intent.Targets);
      }

      Debug.LogError($"Type mismatch: Expected {typeof(T)}, got {intent.Values.GetType()}");
      throw new ArgumentException();
    }
  }
}
