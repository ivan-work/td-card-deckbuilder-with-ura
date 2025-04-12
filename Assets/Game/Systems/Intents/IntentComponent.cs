using System.Collections.Generic;
using System.Linq;
using Architecture;
using Effects;
using Intents.Engine;
using UnityEngine;

namespace Intents {
  public class IntentComponent : MonoBehaviour, IIntentHolder {
    private readonly List<Intent> queuedIntents = new();
    private readonly List<GameObject> displays = new();

    private void Awake() {
      EventManager.Instance.PhaseCreateIntents.AddListener(onPhaseCreateIntents);
      EventManager.Instance.ImsWriteIntents.AddListener(onWriteIntents);
    }

    private void onPhaseCreateIntents() {
      GetComponents<IHasIntent>()
        .Where(component => component.isActiveAndEnabled)
        .ToList()
        .ForEach(component => { component.WriteIntents(this); });
    }

    private void onWriteIntents(IntentSystem intentSystem) {
      intentSystem.AddIntents(queuedIntents, false);
      queuedIntents.Clear();
      displays.ForEach(Destroy);
      displays.Clear();
    }

    public void AddIntents(IEnumerable<Intent> intents, bool toFront) {
      AddIntents(intents.ToArray());
    }

    public void AddIntents(params Intent[] intents) {
      queuedIntents.AddRange(intents);
      displays.AddRange(
        intents
          .Select(intent => intent.Behaviour.CreateDisplay(intent))
          .MyNotNull()
      );
    }
  }
}
