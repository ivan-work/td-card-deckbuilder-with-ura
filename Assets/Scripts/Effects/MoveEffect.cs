﻿using System.Diagnostics;
using System.Linq;
using Architecture;
using Effects.EffectAnimations;
using Unity.VisualScripting;
using UnityEngine;
using Debug = UnityEngine.Debug;


namespace Effects {
  [DebuggerDisplay("ME:([sourcePos]=>[targetPos])")]
  public class MoveEffect : SimpleComponentEffect {
    private readonly MoveComponent moveComponent;
    private readonly Vector2Int direction;

    private Vector2Int sourcePos => moveComponent.gridComponent.gridPos;
    private Vector2Int targetPos => sourcePos + direction;

    public MoveEffect(MoveComponent moveComponent, Vector2Int direction) : base(moveComponent) {
      this.moveComponent = moveComponent;
      this.direction = direction;
    }

    public override void start(ActorManager am, GridSystem gridSystem) {
      if (moveComponent.gameObject.IsDestroyed()) return; // TODO better death

      bool hasPath = gridSystem.getGridEntitiesSpecial<PathComponent>(targetPos).Any();
      bool hasMob = gridSystem.getGridEntitiesSpecial<MoveComponent>(targetPos).Any();

      Debug.Log($"Starting {this}: {hasPath && !hasMob}");


      if (hasPath && !hasMob) {
        animation = new MoveAnimation {
          component = moveComponent,
          sourcePosition = gridSystem.gridPos2World(sourcePos),
          targetPosition = gridSystem.gridPos2World(targetPos)
        };
        moveComponent.gridComponent.moveTo(targetPos);
      } else {
        animation = new MoveAttemptAnimation {
          component = moveComponent,
          sourcePosition = gridSystem.gridPos2World(sourcePos),
          targetPosition = gridSystem.gridPos2World(targetPos)
        };
      }
    }

    public override string ToString() {
      return $"MoveEffect({sourcePos}+{direction})";
    }
  }
}
