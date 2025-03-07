using System;
using JetBrains.Annotations;
using UnityEngine;

namespace Intents.Engine {
  [Serializable]
  public class IntentTargets {
    [CanBeNull] public GameObject TargetGameObject { get; init; }
    public Vector2Int? TargetPos { get; init; }
    
    public IntentTargets([CanBeNull] GameObject targetGameObject, Vector2Int? targetPos) {
      TargetGameObject = targetGameObject;
      TargetPos = targetPos;
    }
  }
}
