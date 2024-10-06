using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Deck : MonoBehaviour {
  private void Start() {
    EventManager.DeckUpdate.AddListener(UpdateDeckCounter);
  }

  void UpdateDeckCounter() {
    gameObject.GetComponentInChildren<TextMeshPro>().text = $"{GameManager.Instance.deck.Count}";
  }
}
