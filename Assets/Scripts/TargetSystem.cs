using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSystem : MonoBehaviour {
  [SerializeField] private GameObject mouseIndicator;
  [SerializeField] private InputManager inputManager;

  private void Update() {
  }

  private void OnEnable() {
    Debug.Log("Targeting enabled");
  }

  private void OnDisable() {
    Debug.Log("Targeting disabled");    
  }
}
