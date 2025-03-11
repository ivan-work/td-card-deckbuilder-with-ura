using Effects;
using Intents.Engine;
using Intents.IntentBehaviours;
using UnityEngine;

namespace Status.StatusData {
  public class BaseStatusData : ScriptableObject {
    [SerializeField] public string displayName;
    [SerializeField] public Sprite icon;
    public virtual void OnMove(StatusContext context) { }

    public virtual void OnEndTurn(StatusContext context) { }
    
    public virtual void OnDamage(StatusContext statusContext, Intent<DamageIntentValues> intent) { }

  }
}
