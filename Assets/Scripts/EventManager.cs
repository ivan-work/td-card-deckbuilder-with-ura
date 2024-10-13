using UnityEngine.Events;

public static class EventManager {
  public static UnityEvent DeckUpdate = new();
  public static void OnDeckUpdate() => DeckUpdate.Invoke();

  public static UnityEvent EndTurn = new();

  public static UnityEvent<Card> OnCardClicked = new();
}