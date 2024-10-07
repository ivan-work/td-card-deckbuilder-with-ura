using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardPrefab : MonoBehaviour {
  [SerializeField] public Card card;

  public void OnInstantiate() {
    gameObject.GetComponentInChildren<TextMeshPro>().text = $"{card.name}";
  }

  // public void OnPointerClick(PointerEventData eventData) {
  //   throw new System.NotImplementedException();
  // }
}