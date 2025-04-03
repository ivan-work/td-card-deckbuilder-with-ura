namespace Effects {
  // public class MoveEffect : SimpleComponentEffect {
  //   private readonly MoveComponent moveComponent;
  //   private readonly Vector2Int direction;
  //
  //   private readonly Vector2Int sourcePos;
  //   private Vector2Int targetPos => sourcePos + direction;
  //
  //   public MoveEffect(MoveComponent moveComponent, Vector2Int direction) : base(moveComponent) {
  //     this.moveComponent = moveComponent;
  //     this.direction = direction;
  //     sourcePos = moveComponent.gridComponent.GridLoc;
  //   }
  //
  //   public override void start(ActorManager am, GridSystem GridSystem) {
  //     if (moveComponent.gameObject.IsDestroyed()) return; // TODO better death
  //
  //     bool hasPath = GridSystem.GetGridEntities<PathComponent>(targetPos).Any();
  //     bool hasMob = GridSystem.GetGridEntities<MoveComponent>(targetPos).Any();
  //
  //     Debug.Log($"Starting {this}: {hasPath && !hasMob}");
  //
  //
  //     if (hasPath && !hasMob) {
  //       animation = new MoveAnimation(moveComponent, GridSystem.GridLoc2World(sourcePos), GridSystem.GridLoc2World(targetPos));
  //       moveComponent.gridComponent.MoveTo(targetPos);
  //       SendEvents(am, GridSystem);
  //     } else {
  //       animation = new MoveAttemptAnimation(moveComponent, GridSystem.GridLoc2World(sourcePos),
  //         GridSystem.GridLoc2World(targetPos));
  //     }
  //   }
  //
  //   private void SendEvents(ActorManager am, GridSystem GridSystem) {
  //     // GridSystem.GetGridEntities<TrapComponent>(targetPos).ToList()
  //     //   .ForEach(trapComponent => trapComponent.OnEntityEnter(am, moveComponent.gridComponent));
  //     //
  //     // moveComponent.GetComponents<StatusComponent>().ToList().ForEach(statusComponent => {
  //     //   statusComponent.OnMove(am);
  //     // });
  //   }
  //
  //
  //   public override string ToString() {
  //     return $"MoveEffect({sourcePos}+{direction})";
  //   }
  // }
}
