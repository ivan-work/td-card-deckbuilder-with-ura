using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Abilities {
  [CreateAssetMenu(menuName = "Ability/MultiAreaAbility")]
  public class MultiAreaAbility : Ability, IMultiClickTargetingContextConfig {
    [SerializeField] private int _maxClicks = 3;
    public int MaxClicks => _maxClicks;

    [SerializeField] public List<Vector2Int> TargetArea = new() {
      Vector2Int.zero,
      new Vector2Int(0, -1),
      new Vector2Int(+1, -1),
      new Vector2Int(+1, 0),
      new Vector2Int(0, +1),
      new Vector2Int(-1, +1),
      new Vector2Int(-1, 0),
    };

    private GameObject indicator;
    private LineRenderer lineRenderer;

    private void Awake() {
      indicator = new GameObject();
      lineRenderer = indicator.AddComponent<LineRenderer>();
      lineRenderer.startWidth = .25f;
      lineRenderer.endWidth = .25f;
      lineRenderer.numCapVertices = 3;
      lineRenderer.numCornerVertices = 3;
      lineRenderer.loop = true;
    }

    public override ITargetingContext CreateTargetingContext() {
      return new MultiClickTargetingContext(this);
    }


    public List<Vector2Int> GetAffectedLocs(AbilityContext context, ITarget target, IEnumerable<IEnumerable<Vector2Int>> lockedAreas) {
      return TargetArea
        .Select(offset => GridSystem.AxialToOffset(GridSystem.OffsetToAxial(target.GetLoc()) + offset))
        .ToList();
    }
  }
}
