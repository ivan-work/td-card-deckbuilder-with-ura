using UnityEngine;

namespace Intents.Engine {
  // #TODO Nullable
  public class Intent {
    public GameObject Source { get; init; } = null!;
    public IntentBehaviour Behaviour { get; init; } = null!;
    public IntentValues Values { get; init; } = null!;
    public IntentTargets Targets { get; init; } = null!;
  }

  public class Intent<T> where T : IntentValues, new() {
    public GameObject Source { get; init; } = null!;
    public IntentTargets Targets { get; init; } = null!;
    public IntentBehaviour<T> Behaviour { get; init; } = null!;
    public T Values { get; init; } = null!;
  }
}
