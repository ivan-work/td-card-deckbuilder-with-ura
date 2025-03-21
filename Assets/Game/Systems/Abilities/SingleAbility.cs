using System.Collections.Generic;
using Intents;
using UnityEngine;

namespace Abilities {
  [CreateAssetMenu(menuName = "Ability/SingleAbility")]
  public class SingleAbility : Ability {
    [SerializeField] public int MaxClicks = 3;

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

    public void DrawIndicator(List<Vector2Int> affectedPoses, List<ITarget> targets) {
      // var gridSystem = FindAnyObjectByType<GridSystem>();
      // var positions = affectedPoses
      //   .Select(pos => gridSystem.gridPos2World(pos, 1).With(y: .3f))
      //   .ToArray();
      // lineRenderer.positionCount = positions.Count();
      // lineRenderer.SetPositions(positions);
    }

    public override ITargetingContext CreateTargetingContext() {
      return new SingleTargetModeContext(this);
    }
  }
}
