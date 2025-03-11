using System.Collections.Generic;
using System.Linq;
using Components;
using Intents;
using Intents.Engine;
using UnityEngine;

public class TrapIntentComponent : MonoBehaviour {
  [SerializeField] private List<IntentFactory> IntentCreators;

  public void OnEntityEnter(IntentSystem intentSystem, GridComponent targetEntity) {
    intentSystem.AddIntents(
      IntentCreators
        .Select(intentCreator => intentCreator
          .CreateIntent(
            gameObject,
            new IntentTargets(targetEntity.gameObject, targetEntity.gridPos)
          )
        )
        .ToArray()
    );
  }
}
