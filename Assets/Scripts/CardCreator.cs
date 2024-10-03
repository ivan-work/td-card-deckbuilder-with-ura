using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardCreator : MonoBehaviour {
  [SerializeField] CardPrefab example;

  // Start is called before the first frame update
  void Start() {
    CardPrefab duplicate = Instantiate(example);
    duplicate.transform.position = new Vector3(-5, 0);
      
  }

  // Update is called once per frame
  void Update() {
      
  }
}
