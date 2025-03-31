using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
[SelectionBase]
public class Deck : MonoBehaviour, IPointerClickHandler {
  private void Awake() {
    EventManager.Instance.CardDraw.AddListener(UpdateDeckCounter);
  }

  private void UpdateDeckCounter(Card card) {
    gameObject.GetComponentInChildren<TextMeshPro>().text = $"{GameManager.Instance.deck.Count}";
  }  
  
  public void OnPointerClick(PointerEventData eventData) {
    GameManager.Instance.DrawHand();
  }
}
