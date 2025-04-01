using System.Collections.Generic;

namespace Intents.Engine {
  public interface IIntentHolder {
    void AddIntents(IEnumerable<Intent> intents, bool toFront);
    void AddIntents(params Intent[] intents);
  }
}
