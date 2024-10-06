using UnityEngine.Events;

public static class EventManager {
  public static UnityEvent DeckUpdate = new UnityEvent();
  public static void OnDeckUpdate() => DeckUpdate.Invoke();

  public static UnityEvent EndTurn = new UnityEvent();
}