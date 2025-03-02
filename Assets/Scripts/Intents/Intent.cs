using UnityEngine;

namespace Intents {
  public class Intent<T> where T : BaseIntentValues {
    public GameObject Source { get; init; }
    public GameObject Target { get; init; }
    public BaseIntentData<T> Data { get; init; }
    public T Values { get; init; }
  }

  public class AnyIntent: Intent<BaseIntentValues> { }
}
