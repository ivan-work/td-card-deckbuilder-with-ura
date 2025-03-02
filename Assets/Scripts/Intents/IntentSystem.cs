using System.Collections.Generic;

namespace Intents {
  public class IntentSystem {
    private readonly List<AnyIntent> queue = new();

    public void AddIntent(AnyIntent intent) {
      queue.Add(intent);
    }

    public void PerformIntents() {
      foreach (var intent in queue) {
        var context = new IntentContext<BaseIntentValues> {
          Source = intent.Source,
          Target = intent.Target,
          Values = intent.Values,
          IntentSystem = this,
        };
        intent.Data.PerformIntent(context);
      }
    }
  }
}
