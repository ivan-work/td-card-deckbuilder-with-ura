using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardPrefab : MonoBehaviour {
  [SerializeField] public Card card;

  public void OnInstantiate() {
    gameObject.GetComponentInChildren<TextMeshPro>().text = $"{card.name}";
  }
}