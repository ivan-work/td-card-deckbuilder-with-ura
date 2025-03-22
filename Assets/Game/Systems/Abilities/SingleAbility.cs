using System.Collections.Generic;
using System.Linq;
using Intents;
using Intents.Engine;
using Unity.VisualScripting;
using UnityEngine;

namespace Abilities {
  [CreateAssetMenu(menuName = "Ability/SingleAbility")]
  public class SingleAbility : Ability {
    [SerializeField] public int MaxClicks = 1;

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

  public class SingleTargetModeContext : ITargetingContext {
    private readonly SingleAbility ability;
    private readonly HashSet<ITarget> lockedTargets = new();
    private readonly HashSet<ITarget> currentTargets = new();

    private int clickCount = 0;

    public SingleTargetModeContext(SingleAbility ability) {
      this.ability = ability;
    }

    private List<Vector2Int> GetAffectedPoses(Vector2Int targetPos) {
      var offsets = ability.TargetArea;
      return offsets.Select(offset => GridSystem.AxialToOffset(GridSystem.OffsetToAxial(targetPos) + offset)).ToList();
    }

    public void OnHoverStart(AbilityContext abilityContext, ITarget potentialTarget) {
      var affectedPoses = GetAffectedPoses(potentialTarget.GetPos());

      var potentialTargets = affectedPoses
        .SelectMany(pos => abilityContext.GlobalContext.GridSystem.getGridEntitiesSpecial<ITarget>(pos))
        .Where(ability.CheckTarget)
        .ToList();

      potentialTargets
        .ForEach(target => {
          currentTargets.Add(target);
          target.Highlight(true);
        });

      // abilityContext.TargetingSettings.DrawIndicator(affectedPoses, targets);
    }

    public bool ConfirmTarget(AbilityContext abilityContext, ITarget potentialTarget) {
      clickCount++;
      lockedTargets.AddRange(currentTargets);
      if (clickCount < ability.MaxClicks) return false;
      
      abilityContext.GlobalContext.IntentSystem.AddImmediateIntents(
        lockedTargets
          // .Select(target => abilityContext.Ability.IntentFactory.CreateIntent(null, new IntentTargets(null, target.GetPos())))
          .Where(target => target.GetGameObject() is not null)
          .Select(target => abilityContext.Ability.IntentFactory.CreateIntent(abilityContext.Source, new IntentTargets(target.GetGameObject()!, null)))
          .ToArray()
      );
      return true;
    }

    public void OnHoverStop(AbilityContext context, ITarget potentialTarget) {
      foreach (var target in currentTargets) {
        if (!lockedTargets.Contains(target)) {
          target.Highlight(false);
        }
      }

      currentTargets.Clear();
    }

    public void Stop() {
      foreach (var target in lockedTargets) {
        target.Highlight(false);
      }

      foreach (var target in currentTargets) {
        target.Highlight(false);
      }
    }
  }
}
