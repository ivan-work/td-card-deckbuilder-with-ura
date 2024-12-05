using System.Linq;
using UnityEngine;

struct MoveAnimation {
  public float time;
  public Vector3 sourcePosition;
  public Vector3 targetPosition;
}

public class MoveEffect : BaseEffect {
  private MoveComponent component;
  private Vector2Int direction;
  private MoveAnimation animation;

  public MoveEffect(MoveComponent component, Vector2Int direction) {
    this.component = component;
    this.direction = direction;
  }

  public override void start() {
    var gridComponent = component.gridComponent;

    var sourcePos = gridComponent.gridPos;
    var targetPos = sourcePos + direction;

    var hasPath = gridComponent.gridSystem.getGridEntitiesSpecial<PathComponent>(targetPos).Any();
    var hasMob = gridComponent.gridSystem.getGridEntitiesSpecial<MoveComponent>(targetPos).Any();

    if (hasPath && !hasMob) {
      gridComponent.moveTo(targetPos);

      animation = new MoveAnimation {
        time = 0f,
        sourcePosition = gridComponent.gridPos2World(sourcePos),
        targetPosition = gridComponent.gridPos2World(targetPos)
      };
      active = true;
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
}