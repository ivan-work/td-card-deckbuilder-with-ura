using System.Diagnostics;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Debug = UnityEngine.Debug;


namespace Effects {
  [DebuggerDisplay("ME:([sourcePos]=>[targetPos])")]
  public class MoveEffect : BaseEffect {
    private readonly MoveComponent component;
    private readonly Vector2Int direction;
    private BaseAnimation animation;


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
          sourcePosition = gridComponent.gridPos2World(sourcePos),
          targetPosition = gridComponent.gridPos2World(targetPos)
        };
        gridComponent.moveTo(targetPos);
      } else {
        animation = new MoveAttemptAnimation() {
          sourcePosition = gridComponent.gridPos2World(sourcePos),
          targetPosition = gridComponent.gridPos2World(targetPos)
        };
      }

      isActive = true;
    }

    protected override void animate() {
      if (component.IsDestroyed()) {
        // TODO better death
        isActive = false;
        return;
      }

      isActive = animation.animate(component);
    }

    public override string ToString() {
      return $"MoveEffect({sourcePos}+{direction})";
    }
  }
}
