using System.Collections.Generic;
using System.Linq;
using Intents.Engine;
using Unity.VisualScripting;
using UnityEngine;

namespace Abilities {
  [CreateAssetMenu(menuName = "Ability/MultiAreaAbility")]
  public class MultiAreaAbility : Ability {
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

    public override ITargetingContext CreateTargetingContext() {
      return new MultiAreaTargetModeContext(this);
    }
  }

  public class MultiAreaTargetModeContext : ITargetingContext {
    private readonly MultiAreaAbility ability;
    private readonly HashSet<Vector2Int> allClickedLocs = new(); // Все точки, которые юзер кликнул
    private readonly HashSet<HashSet<Vector2Int>> lockedAreas = new(); // Области, которые юзер кликнул
    private List<Vector2Int>? hoverLocs; // Цели которые ховерит юзер

    public MultiAreaTargetModeContext(MultiAreaAbility ability) {
      this.ability = ability;
    }

    public void OnHoverStart(AbilityContext abilityContext, ITarget potentialTarget) {
      hoverLocs = GetAffectedLocs(potentialTarget.GetLoc());

      hoverLocs
        .ForEach(loc => {
          var allTargets = abilityContext.GlobalContext.GridSystem.GetGridEntities<ITarget>(loc).ToList();
          var cellTargets = allTargets.Where(target => !target.IsCell).ToList();
          var cell = allTargets.Find(target => target.IsCell);
          var isAnyTargetValid = false;

          cellTargets.ForEach(target => {
            var isValid = abilityContext.Ability.CheckTarget(target);
            target.Highlight(true, isValid);
            if (isValid) {
              isAnyTargetValid = true;
            }
          });

          cell?.Highlight(true, isAnyTargetValid);
        });
    }

    public void OnHoverStop(AbilityContext context, ITarget potentialTarget) {
      if (hoverLocs != null) {
        foreach (var loc in hoverLocs) {
          if (!allClickedLocs.Contains(loc)) {
            stopHighlightingLoc(context, loc);
          }
        }

        hoverLocs.Clear();
      }
    }

    public bool ConfirmTarget(AbilityContext abilityContext, ITarget potentialTarget) {
      // Assert.AreEqual(hoverTarget, potentialTarget, $"в .ConfirmTarget разные hoverTarget({hoverTarget}) и potentialTarget({potentialTarget})!!!");
      if (hoverLocs is not null) {
        allClickedLocs.AddRange(hoverLocs);
        lockedAreas.Add(new HashSet<Vector2Int>(hoverLocs));
      }

      if (lockedAreas.Count < ability.MaxClicks) return false;

      foreach (var lockedArea in lockedAreas) {
        abilityContext.GlobalContext.IntentSystem.AddIntents(
          abilityContext.Ability.IntentFactory.CreateIntent(
            abilityContext.Source,
            IntentTargets.Create(lockedArea.ToArray())
          )
        );
      }

      return true;
    }

    public void Stop(AbilityContext context) {
      foreach (var loc in allClickedLocs) {
        stopHighlightingLoc(context, loc);
      }

      if (hoverLocs != null) {
        foreach (var loc in hoverLocs) {
          stopHighlightingLoc(context, loc);
        }
      }
    }

    protected static void stopHighlightingLoc(AbilityContext context, Vector2Int loc) {
      foreach (var target in context.GlobalContext.GridSystem.GetGridEntities<ITarget>(loc)) {
        target.Highlight(false, false);
      }
    }

    private List<Vector2Int> GetAffectedLocs(Vector2Int targetPos) {
      return ability.TargetArea.Select(offset => GridSystem.AxialToOffset(GridSystem.OffsetToAxial(targetPos) + offset)).ToList();
    }
  }
}
