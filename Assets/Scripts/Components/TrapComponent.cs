using System.Collections.Generic;
using System.Linq;
using Components;
using Intents;
using Intents.Engine;
using UnityEngine;

public class TrapComponent : MonoBehaviour, IReactToEntityEnter {
  [SerializeField] private List<IntentFactory> IntentCreators;
  public void OnEntityEnter(IntentGlobalContext context, GameObject targetEntity) {
    context.IntentSystem.AddIntents(IntentCreators.Select(intentCreator =>
        intentCreator.CreateIntent(gameObject, new IntentTargets(targetEntity, null)))
      .ToArray());
  }
}
