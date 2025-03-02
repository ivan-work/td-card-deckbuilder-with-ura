using System;
using Unity.Mathematics;
using UnityEngine;

namespace Intents {
  [Serializable]
  public class MoveIntentValues : BaseIntentValues {
    public int2 Direction;
  }

  [CreateAssetMenu]
  public class MoveIntentData : BaseIntentData<MoveIntentValues> {
    public override void PerformIntent(IntentContext<MoveIntentValues> context) {
      throw new NotImplementedException();
    }
  }
}
