using System.Collections.Generic;
using System.Linq;
using Intents;
using Intents.Engine;
using Intents.IReactions;
using UnityEngine;

namespace Components {
  public class TrapComponent : MonoBehaviour, IReactToEntityEnter {
    [SerializeField] private List<IntentFactory> _intentCreators = new();

    public void OnEntityEnter(IntentGlobalContext context, GameObject targetEntity) {
      context.IntentSystem.AddIntents(_intentCreators.Select(intentCreator =>
          intentCreator.CreateIntent(gameObject, IntentTargets.Create(targetEntity)))
        .ToArray());
    }
  }
}
