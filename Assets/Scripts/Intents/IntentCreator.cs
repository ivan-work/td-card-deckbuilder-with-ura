using System;
using Intents.Engine;
using UnityEngine;

namespace Intents {
  [Serializable]
  public class IntentCreator {
    [SerializeReference] private BaseIntentData IntentData;
    [SerializeReference] private BaseIntentDataValues IntentDataValues;

    public AnyIntent CreateIntent(GameObject source, IntentTargetValues targetValues) {
      return new AnyIntent(
        source: source, 
        data: IntentData as BaseIntentData<BaseIntentDataValues, IntentTargetValues>, 
        dataValues: IntentDataValues, 
        targetValues: targetValues
      );
    }
  }
}
