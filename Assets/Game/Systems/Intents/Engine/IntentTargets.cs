using System;
using UnityEngine;

namespace Intents.Engine {
  [Serializable]
  public class IntentTargets {
    public GameObject? TargetGameObject { get; init; }
    public Vector2Int? TargetPos { get; init; }
    
    public IntentTargets(GameObject? targetGameObject, Vector2Int? targetPos) {
      TargetGameObject = targetGameObject;
      TargetPos = targetPos;
    }
  }
}
