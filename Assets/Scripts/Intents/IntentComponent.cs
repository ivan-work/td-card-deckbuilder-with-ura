using System.Linq;
using Architecture;
using Effects;
using UnityEngine;
using UnityEngine.Events;

namespace Intents {
  public class IntentComponent : MonoBehaviour {
    [SerializeField] private UnityEvent OnDamage;
    private void Awake() {
      // EventManager.AmStartRequestIntent.AddListener(OnStartRequestIntent);
    }

    private void OnStartRequestIntent(ActorManager actorManager) {
      // GetComponents<IHasIntent>()
      //   .Where(component => component.isActiveAndEnabled)
      //   .ToList()
      //   .ForEach(component => { component.getIntents(actorManager); });
    }
  }
}
