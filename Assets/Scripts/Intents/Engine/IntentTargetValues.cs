using JetBrains.Annotations;
using UnityEngine;

namespace Intents.Engine {
  public struct IntentTargetValues {
    [CanBeNull] public readonly Vector2Int? TargetPos;
    [CanBeNull] public readonly GameObject TargetGo;

    public IntentTargetValues([CanBeNull] GameObject targetGo, [CanBeNull] Vector2Int? targetPos) {
      TargetPos = targetPos;
      TargetGo = targetGo;
    }
  }
}
