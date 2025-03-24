using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Abilities {
  [CreateAssetMenu(menuName = "Ability/LineAbility")]
  public class LineAbility : Ability, IMultiClickTargetingContextConfig {
    [SerializeField] private int _maxClicks = 3;
    public int MaxClicks => _maxClicks;

    public GameObject? Indicator { get; }

    public override ITargetingContext CreateTargetingContext() {
      return new MultiClickTargetingContext(this);
    }

    public List<Vector2Int> GetAffectedLocs(AbilityContext context, ITarget target, IEnumerable<IEnumerable<Vector2Int>> lockedAreas) {
      var previousPos = (lockedAreas.LastOrDefault()?.LastOrDefault() ?? target.GetLoc());
      return GridSystem.GetLine(previousPos, target.GetLoc()).ToList();
    }
  }
}
