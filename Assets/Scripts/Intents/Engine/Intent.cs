using UnityEngine;

namespace Intents.Engine {
  public class Intent<TDataValues, TTargetValues> where TDataValues : BaseIntentDataValues {
    public readonly GameObject Source;
    public readonly BaseIntentData<TDataValues, TTargetValues> Data;
    public readonly TDataValues DataValues;
    public readonly TTargetValues TargetValues;

    public Intent(GameObject source, BaseIntentData<TDataValues, TTargetValues> data, TDataValues dataValues, TTargetValues targetValues) {
      Source = source;
      Data = data;
      DataValues = dataValues;
      TargetValues = targetValues;
    }
  }

  public class AnyIntent : Intent<BaseIntentDataValues, IntentTargetValues> {
    public AnyIntent(
      GameObject source,
      BaseIntentData<BaseIntentDataValues, IntentTargetValues> data,
      BaseIntentDataValues dataValues,
      IntentTargetValues targetValues
    ) : base(source, data, dataValues, targetValues) { }
  }
}
