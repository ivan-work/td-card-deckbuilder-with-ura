using System;
using Intents.Engine;
using UnityEngine;

namespace Intents {
  [Serializable]
  public class IntentCreator {
    [SerializeReference] private BaseIntentData<BaseIntentDataValues> IntentData;
    [SerializeReference] private BaseIntentDataValues IntentDataValues;

    public AnyIntent CreateIntent(GameObject source, IntentTargetValues targetValues) {
      var intent = new AnyIntent(
        source: source, 
        data: IntentData, 
        dataValues: IntentDataValues, 
        targetValues: targetValues
      );
      Debug.Log($"IntentData = [{IntentData}] => intent.Data [{intent.Data}]");
      return intent;
    }
  }
}
