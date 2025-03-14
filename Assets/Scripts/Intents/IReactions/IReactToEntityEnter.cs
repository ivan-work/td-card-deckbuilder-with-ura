using Intents.Engine;
using UnityEngine;

public interface IReactToEntityEnter {
  public void OnEntityEnter(IntentGlobalContext context, GameObject targetEntity);
}
