using System.Linq;
using Intents;
using Intents.Engine;
using UnityEngine;

namespace Cards {
  [CreateAssetMenu(menuName = "Card/TestCard")]
  public class TestCard : Card {
    [SerializeField] private IntentCreator IntentCreator;

    public override void DoCardAction(IntentGlobalContext context, Vector2Int[] gridPoses) {
      context.IntentManagementSystem.AddImmediateIntents(
        gridPoses
          .Select(gridPos => IntentCreator.CreateIntent(null, new IntentTargetValues(null, gridPos)))
          .ToArray()
      );
    }
  }
}
