using System.Diagnostics;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class PushEffect : BaseEffect {
  private readonly MoveComponent component;
  private readonly Vector2Int direction;
  private readonly int force;
  private BaseAnimation animation;

  private Vector2Int sourcePos => component.gridComponent.gridPos;
  private Vector2Int targetPos => sourcePos + direction;

  public PushEffect(MoveComponent component, Vector2Int direction, int force) {
    this.component = component;
    this.direction = direction;
    this.force = force;
  }

  public override void start(ActorManager am) {
    if (component.gameObject.IsDestroyed()) return; // TODO better death
    
    var gridComponent = component.gridComponent;
    var hasPath = gridComponent.gridSystem.getGridEntitiesSpecial<PathComponent>(targetPos).Any();
    var hasMobs = gridComponent.gridSystem.getGridEntitiesSpecial<MoveComponent>(targetPos).Any();
    if (hasPath && !hasMobs) {
      animation = new MoveAnimation {
        sourcePosition = gridComponent.gridPos2World(sourcePos),
        targetPosition = gridComponent.gridPos2World(targetPos)
      };
      isActive = true;
      gridComponent.moveTo(targetPos);
      if (force > 1) {
        am.addImmediateEffects(new PushEffect(component, direction, force - 1));
      }
    } else {
      am.addImmediateEffects(
        new DamageEffect(gridComponent.gridSystem, sourcePos, force),
        new DamageEffect(gridComponent.gridSystem, targetPos, force));
        animation = new MoveAttemptAnimation {
          sourcePosition = gridComponent.gridPos2World(sourcePos),
          targetPosition = gridComponent.gridPos2World(targetPos)
        };
        isActive = true;
    }
  }

  protected override void animate() {
    if (component.IsDestroyed()) {
      // TODO better death
      isActive = false;
      return;
    }

    isActive = animation.animate(component);
  }
}