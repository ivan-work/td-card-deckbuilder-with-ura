using Intents;
using Intents.Engine;

namespace Effects {
  public interface IHasIntent {
    bool isActiveAndEnabled { get; }
    void WriteIntents(IIntentHolder intentHolder);
  }
}
