using System.Linq;
using Effects;
using Unity.VisualScripting;
using UnityEngine;

public class ActorComponent : MonoBehaviour {
  private void Awake() {
    EventManager.AmStartRequestIntent.AddListener(OnStartRequestIntent);
  }

  private void OnStartRequestIntent(ActorManager actorManager) {
    GetComponents<IHasIntent>()
      .Where(component => component.isActiveAndEnabled)
      .ToList()
      .ForEach(component => {
        component.getIntents(actorManager);
      });
  }
}
