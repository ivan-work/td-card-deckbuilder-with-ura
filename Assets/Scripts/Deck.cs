using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Deck : MonoBehaviour {
  // Start is called before the first frame update
  void Start() {

  }

  // Update is called once per frame
  void Update() {
    gameObject.GetComponentInChildren<TextMeshPro>().text = $"{GameManager.Instance.deck.Count}";
  }
}
