using System.Collections.Generic;
using System.Linq;
using Architecture;
using Intents.Engine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions;

namespace Abilities {
  public class LineAbility : Ability {
    [SerializeField] public int MaxClicks = 2;

    public override ITargetingContext CreateTargetingContext() {
      return new LineTargetingContext(this);
    }
  }

  public class LineTargetingContext : ITargetingContext {
    private readonly LineAbility ability;
    private readonly HashSet<ITarget> pivotTargets = new(); // Точки на линии, которые юзер кликнул
    private readonly HashSet<ITarget> allTargets = new(); // Все цели
    private ITarget? hoverTarget; // Конкретная цель над которой юзер ховерит
    private readonly HashSet<ITarget> hoverTargets = new(); // Линия от последней точки к юзеру

    public LineTargetingContext(LineAbility ability) {
      this.ability = ability;
    }

    public void OnHoverStart(AbilityContext abilityContext, ITarget potentialTarget) {
      hoverTarget = potentialTarget;
      var previousPos = (pivotTargets.LastOrDefault() ?? potentialTarget).GetLoc();
      var currentLineTargets = GridSystem.GetLine(previousPos, hoverTarget.GetLoc());

      currentLineTargets
        .SelectMany(pos => abilityContext.GlobalContext.GridSystem.GetGridEntities<ITarget>(pos))
        .Where(ability.CheckTarget)
        .ToList()
        .ForEach(
          target => {
            hoverTargets.Add(target);
            target.Highlight(true, true);
          }
        );
    }

    public void OnHoverStop(AbilityContext abilityContext, ITarget potentialTarget) {
      foreach (var target in hoverTargets) {
        if (!allTargets.Contains(target)) {
          target.Highlight(false, true);
        }
      }

      hoverTargets.Clear();
    }

    public bool ConfirmTarget(AbilityContext abilityContext, ITarget potentialTarget) {
      Assert.AreEqual(hoverTarget, potentialTarget, $"в .ConfirmTarget разные hoverTarget({hoverTarget}) и potentialTarget({potentialTarget})!!!");
      pivotTargets.Add(potentialTarget);
      allTargets.AddRange(hoverTargets);
      if (pivotTargets.Count < ability.MaxClicks) return false;

      var targets = allTargets
        .Select(target => target.GetGameObject())
        .MyNotNull()
        .ToArray();
      var pivotPoses = pivotTargets
        .Select(target => target.GetLoc())
        .ToArray();
      abilityContext.GlobalContext.IntentSystem.AddIntents(
        abilityContext.Ability.IntentFactory.CreateIntent(
          abilityContext.Source,
          IntentTargets.Create(targets, pivotPoses)
        )
      );
      return true;
    }

    public void Stop(AbilityContext context) {
      foreach (var target in allTargets) {
        target.Highlight(false, true);
      }

      foreach (var target in hoverTargets) {
        target.Highlight(false, true);
      }
    }
  }
}
