using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardPrefab : MonoBehaviour, IPointerClickHandler {
  [SerializeField] public Card card;

  public void Start() {
    gameObject.GetComponentInChildren<TextMeshPro>().text = $"{card.name}";
  }
  
  public void OnPointerClick(PointerEventData eventData) {
    EventManager.CardClicked.Invoke(card);
  }
}