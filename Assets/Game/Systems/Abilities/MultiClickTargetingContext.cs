using System.Collections.Generic;
using System.Linq;
using Intents.Engine;
using Unity.VisualScripting;
using UnityEngine;

namespace Abilities {
  public interface IMultiClickTargetingContextConfig {
    int MaxClicks { get; }
    List<Vector2Int> GetAffectedLocs(AbilityContext context, ITarget target, IEnumerable<IEnumerable<Vector2Int>> lockedAreas);
  }

  public class MultiClickTargetingContext : ITargetingContext {
    private readonly IMultiClickTargetingContextConfig config;
    private readonly HashSet<Vector2Int> allClickedLocs = new(); // Все точки, которые юзер кликнул
    private readonly HashSet<HashSet<Vector2Int>> lockedAreas = new(); // Области, которые юзер кликнул
    private List<Vector2Int>? hoverLocs; // Цели которые ховерит юзер

    public MultiClickTargetingContext(IMultiClickTargetingContextConfig config) {
      this.config = config;
    }

    public void OnHoverStart(AbilityContext context, ITarget potentialTarget) {
      hoverLocs = config.GetAffectedLocs(context, potentialTarget, lockedAreas);

      hoverLocs
        .ForEach(loc => {
          var allTargets = context.GlobalContext.GridSystem.GetGridEntities<ITarget>(loc).ToList();
          var cellTargets = allTargets.Where(target => !target.IsCell).ToList();
          var cell = allTargets.Find(target => target.IsCell);
          var isAnyTargetValid = false;

          cellTargets.ForEach(target => {
            var isValid = context.Ability.CheckTarget(target);
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

    public bool ConfirmTarget(AbilityContext context, ITarget potentialTarget) {
      // Assert.AreEqual(hoverTarget, potentialTarget, $"в .ConfirmTarget разные hoverTarget({hoverTarget}) и potentialTarget({potentialTarget})!!!");
      if (hoverLocs is not null) {
        allClickedLocs.AddRange(hoverLocs);
        lockedAreas.Add(new HashSet<Vector2Int>(hoverLocs));
      }

      if (lockedAreas.Count < config.MaxClicks) return false;

      foreach (var lockedArea in lockedAreas) {
        context.GlobalContext.IntentSystem.AddIntents(
          context.Ability.IntentFactory.CreateIntent(
            context.Source,
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

    private static void stopHighlightingLoc(AbilityContext context, Vector2Int loc) {
      foreach (var target in context.GlobalContext.GridSystem.GetGridEntities<ITarget>(loc)) {
        target.Highlight(false, false);
      }
    }
  }
}
