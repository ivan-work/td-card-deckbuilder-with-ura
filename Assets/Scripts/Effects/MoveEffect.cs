using System.Diagnostics;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Debug = UnityEngine.Debug;

public struct MoveAnimation {
  public float time;
  public Vector3 sourcePosition;
  public Vector3 targetPosition;
  public float duration;
}

[DebuggerDisplay("ME:([sourcePos]=>[targetPos])")]
public class MoveEffect : BaseEffect {
  private readonly MoveComponent component;
  private readonly Vector2Int direction;
  private MoveAnimation animation;

  private Vector2Int sourcePos => component.gridComponent.gridPos;
  private Vector2Int targetPos => sourcePos + direction;

  public MoveEffect(MoveComponent component, Vector2Int direction) {
    this.component = component;
    this.direction = direction;
  }

  public override void start(ActorManager am) {
    if (component.gameObject.IsDestroyed()) return; // TODO better death
    
    var gridComponent = component.gridComponent;

    var hasPath = gridComponent.gridSystem.getGridEntitiesSpecial<PathComponent>(targetPos).Any();
    var hasMob = gridComponent.gridSystem.getGridEntitiesSpecial<MoveComponent>(targetPos).Any();

    Debug.Log($"Starting {this}: {hasPath && !hasMob}");
    if (hasPath && !hasMob) {
      animation = new MoveAnimation {
        time = 0f,
        duration = 0.3f,
        sourcePosition = gridComponent.gridPos2World(sourcePos),
        targetPosition = gridComponent.gridPos2World(targetPos)
      };
      isActive = true;
      
      gridComponent.moveTo(targetPos);
    }
  }

  protected override void animate() {
    if (component.IsDestroyed()) {
      // TODO better death
      isActive = false;
      return;
    }

    animation.time += Time.deltaTime;
    var percents = animation.time / animation.duration;
    component.gameObject.transform.position =
      Vector3.Lerp(animation.sourcePosition, animation.targetPosition, percents);

    if (percents >= 1) {
      isActive = false;
    }
  }

  public override string ToString() {
    return $"MoveEffect({sourcePos}+{direction})";
  }
}