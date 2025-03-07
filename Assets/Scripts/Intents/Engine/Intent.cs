using UnityEngine;

namespace Intents.Engine {
  public class Intent {
    public GameObject Source { get; init; }
    public IntentBehaviour Behaviour { get; init; }
    public IntentValues Values { get; init; }
    public IntentTargets Targets { get; init; }
  }

  public class Intent<T> where T : IntentValues {
    public GameObject Source { get; init; }
    public IntentBehaviour<T> Behaviour { get; init; }
    public T Values { get; init; }
    public IntentTargets Targets { get; init; }
  }
}
