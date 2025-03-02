using System.Collections.Generic;
using UnityEngine;

namespace Intents {
  public class IntentTester : MonoBehaviour {
    [SerializeField] private IntentCreator Creator;
    [SerializeField] private List<IntentCreator> ListOfCreators;
    public GameObject Source;
    public GameObject Target;

    public void Awake() {
      var system = new IntentSystem();
      var intent = Creator.Spawn();
      system.AddIntent(intent);
      system.PerformIntents();
    }
  }
}
