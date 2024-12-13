using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ActorComponent : MonoBehaviour {
  private void Awake() {
    EventManager.AmStartRequestIntent.AddListener(OnStartRequestIntent);
  }

  private void OnStartRequestIntent(ActorManager actorManager) {
    var intents = GetComponents<IHasIntent>()
      .SelectMany(intentMaker => intentMaker.getIntents());
    actorManager.addEffects(intents);
  }
}