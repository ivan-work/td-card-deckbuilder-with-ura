using System.Linq;
using GridSystem;
using IntentBehaviours;
using Intents.Engine;
using UnityEngine;

namespace Intents.IntentBehaviours {
  [CreateAssetMenu(fileName = "IntentBehaviours/MoveIntentBehaviour")]
  public class StepIntentBehaviour : MoveIntentBehaviour {
    protected override Vector2Int? GetTargetLoc(Intent<IntentValues> intent) {
      var direction = GetDirection(intent);
      if (intent.Source.TryGetComponent<GridComponent>(out var gridComponent) && direction.HasValue) {
        var sourceLoc = gridComponent.GridLoc;
        var targetLoc = direction.Value + sourceLoc;
        return targetLoc;
      }

      return null;
    }
    private static Vector2Int? GetDirection(Intent<IntentValues> intent) {
      return intent.Targets.Locations.FirstOrDefault();
    }
  }
}
