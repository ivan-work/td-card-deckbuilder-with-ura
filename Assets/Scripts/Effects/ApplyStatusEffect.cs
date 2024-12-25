using System;
using Architecture;
using Components;
using Status;
using UnityEditor;
using UnityEngine;

namespace Effects {
  public class ApplyStatusEffect : BaseEffect {
    private readonly GameObject target;
    private readonly BaseStatus status;

    public ApplyStatusEffect(GameObject target, BaseStatus status) {  
      this.target = target;
      this.status = status;
    }
    public override void start(ActorManager am, GridSystem gridSystem) {
      var statusComponent = target.AddComponent<StatusComponent>();
      statusComponent.status = status;
    }
  }
}
