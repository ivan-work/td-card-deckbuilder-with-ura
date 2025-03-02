using UnityEngine;

namespace Intents {
  public interface IIntentGlobalContext {
    public IntentSystem IntentSystem { get; init; }
  }
  public interface IIntentLocalValues {
    public IntentSystem IntentSystem { get; init; }
  }
  public interface IIntentDataValues {
    public IntentSystem IntentSystem { get; init; }
  }
  
  public class IntentContext<TDataValues, TTargetValues> where TDataValues : BaseIntentValues {
    public GameObject Source { get; init; }

    public TDataValues DataValues { get; init; }
    
    public TTargetValues TargetValues { get; init; }
  }
}
