using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Intents;
using Intents.Engine;
using Intents.IntentBehaviours;
using Unity.VisualScripting;
using UnityEngine;
using Utils;

namespace Abilities {
  public interface ITarget {
    public void Highlight(bool enable);
    public Vector2Int GetPos();
    public GameObject? GetGameObject();
  }

  public interface ITargetCondition {
    public bool isValidTarget(ITarget target);
  }

  public class IsCellTargetCondition : ITargetCondition {
    public bool isValidTarget(ITarget target) {
      return target.GetGameObject() is null;
    }
  }

  public class AbilityContext {
    public readonly IEnumerable<ITargetCondition> Conditions;
    public readonly Ability Ability;
    public readonly ITargetingContext TargetingContext;
    public readonly IntentGlobalContext GlobalContext;
    public readonly GameObject Source;

    public AbilityContext(GameObject source, Ability ability, IntentGlobalContext intentGlobalContext) {
      Ability = ability;
      Conditions = ability.Conditions;
      TargetingContext = ability.CreateTargetingContext();
      GlobalContext = intentGlobalContext;
      Source = source;
    }

  }

  public interface ITargetingContext {
    void Stop();
    void OnHoverStart(AbilityContext context, ITarget potentialTarget);
    void OnHoverStop(AbilityContext context, ITarget potentialTarget);
    bool ConfirmTarget(AbilityContext context, ITarget potentialTarget);
  }

  public class SingleTargetModeContext : ITargetingContext {
    private readonly SingleAbility settings;
    private readonly HashSet<ITarget> lockedTargets = new();
    private readonly HashSet<ITarget> currentTargets = new();

    private int clickCount = 0;

    public SingleTargetModeContext(SingleAbility settings) {
      this.settings = settings;
    }

    private List<Vector2Int> GetAffectedPoses(Vector2Int targetPos) {
      var offsets = settings.TargetArea;
      return offsets.Select(offset => GridSystem.AxialToOddr(GridSystem.OddrToAxial(targetPos) + offset)).ToList();
    }

    public void OnHoverStart(AbilityContext context, ITarget potentialTarget) {
      var affectedPoses = GetAffectedPoses(potentialTarget.GetPos());

      var potentialTargets = affectedPoses
        .SelectMany(pos => context.GlobalContext.GridSystem.getGridEntitiesSpecial<ITarget>(pos))
        .Where(t => CheckTarget(context, t))
        .ToList();

      potentialTargets
        .ToList()
        .ForEach(target => {
          currentTargets.Add(target);
          target.Highlight(true);
        });

      // context.TargetingSettings.DrawIndicator(affectedPoses, targets);
    }

    public bool ConfirmTarget(AbilityContext abilityContext, ITarget potentialTarget) {
      clickCount++;
      lockedTargets.AddRange(currentTargets);
      if (clickCount > settings.MaxClicks) {
        abilityContext.GlobalContext.IntentSystem.AddIntents(
          lockedTargets
            // .Select(target => abilityContext.Ability.IntentFactory.CreateIntent(null, new IntentTargets(null, target.GetPos())))
            .Where(target => target.GetGameObject() is not null)
            .Select(target => abilityContext.Ability.IntentFactory.CreateIntent(abilityContext.Source, new IntentTargets(target.GetGameObject()!, null)))
            .ToArray()
        );
        return true;
      }

      return false;
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

    private static bool CheckTarget(AbilityContext abilityContext, ITarget target) {
      return abilityContext.Conditions
        .All(condition => condition.isValidTarget(target));
    }
  }

  public class AbilityManager : Architecture.Singleton<AbilityManager> {
    private AbilityContext? context;
    private GridSystem gridSystem = null!;

    protected override void OnAwake() {
      gridSystem = this.AssertFind<GridSystem>();
      var intentSystem = this.AssertFind<IntentSystem>();
      var ability = ScriptableObject.CreateInstance<SingleAbility>();
      ability.Name = "Fierbol";
      ability.Icon = Texture2D.redTexture;
      ability.IntentFactory = new IntentFactory();
      ability.IntentFactory.Behaviour = ScriptableObject.CreateInstance<DamageIntentBehaviour>();
      ability.IntentFactory.Values = new DamageIntentValues() {Damage = 3, DamageType = DamageType.Fire};
      ability.Conditions = new();
      context = new AbilityContext(gameObject, ability, new IntentGlobalContext() {IntentSystem = intentSystem, GridSystem = gridSystem});
    }

    public void OnHoverStart(ITarget potentialTarget) {
      context?.TargetingContext.OnHoverStart(context, potentialTarget);
    }

    public void OnHoverStop(ITarget potentialTarget) {
      context?.TargetingContext.OnHoverStop(context, potentialTarget);
    }

    public void ConfirmTarget(ITarget potentialTarget) {
      if (context is not null && context.TargetingContext.ConfirmTarget(context, potentialTarget)) {
        stopAbility();
      }
    }

    public void Update() {
      if (Input.GetMouseButtonDown(1) && context is not null) {
        stopAbility();
      }
    }

    private void stopAbility() {
      context?.TargetingContext.Stop();
      context = null;
    }
  }
}
