namespace Intents.Engine {
  // #TODO Nullable
  public class IntentGlobalContext {
    public IIntentHolder IntentHolder { get; init; } = null!;
    public GridSystem.GridSystem GridSystem { get; init; } = null!;
  }
}
