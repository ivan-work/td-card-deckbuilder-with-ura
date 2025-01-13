using System;
using Architecture;
using Components;
using Status;
using UnityEditor;
using UnityEngine;

namespace Effects {
  public class ApplyStatusEffect : BaseEffect {
    private readonly StatusComponent target;
    private readonly StatusStruct statusStruct;

    public ApplyStatusEffect(StatusComponent target, StatusStruct statusStruct) {  
      this.target = target;
      this.statusStruct = statusStruct;
    }
    public override void start(ActorManager am, GridSystem gridSystem) {
      target.addStatus(statusStruct, am);
    }
  }
}
