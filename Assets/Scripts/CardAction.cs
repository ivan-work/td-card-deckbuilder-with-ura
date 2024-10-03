using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardAction : MonoBehaviour {
  // Start is called before the first frame update
  void Start() {
    Debug.Log("hello");
    TextMeshPro text = gameObject.GetComponent<TextMeshPro>();
    text.text = "Pierce";
  }

  // Update is called once per frame
  void Update() {

  }
}
