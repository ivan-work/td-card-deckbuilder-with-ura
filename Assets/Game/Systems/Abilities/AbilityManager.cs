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
    public readonly TargetingSettings TargetingSettings;
    public readonly ITargetingContext TargetingContext;
    public readonly IntentGlobalContext GlobalContext;

    public AbilityContext(Ability ability, IntentGlobalContext intentGlobalContext) {
      Ability = ability;
      Conditions = ability.Conditions;
      TargetingSettings = ability._targetingSettings;
      TargetingContext = TargetingSettings.CreateTargetingContext();
      GlobalContext = intentGlobalContext;
    }
  }

  public interface ITargetingContext {
    void Stop();
    void OnHoverStart(AbilityContext context, ITarget potentialTarget);
    void OnHoverStop(AbilityContext context, ITarget potentialTarget);
    void ConfirmTarget(AbilityContext context, ITarget potentialTarget);
  }

  public abstract class TargetingSettings : ScriptableObject {
    public abstract ITargetingContext CreateTargetingContext();
  }

  public class SingleTargetModeContext : ITargetingContext {
    private readonly SingleTargetingSettings settings;
    private readonly HashSet<ITarget> lockedTargets = new();
    private readonly HashSet<ITarget> currentTargets = new();

    private int clickCount = 0;

    public SingleTargetModeContext(SingleTargetingSettings targetingSettings) {
      settings = targetingSettings;
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

    public void ConfirmTarget(AbilityContext context, ITarget potentialTarget) {
      clickCount++;
      if (clickCount <= settings.MaxClicks) {
        lockedTargets.AddRange(currentTargets);
      } else {
        Debug.Log("FIRE");
      }
      // context.Ability.Perform...
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

  public class SingleTargetingSettings : TargetingSettings {
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

    public override ITargetingContext CreateTargetingContext() {
      return new SingleTargetModeContext(this);
    }

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
      var gridSystem = FindAnyObjectByType<GridSystem>();
      var positions = affectedPoses
        .Select(pos => gridSystem.gridPos2World(pos, 1).With(y: .3f))
        .ToArray();
      lineRenderer.positionCount = positions.Count();
      lineRenderer.SetPositions(positions);
    }
  }

  public class LineTargetingSettings : TargetingSettings {
    public override ITargetingContext CreateTargetingContext() {
      return null;
      // return new SingleTargetModeContext();
    }
  }

  public class AbilityManager : Architecture.Singleton<AbilityManager> {
    private AbilityContext? context;
    private GridSystem gridSystem = null!;

    protected override void OnAwake() {
      gridSystem = this.AssertFind<GridSystem>();
      var intentSystem = this.AssertFind<IntentSystem>();
      var ability = ScriptableObject.CreateInstance<Ability>();
      ability.Name = "Fierbol";
      ability.Icon = Texture2D.redTexture;
      ability.IntentFactory = new IntentFactory();
      ability.IntentFactory.Behaviour = ScriptableObject.CreateInstance<DamageIntentBehaviour>();
      ability.IntentFactory.Values = new DamageIntentValues() {Damage = 3, DamageType = DamageType.Fire};
      ability.Conditions = new();
      ability._targetingSettings = ScriptableObject.CreateInstance<SingleTargetingSettings>();
      context = new AbilityContext(ability, new IntentGlobalContext() {IntentSystem = intentSystem, GridSystem = gridSystem});
    }

    public void OnHoverStart(ITarget potentialTarget) {
      context?.TargetingContext.OnHoverStart(context, potentialTarget);
    }

    public void OnHoverStop(ITarget potentialTarget) {
      context?.TargetingContext.OnHoverStop(context, potentialTarget);
    }

    public void ConfirmTarget(ITarget potentialTarget) {
      context?.TargetingContext.ConfirmTarget(context, potentialTarget);
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

    private void PerformAbility(AbilityContext localContext) {
      localContext.GlobalContext.IntentSystem.AddImmediateIntents(
        localContext.TargetingContext.GetTargets
          .Select(gridPos => localContext.Ability.IntentFactory.CreateIntent(null, new IntentTargets(null, gridPos)))
          .ToArray()
      );
    }
  }
}
