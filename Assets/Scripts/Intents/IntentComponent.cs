using System.Linq;
using Effects;
using UnityEngine;

namespace Intents {
  public class IntentComponent : MonoBehaviour {
    private void Awake() {
      EventManager.ImsStartRequestIntent.AddListener(OnStartRequestIntent);
    }

    private void OnStartRequestIntent(IntentSystem intentSystem) {
      GetComponents<IHasIntent>()
        .Where(component => component.isActiveAndEnabled)
        .ToList()
        .ForEach(component => { component.WriteIntents(intentSystem); });
    }
  }
}
