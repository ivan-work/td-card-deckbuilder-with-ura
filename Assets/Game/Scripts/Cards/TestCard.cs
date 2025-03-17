using System.Linq;
using Intents;
using Intents.Engine;
using UnityEngine;

namespace Cards {
  [CreateAssetMenu(menuName = "Card/TestCard")]
  public class TestCard : Card {
    [SerializeField] private IntentFactory IntentFactory;

    public override void DoCardAction(IntentGlobalContext context, Vector2Int[] gridPoses) {
      context.IntentSystem.AddImmediateIntents(
        gridPoses
          .Select(gridPos => IntentFactory.CreateIntent(null, new IntentTargets(null, gridPos)))
          .ToArray()
      );
    }
  }
}
