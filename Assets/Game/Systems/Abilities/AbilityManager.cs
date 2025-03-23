using Architecture;
using IntentBehaviours;
using Intents;
using Intents.Engine;
using UnityEngine;

namespace Abilities {
  public interface ITarget {
    public void Highlight(bool isEnable, bool isValid);
    public Vector2Int GetLoc();
    public GameObject GetGameObject();

    public bool IsCell { get; }
  }

  public interface ITargetCondition {
    public bool isValidTarget(ITarget target);
  }

  public class IsCellTargetCondition : ITargetCondition {
    public bool isValidTarget(ITarget target) {
      return target.IsCell;
    }
  }

  public class AbilityContext {
    public readonly Ability Ability;
    public readonly ITargetingContext TargetingContext;
    public readonly IntentGlobalContext GlobalContext;
    public readonly GameObject Source;

    public AbilityContext(GameObject source, Ability ability, IntentGlobalContext intentGlobalContext) {
      Ability = ability;
      TargetingContext = ability.CreateTargetingContext();
      GlobalContext = intentGlobalContext;
      Source = source;
    }
  }

  public interface ITargetingContext {
    void OnHoverStart(AbilityContext context, ITarget potentialTarget);
    void OnHoverStop(AbilityContext context, ITarget potentialTarget);
    bool ConfirmTarget(AbilityContext context, ITarget potentialTarget);
    void Stop(AbilityContext context);
  }

  public class AbilityManager : Singleton<AbilityManager> {
    private AbilityContext? context;
    private GridSystem gridSystem = null!;

    protected override void OnAwake() {
      gridSystem = this.AssertFind<GridSystem>();
      var intentSystem = this.AssertFind<IntentSystem>();
      var ability = ScriptableObject.CreateInstance<LineAbility>();
      ability.MaxClicks = 4;
      ability.Name = "Fierbol";
      ability.Icon = Texture2D.redTexture;
      ability.IntentFactory = new IntentFactory();
      ability.IntentFactory.Behaviour = ScriptableObject.CreateInstance<DamageIntentBehaviour>();
      ability.IntentFactory.Values = new DamageIntentValues() { Damage = 3, DamageType = DamageType.Fire };
      ability.Conditions = new();
      context = new AbilityContext(gameObject, ability, new IntentGlobalContext() { IntentSystem = intentSystem, GridSystem = gridSystem });
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
      context?.TargetingContext.Stop(context);
      context = null;
    }
  }
}
