using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Intents.Engine;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

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
      var previousPos = (pivotTargets.LastOrDefault() ?? potentialTarget).GetPos();
      var currentLineTargets = GridSystem.GetLine(previousPos, hoverTarget.GetPos());

      currentLineTargets
        .SelectMany(pos => abilityContext.GlobalContext.GridSystem.getGridEntitiesSpecial<ITarget>(pos))
        .Where(ability.CheckTarget)
        .ToList()
        .ForEach(target => {
          hoverTargets.Add(target);
          target.Highlight(true);
        });
    }

    public void OnHoverStop(AbilityContext abilityContext, ITarget potentialTarget) {
      foreach (var target in hoverTargets) {
        if (!allTargets.Contains(target)) {
          target.Highlight(false);
        }
      }

      hoverTargets.Clear();
    }

    public bool ConfirmTarget(AbilityContext abilityContext, ITarget potentialTarget) {
      Assert.AreEqual(hoverTarget, potentialTarget, $"в .ConfirmTarget не должно быть разных hoverTarget({hoverTarget}) и potentialTarget({potentialTarget})!!!");
      pivotTargets.Add(potentialTarget);
      allTargets.AddRange(hoverTargets);
      if (pivotTargets.Count < ability.MaxClicks) return false;

      abilityContext.GlobalContext.IntentSystem.AddImmediateIntents(
        allTargets
          .Where(target => target.GetGameObject() is not null)
          .Select(target => abilityContext.Ability.IntentFactory.CreateIntent(abilityContext.Source, new IntentTargets(target.GetGameObject()!, null)))
          .ToArray()
      );
      return true;
    }

    public void Stop() {
      foreach (var target in allTargets) {
        target.Highlight(false);
      }

      foreach (var target in hoverTargets) {
        target.Highlight(false);
      }
    }
  }
}
