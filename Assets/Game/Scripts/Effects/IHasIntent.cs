using Intents;

namespace Effects {
  public interface IHasIntent {
    bool isActiveAndEnabled { get; }
    void WriteIntents(IntentComponent intentComponent);
  }
}
