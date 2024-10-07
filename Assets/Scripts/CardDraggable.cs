using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IPointerClickHandler {
  [SerializeField] TargetSystem targetSystem;
  
  public void OnPointerClick(PointerEventData eventData) {
    targetSystem.OnCardClicked();
  }
}