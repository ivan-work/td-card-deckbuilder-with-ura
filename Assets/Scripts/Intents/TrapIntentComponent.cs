using System.Collections.Generic;
using System.Linq;
using Components;
using Intents;
using Intents.Engine;
using UnityEngine;

public class TrapIntentComponent : MonoBehaviour {
  [SerializeField] private List<IntentCreator> IntentCreators;

  public void OnEntityEnter(IntentSystem intentSystem, GridComponent targetEntity) {
    intentSystem.AddIntents(
      IntentCreators
        .Select(intentCreator => intentCreator
          .CreateIntent(gameObject, new IntentTargetValues(targetEntity.gameObject, targetEntity.gridPos))
        )
        .ToArray()
    );
  }
}
