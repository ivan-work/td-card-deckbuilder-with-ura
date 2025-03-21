using System;
using System.Collections.Generic;
using System.Linq;
using Intents;
using Intents.Engine;
using UnityEngine;

namespace Cards {
  [CreateAssetMenu(menuName = "Ability/CenterWithSplashAbility")]
  public class CenterWithSplashAbility : Card {
    [SerializeField] private List<IntentFactory> _intentFactoryCenter = new();
    [SerializeField] private List<IntentFactory> _intentFactorySplash = new();

    public override void DoCardAction(IntentGlobalContext context, Vector2Int[] gridPoses) {
      // var pivot = Vector2Int.zero;
      // var centerCell = pivot;
      // var splashCells = area.Select(cell => cell + pivot);
      // var centerIntents = _intentFactoryCenter.Select(intentFactory => intentFactory.CreateIntent(null, new IntentTargets(null, pivot)));
      // var splashIntents = _intentFactoryCenter.Select(intentFactory => intentFactory.CreateIntent(null, new IntentTargets(null, splashCells)));
      // context.IntentSystem.AddImmediateIntents(
      //   gridPoses
      //     .Select(gridPos => IntentFactory.CreateIntent(null, new IntentTargets(null, gridPos)))
      //     .ToArray()
      // );
    }
  }
}
