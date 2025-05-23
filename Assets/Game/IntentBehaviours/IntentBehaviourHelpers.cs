using System.Collections.Generic;
using System.Linq;
using Architecture;
using Intents.Engine;

namespace IntentBehaviours {
  public static class IntentBehaviourHelpers {
    public static IEnumerable<T> GetTargets<T>(IntentTargets intentTargets, GridSystem.GridSystem gridSystem)
      where T : class {
      if (intentTargets.GameObjects.Length > 0) {
        return intentTargets
          .GameObjects
          .Select(el => el.GetComponent<T>())
          .MyNotNull();
      }
      
      return intentTargets.Locations
        .SelectMany(loc => gridSystem.GetGridEntities<T>(loc));
    }
  }
}
