namespace Effects {
  // public class PushEffect : SimpleComponentEffect {
  //   private readonly MoveComponent moveComponent;
  //   private readonly Vector2Int direction;
  //   private readonly int force;
  //
  //   private Vector2Int sourcePos => moveComponent.gridComponent.gridPos;
  //   private Vector2Int targetPos => sourcePos + direction;
  //
  //   public PushEffect(MoveComponent moveComponent, Vector2Int direction, int force) : base(moveComponent) {
  //     this.moveComponent = moveComponent;
  //     this.direction = direction;
  //     this.force = force;
  //   }
  //
  //   public override void start(ActorManager am, GridSystem gridSystem) {
  //     if (moveComponent.gameObject.IsDestroyed()) return; // TODO better death
  //
  //     bool hasPath = gridSystem.getGridEntitiesSpecial<PathComponent>(targetPos).Any();
  //     bool hasMobs = gridSystem.getGridEntitiesSpecial<MoveComponent>(targetPos).Any();
  //     if (hasPath && !hasMobs) {
  //       animation = new MoveAnimation(
  //         moveComponent,
  //         gridSystem.gridPos2World(sourcePos),
  //         gridSystem.gridPos2World(targetPos)
  //       );
  //       moveComponent.gridComponent.moveTo(targetPos);
  //       if (force > 1) {
  //         am.addImmediateEffects(new PushEffect(moveComponent, direction, force - 1));
  //       }
  //     } else {
  //       am.addImmediateEffects(
  //         new DamageEffect(sourcePos, DamageType.Blunt, force),
  //         new DamageEffect(targetPos, DamageType.Blunt, force)
  //       );
  //       animation = new MoveAttemptAnimation(
  //         moveComponent,
  //         gridSystem.gridPos2World(sourcePos),
  //         gridSystem.gridPos2World(targetPos)
  //       );
  //     }
  //   }
  // }
}
