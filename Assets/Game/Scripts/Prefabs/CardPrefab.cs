using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
[SelectionBase]
public class CardPrefab : MonoBehaviour, IPointerClickHandler {
  [SerializeField] public Card Card;

  public void Start() {
    gameObject.GetComponentInChildren<TextMeshPro>().text = $"{Card.Name}";
  }
  
  public void OnPointerClick(PointerEventData eventData) {
    EventManager.Instance.StartAbility.Invoke(gameObject, Card.Ability);
  }
}
