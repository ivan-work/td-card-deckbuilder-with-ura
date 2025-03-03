using System.Collections.Generic;
using Intents.Engine;
using UnityEngine;

namespace Intents {
  public class IntentTester : MonoBehaviour {
    [SerializeField] private IntentCreator Creator;
    [SerializeField] private List<IntentCreator> ListOfCreators;
    public GameObject Source;
    public GameObject TargetGameObject;
    public Vector2Int TargetPosition;

    public void Awake() {
      var system = new IntentSystem();
      system.AddIntents(Creator.CreateIntent(Source, new IntentTargetValues(TargetGameObject, TargetPosition)));
      system.PerformIntents();
    }
  }
}
