using Intents.Engine;
using UnityEngine;

namespace Intents.IReactions {
  public interface IReactToEntityEnter {
    public void OnEntityEnter(IntentGlobalContext context, GameObject targetEntity);
  }
}
