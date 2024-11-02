using UnityEngine.Events;

public static class EventManager {
  public static UnityEvent DeckUpdate = new();
  public static void OnDeckUpdate() => DeckUpdate.Invoke();

  public static UnityEvent EndTurn = new();

  public static UnityEvent<Card> CardClicked = new();
  public static UnityEvent<Card> CardDraw = new();
  public static UnityEvent<Card> CardDiscard = new();
  
  public static readonly CoroutineList PhaseActionFast = new ();
  public static readonly CoroutineList PhaseMove = new ();
  public static readonly CoroutineList PhaseActionSlow = new ();
}