using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Deck : MonoBehaviour, IPointerClickHandler {
  private void Awake() {
    EventManager.CardDraw.AddListener(UpdateDeckCounter);
  }

  void UpdateDeckCounter(Card card) {
    gameObject.GetComponentInChildren<TextMeshPro>().text = $"{GameManager.Instance.deck.Count}";
  }  
  
  public void OnPointerClick(PointerEventData eventData) {
    GameManager.Instance.DrawHand();
    EventManager.PhasePlayerIntent.Invoke();
  }
}
