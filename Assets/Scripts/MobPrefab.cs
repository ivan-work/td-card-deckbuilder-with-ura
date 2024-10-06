using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using UnityEngine;

public class MobPrefab : MonoBehaviour {
  void Start() {
    EventManager.EndTurn.AddListener(OnEndTurn);
  }
  
  void OnEndTurn() {
    gameObject.transform.position += Vector3.right;
  }
}