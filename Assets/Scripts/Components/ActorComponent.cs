using UnityEngine;

public class ActorComponent : MonoBehaviour {
  private void Awake() {
    EventManager.AmStartRequestIntent.AddListener(OnStartRequestIntent);
  }

  private void OnStartRequestIntent(ActorManager actorManager) {
    var effect = new MoveEffect(GetComponent<MoveComponent>(), new Vector2Int(1, 0));
    actorManager.addEffect(effect);
  }
}