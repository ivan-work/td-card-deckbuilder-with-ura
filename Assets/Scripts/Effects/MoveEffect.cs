﻿using System.Diagnostics;
using System.Linq;
using Architecture;
using Components;
using Effects.EffectAnimations;
using Status;
using Unity.VisualScripting;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class Dog {
  public static string Name = "Name";
  public string View = "View";
}

public class test {
  public void Test() {
   Dog Rex = new Dog();
   Dog Oleg = new Dog();
   
   Dog.Name = "Rex";
  }
}
namespace Effects {
  [DebuggerDisplay("ME:([sourcePos]=>[targetPos])")]
  public class MoveEffect : SimpleComponentEffect {
    private readonly MoveComponent moveComponent;
    private readonly Vector2Int direction;

    private readonly Vector2Int sourcePos;
    private Vector2Int targetPos => sourcePos + direction;

    public MoveEffect(MoveComponent moveComponent, Vector2Int direction) : base(moveComponent) {
      this.moveComponent = moveComponent;
      this.direction = direction;
      sourcePos = moveComponent.gridComponent.gridPos;
    }

    public override void start(ActorManager am, GridSystem gridSystem) {
      if (moveComponent.gameObject.IsDestroyed()) return; // TODO better death

      bool hasPath = gridSystem.getGridEntitiesSpecial<PathComponent>(targetPos).Any();
      bool hasMob = gridSystem.getGridEntitiesSpecial<MoveComponent>(targetPos).Any();

      Debug.Log($"Starting {this}: {hasPath && !hasMob}");


      if (hasPath && !hasMob) {
        animation = new MoveAnimation(moveComponent, gridSystem.gridPos2World(sourcePos), gridSystem.gridPos2World(targetPos));
        moveComponent.gridComponent.moveTo(targetPos);
        SendEvents(am, gridSystem);
      } else {
        animation = new MoveAttemptAnimation(moveComponent, gridSystem.gridPos2World(sourcePos),
          gridSystem.gridPos2World(targetPos));
      }
    }

    private void SendEvents(ActorManager am, GridSystem gridSystem) {
      gridSystem.getGridEntitiesSpecial<TrapComponent>(targetPos).ToList()
        .ForEach(trapComponent => trapComponent.OnEntityEnter(am, moveComponent.gridComponent));

      moveComponent.GetComponents<StatusComponent>().ToList().ForEach(statusComponent => {
        statusComponent.OnMove(am);
      });
    }


    public override string ToString() {
      return $"MoveEffect({sourcePos}+{direction})";
    }
  }
}
