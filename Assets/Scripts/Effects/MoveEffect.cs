using System.Diagnostics;
using System.Linq;
using UnityEngine;
using Debug = UnityEngine.Debug;

struct MoveAnimation {
  public float time;
  public Vector3 sourcePosition;
  public Vector3 targetPosition;
}

[DebuggerDisplay("ME:([sourcePos]=>[targetPos])")]
public class MoveEffect : BaseEffect {
  private readonly MoveComponent component;
  private readonly Vector2Int targetPos;
  private MoveAnimation animation;

  private Vector2Int sourcePos => component.gridComponent.gridPos;

  public MoveEffect(MoveComponent component, Vector2Int targetPos) {
    this.component = component;
    this.targetPos = targetPos;
  }

  public override void start() {
    var gridComponent = component.gridComponent;

    var hasPath = gridComponent.gridSystem.getGridEntitiesSpecial<PathComponent>(targetPos).Any();
    var hasMob = gridComponent.gridSystem.getGridEntitiesSpecial<MoveComponent>(targetPos).Any();

    Debug.Log($"Starting {this}: {hasPath && !hasMob}");
    if (hasPath && !hasMob) {
      animation = new MoveAnimation {
        time = 0f,
        sourcePosition = gridComponent.gridPos2World(sourcePos),
        targetPosition = gridComponent.gridPos2World(targetPos)
      };
      active = true;
      
      gridComponent.moveTo(targetPos);
    }
  }

  protected override void animate() {
    var duration = 1f;
    animation.time += Time.deltaTime;
    var percents = animation.time / duration;
    component.gameObject.transform.position =
      Vector3.Lerp(animation.sourcePosition, animation.targetPosition, percents);

    if (percents >= 1) {
      active = false;
    }
  }

  public override string ToString() {
    return $"MoveEffect({sourcePos}=>{targetPos})";
  }
}