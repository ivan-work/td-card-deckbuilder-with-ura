using System.Linq;
using Intents.Engine;
using UnityEngine;

namespace IntentBehaviours {
  [CreateAssetMenu(fileName = "IntentBehaviours/TeleportIntentBehaviour")]
  public class TeleportIntentBehaviour : MoveIntentBehaviour {
    protected override Vector2Int? GetTargetLoc(Intent<IntentValues> intent) {
      return intent.Targets.Locations.FirstOrDefault();
    }
  }
}
