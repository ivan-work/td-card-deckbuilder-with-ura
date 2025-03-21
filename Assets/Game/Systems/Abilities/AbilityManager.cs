using System.Collections.Generic;
using System.Linq;
using Architecture;
using Intents;
using Intents.Engine;
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

  public class TargetingContext {
    public readonly HashSet<ITarget> Targets = new();
    public IEnumerable<ITargetCondition> Conditions;
    public Ability Ability;
    public TargetMode TargetMode;
    public IntentGlobalContext GlobalContext;

    public TargetingContext(Ability ability, IntentGlobalContext intentGlobalContext) {
      Ability = ability;
      Conditions = ability.Conditions;
      TargetMode = ability.TargetMode;
      GlobalContext = intentGlobalContext;
    }
  }

  public abstract class TargetMode : ScriptableObject {
    [SerializeField] private int _targetsCount = 1;


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

    public int TargetsCount => _targetsCount;

    [SerializeField] public List<Vector2Int> TargetArea = new() {
      Vector2Int.zero,
      new Vector2Int(1, 0),
      new Vector2Int(1, -1),
      new Vector2Int(0, -1),
      new Vector2Int(-1, 0),
      new Vector2Int(-1, 1),
      new Vector2Int(0, 1),
    };

    public List<Vector2Int> GetAffectedCells(Vector2Int pivotPos) {
      var offsets = TargetArea;
      var targetPos = pivotPos;
      return offsets.Select(offset => targetPos + offset).ToList();
    }

    public void DrawIndicator(List<Vector2Int> affectedCells, List<ITarget> targets) {
      var gridSystem = FindAnyObjectByType<GridSystem>();
      var positions = affectedCells
        .Select(pos => gridSystem.gridPos2World(pos, 1).With(y: .3f))
        .ToArray();
      lineRenderer.positionCount = positions.Count();
      lineRenderer.SetPositions(positions);
    }
  }

  public class SingleTargetMode : TargetMode { }

  public class AbilityManager : Singleton<AbilityManager> {
    private TargetingContext? context;
    private GridSystem gridSystem = null!;

    protected override void OnAwake() {
      gridSystem = this.AssertFind<GridSystem>();
      var intentSystem = this.AssertFind<IntentSystem>();
      var ability = ScriptableObject.CreateInstance<Ability>();
      ability._name = "Fierbol";
      ability._icon = Texture2D.redTexture;
      ability._intentFactory = new IntentFactory();
      ability.Conditions = new();
      ability.TargetMode = ScriptableObject.CreateInstance<SingleTargetMode>();
      context = new TargetingContext(ability, new IntentGlobalContext() {IntentSystem = intentSystem, GridSystem = gridSystem});
    }

    public void OnHoverStart(ITarget potentialTarget) {
      if (context is not null) {
        var affectedCells = context.TargetMode.GetAffectedCells(potentialTarget.GetPos());

        var targets = affectedCells
          .SelectMany(pos => gridSystem.getGridEntitiesSpecial<ITarget>(pos))
          .Where(checkTarget)
          .ToList();

        targets
          .ForEach(target => {
            context.Targets.Add(target);
            target.Highlight(true);
          });

        context.TargetMode.DrawIndicator(affectedCells, targets);
      }
    }

    public void OnHoverStop(ITarget potentialTarget) {
      if (context is not null) {
        foreach (var target in context.Targets) {
          target.Highlight(false);
        }

        context.Targets.Clear();
      }
    }

    public void OnClick(ITarget potentialTarget) {
      if (context is not null) {
        context.Targets.Add(potentialTarget);
        if (context.Targets.Count >= context.TargetMode.TargetsCount) {
          PerformAbility(context);
        }
      }
    }

    private void PerformAbility(TargetingContext localContext) {
      localContext.GlobalContext.IntentSystem.AddImmediateIntents(
        localContext.TargetMode.TargetArea
          .Select(gridPos => localContext.Ability._intentFactory.CreateIntent(null, new IntentTargets(null, gridPos)))
          .ToArray()
      );
    }

    private bool checkTarget(ITarget target) {
      // TODO проверять все цели на все кондишены
      if (context is not null) {
        return context.Conditions
          .All(condition => condition.isValidTarget(target));
      }

      return false;
    }
  }
}
